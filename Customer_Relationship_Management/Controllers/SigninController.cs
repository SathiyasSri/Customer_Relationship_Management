using Customer_Relationship_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer_Relationship_Management.Controllers
{
    public class SigninController : Controller
    {
        EmpEntities db = new EmpEntities();
        public ActionResult signin()
        {
            return View();
        }
        // GET: Signin
        [HttpPost]
        public ActionResult signin(Signup signup)
        {
            if (ModelState.IsValid)
            {
                db.Signups.Add(signup);
                db.SaveChanges();
                if (signup.UserId > 0)
                {
                    TempData["Mssg"] = "Inserted Successfully";
                    ModelState.Clear();
                }
                else
                {
                    TempData["msg"] = "Incorrect username/password";
                }
                ModelState.Clear();
            }
            return View(signup);
        }
       
    }
}