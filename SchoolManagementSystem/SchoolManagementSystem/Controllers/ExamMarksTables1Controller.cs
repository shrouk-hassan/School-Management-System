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
    public class ExamMarksTables1Controller : Controller
    {
        private SchoolMgtSysDbEntities db = new SchoolMgtSysDbEntities();

        // GET: ExamMarksTables1
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var examMarksTables = db.ExamMarksTables.Include(e => e.ClassSubjectTable).Include(e => e.StudentTable).Include(e => e.UserTable).OrderByDescending(e => e.MarksID);
            return View(examMarksTables.ToList());
        }

        // GET: ExamMarksTables1/Details/5
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
            ExamMarksTable examMarksTable = db.ExamMarksTables.Find(id);
            if (examMarksTable == null)
            {
                return HttpNotFound();
            }
            return View(examMarksTable);
        }

        // GET: ExamMarksTables1/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjectTables, "ClassSubjectID", "Name");
            ViewBag.ExamID = new SelectList(db.ExamTables, "ExamID", "Title");

            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name");
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName");
            return View();
        }

        public ActionResult GetByPromotID(string sid)
        {
            int promoteid = Convert.ToInt32(sid);
            var promoterecord = db.StudentPromoteTables.Find(promoteid);
            List<StudentTable> stdlist = new List<StudentTable>();
            stdlist.Add(new StudentTable {StudentID =promoterecord.StudentID, Name = promoterecord.StudentTable.Name });
            var student = promoterecord.StudentTable.Name;
            List<ClassSubjectTable> listsubjects = new List<ClassSubjectTable>();
            var classsubjects = db.ClassSubjectTables.Where(c => c.ClassID == promoterecord.ClassID && c.IsActive == true);
            foreach(var subj in classsubjects)
            {
                listsubjects.Add(new ClassSubjectTable {ClassID =subj.ClassSubjectID, Name = subj.Name });
            }
            return Json(new { std = stdlist, subjects = listsubjects }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetTotlMarks(string sid)
        {
            int classsubjectid = Convert.ToInt32(sid);
           var totalmarks = db.ClassSubjectTables.Find(classsubjectid).SubjectTable.SubjectID;
          
            return Json(new { data = totalmarks }, JsonRequestBehavior.AllowGet);
        }
        // POST: ExamMarksTables1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ExamMarksTable examMarksTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            examMarksTable.UserID = userid;
            if (ModelState.IsValid)
            {
                db.ExamMarksTables.Add(examMarksTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjectTables, "ClassSubjectID", "Name", examMarksTable.ClassSubjectID);
            ViewBag.ExamID = new SelectList(db.ExamTables, "ExamID", "Title",examMarksTable.ExamID);

            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", examMarksTable.StudentID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", examMarksTable.UserID);
            return View(examMarksTable);
        }

        // GET: ExamMarksTables1/Edit/5
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
            ExamMarksTable examMarksTable = db.ExamMarksTables.Find(id);
            if (examMarksTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjectTables, "ClassSubjectID", "Name", examMarksTable.ClassSubjectID);
            ViewBag.ExamID = new SelectList(db.ExamTables, "ExamID", "Title", examMarksTable.ExamID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", examMarksTable.StudentID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", examMarksTable.UserID);
            return View(examMarksTable);
        }

        // POST: ExamMarksTables1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( ExamMarksTable examMarksTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            examMarksTable.UserID = userid;
            if (ModelState.IsValid)
            {
                db.Entry(examMarksTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassSubjectID = new SelectList(db.ClassSubjectTables, "ClassSubjectID", "Name", examMarksTable.ClassSubjectID);
            ViewBag.ExamID = new SelectList(db.ExamTables, "ExamID", "Title", examMarksTable.ExamID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", examMarksTable.StudentID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", examMarksTable.UserID);
            return View(examMarksTable);
        }

        // GET: ExamMarksTables1/Delete/5
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
            ExamMarksTable examMarksTable = db.ExamMarksTables.Find(id);
            if (examMarksTable == null)
            {
                return HttpNotFound();
            }
            return View(examMarksTable);
        }

        // POST: ExamMarksTables1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ExamMarksTable examMarksTable = db.ExamMarksTables.Find(id);
            db.ExamMarksTables.Remove(examMarksTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

   
    }
}
