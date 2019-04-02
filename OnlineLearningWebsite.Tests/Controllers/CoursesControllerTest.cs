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

        // Test 14
        [TestMethod]
        public void EditViewLoads()
        {
            //arrange

            //act
            ViewResult result = controller.Edit(1) as ViewResult;

            //assert
            Assert.AreEqual("Edit", result.ViewName);
        }

        // Test 15
        [TestMethod]
        public void EditViewWithRightCourseId()
        {
            //arrange

            //act
            var result = ((ViewResult)controller.Edit(1)).Model;

            //assert
            Assert.AreEqual(courses.SingleOrDefault(c => c.CourseId == 1), result);
        }

        // Test 16
        [TestMethod] 
        public void EditWithWrongCourseId()
        {
            //arrange

            //act
            HttpNotFoundResult result = controller.Edit(7) as HttpNotFoundResult;

            //assert
            Assert.AreEqual(404, result.StatusCode);
        }

         // Test 17
         [TestMethod]
         public void EditWithNullCourseId()
         {
            int? nullCourse = null;
            
            //arrange

            //act
            HttpStatusCodeResult result = controller.Edit(nullCourse) as HttpStatusCodeResult;

            //assert
            Assert.AreEqual(400, result.StatusCode);
         }

         // Test 18
         [TestMethod]
         public void EditPostMethodLoadsRightIndexView()
         {
             //arrange

             // act
             RedirectToRouteResult result = (RedirectToRouteResult)controller.Edit(courses[0]);

             //assert
             Assert.AreEqual("Index", result.RouteValues["action"]);
         }

         // Test 19
         [TestMethod]
         public void EditPostMethodLoadsInvalidView()
         {
            Course wrong = new Course { CourseId = 40 };

            // arrange
            controller.ModelState.AddModelError("Erroe", "Don't work");

            // act
            ViewResult result = (ViewResult)controller.Edit(wrong);

            // assert
            Assert.AreEqual("Edit", result.ViewName);
         }

        // Test 20
        [TestMethod]
        public void EditPostMethodLoadsInvalidCourse()
        {
            Course wrong = new Course { CourseId = 90 };

            // arrange
            controller.ModelState.AddModelError("Error", "Don't work");
            // act
            Course result = (Course)((ViewResult)controller.Edit(wrong)).Model;

            // assert
            Assert.AreEqual(wrong, result);
        }

        // Test 21
        [TestMethod]
        public void CreateViewLoads()
        {
            //arrange

            //act
            ViewResult result = controller.Create() as ViewResult;

            //assert
            Assert.AreEqual("Create", result.ViewName);
        }

        // Test 22
        [TestMethod]
        public void CreateValidCourse()
        {
            Course validCourse = new Course { CourseId = 4, CourseName = "SQL", CourseLevel = "Beginner", Price = 35, CategoryId = 103 };

            //act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(validCourse);

            //assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        // Test 23
        [TestMethod]
        public void CreateInvalidCourse()
        {
            Course InvalidCourse = new Course();
            
            //arrange
            controller.ModelState.AddModelError("Error", "Don't Work");

            //act
            ViewResult result = (ViewResult)controller.Create(InvalidCourse);

            //assert
            Assert.AreEqual("Create", result.ViewName);
        }
    }
}
