using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Motormechs.Web.Startup))]
namespace Motormechs.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
