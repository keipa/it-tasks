using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cloudweb.Startup))]
namespace cloudweb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);




        }
    }
}
