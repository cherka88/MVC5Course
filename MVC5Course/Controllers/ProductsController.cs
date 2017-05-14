using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;
using System.Data.Entity.Infrastructure;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {
        ProductRepository repo = RepositoryHelper.GetProductRepository();
        //private FabricsEntities db = new FabricsEntities();

        // GET: Products
        [OutputCache(Duration = 5,Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Index(bool active = true)
        {
            //var repo = new ProductRepository();
            //repo.UnitOfWork = GetUnitOfWork(); 兩行做法可以變成一行 line:16

            var queryable = repo.Get商品資料列表(active, showall: true);

            return View(queryable);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Get單筆資料ByID(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(DbUpdateException),View = "DbUpdateException_error")]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                repo.Add(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repo.Get單筆資料ByID(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection fc)
        {
            //[Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product
            var product = repo.Get單筆資料ByID(id);
            // TryUpdateModel(query, "query") //示範加上 prefix 的 TryUpdateModel 用法
            if (TryUpdateModel<Product>(product, new string[] { "ProductId", "ProductName", "Price", "Active", "Stock" }))
            {
                //db.Entry(product).State = EntityState.Modified;
                //db.SaveChanges();
                //repo.Update(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repo.Get單筆資料ByID(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Product product = db.Product.Find(id);
            //db.Product.Remove(product);
            //db.SaveChanges();
            Product product = repo.Get單筆資料ByID(id); //不是 nullable 不用 value
            repo.Delete(product);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
        public ActionResult ProductList(SearchRtn query)
        {
            if(query == null) //修正搜尋功能query null時異常
            {
                ViewBag.query = new SearchRtn()
                {
                    price = 1,
                    stock = 200
                };
            }
            GetPrductRtn(query);
            return View();
        }

        private void GetPrductRtn(SearchRtn query)
        {
            var data = repo.Get商品資料列表(true, true);
            if (!string.IsNullOrEmpty(query.q))
            {
                data = data.Where(p => p.ProductName.Contains(query.q));
            }
            ViewData.Model = data.Where(p => p.Stock > query.stock && p.Price > query.price)
                                 .Select(p => new ProductLite
                                 {
                                     ProductId = p.ProductId,
                                     ProductName = p.ProductName,
                                     Price = p.Price,
                                     Stock = p.Stock

                                 }).Take(20);
            ViewBag.query = query;
        }

        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductLite data)
        {
            if (ModelState.IsValid)
            {
                TempData["createinfo"] = data;
                return RedirectToAction("ProductList");
                
            }
            return View();
        }
        [HttpPost]
        public ActionResult ProductList(SearchRtn query, ProductBatchUpdate[] items)
        {

            if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    var prod = db.Product.Find(item.ProductId);
                    prod.Price = item.Price;
                    prod.Stock = item.Stock;
                }
                db.SaveChanges();
                
                return RedirectToAction("ProductList", query);
            }
            GetPrductRtn(query);
            return View("ProductList");
        }
    }
}
