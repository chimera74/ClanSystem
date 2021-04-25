using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClanSystem.Startup))]
namespace ClanSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
