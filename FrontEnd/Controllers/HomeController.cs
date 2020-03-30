using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

using System.Net;

using System.Net.Http.Headers;
using System.Threading.Tasks;
using FrontEnd.Models;
using System.Web.Script.Serialization;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private static readonly HttpClient client;

        static HomeController()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            var url = string.Format(System.Configuration.ConfigurationManager.AppSettings["BackEndAPIURL"], "Values");
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            ViewBag.Message = content;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<ActionResult> Course()
        {
            var url = string.Format(System.Configuration.ConfigurationManager.AppSettings["BackEndAPIURL"], "Courses");
            var response = await client.GetAsync(url);
            string data = await response.Content.ReadAsStringAsync();
            //use JavaScriptSerializer from System.Web.Script.Serialization
            var JSserializer = new JavaScriptSerializer();
            //deserialize to your class
            var courses = JSserializer.Deserialize<List<Course>>(data);                        

            return View(courses);

        }

        public async Task<ActionResult> Student()
        {
            var url = string.Format(System.Configuration.ConfigurationManager.AppSettings["BackEndAPIURL"], "Students");
            var response = await client.GetAsync(url);
            string data = await response.Content.ReadAsStringAsync();
            //use JavaScriptSerializer from System.Web.Script.Serialization
            var JSserializer = new JavaScriptSerializer();
            //deserialize to your class
            var students = JSserializer.Deserialize<List<Student>>(data);

            return View(students);
        }

        public ActionResult Enrollment()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}