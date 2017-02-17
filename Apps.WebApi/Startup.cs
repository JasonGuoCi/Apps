using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Apps.WebApi.Startup))]
namespace Apps.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
