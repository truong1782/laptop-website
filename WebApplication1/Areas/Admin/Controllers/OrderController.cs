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
        DBLAPTOPEntities db = new DBLAPTOPEntities();
        // GET: Admin/Order
        public ActionResult Order()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(db.Orders.ToList());
        }

        public ActionResult deleteOrder(int? id)
        {
            if (Session["userID"] == null || id == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (String.IsNullOrEmpty(id.ToString()))
            {
                return RedirectToAction("Order");
            }
            else
            {
                deleteOrderDetail(id);
                Order ord = db.Orders.Find(id);
                db.Orders.Remove(ord);
                db.SaveChanges();
                return RedirectToAction("Order");
            }
        }

        public ActionResult orderDetail(int? id)
        {
            if (Session["userID"] == null || id == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (String.IsNullOrEmpty(id.ToString()))
            {
                return RedirectToAction("Order");
            }
            else
            {
                List<OrderDetail> listOrderDetail = (from o in db.OrderDetails
                                                     where o.orderID == id
                                                     select o).ToList();
                return View(listOrderDetail);
            }
        }

        public ActionResult changeOrderStatus(int? id)
        {
            if (Session["userID"] == null || id == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (String.IsNullOrEmpty(id.ToString()))
            {
                return RedirectToAction("Order");
            }
            else
            {
                Order ord = db.Orders.Find(id);
                ord.status = !ord.status;
                db.SaveChanges();
                return RedirectToAction("Order");
            }
        }

        private void deleteOrderDetail(int? id)
        {
            List<OrderDetail> ordDetail = db.OrderDetails.ToList();
            foreach (var item in ordDetail)
            {
                if (item.orderID == id)
                {
                    db.OrderDetails.Remove(item);
                }
            }
            db.SaveChanges();
        }
    }
}