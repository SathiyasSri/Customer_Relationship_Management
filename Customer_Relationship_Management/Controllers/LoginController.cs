using Customer_Relationship_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Customer_Relationship_Management.Controllers
{
    public class LoginController : Controller
    {
        EmpEntities empEntities = new EmpEntities();
        // GET: Login
        public ActionResult Login()
        {
            EmpEntities db = new EmpEntities();
            List<Roletype> list = empEntities.Roletypes.ToList();
            ViewBag.RoleType = new SelectList(list, "RoleName", "RoleName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Signup signup)
        {
            EmpEntities db = new EmpEntities();
           
            var user = (from userlist in db.Signups where userlist.Username.ToString() == signup.Username.ToString() && userlist.Password == signup.Password && userlist.RoleName==signup.RoleName
                            select new { userlist.UserId, userlist.Username}).ToList();
               
                if(signup.RoleName== "Employee" && user.FirstOrDefault() != null)
                {
                    Session["Username"] = user.FirstOrDefault().Username;
                    Session["UserId"] = user.FirstOrDefault().UserId;
                    return Redirect("/Employee/Dashboard");
                }
               
                else if (signup.RoleName == "Manager" && user.FirstOrDefault() != null)
                {
                    Session["Username"] = user.FirstOrDefault().Username;
                    Session["UserId"] = user.FirstOrDefault().UserId;
                    return Redirect("/Manager/Dashboard");
                }
                else
                {
                    TempData["msg"] = "Invalid login credentials.";
                }
            
            return View(signup);
        }
    }
}