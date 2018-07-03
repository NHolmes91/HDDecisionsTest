using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PreQualTool.Startup))]
namespace PreQualTool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
