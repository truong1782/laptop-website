using System;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        DBLAPTOPEntities db = new DBLAPTOPEntities();
        // GET: Admin/Product


        public ActionResult Index()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var productList = db.Products.ToList();
            return View(productList);
        }

        public ActionResult Create()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Category = db.Categories.ToList();
            ViewBag.Brand = db.Brands.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult createProduct(Product productInfo, HttpPostedFileBase ImageUpload)
        {
            productInfo.dateCreate = DateTime.Now;
            if (ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                string extension = Path.GetExtension(ImageUpload.FileName);
                fileName += extension;
                productInfo.image = "~/images/sanpham/" + fileName;
                ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/images/sanpham"), fileName));
                db.Products.Add(productInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                productInfo.image = "~/images/sanpham/none.png";
                db.Products.Add(productInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (Session["userID"] == null || id == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Product prod = db.Products.Find(id);
            ViewBag.Category = db.Categories.ToList();
            ViewBag.Brand = db.Brands.ToList();
            return View(prod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection frm)
        {
            var editedProd = db.Products.Find(Int32.Parse(frm["productID"]));
            editedProd.productName = frm["productName"];
            editedProd.categoryID = Int32.Parse(frm["categoryID"]);
            editedProd.brandID = Int32.Parse(frm["brandID"]);
            editedProd.productPrice = Int32.Parse(frm["productPrice"]);
            editedProd.amount = Int32.Parse(frm["amount"]);
            editedProd.productDetail = frm["productDetail"];
            editedProd.dateCreate = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int? id)
        {
            if (id == null || Session["userID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Product pro = db.Products.Find(id);
            db.Products.Remove(pro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Detail(int? id)
        {
            if (id == null || Session["userID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Product pro = db.Products.Find(id);
            return View(pro);
        }

        public ActionResult uploadImage(FormCollection frm, HttpPostedFileBase ImageUpload)
        {
            var prod = db.Products.Find(Int32.Parse(frm["productID"]));
            if (ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                string extension = Path.GetExtension(ImageUpload.FileName);
                fileName += extension;
                prod.image = "~/images/sanpham/" + fileName; ;
                ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/images/sanpham"), fileName));

                db.SaveChanges();

                return RedirectToAction("Edit", "Product", new { id = Int32.Parse(frm["productID"]) });
            }
            else
            {
                prod.image = "~/images/sanpham/none.png";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}