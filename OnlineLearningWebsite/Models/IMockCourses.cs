using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningWebsite.Models
{
    public interface IMockCourses
    {
        IQueryable<Course> Courses { get; }
        IQueryable<Category> Categories { get; }

        Course Save(Course course);

        void Delete(Course course);

        void Dispose();

    }
}
