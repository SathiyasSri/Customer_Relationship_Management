using Customer_Relationship_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Customer_Relationship_Management.Controllers
{
    public class CustomersController : Controller
    {
        CustomerEntities db = new CustomerEntities();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Signupcustomer signupcustomer)
        {
            CustomerEntities ce = new CustomerEntities();
            
            var custuser = (from userlist in ce.Signupcustomers
                            where  userlist.Username.ToString() == signupcustomer.Username.ToString() && userlist.Password == signupcustomer.Password 
                            select new { userlist.CustomerId, userlist.Username }).ToList();
             if ( custuser.FirstOrDefault() != null)
            {
                Session["Username"] = custuser.FirstOrDefault().Username;
                Session["CustomerId"] = custuser.FirstOrDefault().CustomerId;
                return Redirect("/Customers/Index");
            }
            else
            {
                TempData["msg"] = "Invalid login credentials.";
            }
                return View();
        }
            public ActionResult custSignup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CustSignup(Signupcustomer signupcustomer,string roll)
        {
            CustomerEntities customerEntities = new CustomerEntities();
            var custid = signupcustomer.CustomerId;
            var customerid = (from x in customerEntities.customers where custid == x.customerId select x).ToList();
            var notavl = (from x in customerEntities.customers where custid != x.customerId select x).ToList();
            if (ModelState.IsValid)
            {
                if (customerid.Count > 0)
                {
                    ViewBag.allotduplicate = "* " + signupcustomer.CustomerId+ " already exits";
                    return View();
                }
                else if (notavl.Count > 0)
                {
                    ViewBag.allotduplicate = "* " + signupcustomer.CustomerId + " Not Avaliable!";
                    return View();
                }
                else if (ModelState.IsValid)
                {
                    db.Signupcustomers.Add(signupcustomer);

                    db.SaveChanges();
                }

                if (signupcustomer.CustomerId > 0)
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
            return View(signupcustomer);
        }

        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(customer customer)
        {
            var cid = customer.customerId;

            var userId = (from x in db.customers where cid == x.customerId select x).ToList();
            var notavl = (from x in db.customers where cid != x.customerId select x).ToList();

            if (ModelState.IsValid)
            {
                if (userId.Count > 0)
                {
                    ViewBag.duplicate = "* ID " + customer.customerId + " already exits";
                    return View();
                }
                else if (notavl.Count > 0)
                {
                    ViewBag.duplicate = "* ID " + customer.customerId + " Not Avaliable!";
                    return View();
                }
                else
                {
                    db.customers.Add(customer);

                    if (db.SaveChanges() > 0)
                    {
                        TempData["save"] = "saved";
                        return RedirectToAction("Index", "Customers");
                    }
                    else
                    {
                        TempData["unsave"] = "try again";
                        return View();
                    }
                }
            }
            else
            {
                return View();
            }

        }

        public ActionResult Queries()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Queries(query Query)
        {
            db.queries.Add(Query);

            if (ModelState.IsValid)
            {
                db.SaveChanges();
                TempData["Qmsg1"] = "success";
                ModelState.Clear();
                return RedirectToAction("QueriesList", "Customers");
            }
            else
            {
                TempData["Qmsg2"] = "try again";
                return View();
            }

        }
        public ActionResult Querieslist()
        {
            return View(db.queries.ToList());
        }
        public ActionResult QDelete(int Id)
        {
            db.queries.Remove(db.queries.Where(x => x.Queryno == Id).FirstOrDefault());
            db.SaveChanges();
            return RedirectToAction("QueriesList", "Customers");

        }
        public ActionResult Customerreq()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Customerreq(Request request)
        {
                db.Requests.Add(request);

            if (ModelState.IsValid)
            {
                db.SaveChanges();
                TempData["Qmsg1"] = "success";
                ModelState.Clear();
                return RedirectToAction("RequestList", "Customers");
            }
            else
            {
                TempData["Qmsg2"] = "try again";
                return View();
            }

        }
        public ActionResult Requestlist()
        {
            return View(db.Requests.ToList());
        }
        public ActionResult RDelete(int Id)
        {
            db.Requests.Remove(db.Requests.Where(x => x.Requestno == Id).FirstOrDefault());
            db.SaveChanges();
            return RedirectToAction("RequestList", "Customers");

        }
    }
}