using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class ExamReportController : Controller
    {
        // GET: ExamReport
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("http://localhost:60411/api/exammarks").Result;
            string result = response.Content.ReadAsStringAsync().Result;
            ViewBag.Message = JsonConvert.DeserializeObject(result);
            return View();
        }
    }
}