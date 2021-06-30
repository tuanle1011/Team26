using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ComTam_Store.Models;

namespace ComTam_Store.Controllers
{
    public class ChitietHDsController : Controller
    {
        private CT25Team26Entities db = new CT25Team26Entities();

        // GET: ChitietHDs
        public async Task<ActionResult> Index()
        {
            var chitietHDs = db.ChitietHDs.Include(c => c.HoaDon).Include(c => c.Product);
            return View(await chitietHDs.ToListAsync());
        }

        // GET: ChitietHDs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChitietHD chitietHD = await db.ChitietHDs.FindAsync(id);
            if (chitietHD == null)
            {
                return HttpNotFound();
            }
            return View(chitietHD);
        }

        // GET: ChitietHDs/Create
        public ActionResult Create()
        {
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "TenKh");
            ViewBag.MaSP = new SelectList(db.Products, "IdSP", "SPName");
            return View();
        }

        // POST: ChitietHDs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaSP,MaHD,SoLuong,Gia")] ChitietHD chitietHD)
        {
            if (ModelState.IsValid)
            {
                db.ChitietHDs.Add(chitietHD);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "TenKh", chitietHD.MaHD);
            ViewBag.MaSP = new SelectList(db.Products, "IdSP", "SPName", chitietHD.MaSP);
            return View(chitietHD);
        }

        // GET: ChitietHDs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChitietHD chitietHD = await db.ChitietHDs.FindAsync(id);
            if (chitietHD == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "TenKh", chitietHD.MaHD);
            ViewBag.MaSP = new SelectList(db.Products, "IdSP", "SPName", chitietHD.MaSP);
            return View(chitietHD);
        }

        // POST: ChitietHDs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaSP,MaHD,SoLuong,Gia")] ChitietHD chitietHD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chitietHD).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "TenKh", chitietHD.MaHD);
            ViewBag.MaSP = new SelectList(db.Products, "IdSP", "SPName", chitietHD.MaSP);
            return View(chitietHD);
        }

        // GET: ChitietHDs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChitietHD chitietHD = await db.ChitietHDs.FindAsync(id);
            if (chitietHD == null)
            {
                return HttpNotFound();
            }
            return View(chitietHD);
        }

        // POST: ChitietHDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ChitietHD chitietHD = await db.ChitietHDs.FindAsync(id);
            db.ChitietHDs.Remove(chitietHD);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
