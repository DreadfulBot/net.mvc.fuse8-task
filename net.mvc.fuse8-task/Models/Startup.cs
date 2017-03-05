using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(net.mvc.fuse8_task.Startup))]
namespace net.mvc.fuse8_task
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
