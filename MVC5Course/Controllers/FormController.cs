using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class FormController : Controller
    {
        private FabricsEntities db = new FabricsEntities();
        // GET: Form
        public ActionResult Index()
        {
            var all = db.Product.AsQueryable();

            var data = all.Where(p => p.Active == true &&  p.Is刪除 != true).Take(20);

            return View(data);
        }
        public ActionResult Edit(int id)
        {
            return View(db.Product.Find(id));
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            var product = db.Product.Find(id);
            if(TryUpdateModel(product,includeProperties: new string[] { "ProductName" }))
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}