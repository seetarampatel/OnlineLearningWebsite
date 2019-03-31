using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLearningWebsite.Models
{
    public class IDataCourses : IMockCourses
    {
        private DbModel db = new DbModel();

        public IQueryable<Course> Courses { get { return db.Courses; } }

        public IQueryable<Category> Categories { get { return db.Categories; } }

        public void Delete(Course course)
        {
            db.Courses.Remove(course);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Course Save(Course course)
        {
            if (course.CourseId == 0)
            {
                db.Courses.Add(course);
            }
            else
            {
                db.Entry(course).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return course;
        }
    }
}