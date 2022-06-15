using Customer_Relationship_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;
using System.Net;

namespace Customer_Relationship_Management.Controllers
{
    public class EmployeeController : Controller
    {
        EmpEntities empEntities = new EmpEntities();
        CustomerEntities ce = new CustomerEntities();

        public ActionResult Dashboard()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Dashboard(string Sorting_Order, string Search_Data, string Search, int? Page_No, string Filter_Value)
        {
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.CustomerId = String.IsNullOrEmpty(Search_Data) ? "Custid" : "";
            ViewBag.SortingName = String.IsNullOrEmpty(Search_Data) ? "Name" : "";
            ViewBag.SortingEmpid = String.IsNullOrEmpty(Search_Data) ? "EmpId" : "";
            ViewBag.SortingEmail = String.IsNullOrEmpty(Search_Data) ? "Email" : "";
            ViewBag.SortingAddress = String.IsNullOrEmpty(Search_Data) ? "Address" : "";
            ViewBag.SortingPhone = String.IsNullOrEmpty(Search_Data) ? "Phone" : "";

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }
            ViewBag.FilterValue = Search_Data;
            ViewBag.FilterValue = Search_Data;
            var customers = from cust in ce.customers select cust ;

            if (Search_Data == "Name")
            {
                var model = ce.customers.Where(cust => cust.customerName == Search || Search == null).ToList().ToPagedList(Page_No ?? 1, 3);
                return View(model);
            }
            else if (Search_Data == "Empid")
            {
                var model = ce.customers.Where(cust => cust.EmployeeId.ToString() == Search || Search == null).ToList().ToPagedList(Page_No ?? 1, 3);
                return View(model);
            }
            switch (Sorting_Order)
            {
                case "Custid":
                    customers = customers.OrderBy(cust => cust.customerId);
                    break;
                case "Name":
                    customers = customers.OrderBy(cust => cust.customerName);
                    break;
                case "EmpId":
                    customers = customers.OrderBy(cust => cust.EmployeeId);
                    break;
                case "Address":
                    customers = customers.OrderBy(cust => cust.contactaddress);
                    break;
                case "Phone":
                    customers = customers.OrderBy(cust => cust.phone);
                    break;
                case "Email":
                    customers = customers.OrderBy(cust => cust.email);
                    break;
                default:
                    customers = customers.OrderBy(cust => cust.customerName);
                    break;
            }

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            return View(customers.ToPagedList(No_Of_Page, Size_Of_Page));
        }
        public ActionResult Update([Bind(Include = "customerId,customerName,phone,email,contactaddress,EmployeeId")]  int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = ce.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public ActionResult Update([Bind(Include = "customerId,customerName,phone,email,contactaddress,EmployeeId")] customer customer)
        {
            if (ModelState.IsValid)
            {
                ce.Entry(customer).State = EntityState.Modified;
                ce.SaveChanges();
                return RedirectToAction("Dashboard", "Employee");
            }
            return RedirectToAction("Dashboard", "Employee");

        }
        public ActionResult Profiles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = empEntities.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profiles(employee empl)
        {
            if (ModelState.IsValid)
            {
                empEntities.Entry(empl).State = EntityState.Modified;
                empEntities.SaveChanges();
                return RedirectToAction("Dashboard", "Employee");
            }
            return View(empl);
        }
        
        public ActionResult CustomerQuery()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CustomerQuery(string Sorting_Order, string Search_Data, string Search, int? Page_No, string Filter_Value)
        {
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.CustomerId = String.IsNullOrEmpty(Search_Data) ? "Custid" : "";
            ViewBag.SortingEmployeeId  = String.IsNullOrEmpty(Search_Data) ? "empid" : "";
            ViewBag.SortingQsubject = String.IsNullOrEmpty(Search_Data) ? "qsub" : "";
            ViewBag.SortingQmessage = String.IsNullOrEmpty(Search_Data) ? "qmess" : "";
            ViewBag.SortingcustEmail = String.IsNullOrEmpty(Search_Data) ? "email" : "";
            ViewBag.SortingQueryno = String.IsNullOrEmpty(Search_Data) ? "qno" : "";

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }
            ViewBag.FilterValue = Search_Data;
            ViewBag.FilterValue = Search_Data;
            var quers = from query in ce.queries select query;

            if (Search_Data == "Name")
            {
                var model = ce.queries.Where(query => query.custname == Search || Search == null).ToList().ToPagedList(Page_No ?? 1, 3);
                return View(model);
            }
            else if (Search_Data == "CustomerId")
            {
                var model = ce.queries.Where(query => query.custId.ToString() == Search || Search == null).ToList().ToPagedList(Page_No ?? 1, 3);
                return View(model);
            }
            switch (Sorting_Order)
            {
                case "Custid":
                    quers = quers.OrderBy(query => query.custname);
                    break;
                case "Name":
                    quers = quers.OrderBy(query => query.EmpId);
                    break;
                case "EmpId":
                    quers =  quers.OrderBy(query => query.Qsubject);
                    break;
                case "Address":
                    quers = quers.OrderBy(query => query.Qmessage);
                    break;
                case "Phone":
                    quers = quers.OrderBy(query => query.custEmail);
                    break;
                case "Email":
                    quers= quers.OrderBy(query => query.Queryno);
                    break;
                default:
                    quers= quers.OrderBy(query => query.custname);
                    break;
            }

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            return View(quers.ToPagedList(No_Of_Page, Size_Of_Page));
        }


        public ActionResult CustomerRequest()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CustomerResquest(string Sorting_Order, string Search_Data, string Search, int? Page_No, string Filter_Value)
        {
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.CustomerId = String.IsNullOrEmpty(Search_Data) ? "custId" : "";
            ViewBag.SortingEmployeeId = String.IsNullOrEmpty(Search_Data) ? "custname" : "";
            ViewBag.SortingQsubject = String.IsNullOrEmpty(Search_Data) ? "EmpId" : "";
            ViewBag.SortingQmessage = String.IsNullOrEmpty(Search_Data) ? "custEmail" : "";
            ViewBag.SortingcustEmail = String.IsNullOrEmpty(Search_Data) ? "message" : "";
            ViewBag.SortingQueryno = String.IsNullOrEmpty(Search_Data) ? "requestno" : "";

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }
            ViewBag.FilterValue = Search_Data;
            ViewBag.FilterValue = Search_Data;
            var crequest = from Request in ce.Requests select Request;

            if (Search_Data == "Name")
            {
                var model = ce.Requests.Where(Request => Request.custname == Search || Search == null).ToList().ToPagedList(Page_No ?? 1, 3);
                return View(model);
            }
            else if (Search_Data == "CustomerId")
            {
                var model = ce.Requests.Where(Request => Request.custId.ToString() == Search || Search == null).ToList().ToPagedList(Page_No ?? 1, 3);
                return View(model);
            }
            switch (Sorting_Order)
            {
                case "custId":
                    crequest = crequest.OrderBy(Request=> Request.custId);
                    break;
                case "custname":
                    crequest = crequest.OrderBy(Request=> Request.custname);
                    break;
                case "EmpId":
                    crequest = crequest.OrderBy(Request => Request.EmpId);
                    break;
                case "custEmail":
                    crequest = crequest.OrderBy(Request => Request.custEmail);
                    break;
                case "message":
                    crequest = crequest.OrderBy(Request => Request.message);
                    break;
                case "requestno":
                    crequest = crequest.OrderBy(Request => Request.Requestno);
                    break;
                default:
                    crequest = crequest.OrderBy(Request => Request.custId);
                    break;
            }

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            return View(crequest.ToPagedList(No_Of_Page, Size_Of_Page));
        }


        public ActionResult Payrollinfo()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Payrollinfo(string Sorting_Order, string Search_Data, string Search, int? Page_No, string Filter_Value)
        {
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingEmp_Id = String.IsNullOrEmpty(Search_Data) ? "Empid" : "";
            ViewBag.SortingClassType = String.IsNullOrEmpty(Search_Data) ? "ClassType" : "";
            ViewBag.Totalsalary = String.IsNullOrEmpty(Search_Data) ? "Total" : "";
            
            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }
            ViewBag.FilterValue = Search_Data;
            ViewBag.FilterValue = Search_Data;
            var PayRolls = from PayRoll in empEntities.PayRolls select PayRoll;

            if (Search_Data == "Empid")
            {
                var model = ce.queries.Where(query => query.custname == Search || Search == null).ToList().ToPagedList(Page_No ?? 1, 3);
                return View(model);
            }
            
            switch (Sorting_Order)
            {
                case "Empid":
                    PayRolls = PayRolls.OrderBy(PayRoll => PayRoll.Emp_Id);
                    break;
                case "ClassType":
                    PayRolls = PayRolls.OrderBy(PayRoll => PayRoll.ClassType);
                    break;
                case "Total":
                    PayRolls= PayRolls.OrderBy(PayRoll => PayRoll.Totalsalary);
                    break;
                default:
                    PayRolls = PayRolls.OrderBy(PayRoll => PayRoll.Emp_Id);
                    break;
            }

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            return View(PayRolls.ToPagedList(No_Of_Page, Size_Of_Page));
        }
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                var did = ce.customers.Where(x => x.customerId == id).FirstOrDefault();
                if (did != null)
                {
                    ce.Entry(did).State = EntityState.Deleted;
                    ce.SaveChanges();
                }
            }
            return RedirectToAction("Dashboard");
        }
        
    }
 }