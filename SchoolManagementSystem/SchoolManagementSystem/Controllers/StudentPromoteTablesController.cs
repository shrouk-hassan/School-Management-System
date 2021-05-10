using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseAccess;

namespace SchoolManagementSystem.Controllers
{
    public class StudentPromoteTablesController : Controller
    {
        private SchoolMgtSysDbEntities db = new SchoolMgtSysDbEntities();

        // GET: StudentPromoteTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var studentPromoteTables = db.StudentPromoteTables.Include(s => s.ClassTable).Include(s => s.ProgrameSessionTable).Include(s => s.SectionTable).Include(s => s.StudentTable).OrderByDescending(e=>e.StudentPromotedID);
            return View(studentPromoteTables.ToList());
        }
        public ActionResult GetPromotClsList(string sid)
        {
            int studentid = Convert.ToInt32(sid);
            var student = db.StudentTables.Find(studentid);
            int promoteid = 0;
            try
            {
                promoteid = db.StudentPromoteTables.Where(p => p.StudentID == studentid).Max(m => m.StudentPromotedID);
            }
            catch { promoteid = 0; }
            List<ClassTable> classTable = new List<ClassTable>();
            if (promoteid > 0)
            {
                var promotetable = db.StudentPromoteTables.Find(promoteid);
                foreach (var item in db.ClassTables.Where(cls => cls.ClassID > promotetable.ClassID))
                {
                    classTable.Add(new ClassTable { ClassID = item.ClassID, Name = item.Name });
                }
            }
            else
            {
                foreach (var cls in db.ClassTables.Where(cls => cls.ClassID > student.ClassID))
                {
                    classTable.Add(new ClassTable { ClassID = cls.ClassID, Name = cls.Name });
                }
            }
            return Json(new { data = classTable }, JsonRequestBehavior.AllowGet);
        }

        // GET: StudentPromoteTables/Details/5
        public ActionResult Details(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentPromoteTable studentPromoteTable = db.StudentPromoteTables.Find(id);
            if (studentPromoteTable == null)
            {
                return HttpNotFound();
            }
            return View(studentPromoteTable);
        }

        // GET: StudentPromoteTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name");
            ViewBag.ProgramSessionID = new SelectList(db.ProgrameSessionTables, "ProgrameSessionID", "Details");
            ViewBag.SectionID = new SelectList(db.SectionTables, "SectionID", "SectionName");
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name");
            return View();
        }

        // POST: StudentPromoteTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentPromoteTable studentPromoteTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.StudentPromoteTables.Add(studentPromoteTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name", studentPromoteTable.ClassID);
            ViewBag.ProgramSessionID = new SelectList(db.ProgrameSessionTables, "ProgrameSessionID", "Details", studentPromoteTable.ProgramSessionID);
            ViewBag.SectionID = new SelectList(db.SectionTables, "SectionID", "SectionName", studentPromoteTable.SectionID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", studentPromoteTable.StudentID);
            return View(studentPromoteTable);
        }

        // GET: StudentPromoteTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentPromoteTable studentPromoteTable = db.StudentPromoteTables.Find(id);
            if (studentPromoteTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name", studentPromoteTable.ClassID);
            ViewBag.ProgramSessionID = new SelectList(db.ProgrameSessionTables, "ProgrameSessionID", "Details", studentPromoteTable.ProgramSessionID);
            ViewBag.SectionID = new SelectList(db.SectionTables, "SectionID", "SectionName", studentPromoteTable.SectionID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", studentPromoteTable.StudentID);
            return View(studentPromoteTable);
        }

        // POST: StudentPromoteTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( StudentPromoteTable studentPromoteTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(studentPromoteTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name", studentPromoteTable.ClassID);
            ViewBag.ProgramSessionID = new SelectList(db.ProgrameSessionTables, "ProgrameSessionID", "Details", studentPromoteTable.ProgramSessionID);
            ViewBag.SectionID = new SelectList(db.SectionTables, "SectionID", "SectionName", studentPromoteTable.SectionID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", studentPromoteTable.StudentID);
            return View(studentPromoteTable);
        }

        // GET: StudentPromoteTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentPromoteTable studentPromoteTable = db.StudentPromoteTables.Find(id);
            if (studentPromoteTable == null)
            {
                return HttpNotFound();
            }
            return View(studentPromoteTable);
        }

        // POST: StudentPromoteTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            StudentPromoteTable studentPromoteTable = db.StudentPromoteTables.Find(id);
            db.StudentPromoteTables.Remove(studentPromoteTable);
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
