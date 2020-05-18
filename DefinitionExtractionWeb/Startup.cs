using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DefinitionExtractionWeb.Startup))]
namespace DefinitionExtractionWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
