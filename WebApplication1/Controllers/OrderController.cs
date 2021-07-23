using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
namespace WebApplication1.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        DBLAPTOPEntities db = new DBLAPTOPEntities();
        
        public ActionResult customerOrders(int? id)
        {
            var orders = db.Orders.Where(o => o.userID == id);
            return View(orders);
        }

        public ActionResult customerOrderDetail(int? id)
        {
            var orders = db.OrderDetails.Where(o => o.orderID == id);
            return View(orders);
        }
    }
}