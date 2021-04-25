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
        ITimeLogic tdb = new DBTimeLogic();

        

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Admin()
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
        public ActionResult Start(ModelCombiner mc)
        {
            mc.timemodel = new Time();
            mc.timemodel.StartTime = DateTime.Now;
            return View("TimeStop", mc);
        }
        
        
        [HttpPost]
        public ActionResult Stop(ModelCombiner mc)
        {
            mc.employeemodel = (Employee)Session["user"];
            if (mc.employeemodel.GivenEmail != null)
            {
                mc.timemodel.EndTime = DateTime.Now;
                mc.timemodel.ResultTime = mc.timemodel.EndTime - mc.timemodel.StartTime;
                tdb.SQLAddTime(mc.timemodel, mc.employeemodel.GivenEmail);
                return View("TimeStop", mc);
            }
            else
            {
                return View("Login");
            }           
        }

        public ActionResult EmployeeList()
        {            
            ViewBag.Message = db;
            return View();
        }


        public ActionResult TimeView()
        {
            ModelCombiner mc = new ModelCombiner();
            mc.employeemodel = (Employee)Session["User"];
            ViewBag.Message = tdb.SQLReadTimebyID(mc.employeemodel.GivenEmail);   
            return View();
        }
    }
}