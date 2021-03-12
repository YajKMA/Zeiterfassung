using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using DB;
using Zeiterfassung_Domain;
using Zeiterfassung_Domain.Interface;

namespace Zeiterfassung.Controllers
{
    public class HomeController : Controller
    {


        IEmployeeLogic db = new DBEmployeeLogic();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InsertEmployees(Employee employee)
        {
            db.SQLAddEmployee(employee);
            return View("../Login/Login", employee);
        }

        public ActionResult EditEmployee(int id)
        {
            Employee employee = db.SQLReadEmployeebyID(id);
            if (employee != null)
            {
                return View("EditEmployee", employee);
            }
            return View("EmployeeList");
        }



        public ActionResult test()
        {
            return View("test");
        }

        [HttpPost]
        public ActionResult Index(Zeiterfassung_Domain.Employee model)
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult TimeStart()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Start(Zeiterfassung_Domain.Time model)
        {
            model.StartTime = DateTime.Now;
            return View("TimeStop", model);
        }
        
        [HttpPost]
        public ActionResult Stop(Zeiterfassung_Domain.Time model)
        {
            model.EndTime = DateTime.Now;
            model.ResultTime = model.EndTime - model.StartTime;
            return View("TimeStop", model);
        }

        public ActionResult EmployeeList()
        {            
            ViewBag.Message = db;
            return View();
        }
    }
}