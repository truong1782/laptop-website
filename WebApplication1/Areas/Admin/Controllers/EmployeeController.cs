using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Admin.Models;
namespace WebApplication1.Areas.Admin.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Admin/Employee
        DBContext db = new DBContext();
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
    }
}