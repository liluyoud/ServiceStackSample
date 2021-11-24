using Escola.Core.Entities;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Escola.Api
{
    public class ConfigureDb : IConfigureServices, IConfigureAppHost
    {
        IConfiguration Configuration { get; }
        public ConfigureDb(IConfiguration configuration) => Configuration = configuration;

        public void Configure(IServiceCollection services)
        {
            services.AddSingleton<IDbConnectionFactory>(new OrmLiteConnectionFactory(
                Configuration.GetConnectionString("DefaultConnection"),
                SqlServer2019Dialect.Provider));
        }

        public void Configure(IAppHost appHost)
        {
            appHost.GetPlugin<SharpPagesFeature>()?.ScriptMethods.Add(new DbScriptsAsync());

            using var db = appHost.Resolve<IDbConnectionFactory>().Open();
            if (db.CreateTableIfNotExists<City>())
            {
            }

            if (db.CreateTableIfNotExists<School>())
            {
            }
        }
    }
}
