using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Apps.Admins.Startup))]
namespace Apps.Admins
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
