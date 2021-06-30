using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        // GET: Login
        DBContext db = new DBContext();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {
            var userName = frm["userName"];
            var password = frm["password"];
            User user=  db.Users.SingleOrDefault(u => u.userName == userName && u.password == password);
            if (user != null)
            {
                Session["userID"] = user.userID;
                Session["fullName"] = user.fullName;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User user)
        {
            return View();
        }
    }
}