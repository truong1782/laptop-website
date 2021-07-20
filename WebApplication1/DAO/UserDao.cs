using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.Data;
namespace WebApplication1.DAO
{
    
    public class UserDao
    {
        DBLAPTOPEntities db = new DBLAPTOPEntities();

        public bool isExisted(string idSocial)
        {
            var user = db.Users.FirstOrDefault(u => u.idSocial == idSocial);
            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}