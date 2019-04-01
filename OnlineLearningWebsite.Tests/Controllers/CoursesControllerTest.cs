using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// References
using System.Web.Mvc;
using OnlineLearningWebsite.Controllers;
using Moq;
using OnlineLearningWebsite.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineLearningWebsite.Tests.Controllers
{
    [TestClass]
    public class CoursesControllerTest
    {
        CoursesController controller;
        List<Course> courses;
        List<Category> categories;
        Mock<IMockCourses> mock;

        [TestInitialize]
        public void TestInitialize()
        {
            categories = new List<Category>
            {
                new Category {CategoryId = 100, CategoryArea = "Computer Science", CategoryName = "Web Development", CategoryReview = 9},
                new Category {CategoryId = 101, CategoryArea = "Business", CategoryName = "Business Analyst", CategoryReview = 8},
                new Category {CategoryId = 102, CategoryArea = "Design and Arts", CategoryName = "UX Designer", CategoryReview = 7},
            };

            courses = new List<Course>
            {
                new Course {CourseId = 1, CourseName = "Java", CourseLevel = "Beginner", Price = 15, CategoryId = 100},
                new Course {CourseId = 2, CourseName = "Python", CourseLevel = "Intermediate", Price = 20, CategoryId = 101},
                new Course {CourseId = 3, CourseName = "JavaScript", CourseLevel = "Advanced", Price = 25, CategoryId = 102}
            };

            mock = new Mock<IMockCourses>();
            mock.Setup(c => c.Courses).Returns(courses.AsQueryable());

            controller = new CoursesController(mock.Object);
        }

        // Test 1
        [TestMethod]
        public void IndexViewLoads()
        {
            //arrange

            //act
            ViewResult result = controller.Index() as ViewResult;

            //assert
            Assert.AreEqual("Index", result.ViewName);
        }

        // Test 2
        [TestMethod]
        public void IndexLoadsCourses()
        {
            //arrange

            //act
            var results = (List<Course>)((ViewResult)controller.Index()).Model;

            //assert
            CollectionAssert.AreEqual(courses, results);
        }

        // Test 3
        [TestMethod]
        public void DetailsViewLoads()
        {
            // arrange

            //act
            ViewResult result = controller.Details(1) as ViewResult;

            //assert
            Assert.AreEqual("Details", result.ViewName);
        }

        // Test 4
        [TestMethod]
        public void DetailsViewWithWrongCourseId()
        {
            //arrange

            //act
            HttpNotFoundResult result = controller.Details(4) as HttpNotFoundResult;

            //assert
            Assert.AreEqual(404, result.StatusCode);
        }

        // Test 5
        [TestMethod]
        public void DetailsViewWithRightCourseId()
        {
            //arrange

            //act
            var result = ((ViewResult)controller.Details(2)).Model;

            //assert
            Assert.AreEqual(courses.SingleOrDefault(c => c.CourseId == 2), result);
        }

        // Test 6
        [TestMethod]
        public void DetailsViewwithNullObject()
        {
            //arrange

            //act
            HttpStatusCodeResult result = controller.Details(null) as HttpStatusCodeResult;

            //assert
            Assert.AreEqual(400, result.StatusCode);
        }

        // Test 7
        [TestMethod]
        public void DeleteViewLoad()
        {
            //arrange

            //act
            ViewResult result = controller.Delete(2) as ViewResult;

            //assert
            Assert.AreEqual("Delete", result.ViewName);
        }

        // Test 8
        [TestMethod]
        public void DeleteWithWrongCourseId()
        {
            //arrange

            //act
            HttpNotFoundResult result = controller.Delete(4) as HttpNotFoundResult;

            //assert
            Assert.AreEqual(404, result.StatusCode);
        }

        // Test 9
        [TestMethod]
        public void DeleteWithRightCourseId()
        {
            //arrange

            //act
            var result = ((ViewResult)controller.Delete(2)).Model;

            //assert
            Assert.AreEqual(courses.SingleOrDefault(c => c.CourseId == 2), result);
        }

        // Test 10
        [TestMethod]
        public void DeleteWithNullCourseObject()
        {
            //arrange

            //act
            HttpStatusCodeResult result = controller.Delete(null) as HttpStatusCodeResult;

            //assert
            Assert.AreEqual(400, result.StatusCode);
        }

        // Test 11
        [TestMethod]
        public void DeleteConfirmedViewLoad()
        {
            //arrange

            //act
            ViewResult result = controller.Delete(2) as ViewResult;

            //assert
            Assert.AreEqual("Delete", result.ViewName);
        }

        // Test 12
        [TestMethod]
        public void DeleteConfimedRedirectIsValid()
        {
            //arrange
            RedirectToRouteResult result = controller.DeleteConfirmed(2) as RedirectToRouteResult;

            //act
            var listOfResult = result.RouteValues.ToArray();

            //assert
            Assert.AreEqual("Index", listOfResult[0].Value);
        }

        // Test 13
        [TestMethod]
        public void DeleteConfimedRedirectIsInvalid()
        {
            //arrange
            RedirectToRouteResult result = controller.DeleteConfirmed(5) as RedirectToRouteResult;

            //act
            var listOfResult = result.RouteValues.ToArray();

            //assert
            Assert.AreEqual("Index", listOfResult[0].Value);
        } 
    }
}
