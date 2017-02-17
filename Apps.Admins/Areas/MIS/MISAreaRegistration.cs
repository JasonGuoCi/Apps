using System.Web.Mvc;

namespace Apps.Admins.Areas.MIS
{
    public class MISAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MIS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MIS_default",
                "MIS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}