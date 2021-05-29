using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(THE_APP.Startup))]
namespace THE_APP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
