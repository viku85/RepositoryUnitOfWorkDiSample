using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyProject.Web.Startup))]
namespace MyProject.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
