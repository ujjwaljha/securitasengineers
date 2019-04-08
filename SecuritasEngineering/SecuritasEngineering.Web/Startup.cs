using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SecuritasEngineering.Web.Startup))]
namespace SecuritasEngineering.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
