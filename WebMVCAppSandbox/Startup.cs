using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebMVCAppSandbox.Startup))]
namespace WebMVCAppSandbox
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
