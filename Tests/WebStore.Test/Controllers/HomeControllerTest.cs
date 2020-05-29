using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Controllers;

using Assert = Xunit.Assert;//Создадим псевдоним класса Assert, что бы не использовать встроенный аналог 
namespace WebStore.Test.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Blog_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Blog();
            Assert.IsType<ViewResult>(result);
        }
        [TestMethod]
        public void BlogSingle_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.BlogSingle();
            Assert.IsType<ViewResult>(result);
        }
        [TestMethod]
        public void SomeAction_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.SomeAction();
            Assert.IsType<ViewResult>(result);
        }
        [TestMethod]
        public void Error404_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Error404();
            Assert.IsType<ViewResult>(result);
        }
        [TestMethod]
        public void ContactUs_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.ContactUs();
            Assert.IsType<ViewResult>(result);
        }
        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void Throw_throw_ApplicationException()
        {
            var controller = new HomeController();
            var result = controller.Throw(string.Empty);
        }
        [TestMethod]
        public void Throw_throw_ApplicationException_whith_Message()
        {
            var controller = new HomeController();
            const string expected_message_string = "Message!";
            //var result = controller.Throw(string.Empty);
            var exception = Assert.Throws<ApplicationException>(() => controller.Throw(expected_message_string));
            Assert.Equal(expected_message_string, exception.Message);
        }

    }
}
