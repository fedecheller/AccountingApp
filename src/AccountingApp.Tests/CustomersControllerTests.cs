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
    public class CustomersControllerTests
    {
        private ApplicationDbContext _applicationDbContext;

        public CustomersControllerTests()
        {
            initContext();
        }

        [Fact(DisplayName = "CustomersController:Index should return default view")]
        public async Task Index_should_return_default_view()
        {
            var controller = new CustomersController(_applicationDbContext);
            var viewResult = await controller.Index();
            var viewName = ((ViewResult)viewResult).ViewName;

            Assert.True(string.IsNullOrEmpty(viewName) || viewName == "Index");
        }

        [Fact(DisplayName = "CustomersController:Index should return a list of customer")]
        public async Task Index_should_return_list()
        {
            var controller = new CustomersController(_applicationDbContext);
            var viewResult = await controller.Index();

            List<Customer> customers = ((List<Customer>)((ViewResult)viewResult).Model);

            Assert.IsType<List<Customer>>(customers);
            Assert.True(customers.Count > 0, "List of customer");
            Assert.Equal(customers.Count, 10);
            Assert.Equal(customers[0].ID, 1);

        }

        private void initContext()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase();

            var context = new ApplicationDbContext(builder.Options);

            if (context.Customer.Count() == 0)
            {
                var customers = Enumerable.Range(1, 10)
                .Select(i => new Customer { ID = i, Name = $"Customer_{i}" });
                context.Customer.AddRange(customers);

                int changed = context.SaveChanges();
            }
            _applicationDbContext = context;
        }
    }
}
