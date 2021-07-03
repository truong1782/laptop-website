using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
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

        
        public ActionResult insertEmployee()
        {
            ViewBag.Roles = db.Roles.Where(e => e.roleID != 1 && e.roleID != 2).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult insertEmployee(User emp, HttpPostedFileBase ImageUpload)
        {
            if (ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                string extension = Path.GetExtension(ImageUpload.FileName);
                fileName += extension;
                emp.image = fileName;
                ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/images/user/employee"), fileName));
                db.Users.Add(emp);
                db.SaveChanges();
                return RedirectToAction("Employee");
            }
            else
            {
                emp.image = "default-employee.jpg";
                db.Users.Add(emp);
                db.SaveChanges();
                return RedirectToAction("Employee");
            }
        }

        [HttpGet]
        public ActionResult editEmployee(int id)
        {
            ViewBag.Roles = db.Roles.Where(e => e.roleID != 1 && e.roleID != 2).ToList();
            return View(db.Users.Find(id));
        }

        [HttpPost]
        public ActionResult editEmployee(FormCollection frm)
        {
            var emp = db.Users.Find(Int32.Parse(frm["userID"]));
            emp.roleID = Int32.Parse(frm["roleID"]);
            emp.fullName = frm["fullName"];
            emp.userName = frm["userName"];
            emp.password = frm["password"];
            emp.email = frm["email"];
            emp.phoneNumber = frm["phoneNumber"];
            emp.address = frm["address"];
            emp.gender = bool.Parse(frm["gender"]);
            db.SaveChanges();
            return RedirectToAction("Employee");
        }

        public ActionResult deleteEmployee(int id)
        {
            var selectedEmp = db.Users.Find(id);
            db.Users.Remove(selectedEmp);
            db.SaveChanges();
            return RedirectToAction("Employee", "Employee");
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
                ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/images/user/employee"), fileName));
                db.SaveChanges();
                return RedirectToAction("editEmployee", "Employee", new { id = Int32.Parse(frm["userID"]) });
            }
            else
            {
                user.image = "default-employee.jpg";
                db.SaveChanges();
                return RedirectToAction("editEmployee");
            }
        }
    }
}