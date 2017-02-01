using AccountingApp.Controllers;
using AccountingApp.Data;
using AccountingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AccountingApp.Tests
{
    public class ProjectsControllerTests
    {
        private ApplicationDbContext _applicationDbContext;

        public ProjectsControllerTests()
        {
            initContext();
        }

        [Fact(DisplayName = "ProjectsController:Index should return default view")]
        public async Task Index_should_return_default_view()
        {
            var controller = new ProjectsController(_applicationDbContext);
            var viewResult = await controller.Index();
            var viewName = ((ViewResult)viewResult).ViewName;

            Assert.True(string.IsNullOrEmpty(viewName) || viewName == "Index");
        }

        [Fact(DisplayName = "ProjectsController:Index should return a list of projects")]
        public async Task Index_should_return_list()
        {
            var controller = new ProjectsController(_applicationDbContext);
            var viewResult = await controller.Index();

            List<Project> projects = ((List<Project>)((ViewResult)viewResult).Model);

            Assert.IsType<List<Project>>(projects);
            Assert.True(projects.Count > 0, "List of project");
            Assert.Equal(projects.Count, 10);
            Assert.Equal(projects[0].ID, 1);
        }

        private void initContext()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase();

            var context = new ApplicationDbContext(builder.Options);

            if (context.Project.Count() == 0)
            {
                var projects = Enumerable.Range(1, 10)
                .Select(i => new Project { ID = i, Name = $"Project_{i}" });
                context.Project.AddRange(projects);

                int changed = context.SaveChanges();
            }
            _applicationDbContext = context;
        }
    }
}
