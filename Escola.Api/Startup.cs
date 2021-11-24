using ServiceStack;
using System.Configuration;

namespace Escola.Api;

public class Startup : ModularStartup
{
    public new void ConfigureServices(IServiceCollection services)
    {
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseServiceStack(new AppHost
        {
            AppSettings = new NetCoreAppSettings(Configuration)
        });
    }
}

