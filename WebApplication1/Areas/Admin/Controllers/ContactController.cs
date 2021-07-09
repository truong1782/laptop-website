using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
using WebApplication1.Services;
namespace WebApplication1.Areas.Admin.Controllers
{
    public class ContactController : Controller
    {
        // GET: Admin/Contact
        DBContext db = new DBContext();
        EmailService emailService = new EmailService();
        public ActionResult listContact()
        {
            var listContact = db.Contacts.ToList();
            return View(listContact);
        }

        public ActionResult contactDetails(int id)
        {
            var contact = db.Contacts.Find(id);
            contact.status = true;
            db.SaveChanges();
            return View(contact);
        }

        public ActionResult deleteContact(int id)
        {
            var contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("listContact", "Contact");
        }

        [HttpGet]
        public ActionResult feedback(int id)
        {
            var contact = db.Contacts.Find(id);
            return View(contact);
        }

        [HttpPost]
        public ActionResult feedback(FormCollection frm, int id)
        {
            string Address = frm["address"];
            string Title = frm["title"];
            string Message = frm["message"];
            emailService.sendEmail(Address, Title, Message);

            return RedirectToAction("listContact", "Contact");
        }
    }
}