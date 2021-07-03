using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
namespace WebApplication1.Controllers
{
    public class CustomerController : Controller
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
            User user=  db.Users.SingleOrDefault(u => u.userName == userName && u.password == password && u.roleID == 1);
            if (user != null)
            {
                Session["userID"] = user.userID;
                Session["fullName"] = user.fullName;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["userID"] = null;
            Session["fullName"] = null;
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(FormCollection collection)
        {
            User user = new User();
            user.fullName = collection["HoTen"];
            user.userName = collection["TaiKhoan"];
            user.password = collection["MatKhau"];
            user.email = collection["Email"];
            user.phoneNumber = collection["Sdt"];
            user.address = collection["DiaChi"];
            user.gender = bool.Parse(collection["GioiTinh"]);
            user.dateOfBirth = DateTime.Parse(collection["NgaySinh"]);
            user.roleID = 1;
            if (String.IsNullOrEmpty(user.fullName))
            {
                ViewData["Loi1"] = "Họ tên không được để trống";
            }
            else if (String.IsNullOrEmpty(user.userName))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(user.password))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(user.email))
            {
                ViewData["Loi5"] = "Phải nhập email";
            }
            else if (String.IsNullOrEmpty(user.phoneNumber))
            {
                ViewData["Loi6"] = "Phải nhập số điện thoại";
            }
            else if (String.IsNullOrEmpty(user.address))
            {
                ViewData["Loi7"] = "Địa chỉ không được để trống";
            }
            else
            {
                if (collection["XacNhanMatKhau"] == collection["MatKhau"])
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewData["Loi4"] = " Vui lòng xác nhận lại mật khẩu";
                }

                return this.SignUp();
            }
            return this.SignUp();
        }


    }
}