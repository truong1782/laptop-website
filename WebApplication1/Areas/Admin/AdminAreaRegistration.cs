using System.Web.Mvc;

namespace WebApplication1.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "WebApplication1.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_login",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                new[] { "WebApplication1.Areas.Admin.Controllers" }
            );
        }
    }
}