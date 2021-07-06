using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
namespace WebApplication1.Areas.Admin.Controllers
{
    public class ContactController : Controller
    {
        // GET: Admin/Contact
        DBContext db = new DBContext();
        public ActionResult listContact()
        {
            var listContact = db.Contacts.ToList();
            return View(listContact);
        }
    }
}