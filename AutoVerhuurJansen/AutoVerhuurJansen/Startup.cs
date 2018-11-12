using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutoVerhuurJansen.Startup))]
namespace AutoVerhuurJansen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
