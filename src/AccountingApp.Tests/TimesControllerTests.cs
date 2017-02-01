using AccountingApp.Controllers;
using AccountingApp.Data;
using AccountingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace AccountingApp.Tests
{
    public class TimesControllerTests
    {
        private ApplicationDbContext _applicationDbContext;

        public TimesControllerTests()
        {
            initContext();
        }

        [Fact(DisplayName = "TimesController:Index should return default view")]
        public async Task Index_should_return_default_view()
        {
            var controller = new TimesController(_applicationDbContext);
            var viewResult = await controller.Index(null, null, null, null);
            var viewName = ((ViewResult)viewResult).ViewName;

            Assert.True(string.IsNullOrEmpty(viewName) || viewName == "Index");
        }

        [Fact(DisplayName = "TimesController:Edit should return edit view")]
        public async Task Edit_should_return_edit_view()
        {
            var controller = new TimesController(_applicationDbContext);
            var viewResult = await controller.Edit(1);
            var viewName = ((ViewResult)viewResult).ViewName;

            var model = ((ViewResult)viewResult).Model;
            Time time = ((TimeViewModel)model).time;

            Assert.True(string.IsNullOrEmpty(viewName) || viewName == "Edit");
            Assert.IsType<TimeViewModel>(model);
            Assert.Equal(time.Duration, 8);
            Assert.Contains("Time", time.Memo);
        }

        [Fact(DisplayName = "TimesController:Index should return a list of time")]
        public async Task Index_should_return_list()
        {
            var controller = new TimesController(_applicationDbContext);
            var viewResult = await controller.Index(null, null, null, null);

            var model = ((ViewResult)viewResult).Model;
            List<Time> times = ((TimesViewModel)model).times;

            Assert.IsType<List<Time>>(times);
            Assert.True(times.Count > 0, "List of time");
            Assert.Equal(times.Count, 10);
            Assert.Equal(times[0].ID, 1);
        }

        private void initContext()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase();

            var context = new ApplicationDbContext(builder.Options);

            if(context.Time.Count() == 0)
            { 
                var times = Enumerable.Range(1, 10)
                    .Select(i => new Time { ID = i, Billed = false, CustomerID = i, Date = DateTime.Today.AddDays(i - 1), Duration = 8, Memo = $"Time_{i}", ProjectID = i });
                context.Time.AddRange(times);

                int changed = context.SaveChanges();
            }
            _applicationDbContext = context;
        }
    }
}
