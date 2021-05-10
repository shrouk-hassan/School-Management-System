using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class EmployeeCertficateController : Controller
    {
        private SchoolMgtSysDbEntities db = new SchoolMgtSysDbEntities();
        // GET: EmployeeCertficate
        public ActionResult ExperienceC(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var employee = db.StaffTables.Where(s => s.StaffID == id).FirstOrDefault();
            //ViewBag.FromDte = employee.RegistrationDate.ToString("yyyy/mm/dd");
            //ViewBag.FromDate = employee.RegistrationDate.ToString();
            if (employee.StaffAttendanceTables != null)
            {
              ViewBag.ToDate = employee.EmployeeLeavingTables.OrderByDescending(s => s.LeavingDate).FirstOrDefault().LeavingDate;

               //// ViewBag.ToDate = employee.StaffAttendanceTables.OrderByDescending(s => s.AttendDate).FirstOrDefault().AttendDate;
            }
            else
            {
                ViewBag.ToDate = DateTime.Now.ToString("yyyy/dd/mm");
            }
            return View();
        }
    }
}