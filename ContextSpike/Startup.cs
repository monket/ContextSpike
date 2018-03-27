using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ContextSpike
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            ConfigureEf(serviceCollection);
            serviceCollection.AddMvc();
        }

        protected virtual void ConfigureEf(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<CharacterContext>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}