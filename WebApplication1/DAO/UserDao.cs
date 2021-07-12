using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.Data;
namespace WebApplication1.DAO
{
    
    public class UserDao
    {
        DBContext db = new DBContext();

        public bool isExisted(string userName)
        {
            var user = db.Users.FirstOrDefault(u => u.userName == userName);
            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}