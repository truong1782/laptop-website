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

        public ActionResult contactDetails(int id)
        {
            var contact = db.Contacts.Find(id);
            return View(contact);
        }

        public ActionResult deleteContact(int id)
        {
            var contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("listContact", "Contact");
        }

        public ActionResult feedback()
        {
            return RedirectToAction("listContact");
        }
    }
}