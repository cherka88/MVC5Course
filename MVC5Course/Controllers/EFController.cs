﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        FabricsEntities db = new FabricsEntities();
        // GET: EF
        public ActionResult Index()
        {
           
            var all = db.Product.AsQueryable();

            var data = all.Where(p => p.Active == true &&  p.Is刪除 != true && p.ProductName.Contains("Black")).Take(20);

            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product prod)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(prod);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var item = db.Product.Find(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(int id, Product prod)
        {
            if (ModelState.IsValid)
            {
                var item = db.Product.Find(id);
                item.ProductName = prod.ProductName;
                item.Price = prod.Price;
                item.Stock = prod.Stock;
                item.Active = prod.Active;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(prod);
        }

        public ActionResult Delete(int id)
        {
            var item = db.Product.Find(id);

            //foreach (var list in item.OrderLine.ToList())
            //{
            //    db.OrderLine.Remove(list);
            //}
            // 等於下面一行語法
            //db.OrderLine.RemoveRange(item.OrderLine);

            //db.Product.Remove(item);
            item.Is刪除 = true;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Details(int? id)
        {
            //var product = db.Product.Find(id);
            //if (product == null)
            //{
            //    return HttpNotFound();
            //}
            var product = db.Database.SqlQuery<Product>("select * from Product where ProductId = @p0", id).FirstOrDefault();
            return View(product);
        }
    }
}