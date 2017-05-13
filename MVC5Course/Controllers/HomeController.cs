using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult PartialAbout()
        {
            ViewBag.Message = "Your application description page.";
 
            if (Request.IsAjaxRequest())
            {
                return PartialView("About");
            }
            else
            {
                return View("About");
            }
        }
        public ActionResult SomeAction()
        {
            return PartialView("SuccessRedirect", "/");
        }
        public ActionResult GetFile()
        {
            return File(Server.MapPath("~/Content/computexKeynote2017_A_250x250.jpg"), "image/jpeg", $"{DateTime.Now.ToString()}.jpg");
        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult UnKnown()
        {
            return View();
        }
    }
}