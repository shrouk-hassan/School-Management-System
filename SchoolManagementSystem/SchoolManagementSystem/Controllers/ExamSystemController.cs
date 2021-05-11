using DatabaseAccess;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class ExamSystemController : Controller
    {
       
        // GET: EXAMSYSTEM
        private SchoolMgtSysDbEntities db = new SchoolMgtSysDbEntities();

        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult tlogin()
        //{
        //    if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
        //    {
        //        return RedirectToAction("Login", "Home");
        //    }
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult slogin()
        //{
        //    if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
        //    {
        //        return RedirectToAction("Login", "Home");
        //    }
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult slogin(StudentTable s)
        //{
        //    StudentTable std = db.StudentTables.Where(x => x.Name == s.Name).SingleOrDefault();
        //    if (std == null)
        //    {
        //        ViewBag.msg = "Invalid Name !!";
        //    }
        //    else
        //    {
        //        Session["UserName"] = std.Name;
        //        return RedirectToAction("StudentExam");
        //    }
        //    return View();
        //}

        [HttpGet]
        public ActionResult AddCategory()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var tbl_category = db.tbl_category.Include(t => t.TBL_ADMIN).Include(t => t.SubjectTable);
            //List<tbl_category> li = db.tbl_category.OrderByDescending(x => x.cat_id).Include(t => t.SubjectTable).ToList();
            List<tbl_category> li = (List<tbl_category>)tbl_category;
            ViewData["list"] = li;
            ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name");

            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(tbl_category cat)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            /// Session["ad_id"] = 1;
            List<tbl_category> li = db.tbl_category.OrderByDescending(x => x.cat_id).ToList();
            ViewData["list"] = li;
            
            Random r = new Random();
            tbl_category c = new tbl_category();
            c.cat_name = cat.cat_name;
            c.cat_fk_adid = cat.cat_fk_adid;
            c.TotalMark = cat.TotalMark;
            c.cat_encyptedstring = cyptop.Encrypt(cat.cat_name.Trim() + r.Next().ToString(), true);
            ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name", cat.SubjectID);
            db.tbl_category.Add(c);
            db.SaveChanges();
            return RedirectToAction("AddCategory");
        }

        [HttpGet]
        public ActionResult Addquestion()
        {
            //int sid = Convert.ToInt32(Session["ad_id"]);
            //List<tbl_category> li = db.tbl_category.Where(x => x.cat_fk_adid == sid).ToList();
            List<tbl_category> li = db.tbl_category.ToList();
            ViewBag.list = new SelectList(li, "cat_id", "cat_name");
            return View();
        }

        [HttpPost]
        public ActionResult Addquestion(TBL_QUESTIONS q)
        {
            //int sid = Convert.ToInt32(Session["ad_id"]);
            //List<tbl_category> li = db.tbl_category.Where(x => x.cat_fk_adid == sid).ToList();
            List<tbl_category> li = db.tbl_category.ToList();
            ViewBag.list = new SelectList(li, "cat_id", "cat_name");
            db.TBL_QUESTIONS.Add(q);
            db.SaveChanges();
            TempData["msg"] = "Question Added Successfully";
            TempData.Keep();
            return RedirectToAction("Addquestion");
        }

        public ActionResult StudentExam()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult StudentExam(string room)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            List<tbl_category> list = db.tbl_category.ToList();

            foreach (var item in list)
            {
                if (item.cat_encyptedstring == room)
                {
                    List<TBL_QUESTIONS> li = db.TBL_QUESTIONS.Where(x => x.q_fk_catid == item.cat_id).ToList();
                    Queue<TBL_QUESTIONS> queue = new Queue<TBL_QUESTIONS>();
                    foreach (TBL_QUESTIONS a in li)
                    {
                        queue.Enqueue(a);
                    }
                    TempData["examid"] = item.cat_id;
                    TempData["questions"] = queue;
                    TempData["score"] = 0;
                    TempData.Keep();
                    return RedirectToAction("QuizStart");

                }
                else
                {
                    ViewBag.error = "No Room Found......";
                }
            }


            return View();
        }
        public ActionResult QuizStart()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }


            TBL_QUESTIONS q = null;
            if (TempData["questions"] != null)
            {
                Queue<TBL_QUESTIONS> qlist = (Queue<TBL_QUESTIONS>)TempData["questions"];
                if (qlist.Count > 0)
                {
                    q = qlist.Peek();
                    qlist.Dequeue();
                    TempData["questions"] = qlist;
                    TempData.Keep();
                }
                else
                {
                    return RedirectToAction("EndExam");
                }
            }
            else
            {
                return RedirectToAction("StudentExam");
            }
            return View(q);

        }
        [HttpPost]
        public ActionResult QuizStart(TBL_QUESTIONS q)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            String correctans = null;
            if (q.OPA != null)
            {
                correctans = "A";
            }
            else if (q.OPB != null)
            {
                correctans = "B";
            }
            else if (q.OPC != null)
            {
                correctans = "C";
            }
            else if (q.OPD != null)
            {

                correctans = "D";
            }
            if (correctans.Equals(q.COP))
            {
                TempData["score"] = Convert.ToInt32(TempData["score"]) + 10;
            }
            TempData.Keep();


            return RedirectToAction("QuizStart");
        }


        public ActionResult EndExam()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            TBL_Scoe e = new TBL_Scoe();
            e.StudentID = Convert.ToInt32(Session["UserID"]);
            e.cat_id = (int)TempData["examid"];
            e.ObtainMark = (int)TempData["score"];
            e.Date = DateTime.UtcNow;
            db.TBL_Scoe.Add(e);
            db.SaveChanges();
            return View();
        }
        public ActionResult viewallquestion(int? id, int? page)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return RedirectToAction("Login", "Home");
            }
            //int pagesize = 15, pageindex = 1;
            //pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            //List<TBL_QUESTIONS> li = db.TBL_QUESTIONS.Where(x => x.q_fk_catid == id).ToList();
            return View(db.TBL_QUESTIONS.Where(x => x.q_fk_catid == id).ToList());
        }

        public ActionResult records()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }
        //EDIT CAT
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
            tbl_category tbl_category = db.tbl_category.Find(id);
            if (tbl_category == null)
            {
                return HttpNotFound();
            }
            ViewBag.cat_fk_adid = new SelectList(db.TBL_ADMIN, "AD_ID", "AD_NAME", tbl_category.cat_fk_adid);
            return View(tbl_category);
        }

        // POST: tbl_category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cat_id,cat_name,cat_fk_adid,cat_encyptedstring,TotalMark")] tbl_category tbl_category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AddCategory");
            }
            ViewBag.cat_fk_adid = new SelectList(db.TBL_ADMIN, "AD_ID", "AD_NAME", tbl_category.cat_fk_adid);
            return View(tbl_category);
        }
        //DELECT CAT
        // GET: tbl_category/Delete/5
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
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            tbl_category tbl_category = db.tbl_category.Find(id);
            db.tbl_category.Remove(tbl_category);
            db.SaveChanges();
            return RedirectToAction("AddCategory");
        }

        // GET: TBL_QUESTIONS/Edit/5
        public ActionResult EditQ(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_QUESTIONS tBL_QUESTIONS = db.TBL_QUESTIONS.Find(id);
            if (tBL_QUESTIONS == null)
            {
                return HttpNotFound();
            }
            ViewBag.q_fk_catid = new SelectList(db.tbl_category, "cat_id", "cat_name", tBL_QUESTIONS.q_fk_catid);
            return View(tBL_QUESTIONS);
        }

        // POST: TBL_QUESTIONS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditQ( TBL_QUESTIONS tBL_QUESTIONS)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(tBL_QUESTIONS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("viewallquestion");
            }
            ViewBag.q_fk_catid = new SelectList(db.tbl_category, "cat_id", "cat_name", tBL_QUESTIONS.q_fk_catid);
            return View(tBL_QUESTIONS);
        }

        // GET: TBL_QUESTIONS/Delete/5
        public ActionResult DeleteQ(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_QUESTIONS tBL_QUESTIONS = db.TBL_QUESTIONS.Find(id);
            if (tBL_QUESTIONS == null)
            {
                return HttpNotFound();
            }
            return View(tBL_QUESTIONS);
        }

        // POST: TBL_QUESTIONS/Delete/5
        [HttpPost, ActionName("DeleteQ")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteQ(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            TBL_QUESTIONS tBL_QUESTIONS = db.TBL_QUESTIONS.Find(id);
            db.TBL_QUESTIONS.Remove(tBL_QUESTIONS);
            db.SaveChanges();
            return RedirectToAction("viewallquestion");
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