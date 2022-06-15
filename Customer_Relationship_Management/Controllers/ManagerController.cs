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
    public class ManagerController : Controller
    {
        EmpEntities empEntities = new EmpEntities();
        CustomerEntities ce = new CustomerEntities();
        // GET: Manager
        public ActionResult Dashboard()
        {
            return View();
        }
        public new ActionResult Profile()
        {
            return View();
        }
        [HttpPost]
        public new ActionResult Profile(employee empl)
        {
            if (ModelState.IsValid)
            {
                empEntities.employees.Add(empl);
                empEntities.SaveChanges();
                if (empl.Employee_Id > 0)
                {
                    TempData["Mess"] = "Inserted";
                }
                else
                {
                    TempData["msg"] = "Incorrect username/password";
                }
                ModelState.Clear();
            }
            return View(empl);
        }



        public ActionResult Employeectrl()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Employeectrl(string Sorting_Order, string Search_Data, string Search, int? Page_No, string Filter_Value)
        {
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.CustomerId = String.IsNullOrEmpty(Search_Data) ? "Empid" : "";
            ViewBag.SortingName = String.IsNullOrEmpty(Search_Data) ? "FName" : "";
            ViewBag.SortingEmpid = String.IsNullOrEmpty(Search_Data) ? "LName" : "";
            ViewBag.SortingEmail = String.IsNullOrEmpty(Search_Data) ? "DateofBirth" : "";
            ViewBag.SortingAddress = String.IsNullOrEmpty(Search_Data) ? "Mail" : "";
            ViewBag.SortingPhone = String.IsNullOrEmpty(Search_Data) ? "Gender" : "";

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

            var employees = from emp in empEntities.employees select emp;
            if (Search_Data == "Name")
            {
                var model = empEntities.employees.Where(emp => emp.Firstname == Search || Search == null).ToList().ToPagedList(Page_No ?? 1, 3);
                return View(model);
            }
            else if (Search_Data == "Empid")
            {
                var model = empEntities.employees.Where(emp => emp.Employee_Id.ToString() == Search || Search == null).ToList().ToPagedList(Page_No ?? 1, 3);
                return View(model);
            }
            switch (Sorting_Order)
            {
                case "Empid":
                    employees = employees.OrderBy(emp => emp.Employee_Id);
                    break;
                case "FName":
                    employees = employees.OrderBy(emp => emp.Firstname);
                    break;
                case "LName":
                    employees = employees.OrderBy(emp => emp.Lastname);
                    break;
                case "DateofBirth":
                    employees = employees.OrderBy(emp => emp.DateofBirth);
                    break;
                case "Email":
                    employees = employees.OrderBy(emp => emp.MailId);
                    break;
                case "Gender":
                    employees = employees.OrderBy(emp => emp.Gender);
                    break;
                default:
                    employees = employees.OrderBy(emp => emp.Employee_Id);
                    break;
            }

            int Size_Of_Page = 4;
            int No_Of_Page = (Page_No ?? 1);
            return View(employees.ToPagedList(No_Of_Page, Size_Of_Page));
        }
        public ActionResult CustomerCtrl()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CustomerCtrl(string Sorting_Order, string Search_Data, string Search, int? Page_No, string Filter_Value)
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

            var customers = from cust in ce.customers select cust;

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

            int Size_Of_Page = 4;
            int No_Of_Page = (Page_No ?? 1);
            return View(customers.ToPagedList(No_Of_Page, Size_Of_Page));
        }
        public ActionResult Update(int? employeeid)

        {
            if (employeeid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = empEntities.employees.Find(employeeid);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        [HttpPost]
        public ActionResult Update(employee employee)
        {
            if (ModelState.IsValid)
            {
                empEntities.Entry(employee).State = EntityState.Modified;
                empEntities.SaveChanges();
                return RedirectToAction("Profile");
            }
            return View(employee);
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
            ViewBag.SortingclassType = String.IsNullOrEmpty(Search_Data) ? "ClassType" : "";
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
                    PayRolls = PayRolls.OrderBy(PayRoll => PayRoll.Totalsalary);
                    break;
                default:
                    PayRolls = PayRolls.OrderBy(PayRoll => PayRoll.Emp_Id);
                    break;
            }

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            return View(PayRolls.ToPagedList(No_Of_Page, Size_Of_Page));
        }
    }
}