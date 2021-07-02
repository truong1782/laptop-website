using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
namespace WebApplication1.Areas.Admin.Controllers
{
    public class EmployeeController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/User
        public ActionResult Employee()
        {
            var allEmployee = db.Users.Where(u => u.roleID != 1 && u.roleID != 2).ToList();
            return View(allEmployee);
        }

        [HttpGet]
        public ActionResult insertEmployee()
        {
            ViewBag.Roles = db.Roles.Where(e => e.roleID != 1 && e.roleID != 2).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult insertEmployee(User emp)
        {
            db.Users.Add(emp);
            db.SaveChanges();
            return RedirectToAction("Employee");
        }
    }
}