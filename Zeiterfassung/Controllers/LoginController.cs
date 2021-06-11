using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Zeiterfassung_Domain.Interface;
using Security;

namespace Zeiterfassung.Controllers
{

    public class LoginController : Controller
    {
        Hashing hash = new Hashing();
        IEmployeeLogic db = new DBEmployeeLogic();
        

        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(Zeiterfassung_Domain.Employee model, Zeiterfassung_Domain.Employee emp)
        {
            emp = db.SQLReadEmployeebyEmail(model.GivenEmail);

            Session["user"] = emp;
            if (emp != null) //if the given email has not been registered yet 
            {
                string hashedGivenPW = hash.GetHash(model.Password);
                if (hashedGivenPW == emp.Password)
                {
                    if (emp.Admin == true)
                    {
                        return RedirectToAction("Admin", "Home");
                    }
                    else
                    {
                        return View("../Home/Index", model);
                    }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
            else
            {
            return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult Logout()
        {
            return View("Login");
        }

        public ActionResult Register()
        {
            return View("Register");
        }

        
        [HttpPost]
        public ActionResult Register(Zeiterfassung_Domain.Employee model, Zeiterfassung_Domain.Employee emp)
        {
            emp = db.SQLReadEmployeebyEmail(model.GivenEmail);

            if (emp == null)
            {
                return View("Login");
            }
            else
            {
                return RedirectToAction("Register", "Login");
            }            
        }
    }
}
