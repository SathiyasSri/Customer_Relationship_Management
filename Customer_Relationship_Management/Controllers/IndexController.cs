using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer_Relationship_Management.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Role()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Select", Value = "0" });
            li.Add(new SelectListItem { Text = "EmployeeSignup", Value = "1" });
            li.Add(new SelectListItem { Text = "CustomerSignup", Value = "2" });
            li.Add(new SelectListItem { Text = "ManagerSignup", Value = "3" });

            ViewData["Role"] = li;
            return View();
        }
        [HttpPost]
        public ActionResult Role(string Role)
        {
            if (Role == "0")
            {
                return View();
            }
            else if (Role == "1")
            {
                return RedirectToAction("signin", "Signin", new { Rolename = "Employee" });
            }
            else if(Role=="2")
            {
                return RedirectToAction("CustSignup", "Customers", new { Rolename = "Customer"});
            }
            else if(Role=="3")
            {
                return RedirectToAction("signin", "Signin", new { Rolename = "Manager"});
            }

            return View();
        }
    }
}