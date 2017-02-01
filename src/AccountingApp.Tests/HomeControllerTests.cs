using System;
using AccountingApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AccountingApp.Tests
{
    public class HomeControllerTests
    {
        public HomeControllerTests()
        {
        }

        [Fact(DisplayName = "HomeController:Index should return default view")]
        public void Index_should_return_default_view()
        {
            var controller = new HomeController();
            var viewResult = (ViewResult)controller.Index();
            var viewName = viewResult.ViewName;

            Assert.True(string.IsNullOrEmpty(viewName) || viewName == "Index");
        }
    }
}
