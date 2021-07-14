using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
namespace WebApplication1.Controllers
{
    public class DiscountController : Controller
    {
        DBLAPTOPEntities db = new DBLAPTOPEntities();
        // GET: Discount
        public ActionResult discountPage()
        {
            return View(db.Discounts.ToList());
        }
    }
}