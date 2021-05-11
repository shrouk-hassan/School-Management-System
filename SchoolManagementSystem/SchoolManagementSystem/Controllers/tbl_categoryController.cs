using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseAccess;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class tbl_categoryController : Controller
    {
        private SchoolMgtSysDbEntities db = new SchoolMgtSysDbEntities();

        // GET: tbl_category
        public ActionResult Index()
        {
           
            var tbl_category = db.tbl_category.Include(t => t.TBL_ADMIN).Include(t => t.SubjectTable);
            return View(tbl_category.ToList());
        }

        // GET: tbl_category/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_category tbl_category = db.tbl_category.Find(id);
            if (tbl_category == null)
            {
                return HttpNotFound();
            }
            return View(tbl_category);
        }

        // GET: tbl_category/Create
        public ActionResult Create()
        {
           
            ViewBag.cat_fk_adid = new SelectList(db.TBL_ADMIN, "AD_ID", "AD_NAME");
            ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name");
            return View();
        }

        // POST: tbl_category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_category cat)
        {
            
            Random r = new Random();
            //tbl_category c = new tbl_category();
            //c.cat_name = cat.cat_name;
            //c.cat_fk_adid = cat.cat_fk_adid;
            //c.TotalMark = cat.TotalMark;
            cat.cat_encyptedstring = cyptop.Encrypt(cat.cat_name.Trim() + r.Next().ToString(), true);
            //ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name", cat.SubjectID);
            //db.tbl_category.Add(c);
            //db.SaveChanges();
            if (ModelState.IsValid)
            {
                db.tbl_category.Add(cat);
                db.SaveChanges();
               
                return RedirectToAction("Index");
            }
            //Random r = new Random();
            //tbl_category c = new tbl_category();
            //c.cat_name = cat.cat_name;
            //c.cat_fk_adid = cat.cat_fk_adid;
            //c.TotalMark = cat.TotalMark;
            //c.cat_encyptedstring = cyptop.Encrypt(cat.cat_name.Trim() + r.Next().ToString(), true);
            //ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name", cat.SubjectID);
            //db.tbl_category.Add(c);
            //db.SaveChanges();
           // return RedirectToAction("AddCategory");


            //ViewBag.cat_fk_adid = new SelectList(db.TBL_ADMIN, "AD_ID", "AD_NAME", tbl_category.cat_fk_adid);
            //ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name", tbl_category.SubjectID);
            return View(cat);
        }

        // GET: tbl_category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_category tbl_category = db.tbl_category.Find(id);
            if (tbl_category == null)
            {
                return HttpNotFound();
            }
            ViewBag.cat_fk_adid = new SelectList(db.TBL_ADMIN, "AD_ID", "AD_NAME", tbl_category.cat_fk_adid);
            ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name", tbl_category.SubjectID);
            return View(tbl_category);
        }

        // POST: tbl_category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( tbl_category tbl_category)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(tbl_category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cat_fk_adid = new SelectList(db.TBL_ADMIN, "AD_ID", "AD_NAME", tbl_category.cat_fk_adid);
            ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name", tbl_category.SubjectID);
            return View(tbl_category);
        }

        // GET: tbl_category/Delete/5
        public ActionResult Delete(int? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_category tbl_category = db.tbl_category.Find(id);
            if (tbl_category == null)
            {
                return HttpNotFound();
            }
            return View(tbl_category);
        }

        // POST: tbl_category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            tbl_category tbl_category = db.tbl_category.Find(id);
            db.tbl_category.Remove(tbl_category);
            db.SaveChanges();
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
