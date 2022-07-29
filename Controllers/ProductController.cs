using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_P04_5.DAL;
using Web_P04_5.Models;

namespace Web_P04_5.Controllers
{
    public class ProductController : Controller
    {
        private ProductDAL productContext = new ProductDAL();

        // GET: ProductController
        public ActionResult Index()
        {
            // Stop accessing the action if not logged in
            // or account not in the "ProductManager" role
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "ProductManager"))
            {
                return RedirectToAction("Index", "Home");
            }
            List<Product> productList = productContext.GetAllProduct();
            return View(productList);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            // Stop accessing the action if not logged in
            // or account not in the "ProductManager" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "ProductManager"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        
        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                //Add staff record to database
                product.ProductId = productContext.Add(product);
                //Redirect user to Staff/Index view
                return RedirectToAction("Index");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(product);
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int? id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "ProductManager"))
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            { //Query string parameter not provided
              //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }

            Product product = productContext.GetDetails(id.Value);

            if (product == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }
            return View(product);
        }

        
        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            //Get branch list for drop-down list
            //in case of the need to return to Edit.cshtml view
            if (ModelState.IsValid)
            {
                //Update staff record to database
                productContext.Update(product);
                return RedirectToAction("Index");
            }
            else
            {
                //Input validation fails, return to the view
                //to display error message
                return View(product);
            }
        }

        
        // GET: ProductController/Delete/5
        public ActionResult Delete(int? id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "ProductManager"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }
            Product product = productContext.GetDetails(id.Value);
            if (product == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }
            return View(product);
        }
    
        // POST: StaffController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Product product)
        {
            // Delete the staff record from database
            productContext.Delete(product.ProductId);
            return RedirectToAction("Index");
        }
    }
}
