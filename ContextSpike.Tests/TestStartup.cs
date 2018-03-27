using Microsoft.Extensions.DependencyInjection;

namespace ContextSpike.Tests
{
    public class TestStartup : Startup
    {
        protected override void ConfigureEf(IServiceCollection serviceCollection)
        {
            // DbContext already configured in TestServerFactory.
            // TODO: Prob a better way with autofac to only register DbContext in Startup if it doesn't already exist.
        }
    }
}