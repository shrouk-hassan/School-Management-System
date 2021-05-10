using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private SchoolMgtSysDbEntities db = new SchoolMgtSysDbEntities();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginUser(string email,string password)
        {
            try
            {
                if(email !=null && password != null)
                {
                    var finduser = db.UserTables.Where(u => u.EmailAddress == email && u.Password == password).ToList();
                    if(finduser.Count() == 1)
                    {
                        Session["UserName"] = finduser[0].UserName;
                        Session["Password"] = finduser[0].Password;
                        Session["UserTypeID"] = finduser[0].UserTypeID;
                        Session["Address"] = finduser[0].Address;
                        Session["FullName"] = finduser[0].FullName;
                        Session["ContactNo"] = finduser[0].ContactNo;
                        Session["EmailAddress"] = finduser[0].EmailAddress;
                        Session["UserID"] = finduser[0].UserID;
                        var userid = finduser[0].UserID;

                        var studentphoto = db.StudentTables.Where(s =>s.UserID == userid).FirstOrDefault();
                        if(studentphoto != null)
                        {
                            Session["Photo"] = studentphoto.Photo;
                        }
                        else
                        {
                            var employee = db.StaffTables.Where(e => e.UserID == userid).FirstOrDefault();
                            Session["Photo"] = employee.Photo;
                        }

                        string url = string.Empty;
                        if(finduser[0].UserTypeID == 2)
                        {
                            return RedirectToAction("About");
                        }
                        else if (finduser[0].UserTypeID == 3)
                        {
                            return RedirectToAction("About");
                        }
                        else if (finduser[0].UserTypeID == 1)
                        {
                            url = "About";
                        }
                        else
                        {
                            url = "About";
                        }
                        return RedirectToAction(url);
                    }
                    else
                    {
                        Session["UserName"] = string.Empty;
                        Session["Password"] = string.Empty;
                        Session["UserTypeID"] = string.Empty;
                        Session["Address"] = string.Empty;
                        Session["FullName"] = string.Empty;
                        Session["ContactNo"] = string.Empty;
                        Session["EmailAddress"] = string.Empty;
                        Session["UserID"] = string.Empty;
                        ViewBag.message = "user Name or Passwerd isn't correct";
                    }
                }
                else
                {
                    Session["UserName"] = string.Empty;
                    Session["Password"] = string.Empty;
                    Session["UserTypeID"] = string.Empty;
                    Session["Address"] = string.Empty;
                    Session["FullName"] = string.Empty;
                    Session["ContactNo"] = string.Empty;
                    Session["EmailAddress"] = string.Empty;
                    Session["UserID"] = string.Empty;
                    ViewBag.message = "Some unexpected issue is occure Please Try Agin";
                }
            }
            catch(Exception ex)
            {
                Session["UserName"] = string.Empty;
                Session["Password"] = string.Empty;
                Session["UserTypeID"] = string.Empty;
                Session["Address"] = string.Empty;
                Session["FullName"] = string.Empty;
                Session["ContactNo"] = string.Empty;
                Session["EmailAddress"] = string.Empty;
                Session["UserID"] = string.Empty;
                ViewBag.message = "Some unexpected issue is occure Please Try Agin";
            }
            return View("Login");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Welcome to School";

            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult ChangePasswordU(string oldpassword, string newpassword, string confirmpassword)
        {
            if(newpassword != confirmpassword)
            {
                ViewBag.Message = "Not Matched !!";
                return View("ChangePassword");
            }
            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            var getuser = db.UserTables.Find(userid);
            if(getuser.Password == oldpassword.Trim())
            {
                getuser.Password = newpassword.Trim();
            }
            else
            {
                ViewBag.Message = "Old Password Is Incorrect !!";
                return View("ChangePassword");
            }
            db.Entry(getuser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Logout");
        }

        public ActionResult Logout()
        {
            Session["UserName"] = string.Empty;
            Session["Password"] = string.Empty;
            Session["UserTypeID"] = string.Empty;
            Session["Address"] = string.Empty;
            Session["FullName"] = string.Empty;
            Session["ContactNo"] = string.Empty;
            Session["EmailAddress"] = string.Empty;
            Session["UserID"] = string.Empty;

            return RedirectToAction("Login");
        }
    }
}