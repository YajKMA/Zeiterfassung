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

        public ActionResult EditEmployee(Employee emp, int id)
        {
            Session["user"] = emp;
            if (emp.Admin == true)
            {
                Employee employee = db.SQLReadEmployeebyID(id);
                if (employee != null)
                {
                    return View("EditEmployee", employee);
                }
                return View("EmployeeList");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }



        [HttpPost]
        public ActionResult Index(Employee model)
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
            if (mc.employeemodel.Email != null)
            {
                mc.timemodel.EndTime = DateTime.Now;
                mc.timemodel.ResultTime = mc.timemodel.EndTime - mc.timemodel.StartTime;
                tdb.SQLAddTime(mc.timemodel, mc.employeemodel.EmployeeNumber);
                return View("TimeStop", mc);
            }
            else
            {
                return View("Login");
            }           
        }

        public ActionResult EmployeeList(Employee emp)
        {
            emp = (Employee)Session["user"];
            if (emp.Admin == true)
            {
                ViewBag.Message = db;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }           
        }


        public ActionResult TimeView()
        {
            ModelCombiner mc = new ModelCombiner();
            mc.employeemodel = (Employee)Session["User"];
            ViewBag.Message = tdb.SQLReadTimebyID(mc.employeemodel.EmployeeNumber);   
            return View();
        }

        public ActionResult AdminTimeView(Employee emp, int id)
        {
            emp = (Employee)Session["user"];
            if (emp.Admin == true)
            {
                ModelCombiner mc = new ModelCombiner();
                mc.employeemodel = db.SQLReadEmployeebyID(id);
                ViewBag.Message = tdb.SQLReadTimebyID(mc.employeemodel.EmployeeNumber);
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }
    }
}