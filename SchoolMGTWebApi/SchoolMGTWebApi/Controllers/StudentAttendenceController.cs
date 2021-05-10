using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolMGTWebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace SchoolMGTWebApi.Controllers
{
    public class StudentAttendenceController : ApiController
    {
        public IHttpActionResult GetAllStudents()
        {
            using (var dataContext = new SchoolMgtSysDbEntities())
            {
                var data = (from s in dataContext.StudentTables
                            join c in dataContext.ClassTables
                            on s.ClassID equals c.ClassID
                            join a in dataContext.AttendanceTables
                            on s.ClassID equals a.ClassID
                            where a.StudentID == s.StudentID
                            select new { Name = s.Name + " " + s.FatherName, Class = c.Name, a.AttendDate, a.AttendTime }).ToList();
                return Ok(data);
            }
        }
    }
}