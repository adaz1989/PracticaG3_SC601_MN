using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(G3_Practice.Startup))]
namespace G3_Practice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
