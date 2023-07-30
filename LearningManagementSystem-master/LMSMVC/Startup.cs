using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LMSMVC.Startup))]
namespace LMSMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
