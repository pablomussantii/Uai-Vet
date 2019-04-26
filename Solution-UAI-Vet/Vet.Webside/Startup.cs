using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vet.Webside.Startup))]
namespace Vet.Webside
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
