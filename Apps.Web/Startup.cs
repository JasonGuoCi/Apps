using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Apps.Web.Startup))]
namespace Apps.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
