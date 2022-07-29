using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WEB_P04_5.Controllers
{
    public class ResponseController : Controller
    {
        //private ResponseDAL ResponseContext = new ResponseDAL();
        // GET: ResponseController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ResponseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ResponseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResponseController/Create
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

        // GET: ResponseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ResponseController/Edit/5
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

        // GET: ResponseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ResponseController/Delete/5
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
