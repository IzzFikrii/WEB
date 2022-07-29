using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEB_P04_5.DAL;
using WEB_P04_5.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace WEB_P04_5.Controllers
{
    public class FeedbackController : Controller
    {
        private FeedbackDAL FeedbackContext = new FeedbackDAL();

        // GET: FeedbackController
        public ActionResult Index()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
                (HttpContext.Session.GetString("Role") != "Customer"))
            {
                return RedirectToAction("Index", "Home");
            }
            List<Feedback> FeedbackList = FeedbackContext.GetAllFeedback();
            return View(FeedbackList);
        }

        // GET: FeedbackController/Details/5
        public ActionResult Details(int id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Customer"))
            {
                return RedirectToAction("Index", "Home");
            }

            Feedback feedback = FeedbackContext.GetDetails(id);
            FeedbackViewModel feedbackVM = MapToFeedbackVM(feedback);
            return View(feedbackVM);  
        }
        public FeedbackViewModel MapToFeedbackVM(Feedback feedback)
        {



            FeedbackViewModel feedbackVM = new FeedbackViewModel
            {

                FeedbackID = feedback.FeedbackID,
                MemberID = feedback.MemberID,
                DateTimePosted = feedback.DateTimePosted,
                Title = feedback.Title,
                Text = feedback.Text,
                ImageFileName = feedback.ImageFileName + ".jpg"
            };

            return feedbackVM;
        }
    

        // GET: FeedbackController/Create
        public ActionResult Create()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Customer"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: FeedbackController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Feedback feedback)
        {

            if (ModelState.IsValid)
            {
                //Add staff record to database
                feedback.FeedbackID = FeedbackContext.Add(feedback);
                //Redirect user to Staff/Index view
                return RedirectToAction("Index");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(feedback);
            }
        }

        // GET: FeedbackController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FeedbackController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: FeedbackController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FeedbackController/Delete/5
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
