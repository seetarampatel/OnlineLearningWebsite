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
    public class CoursesController : Controller
    {
        // private DbModel db = new DbModel();
        IMockCourses db;

        // Default Constructor
        public CoursesController()
        {
            this.db = new IDataCourses();
        }

        public CoursesController(IMockCourses mockdb)
        {
            this.db = mockdb;
        }

        // GET: Courses
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Category);
            return View("Index", courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Course course = db.Courses.Find(id);
            Course course = db.Courses.SingleOrDefault(c => c.CourseId == id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View("Details", course);
        }

        // GET: Courses/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryArea");
            return View("Create");
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "CourseId,CourseName,CourseLevel,Price,CategoryId")] Course course)
        {
            if (ModelState.IsValid)
            {
                // db.Courses.Add(course);
                // db.SaveChanges();
                db.Save(course);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryArea", course.CategoryId);
            return View("Create", course);
        }

        // GET: Courses/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Course course = db.Courses.Find(id);
            Course course = db.Courses.SingleOrDefault(c => c.CourseId == id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryArea", course.CategoryId);
            return View("Edit", course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "CourseId,CourseName,CourseLevel,Price,CategoryId")] Course course)
        {
            if (ModelState.IsValid)
            {
                // db.Entry(course).State = EntityState.Modified;
                // db.SaveChanges();
                db.Save(course);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryArea", course.CategoryId);
            return View("Edit", course);
        }

        // GET: Courses/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Course course = db.Courses.Find(id);
            Course course = db.Courses.SingleOrDefault(c => c.CourseId == id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View("Delete", course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            // Course course = db.Courses.Find(id);
            Course course = db.Courses.SingleOrDefault(c => c.CourseId == id);
            // db.Courses.Remove(course);
            // db.SaveChanges();
            db.Delete(course);
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
