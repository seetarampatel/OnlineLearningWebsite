using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineLearningWebsite.Models;

namespace OnlineLearningWebsite.Controllers
{

    public class CategoriesController : Controller
    {
        // private DbModel db = new DbModel();
        IMockCategories db;

        // this constructor leads to the sql server
        public CategoriesController()
        {
            this.db = new IDataCategories();
        }

        // this constructor leads us to the moq data
        public CategoriesController(IMockCategories mockDb)
        {
            this.db = mockDb;
        }
        // GET: Categories
        public ActionResult Index()
        {
            return View("Index", db.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Category category = db.Categories.Find(id);
            Category category = db.Categories.SingleOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View("Details", category);
        }

        // GET: Categories/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryArea,CategoryName,CategoryReview")] Category category)
        {
            if (ModelState.IsValid)
            {
                // db.Categories.Add(category);
                // db.SaveChanges();
                db.Save(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Category category = db.Categories.Find(id);
            Category category = db.Categories.SingleOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryArea,CategoryName,CategoryReview")] Category category)
        {
            if (ModelState.IsValid)
            {
                // db.Entry(category).State = EntityState.Modified;
                // db.SaveChanges();
                db.Save(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Category category = db.Categories.Find(id);
            Category category = db.Categories.SingleOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View("Delete", category);
        }

        // POST: Categories/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Category category = db.Categories.Find(id);
            Category category = db.Categories.SingleOrDefault(c => c.CategoryId == id);
            // db.Categories.Remove(category);
            // db.SaveChanges();
            db.Delete(category);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
