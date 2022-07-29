using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_P04_5.DAL;
using WEB_P04_5.Models;

namespace WEB_P04_5.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerDAL CustomerContext = new CustomerDAL();

        // GET: CustomerController


        public ActionResult Index()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Customer"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        
        public ActionResult EditInfo(string id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Customer"))
            {
                
            }
            if (id == null)
            { //Query string parameter not provided
              //Return to listing page, not allowed to edit
                
            }
           
            Customer customer = CustomerContext.GetDetails(id);
            if (customer == null)
            {
                //Return to listing page, not allowed to edit
                
            }
            return View(customer);
        }
        

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        
         public ActionResult EditInfo(Customer customer)
         {
            //Get branch list for drop-down list
            //in case of the need to return to  view

             if (ModelState.IsValid)
             {
                //Update staff record to database
                CustomerContext.Update(customer, HttpContext);
                return RedirectToAction("EditInfo");
             }
             else
             {
                 //Input validation fails, return to the view
                 //to display error message
                 return View(customer);
             }
         }
         
        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
