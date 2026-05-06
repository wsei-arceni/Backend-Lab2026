using AppCore.Authorization;
using AppCore.Interfaces;
using Infrastructure.EntityFramework.Context;
using Infrastructure.EntityFramework.Entities;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.UnitOfWork;
using Infrastructure.Memory;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class ContactsInfrastructureModule
{
    public static IServiceCollection AddContactsEfModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICompanyRepository, EfCompanyRepository>();
        services.AddScoped<IPersonRepository, EfPersonRepository>();
        services.AddScoped<IOrganizationRepository, EfOrganizationRepository>();
        services.AddScoped<IContactUnitOfWork, EfContactsUnitOfWork>();
        services.AddDbContext<ContactsDbContext>(options =>
            options.UseSqlite(
                configuration.GetConnectionString("DefaultConnection")));
	        
        services.AddIdentity<CrmUser, CrmRole>(options =>
            {
                options.Password.RequiredLength         = 8;
                options.Password.RequireUppercase       = true;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail         = true;
                options.SignIn.RequireConfirmedEmail    = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan  = TimeSpan.FromMinutes(15);
            })
            .AddEntityFrameworkStores<ContactsDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IDataSeeder, IdentityDbSeeder>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }

    public static IServiceCollection AddContactsMemoryModule(this IServiceCollection services)
    {
        services.AddSingleton<ICompanyRepository, MemoryCompanyRepository>();
        services.AddSingleton<IContactRepository, MemoryContactRepository>();
        services.AddSingleton<IContactUnitOfWork, MemoryContactUnitOfWork>();
        services.AddSingleton<IPersonRepository, MemoryPersonRepository>();
        services.AddSingleton<IOrganizationRepository, MemoryOrganizationRepository>();
        services.AddSingleton<IPersonService, MemoryPersonService>();
        
        return services;
    }
    
    public static IServiceCollection AddJwt(this IServiceCollection services, JwtSettings jwtOptions)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = jwtOptions.GetSymmetricKey(),
                        ClockSkew = TimeSpan.Zero // brak tolerancji czasu
                    };
                }
            );
        services.AddAuthorization(options =>
        {
            // Polityki oparte o role
            // metoda RequireRole akceptuje dowolną liczbę parametrów typu string
            options.AddPolicy(CrmPolicies.AdminOnly.ToString(), policy =>
                policy.RequireRole(UserRole.Administrator.ToString()));

            options.AddPolicy(CrmPolicies.SalesAccess.ToString(), policy =>
                policy.RequireRole(UserRole.Administrator.ToString(), UserRole.SalesManager.ToString(), UserRole.Salesperson.ToString()));
            
            options.AddPolicy(CrmPolicies.SupportAccess.ToString(), policy =>
                policy.RequireRole(UserRole.Administrator.ToString(), UserRole.SupportAgent.ToString()));

            // Polityka złożona — wymaga roli i aktywnego konta
            options.AddPolicy(CrmPolicies.ActiveUser.ToString(), policy =>
                policy
                    .RequireAuthenticatedUser()
                    .RequireClaim("status", SystemUserStatus.Active.ToString()));

            // Polityka oparta o dział
            options.AddPolicy(CrmPolicies.SalesDepartment.ToString(), policy =>
                policy.RequireClaim("department", "Sales"));

            options.AddPolicy(CrmPolicies.ReadOnlyAccess.ToString(), policy =>
                policy.RequireRole(
                    UserRole.Administrator.ToString(),
                    UserRole.SalesManager.ToString(),
                    UserRole.Salesperson.ToString(),
                    UserRole.SupportAgent.ToString(),
                    UserRole.ReadOnly.ToString()));

            options.AddPolicy(CrmPolicies.SalesManagerAccess.ToString(), policy =>
                policy.RequireRole(
                    UserRole.Administrator.ToString(),
                    UserRole.SalesManager.ToString()));

            // Domyślna polityka — każdy zalogowany użytkownik
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            // Polityka fallback — stosowana gdy brak atrybutu [Authorize]
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });
        return services;
    }
}