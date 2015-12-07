using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(nodiceweb.Startup))]
namespace nodiceweb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
