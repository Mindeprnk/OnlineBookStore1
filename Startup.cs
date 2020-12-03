using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineBookStore1.Startup))]
namespace OnlineBookStore1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
