﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [SharedViewBag]
        public ActionResult About()
        {

            throw new ArgumentException("ERROR");
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
        public ActionResult GetJson()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return Json(db.Product.Take(5),JsonRequestBehavior.AllowGet);
        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult UnKnown()
        {
            return View();
        }
        public ActionResult VT()
        {
            return View();
        }

        public ActionResult RazorTest()
        {
            int[] data = new int[] { 1, 2, 3, 4, 5 };
            return PartialView(data);
        }
    }
}