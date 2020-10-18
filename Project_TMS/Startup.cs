using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project_TMS.Startup))]
namespace Project_TMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
