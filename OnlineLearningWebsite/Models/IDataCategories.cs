using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLearningWebsite.Models
{
    public class IDataCategories : IMockCategories
    {
        private DbModel db = new DbModel();
        public IQueryable<Category> Categories { get { return db.Categories; } }

        public void Delete(Category category)
        {
            db.Categories.Remove(category);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Category Save(Category category)
        {
            if (category.CategoryId == 0)
            {
                db.Categories.Add(category);
            }
            else
            {
                db.Entry(category).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return category;

        }
    }
}