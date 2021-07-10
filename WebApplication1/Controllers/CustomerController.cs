using System;
using System.Collections.Generic;
using System.IO;
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


        public ActionResult customerPartial()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {
            var userName = frm["userName"];
            var password = frm["password"];
            User user=  db.Users.SingleOrDefault(u => u.userName == userName && u.password == password && u.roleID == 1);
            if (user != null)
            {
                Session["User"] = user;
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
            user.image = "default-employee.jpg";
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

        public ActionResult customerDetail(int id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        public ActionResult editInformation(FormCollection frm)
        {
            var customer = db.Users.Find(Int32.Parse(frm["userID"]));
            customer.fullName = frm["fullName"];
            customer.email = frm["email"];
            customer.phoneNumber = frm["phoneNumber"];
            customer.address = frm["address"];
            customer.gender = bool.Parse(frm["gender"]);
            customer.dateOfBirth = DateTime.Parse(frm["dateOfBirth"]);
            db.SaveChanges();
            Session["User"] = customer;
            return RedirectToAction("customerDetail", "Customer", new { id = customer.userID });
        }

        public ActionResult uploadImage(FormCollection frm, HttpPostedFileBase ImageUpload)
        {
            var user = db.Users.Find(Int32.Parse(frm["userID"]));
            if (ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                string extension = Path.GetExtension(ImageUpload.FileName);
                fileName += extension;
                user.image = fileName;
                ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/images/user/customer"), fileName));
                (Session["User"] as User).image = fileName;
                db.SaveChanges();
                return RedirectToAction("customerDetail", "Customer", new { id = Int32.Parse(frm["userID"]) });
            }
            else
            {
                user.image = "default-employee.jpg";
                db.SaveChanges();
                return RedirectToAction("customerDetail", "Customer", new { id = Int32.Parse(frm["userID"]) });
            }
        }



    }
}