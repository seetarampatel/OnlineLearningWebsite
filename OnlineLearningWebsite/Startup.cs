using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineLearningWebsite.Startup))]
namespace OnlineLearningWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
