using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;

namespace WebApplication1.Controllers
{
    public class ContactController : Controller
    {
        DBLAPTOPEntities db = new DBLAPTOPEntities();
        // GET: Contact
        [HttpPost]
        public ActionResult ContactPage(FormCollection contactForm)
        {

            Contact contact = new Contact();
            var title = contactForm["title"];
            var fullName = contactForm["fullName"];
            var email = contactForm["email"];
            var phone = contactForm["phonenumber"];
            var detail = contactForm["noidung"];
            contact.title = title;
            contact.fullName = fullName;
            contact.email = email;
            contact.detail = detail;
            contact.phone = phone;
            contact.dateCreate = DateTime.Now;
            contact.status = false;
            db.Contacts.Add(contact);
            db.SaveChanges();
            ViewBag.Notice = "<div class='alert alert-success text-center text-dark' role='alert'>Gửi liên hệ thành công</div>";
            return View();
        }

        public ActionResult ContactPage()
        {
            return View();
        }
    }
}