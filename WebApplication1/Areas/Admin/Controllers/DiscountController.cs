﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
namespace WebApplication1.Areas.Admin.Controllers
{
    public class DiscountController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin/Discount
        public ActionResult listDiscount()
        {
            return View(db.Discounts.ToList());
        }

        [HttpGet]
        public ActionResult insertDiscount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult insertDiscount(Discount discount)
        {
            discount.discountCode = discount.discountCode.ToUpper();
            db.Discounts.Add(discount);
            db.SaveChanges();
            return RedirectToAction("listDiscount");
        }

        [HttpGet]
        public ActionResult editDiscount(int id)
        {
            return View(db.Discounts.Find(id));
        }

        [HttpPost]
        public ActionResult editDiscount(Discount discount)
        {
            discount.discountCode = discount.discountCode.ToUpper();
            db.Discounts.Add(discount);
            db.SaveChanges();
            return RedirectToAction("listDiscount");
        }

        public ActionResult deleteDiscount(int id)
        {
            Discount discount = db.Discounts.Find(id);
            db.Discounts.Remove(discount);
            db.SaveChanges();
            return RedirectToAction("listDiscount");
        }
    }
}