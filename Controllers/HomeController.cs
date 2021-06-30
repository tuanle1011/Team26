using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComTam_Store.Models;

namespace ComTam_Store.Controllers
{
    public class HomeController : Controller
    {
        CT25Team26Entities db = new CT25Team26Entities();
        // GET: Home
    
        public ActionResult Index(string searchBy, string search)
        {
         
            return View(db.Products.ToList());
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User user)
        {   
            if ( db.Users.Any( x => x.TenDangNhap == user.TenDangNhap) && db.Users.Any( x => x.Email == user.Email))
            {
                ViewBag.Notification = "Tên đăng nhập hoặc email này đã có người sử dụng";
                return View();
            }
            else
            {
                db.Users.Add(user);
                db.SaveChanges();
                Session["UserIdSS"] = user.UserId.ToString();
                Session["TenUserSS"] = user.TenUser.ToString();
                Session["EmailSS"] = user.Email.ToString();
                Session["DiaChiSS"] = user.DiaChi.ToString();
                Session["SDTSS"] = user.SDT.ToString();
                return RedirectToAction("Index", "Home");

            }  
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Info ()
        {
            return RedirectToAction("Details","Users");
        }    
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {

            var checkLogin = db.Users.Where(x => x.TenDangNhap.Equals(user.TenDangNhap) && x.MatKhau.Equals(user.MatKhau)).FirstOrDefault();
            if (checkLogin == null)
            {
                ViewBag.Notification = "Sai tên đăng nhập hoặc mật khẩu";
            }
            else
            {
                if (checkLogin.RoleId.ToString() == "1")
                {
                    Session["UserIdSS"] = checkLogin.UserId.ToString();
                    Session["TenDangNhapSS"] = checkLogin.TenDangNhap.ToString();
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    Session["UserIdSS"] = checkLogin.UserId.ToString();
                    Session["TenDangNhapSS"] = checkLogin.TenDangNhap.ToString();
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        public ActionResult Search(string keyword)
        {
            var model = db.Products.ToList();
            model = model.Where(x => x.SPName.ToLower().Contains(keyword)).ToList();
            ViewBag.Keyword = keyword;
            return View("Index", model);
        }
    }
}