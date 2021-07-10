using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
namespace WebApplication1.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Order
        public ActionResult Order()
        {
            return View(db.Orders.ToList());
        }
    }
}