using Customer_Relationship_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using System.Web.Security;
using System.Net;
using System.Data.Entity;

namespace Customer_Relationship_Management.Controllers
{
    public class AdminController : Controller
    {
        CustomerEntities ce = new CustomerEntities();
        EmpEntities empEntities = new EmpEntities();
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
      
        public ActionResult Login(Signup signup)
        {
            EmpEntities db = new EmpEntities();

            var user = (from userlist in db.Admins where userlist.Username == signup.Username && userlist.Password == signup.Password 
                        select new
                        {   
                          userlist.Username
                        }).ToList();

            if (user.FirstOrDefault() != null)
            {
                Session["UserName"] = user.FirstOrDefault().Username;
                return Redirect("/Admin/Dashboard");
            }
            else
            {
                TempData["msg"] = "Invalid login credentials.";
            }
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dashboard(int i)
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
        public new ActionResult CustomerProfile()
        {
            return View();
        }
        [HttpPost]
        public new ActionResult CustomerProfile(customer cust)
        {
            var custid = cust.customerId;
            var custsid = (from x in ce.customers where custid == x.customerId select x).ToList();
            var notavl = (from x in ce.customers where custid != x.customerId select x).ToList();
            if (ModelState.IsValid)
            {
                if (custsid.Count > 0)
                {
                    ViewBag.allotduplicate = "* " + cust.customerId+ " already exits";
                    return View();
                }
                else if (notavl.Count > 0)
                {
                    ViewBag.allotduplicate = "* " + cust.customerId + " Not Avaliable!";
                    return View();
                }
                else { 
                ce.customers.Add(cust);
                ce.SaveChanges();
                if (cust.customerId > 0)
                {
                    TempData["Mess"] = "Inserted";
                }
                else
                {
                    TempData["msg"] = "Incorrect username/password";
                }
                ModelState.Clear();
                }
            }
            return View(cust);
        }


        public ActionResult Employeectrl()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Employeectrl(string Sorting_Order, string Search_Data,string Search ,int? Page_No, string Filter_Value)
        {
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.CustomerId = String.IsNullOrEmpty(Search_Data) ? "Empid" : "";
            ViewBag.SortingFirstname = String.IsNullOrEmpty(Search_Data) ? "FName" : "";
            ViewBag.SortingLastname = String.IsNullOrEmpty(Search_Data) ? "LName" : "";
            ViewBag.SortingEmail = String.IsNullOrEmpty(Search_Data) ? "DateofBirth" : "";
            ViewBag.SortingAddress = String.IsNullOrEmpty(Search_Data) ? "Email" : "";
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
                var model = empEntities.employees.Where(emp => emp.Firstname== Search || Search == null).ToList().ToPagedList(Page_No ?? 1, 3);
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
        public ActionResult CustomerCtrl(string Sorting_Order, string Search_Data, string Search,int? Page_No, string Filter_Value)
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

            var customers = from employee in ce.customers select employee;

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
        [HttpGet]
        public ActionResult Updateemp(int id)

        {
            var prod = empEntities.employees.Where(x => x.Employee_Id == id).FirstOrDefault();
            return View(prod);
        }
        [HttpPost]
        public ActionResult Updateemp(employee employee)
        {
            var input = empEntities.employees.Where(x => x.Employee_Id == employee.Employee_Id).FirstOrDefault();
            if (input != null)
            {
                input.Firstname = employee.Firstname;
                input.Lastname = employee.Lastname;
                input.MailId = employee.MailId;
                input.DateofBirth = employee.DateofBirth;
                input.Gender = employee.Gender;
                empEntities.SaveChanges();
            }

            return RedirectToAction("Index", "Customer");
        }
        public ActionResult Managerctrl()
        {
            return View();
        }

        public ActionResult Allotment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Allotment(AdminEmployee adminEmployee)
        {
            EmpEntities db = new EmpEntities();
            var empId = adminEmployee.EmployeeId;
            var employeeid = (from x in db.AdminEmployees where empId == x.EmployeeId select x).ToList();
            var notavl = (from x in db.AdminEmployees where empId != x.EmployeeId select x).ToList();
            if (ModelState.IsValid)
            {
                if (employeeid.Count > 0)
                {
                    ViewBag.allotduplicate = "* " + adminEmployee.EmployeeId + " already exits";
                    return View();
                }
                else if (notavl.Count > 0)
                {
                    ViewBag.allotduplicate = "* " + adminEmployee.EmployeeId + " Not Avaliable!";
                    return View();
                }
                else
                {
                    db.AdminEmployees.Add(adminEmployee);
                    db.SaveChanges();
                    if (adminEmployee.EmployeeId > 0)
                    {
                        TempData["Mess"] = "Added Successfully";
                        ModelState.Clear();
                    }
                    else
                    {
                        TempData["msg"] = "Incorrect Datatype";
                    }
                    ModelState.Clear();
                }
                return View(adminEmployee);
            }
            else
            {
                return View();
            }
        }

        public ActionResult CustomerAllotment()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerAllotment(AdminCustomer adminCustomer)
        {
            EmpEntities db = new EmpEntities();
            var custid = adminCustomer.CustomerId;
            var employeeid = (from x in db.AdminCustomers where custid== x.CustomerId select x).ToList();
            var notavl = (from x in ce.customers where custid != x.customerId select x).ToList();
            if (ModelState.IsValid)
            {
                if (employeeid.Count > 0)
                {
                    ViewBag.allotduplicate = "* " + adminCustomer.CustomerId + " already exits";
                    return View();
                }
                else if (notavl.Count > 0)
                {
                    ViewBag.allotduplicate = "* " + adminCustomer.CustomerId + " !Not Avaliable";
                    return View();
                }
                else
                {
                    db.AdminCustomers.Add(adminCustomer);
                    db.SaveChanges();
                    if (adminCustomer.CustomerId > 0)
                    {
                        TempData["Mess"] = "Added Successfully";
                        ModelState.Clear();
                    }
                    else
                    {
                        TempData["msg"] = "Incorrect Datatype";
                    }
                    ModelState.Clear();
                }
            }
            return View(adminCustomer);
        }

        public ActionResult empPayroll()
        {
            return View();
        }
       public ActionResult payroll()
        {
            return View();
        }
        [HttpPost]
        public ActionResult payroll(PayRoll payRoll)
        {
            EmpEntities empEntities = new EmpEntities();
            var empid = payRoll.Emp_Id;
            var employeeid = (from x in empEntities.PayRolls where empid == x.Emp_Id select x ).ToList();
            var employees = (from x in empEntities.PayRolls where empid != x.Emp_Id select x).ToList();
            if (ModelState.IsValid)
            {
                if (employeeid.Count > 0)
                {
                    ViewBag.allotduplicate = "* " + payRoll.Emp_Id+ " already exits";
                    return View();
                }
                else if(employees.Count>0)
                {
                    ViewBag.allotduplicate = "* " + payRoll.Emp_Id + " No Record Founded!";
                    return View();
                }
                else
                {
                        empEntities.PayRolls.Add(payRoll);
                        empEntities.SaveChanges();
                        return RedirectToAction("Dashboard");
                    
                }
            }
            return View(payRoll);

        }
        public ActionResult Update([Bind(Include = "customerId,customerName,phone,email,contactaddress,EmployeeId")] int? id)
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
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("Dashboard");

        }

        public ActionResult EmptoCust()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmptoCust(customer customers)
        {
            CustomerEntities cs = new CustomerEntities();
            var custid = customers.customerId;
            var customerid = (from x in cs.customers where custid == x.customerId select x).ToList();
            var notavl = (from x in ce.customers where custid != x.customerId select x).ToList();
            var input = cs.customers.Where(x => x.customerId == customers.customerId).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (customerid.Count > 0)
                {
                    ViewBag.allotduplicate = "* " + customers.customerId + " already exits";
                    return View();
                }
                else if (notavl.Count > 0)
                {
                    ViewBag.allotduplicate = "* " + customers.customerId + " No Record Founded!";
                    return View();
                }
                else if (input != null)
                {
                    input.EmployeeId = customers.EmployeeId;
                    cs.SaveChanges();
                }
            }
            else
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            return View(customers);
        }
        public ActionResult Delete(int? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            employee employee = empEntities.employees.Find(id);
            empEntities.employees.Remove(employee);
            empEntities.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        public ActionResult Deletecust(int? id)
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

        [HttpPost, ActionName("Deletecust")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmeds(int id)
        {
            customer customer = ce.customers.Find(id);
            ce.customers.Remove(customer);
            ce.SaveChanges();
            return RedirectToAction("Dashboard");
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
        public ActionResult DeletePay(int id)
        {
            if (id > 0)
            {
                var paydelete = empEntities.PayRolls.Where(x => x.PayrollId== id).FirstOrDefault();
                if (paydelete!= null)
                {
                    empEntities.Entry(paydelete).State = EntityState.Deleted;
                    empEntities.SaveChanges();
                }
            }
            return RedirectToAction("Dashboard");
        }
    }
}