using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Data;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        DBContext db = new DBContext();

        //Xay dung trang Gio hang
        public ActionResult Cart()
        {
            List<Cart> listCart = getCart();
            if (listCart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.TotalQuantity = TotalQuantity();
            ViewBag.FinalMoney = FinalMoney();
            return View(listCart);
        }

        public List<Cart> getCart()
        {
            List<Cart> listCart = Session["Giohang"] as List<Cart>;
            if (listCart == null)
            {
                //Neu gio hang chua ton tai thi khoi tao listGiohang
                listCart = new List<Cart>();
                Session["Giohang"] = listCart;
            }
            return listCart;
        }

        public ActionResult addCart(int iProductID, string strURL)
        {
            List<Cart> listProduct = getCart();
            //Kiem tra laptop này tồn tại trong Session["Giohang"] chưa?
            Cart product = listProduct.Find(n => n.iProductID == iProductID);
            if (product == null)
            {
                product = new Cart(iProductID);
                listProduct.Add(product);
                return Redirect(strURL);
            }
            else
            {
                product.iSoluong++;
                return Redirect(strURL);
            }
        }


        //Tong so luong
        private int TotalQuantity()
        {
            int iTotalQuantity = 0;
            List<Cart> listCart = Session["GioHang"] as List<Cart>;
            if (listCart != null)
            {
                iTotalQuantity = listCart.Sum(n => n.iSoluong);
            }
            return iTotalQuantity;
        }

        //Tinh tong tien
        private int FinalMoney()
        {
            int iFinalMoney = 0;
            List<Cart> listCart = Session["GioHang"] as List<Cart>;
            if (listCart != null)
            {
                iFinalMoney = listCart.Sum(n => n.dThanhTien);
            }
            return iFinalMoney;
        }
        //Tao Partial view de hien thi thong tin gio hang
        public ActionResult CartPartial()
        {
            ViewBag.TotalQuantity = TotalQuantity();
            ViewBag.FinalMoney = FinalMoney();
            return PartialView();
        }
        //Cap nhat Giỏ hàng
        public ActionResult UpdateCart(FormCollection f)
        {

            //Lay gio hang tu Session
            List<Cart> listCart = getCart();
            string[] quantity = f.GetValues("txtSoluong");
            //Kiem tra sach da co trong Session["Giohang"]
            for (int i = 0; i < listCart.Count(); i++)
            {
                listCart[i].iSoluong = int.Parse(quantity[i]);
            }

            //Neu ton tai thi cho sua Soluong
            return RedirectToAction("Cart");
        }
        public ActionResult DeleteCart(int iProductID)
        {
            //Lay gio hang tu Session
            List<Cart> listCart = getCart();
            //Kiem tra sach da co trong Session["Giohang"]
            Cart product = listCart.SingleOrDefault(n => n.iProductID == iProductID);
            //Neu ton tai thi cho sua Soluong
            if (product != null)
            {
                listCart.RemoveAll(n => n.iProductID == iProductID);
                return RedirectToAction("Cart");

            }
            if (listCart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Cart");
        }
        //Xoa tat ca thong tin trong Gio hang
        public ActionResult DeleteAllCart()
        {
            //Lay gio hang tu Session
            List<Cart> listCart = getCart();
            listCart.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Customer");
            }
            else if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<Cart> listCart = getCart();
            ViewBag.TotalQuantity = TotalQuantity();
            ViewBag.FinalMoney = FinalMoney();
            return View(listCart);
        }

        [HttpPost]
        public ActionResult Checkout(int userid)
        {
            Order order = new Order();
            order.userID = userid;
            order.totalMoney = FinalMoney();
            order.dateCreate = DateTime.Now;
            order.dateArrive = DateTime.Now.AddDays(10);
            order.status = false;
            db.Orders.Add(order);
            db.SaveChanges();
            List<Cart> listCart = getCart();
            foreach (var item in listCart)
            {
                OrderDetail ordDetail = new OrderDetail();
                ordDetail.orderID = order.orderID;
                ordDetail.productID = item.iProductID;
                ordDetail.quantity = item.iSoluong;
                ordDetail.amountMoney = item.dThanhTien;
                db.OrderDetails.Add(ordDetail);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("Finish", "Cart");
        }

        public ActionResult Finish()
        {
            User user = Session["User"] as User;
            return View(db.Users.Find(user.userID));
        }

    }
}