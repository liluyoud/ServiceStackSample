using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.FluentValidation;
using ServiceStack.Web;

namespace Escola.Api;

public class CustomUserSession : AuthUserSession
{
}

public class CustomRegistrationValidator : RegistrationValidator
{
    public CustomRegistrationValidator()
    {
        RuleSet(ApplyTo.Post, () =>
        {
            RuleFor(x => x.DisplayName).NotEmpty();
            RuleFor(x => x.ConfirmPassword).NotEmpty();
        });
    }
}

public class ConfigureAuth : IConfigureAppHost, IConfigureServices
{
    public void Configure(IServiceCollection services)
    {
    }

    public void Configure(IAppHost appHost)
    {
        var AppSettings = appHost.AppSettings;
        appHost.Plugins.Add(new AuthFeature(() => new CustomUserSession(),
            new IAuthProvider[] {
                    new CredentialsAuthProvider(AppSettings),     
                    new FacebookAuthProvider(AppSettings),        
                    new GoogleAuthProvider(AppSettings),          
                    new MicrosoftGraphAuthProvider(AppSettings),  
            }));

        appHost.Plugins.Add(new RegistrationFeature()); 

        appHost.RegisterAs<CustomRegistrationValidator, IValidator<Register>>();
    }
}

public class AppUser : UserAuth
{
    public string? ProfileUrl { get; set; }
    public string? LastLoginIp { get; set; }
    public DateTime? LastLoginDate { get; set; }
}

public class AppUserAuthEvents : AuthEvents
{
    public override void OnAuthenticated(IRequest req, IAuthSession session, IServiceBase authService,
        IAuthTokens tokens, Dictionary<string, string> authInfo)
    {
        var authRepo = HostContext.AppHost.GetAuthRepository(req);
        using (authRepo as IDisposable)
        {
            var userAuth = (AppUser)authRepo.GetUserAuth(session.UserAuthId);
            userAuth.ProfileUrl = session.GetProfileUrl();
            userAuth.LastLoginIp = req.UserHostAddress;
            userAuth.LastLoginDate = DateTime.UtcNow;
            authRepo.SaveUserAuth(userAuth);
        }
    }
}

public class ConfigureAuthRepository : IConfigureAppHost, IConfigureServices, IPreInitPlugin
{
    public void Configure(IServiceCollection services)
    {
        services.AddSingleton<IAuthRepository>(c =>
            new OrmLiteAuthRepository<AppUser, UserAuthDetails>(c.Resolve<IDbConnectionFactory>())
            {
                UseDistinctRoleTables = true
            });
    }

    public void Configure(IAppHost appHost)
    {
        var authRepo = appHost.Resolve<IAuthRepository>();
        authRepo.InitSchema();
        CreateUser(authRepo, "admin", "admin@sapiens.com.br", "Sapiens Admin", "Sapiens@123", roles: new[] { RoleNames.Admin });
    }

    public void BeforePluginsLoaded(IAppHost appHost)
    {
        appHost.AssertPlugin<AuthFeature>().AuthEvents.Add(new AppUserAuthEvents());
    }

    public void CreateUser(IAuthRepository authRepo, string username, string email, string name, string password, string[] roles)
    {
        if (authRepo.GetUserAuthByUserName(email) == null)
        {
            var newAdmin = new AppUser { UserName = username, Email = email, DisplayName = name };
            var user = authRepo.CreateUserAuth(newAdmin, password);
            authRepo.AssignRoles(user, roles);
        }
    }
}