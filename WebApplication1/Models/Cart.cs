using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.Data;
using WebApplication1.Controllers;

namespace WebApplication1.Models
{
    public class Cart
    {
        DBContext db = new DBContext();
        public int iProductID { set; get; }
        public string sProductName { set; get; }
        public string sImage { set; get; }
        public int dAmountMoney { set; get; }
        public int iSoluong { set; get; }
        //public int reducedMoney { set; get; }
        public int dThanhTien
        {
            get { return (iSoluong*dAmountMoney); }
        }
        public Cart(int productID)
        {
            iProductID = productID;
            Product product = db.Products.Single(n => n.productID == iProductID);
            sProductName = product.productName;
            sImage = product.image;
            //reducedMoney = 0;
            dAmountMoney = int.Parse(product.productPrice.ToString());
            iSoluong = 1;
        }
    }
}