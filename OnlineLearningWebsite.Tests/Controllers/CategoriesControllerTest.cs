using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// adding new references
using System.Web.Mvc;
using OnlineLearningWebsite.Controllers;
using Moq;
using System.Linq;
using OnlineLearningWebsite.Models;
using System.Collections.Generic;

namespace OnlineLearningWebsite.Tests.Controllers
{
    [TestClass]
    public class CategoriesControllerTest
    {
        CategoriesController controller;
        List<Category> categories;
        Mock<IMockCategories> mock;

        [TestInitialize] 
        public void TestInitialize()
        {
            categories = new List<Category>()
            {
                new Category { CategoryId = 1, CategoryArea = "Programming", CategoryName = "Java", CategoryReview = 9},
                new Category { CategoryId = 2, CategoryArea = "Business", CategoryName = "Business Analyst", CategoryReview = 8},
                new Category { CategoryId = 3, CategoryArea = "Programming", CategoryName = "Python", CategoryReview = 7}
            };

            mock = new Mock<IMockCategories>();

            mock.Setup(c => c.Categories).Returns(categories.AsQueryable());

            // initialize the controller and inject the mock object
            controller = new CategoriesController(mock.Object);
        }

        [TestMethod]
        public void IndexViewLoads()
        {
            ViewResult result = controller.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexViewCategories()
        {
            var results = (List<Category>)((ViewResult)controller.Index()).Model;

            CollectionAssert.AreEqual(categories, results);
        }

        [TestMethod]
        public void DetailsViewLoads()
        {
            ViewResult result = controller.Details(1) as ViewResult;

            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void DetailsViewWithRightCourseId()
        {
            var result = ((ViewResult)controller.Details(2)).Model;

            //assert
            Assert.AreEqual(categories.SingleOrDefault(c => c.CategoryId == 2), result);
        }

        [TestMethod]
        public void DetailsViewWithWrongCourseId()
        {
            HttpNotFoundResult result = controller.Details(7) as HttpNotFoundResult;

            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public void DetailsViewWithNullObject()
        {
            HttpStatusCodeResult result = controller.Details(null) as HttpStatusCodeResult;

            //assert
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void DeleteViewLoads()
        {
            ViewResult result = controller.Delete(1) as ViewResult;

            Assert.AreEqual("Delete", result.ViewName);
        }

        [TestMethod]
        public void DeleteViewWithRightCourseId()
        {
            var result = ((ViewResult)controller.Delete(1)).Model;

            Assert.AreEqual(categories.SingleOrDefault(c => c.CategoryId == 1), result);
        }

        [TestMethod]
        public void DeleteViewWithWrongCourseId()
        {
            HttpNotFoundResult result = controller.Delete(7) as HttpNotFoundResult;

            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public void DeleteViewWithNullObject()
        {
            HttpStatusCodeResult result = controller.Delete(null) as HttpStatusCodeResult;

            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void DeleteConfirmedViewLoads()
        {
            ViewResult result = controller.Delete(2) as ViewResult;

            Assert.AreEqual("Delete", result.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedRedirectIsValid()
        {
            RedirectToRouteResult result = controller.DeleteConfirmed(2) as RedirectToRouteResult;

            var listOfResult = result.RouteValues.ToArray();

            Assert.AreEqual("Index", listOfResult[0].Value);
        }

        [TestMethod]
        public void DeleteConfirmedRedirectIsInvalid()
        {
            RedirectToRouteResult result = controller.DeleteConfirmed(8) as RedirectToRouteResult;

            var listOfResult = result.RouteValues.ToArray();

            Assert.AreEqual("Index", listOfResult[0].Value);
        }
    }
}
