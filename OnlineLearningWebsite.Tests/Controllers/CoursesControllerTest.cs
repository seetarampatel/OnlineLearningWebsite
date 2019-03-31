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

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
