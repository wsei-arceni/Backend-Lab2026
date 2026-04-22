using AppCore.Interfaces;
using Infrastructure.EntityFramework.Context;
using Infrastructure.EntityFramework.Entities;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.UnitOfWork;
using Infrastructure.Memory;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ContactsInfrastructureModule
{
    public static IServiceCollection AddContactsEfModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICompanyRepository, EfCompanyRepository>();
        services.AddScoped<IPersonRepository, EfPersonRepository>();
        // TODO: Add rest repositories
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
}