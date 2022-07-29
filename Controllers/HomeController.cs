using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using Web_P04_5.Models;
using WEB_P04_5.DAL;
using System.Collections.Generic;
using WEB_P04_5.Models;

namespace Web_P04_5.Controllers
{
    public class HomeController : Controller
    {
        private CustomerDAL CustomerContext = new CustomerDAL();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();

        }


        [HttpPost]
        public ActionResult StaffLogin(IFormCollection formData)
        {
            // Read inputs from textboxes
            string loginID = formData["txtStaffLoginID"].ToString();
            string password = formData["txtStaffPassword"].ToString();

            if (loginID == "sales1234" && password == "passSales")
            {
                // Store Login ID in session with the key “LoginID”
                HttpContext.Session.SetString("LoginID", loginID);
                // Store user role “Staff” as a string in session with the key “Role”
                HttpContext.Session.SetString("Role", "SalesPersonnel");

                DateTime loginDateTime = DateTime.Now;
                HttpContext.Session.SetString("LoginTime", loginDateTime.ToString("dd-MMMM-yy HH:mm:ss tt"));

                // Redirect user to the "StaffMain" view through an action
                return RedirectToAction("StaffMain");
            }

            else if (loginID == "Marketing" && password == "passMarketing")
            {
                // Store Login ID in session with the key “LoginID”
                HttpContext.Session.SetString("LoginID", loginID);
                // Store user role “Staff” as a string in session with the key “Role”
                HttpContext.Session.SetString("Role", "MarketingManager");

                DateTime loginDateTime = DateTime.Now;
                HttpContext.Session.SetString("LoginTime", loginDateTime.ToString("dd-MMMM-yy HH:mm:ss tt"));

                // Redirect user to the "StaffMain" view through an action
                return RedirectToAction("StaffMain");
            }

            else if (loginID == "ProductManager" && password == "passProduct")
            {
                // Store Login ID in session with the key “LoginID”
                HttpContext.Session.SetString("LoginID", loginID);
                // Store user role “Staff” as a string in session with the key “Role”
                HttpContext.Session.SetString("Role", "ProductManager");

                DateTime loginDateTime = DateTime.Now;
                HttpContext.Session.SetString("LoginTime", loginDateTime.ToString("dd-MMMM-yy HH:mm:ss tt"));

                // Redirect user to the "StaffMain" view through an action
                return RedirectToAction("StaffMain");
            }
            else
            {
                // Store an error message in TempData for display at the index view
                TempData["StaffMessage"] = "Invalid Login Credentials!";
                // Redirect user back to the index view through an action
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        
        public ActionResult CustomerLogin(IFormCollection formData)
        {
            // Read inputs from textboxes
            // Email address converted to lowercase

            string loginID = formData["txtMemberLoginID"].ToString();
            string password = formData["txtMemberPassword"].ToString();



            Customer customer = CustomerContext.GetCustomer(loginID, password);


            /*if (loginID == "customer1234" && password == "pass1234")*/
            if (customer != null)
            {
                // Store Login ID in session with the key “LoginID”
                HttpContext.Session.SetString("LoginID", loginID);
                // Store user role “Staff” as a string in session with the key “Role”
                HttpContext.Session.SetString("Role", "Customer");

                HttpContext.Session.SetString("FullName", customer.MName);

                HttpContext.Session.SetString("CustomerPassword", customer.MPassword);

                DateTime loginDateTime = DateTime.Now;
                HttpContext.Session.SetString("LoginTime", loginDateTime.ToString("dd-MMMM-yy HH:mm:ss tt"));

                // Redirect user to the "StaffMain" view through an action
                return RedirectToAction("StaffMain");
            }

            else
            {
                // Store an error message in TempData for display at the index view
                TempData["CustomerMessage"] = "Invalid Login Credentials!";
                // Redirect user back to the index view through an action
                return RedirectToAction("Index");
            }
        }
        public ActionResult StaffMain()
        {
            // Stop accessing the action if not logged in 
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") != null) &&
                (HttpContext.Session.GetString("Role") == "SalesPersonnel") ||
                (HttpContext.Session.GetString("Role") == "MarketingManager")||
                (HttpContext.Session.GetString("Role") == "Customer") ||
                (HttpContext.Session.GetString("Role") == "ProductManager"))
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult LogOut()
        {
            DateTime logoutTime = DateTime.Now;
            DateTime loginDateTime = Convert.ToDateTime(HttpContext.Session.GetString("LoginTime"));
            TimeSpan sessionTime = (logoutTime - loginDateTime);

            // Clear all key-values pairs stored in session state
            HttpContext.Session.Clear();

            TempData["SessionTime"] = "You have logged in for " + String.Format("{0} hours, {1} minutes, {2} seconds", sessionTime.Hours, sessionTime.Minutes, sessionTime.Seconds);

            // Call the Index action of Home controller
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
