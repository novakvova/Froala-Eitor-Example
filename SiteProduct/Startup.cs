using Hangfire;
using Microsoft.Owin;
using Owin;
using SiteProduct.Controllers;

[assembly: OwinStartupAttribute(typeof(SiteProduct.Startup))]
namespace SiteProduct
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("DefaultConnection");
            app.UseHangfireDashboard("/myJobDashboard", new DashboardOptions()
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });

            RecurringJob.AddOrUpdate(
                () => ProductController.ClearImages(), Cron.Minutely()
            );

            app.UseHangfireServer();

        }
    }
}
