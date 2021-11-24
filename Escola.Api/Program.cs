using Escola.Api;
using ServiceStack;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(builder => {
        builder.UseModularStartup<Startup>();
    });

builder.Build().Run();
