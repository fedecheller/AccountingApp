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
    public class BillsControllerTests
    {
        private ApplicationDbContext _applicationDbContext;

        public BillsControllerTests()
        {
            initContext();
        }

        [Fact(DisplayName = "BillsController:Index should return default view")]
        public async Task Index_should_return_default_view()
        {
            var controller = new BillsController(_applicationDbContext);
            var viewResult = await controller.Index();
            var viewName = ((ViewResult)viewResult).ViewName;

            Assert.True(string.IsNullOrEmpty(viewName) || viewName == "Index");
        }

        [Fact(DisplayName = "BillsController:Edit should return edit view")]
        public async Task Edit_should_return_edit_view()
        {
            var controller = new BillsController(_applicationDbContext);
            var viewResult = await controller.Details(1);
            var viewName = ((ViewResult)viewResult).ViewName;

            var model = ((ViewResult)viewResult).Model;
            BillViewModel billViewModel = ((BillViewModel)model);

            Assert.True(string.IsNullOrEmpty(viewName) || viewName == "Edit");
            Assert.IsType<BillViewModel>(model);
            Assert.NotEqual<string>(billViewModel.bill.Times, "");
        }

        [Fact(DisplayName = "BillsController:Index should return a list of bill")]
        public async Task Index_should_return_list()
        {
            var controller = new BillsController(_applicationDbContext);
            var viewResult = await controller.Index();

            var model = ((ViewResult)viewResult).Model;
            List<Bill> bills = ((BillsViewModel)model).bills;

            Assert.IsType<List<Bill>>(bills);
            Assert.True(bills.Count > 0, "List of bills");
            Assert.Equal(bills.Count, 10);
            Assert.Equal(bills[0].ID, 1);

        }

        private void initContext()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase();

            var context = new ApplicationDbContext(builder.Options);

            if (context.Bill.Count() == 0)
            {
                var bills = Enumerable.Range(1, 10)
                .Select(i => new Bill { ID = i, Number = i, Amount = i * 100, CustomerID = i, Date = DateTime.Today.AddDays(i - 1), Times = i.ToString() });
                context.Bill.AddRange(bills);

                int changed = context.SaveChanges();
            }
            _applicationDbContext = context;
        }
    }
}
