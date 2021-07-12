using Facebook;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models.Data;
using WebApplication1.DAO;
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
            User user = db.Users.SingleOrDefault(u => u.userName == userName && u.password == password && u.roleID == 1);
            if (user != null)
            {
                Session["User"] = user;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
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
            user.image = "/images/user/customer/default-employee.jpg";
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
                user.image = "/images/user/customer/" + fileName;
                ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/images/user/customer"), fileName));
                (Session["User"] as User).image = fileName;
                db.SaveChanges();
                return RedirectToAction("customerDetail", "Customer", new { id = Int32.Parse(frm["userID"]) });
            }
            else
            {
                user.image = "/images/user/customer/default-employee.jpg";
                db.SaveChanges();
                return RedirectToAction("customerDetail", "Customer", new { id = Int32.Parse(frm["userID"]) });
            }
        }

        private Uri RediredtUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);

                uriBuilder.Query = null;

                uriBuilder.Fragment = null;

                uriBuilder.Path = Url.Action("FacebookCallback");

                return uriBuilder.Uri;
            }
        }

        [AllowAnonymous]
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "189880406480773",

                client_secret = "434c4c1006e1439306d57aff2340b2c7",

                redirect_uri = RediredtUri.AbsoluteUri,

                response_type = "code",

                scope = "email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new

            {

                client_id = "189880406480773",

                client_secret = "434c4c1006e1439306d57aff2340b2c7",

                redirect_uri = RediredtUri.AbsoluteUri,

                code = code
            });

            var accessToken = result.access_token;

            Session["AccessToken"] = accessToken;

            fb.AccessToken = accessToken;

            dynamic me = fb.Get("me?fields=link,first_name,currency,last_name,email,gender,locale,timezone,verified,picture,age_range,middle_name,birthday,hometown,location");

            string userName = me.email;
            var fbUser = new User();
            var userDao = new UserDao();

            if (userDao.isExisted(userName) == true)
            {
                fbUser = db.Users.FirstOrDefault(u => u.userName == userName);
                Session["User"] = fbUser;
            }
            else
            {
                fbUser.email = me.email;
                fbUser.userName = me.email;
                fbUser.fullName = me.first_name + " " + me.last_name;
                fbUser.image = me.picture.data.url;
                fbUser.roleID = 1;
                fbUser.gender = true;
                db.Users.Add(fbUser);
                db.SaveChanges();
                Session["User"] = fbUser;
            }

            FormsAuthentication.SetAuthCookie(userName, false);
            return RedirectToAction("Index", "Home");
        }

        
    }
}