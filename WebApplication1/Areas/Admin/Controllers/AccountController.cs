using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {
            var username = frm["userName"];
            var password = frm["password"];
            User user = db.Users.SingleOrDefault(u => u.userName == username &&
                                                 u.password == password &&
                                                 u.Role.roleName != "Khách hàng");
            if (user != null)
            {
                Session["userID"] = user.userID;
                Session["fullname"] = user.fullName;
                Session["image"] = user.image;
                return RedirectToAction("Index", "Product");
            }
            ViewBag.Notice = "Sai tài khoản hoặc mật khẩu";
            return View();
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }
    }
}