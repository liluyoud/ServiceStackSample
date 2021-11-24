using Escola.Core.Services;
using Funq;
using ServiceStack;
using ServiceStack.Admin;
using ServiceStack.Data;
using ServiceStack.Validation;

namespace Escola.Api;

public class AppHost : AppHostBase
{
    public AppHost() : base("Escola.Api", typeof(EscolaServices).Assembly) { }

    public override void Configure(Container container)
    {
        SetConfig(new HostConfig
        {
            AddRedirectParamsToQueryString = true,
            DebugMode = AppSettings.Get(nameof(HostConfig.DebugMode), HostingEnvironment.IsDevelopment()),
        });

        Plugins.Add(new ValidationFeature());

        Plugins.Add(new AutoQueryFeature
        {
            MaxLimit = 1000
        });

        Plugins.Add(new AdminUsersFeature());

        container.AddSingleton<ICrudEvents>(c => new OrmLiteCrudEvents(c.Resolve<IDbConnectionFactory>()));
        container.Resolve<ICrudEvents>().InitSchema();
    }
}
