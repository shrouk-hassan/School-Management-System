using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolMGTWebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Data.Entity;
namespace SchoolMGTWebApi.Controllers
{
    public class StaffAttendenceController : ApiController
    {
        public IHttpActionResult GetAllStaff()
        {
            using (var dataContext = new SchoolMgtSysDbEntities())
            {
                var data = (from s in dataContext.StaffTables
                            join sa in dataContext.StaffAttendanceTables
                            on s.StaffID equals sa.StaffID
                            join d in dataContext.DesignationTables
                            on s.DesignationID equals d.DesignationID
                            select new { Name = s.Name, Designation = d.Title, Date = sa.AttendDate, CheckIn = sa.ComingTime, CheckOut = sa.ClosingTime}).ToList();
                return Ok(data);
            }
        }
    }
}