using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class TimeTableReportController : Controller
    {
        private SchoolMgtSysDbEntities db = new SchoolMgtSysDbEntities();

        // GET: TimeTableReport
        public ActionResult TeacherReport(int? id)
        {
            var teacherclas = db.TimeTblTables.Where(t => t.StaffID == id && t.IsActive == true).OrderByDescending(e => e.TimeTableID);
            return View(teacherclas);
        }



        public ActionResult TeacherWiseReport()
        {
            var teacherclas = db.TimeTblTables.Where(t=> t.IsActive == true).OrderBy(e => e.StaffID);
            return View(teacherclas);
        }
        public ActionResult StudentReport(int? id)
        {
            var classid=db.StudentPromoteTables.Where(p => p.StudentID ==id && p.IsActive==true).FirstOrDefault().ClassID;
            //var classsubjectids = db.ClassSubjectTables.Where(cls => cls.ClassID == classid && cls.IsActive == true);
            //List<TimeTblTable> timetable = new List<TimeTblTable>();
            //foreach (var classsubjectid in classsubjectids) {
            //    var subjecttime = db.TimeTblTables.Where(t => t.ClassSubjectID == classsubjectid.ClassSubjectID && t.IsActive == true).FirstOrDefault();
            //    timetable.Add(new TimeTblTable
            //    {
            //        ClassSubjectID = subjecttime.ClassSubjectID,
            //        Day = subjecttime.Day,
            //        EndTime = subjecttime.EndTime,
            //        IsActive = subjecttime.IsActive,
            //        StaffID = subjecttime.StaffID,
            //        StaffTable = subjecttime.StaffTable,
            //        StartTime = subjecttime.StartTime,
            //        TimeTableID = subjecttime.TimeTableID,
            //        UserID = subjecttime.UserID,
            //        UserTable = subjecttime.UserTable
            //    });
            //}
            var subjecttime = db.TimeTblTables.Where(t => t.ClassSubjectTable.ClassID == classid && t.IsActive == true);

            return View(subjecttime);
        }
    }
}