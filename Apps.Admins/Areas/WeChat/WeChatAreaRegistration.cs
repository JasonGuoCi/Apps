using System.Web.Mvc;

namespace Apps.Admins.Areas.WeChat
{
    public class WeChatAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "WeChat";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              "WeChatGlobalization", // 路由名称
              "{lang}/WeChat/{controller}/{action}/{id}", // 带有参数的 URL
                                                          //"WeChat/{controller}/{action}/{id}", // 带有参数的 URL
              new { lang = "zh", controller = "Home", action = "Index", id = UrlParameter.Optional }, // 参数默认值
              new { lang = "^[a-zA-Z]{2}(-[a-zA-Z]{2})?$" }    //参数约束
          );

            context.MapRoute(
                "WeChat_default",
                "WeChat/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}