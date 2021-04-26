using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Zeiterfassung_Domain.Interface;

namespace Zeiterfassung.Controllers
{

    public class LoginController : Controller
    {

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
            if (emp.Admin == true)
            {
                return RedirectToAction("Admin", "Home");
            }
            else
            {
                return View("../Home/Index", model);
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
        public ActionResult Register(Zeiterfassung_Domain.Employee model)
        {
            if (model.Password != null && model.Password == model.RepeatPassword)
            {
                ////Salz wird gewonnen
                //byte[] salt;
                //new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

                ////Hash value erstellen
                //var phash = new Rfc2898DeriveBytes(model.Password, salt, 100000);
                //byte[] hash = phash.GetBytes(20);

                ////Vorbereitung fürs salzen
                //byte[] hashBytes = new byte[36];
                //Array.Copy(salt, 0, hashBytes, 0, 16);
                //Array.Copy(hash, 0, hashBytes, 16, 20);

                ////Ordentliche Menge Salz raufpacken und in einem string speichern
                //string savedPasswordHash = Convert.ToBase64String(hashBytes);
                //model.Password = savedPasswordHash;
                /////Datenbank hinzufügen
                ///
            }
            else
            {

                return View("Register");

            }

            return View("Login");
        }



    }
}
