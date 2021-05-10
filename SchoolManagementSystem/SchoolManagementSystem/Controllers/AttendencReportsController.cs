using DatabaseAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class AttendencReportsController : Controller
    {
        // GET: AttendencReports
        public ActionResult AllStudents()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("http://localhost:60411/api/studentattendence").Result;
            string result = response.Content.ReadAsStringAsync().Result;
            ViewBag.Message = JsonConvert.DeserializeObject(result);
            return View();
        }

        public ActionResult AllStaff()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("http://localhost:60411/api/staffattendence").Result;
            string result = response.Content.ReadAsStringAsync().Result;
            ViewBag.Message = JsonConvert.DeserializeObject(result);
            return View();
        }
    }
}