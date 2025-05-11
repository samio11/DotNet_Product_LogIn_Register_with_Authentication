using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using User_Management_with_login_Register.Context;

namespace User_Management_with_login_Register.Controllers
{
    public class ProductController : Controller
    {
        public TechBroEntities _dbContext;

        public ProductController()
        {
            _dbContext = new TechBroEntities();
        }

        // GET: Product
        public ActionResult Index()
        {
            var products = _dbContext.Product_info.ToList();
            return View(products);
        }

        [HttpGet]
        public ActionResult AddProduct(Product_info p)
        {
            if (p.Id > 0)
                return View(p);
            else
            {
                ModelState.Clear();
                ViewBag.NoData = 0;
                return View();
            }
        }

        [HttpPost]
        public ActionResult CreateProduct(Product_info product)
        {
            if (ModelState.IsValid)
            {
                if (product.Id <= 0)
                {
                    _dbContext.Product_info.Add(product);
                    _dbContext.SaveChanges();
                    TempData["MsgAdd"] = "Product added successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    _dbContext.Entry(product).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    TempData["MsgEdit"] = "Product updated successfully";
                    return RedirectToAction("Index");
                }
            }

            return View("AddProduct", product);
        }

        public ActionResult Delete(int id)
        {
            var found_product = _dbContext.Product_info.Find(id);
            if (found_product != null)
            {
                _dbContext.Product_info.Remove(found_product);
                _dbContext.SaveChanges();
                TempData["MsgRem"] = "Product deleted successfully";
            }

            return RedirectToAction("Index");
        }
    }
}
