﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using DB;
using Security;
using Zeiterfassung_Domain;
using Zeiterfassung_Domain.Interface;

namespace Zeiterfassung.Controllers
{
    
    public class DBController : Controller
    {
        IEmployeeLogic db = new DBEmployeeLogic();

        

        public ActionResult UpdateEmployee(Employee employee)
        {
            Session["user"] = employee;
            if (employee.Admin == true)
            {
                ViewBag.Message = db;
                db.SQLUpdateEmployee(employee);
                return View("../Home/EmployeeList", employee);
            }
            else
            {
                return RedirectToAction("Admin", "Home");
            }
        }


        public ActionResult DeleteEmployee(Employee employee, int id)
        {
            Session["user"] = employee;
            if (employee.Admin == true)
            {
                ViewBag.Message = db;
                db.SQLRemoveEmployee(id);
                return View("../Home/EmployeeList", employee);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}