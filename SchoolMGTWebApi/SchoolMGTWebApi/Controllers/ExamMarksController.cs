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
    public class ExamMarksController : ApiController
    {
        public IHttpActionResult GetAllStudents()
        {
            //using (var dataContext = new SchoolMgtSysDbEntities())
            //{
            //    var data = dataContext.StudentTables.Select(s => new { s.Name, s.FatherName }).ToList();
            //    return Ok(data);
            //}

            using(var dataContext = new SchoolMgtSysDbEntities())
            {
                var data = (from s in dataContext.StudentTables
                           join sc in dataContext.TBL_Scoe
                           on s.StudentID equals sc.StudentID
                           join c in dataContext.tbl_category
                           on sc.cat_id equals c.cat_id
                           select new { Name = (s.Name + " " + s.FatherName), c.cat_name, sc.ObtainMark, c.TotalMark}).ToList();
                return Ok(data);
            }
        }
    }
}
