using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
namespace WebApplication1.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/User
        public ActionResult Employee()
        {
            var allEmployee = db.Users.Where(u => u.Role.roleName == "Nhân viên").ToList();
            return View(allEmployee);
        }
    }
}