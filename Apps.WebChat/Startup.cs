using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Apps.WebChat.Startup))]
namespace Apps.WebChat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
