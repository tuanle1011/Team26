using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComTam_Store.Models;
using System.IO;
using System.Data;
using System.Data.Entity;

namespace ComTam_Store.Controllers
{
    public class ProductController : Controller
    {
        CT25Team26Entities db = new CT25Team26Entities();
        // GET: Product
        public ActionResult Index()
        { 
            return View(db.Products.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View(db.Products.Where( x => x.IdSP == id).FirstOrDefault());
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            Product pro = new Product();
            ViewBag.IdDM = new SelectList(db.DanhMucSPs, "IdDM", "TenDM");
            return View(pro);
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product pro)
        {
            try
            {
                // TODO: Add insert logic here
                if (pro.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(pro.ImageUpload.FileName);
                    string extension = Path.GetExtension(pro.ImageUpload.FileName);
                    fileName += extension;
                    pro.ImageLink = "~/Content/ImageProducts/" + fileName;
                    pro.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/ImageProducts/"), fileName));
                }
                db.Products.Add(pro);
                db.SaveChanges();
                return RedirectToAction("Index","Product");
            }
            catch
            {
                ViewBag.IdDM = new SelectList(db.DanhMucSPs, "IdDM", "TenDM", pro.IdDM);
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View(db.Products.Where(x => x.IdSP == id).FirstOrDefault());
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product pro)
        {
            try
            {
                // TODO: Add update logic here
                if (pro.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(pro.ImageUpload.FileName);
                    string extension = Path.GetExtension(pro.ImageUpload.FileName);
                    fileName += extension;
                    pro.ImageLink = "~/Content/ImageProducts/" + fileName;
                    pro.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/ImageProducts/"), fileName));
                }
                db.Entry(pro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Product");
            }
            catch
            {
                ViewBag.IdDM = new SelectList(db.DanhMucSPs, "IdDM", "TenDM", pro.IdDM);
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Product pro)
        {
            try
            {
                // TODO: Add delete logic here
                pro = db.Products.Where(x => x.IdSP == id).FirstOrDefault();
                db.Products.Remove(pro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
