using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SiteProduct.Startup))]
namespace SiteProduct
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
