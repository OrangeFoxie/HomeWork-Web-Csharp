using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcBookStore.Startup))]
namespace MvcBookStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
