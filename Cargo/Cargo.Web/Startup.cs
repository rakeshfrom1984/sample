using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cargo.Web.Startup))]
namespace Cargo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
