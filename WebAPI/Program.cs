using AppCore.Interfaces;
using AppCore.Module;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Memory;
using Infrastructure.Security;

namespace WebAPI;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddContactsModule(builder.Configuration);
        builder.Services.AddContactsEfModule(builder.Configuration);
        builder.Services.AddMemoryCache();
        builder.Services.AddSingleton<JwtSettings>();
        builder.Services.AddJwt(new JwtSettings(builder.Configuration));

        // builder.Services.AddContactsCoreModule(builder.Configuration);
        // builder.Services.AddContactsMemoryModule();
        // builder.Services.AddSingleton<ICompanyRepository, MemoryCompanyRepository>();
        // builder.Services.AddSingleton<IContactRepository, MemoryContactRepository>();
        // builder.Services.AddSingleton<IContactUnitOfWork, MemoryContactUnitOfWork>();
        // builder.Services.AddSingleton<IPersonRepository, MemoryPersonRepository>();
        // builder.Services.AddSingleton<IOrganizationRepository, MemoryOrganizationRepository>();
        // builder.Services.AddSingleton<IPersonService, MemoryPersonService>();
        
        builder.Services.AddExceptionHandler<ProblemDetailsExceptionHandler>();    
        builder.Services.AddProblemDetails();
        
        builder.Services.AddControllers();
        
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            using var scope = app.Services.CreateScope(); // zasięg dostepu do kontenera DI
            // "wyciągniecie" z kontenera instacji klasy implementującej IDataSeeder
            var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
            await seeder.SeedAsync();    // wywołanie metody Seedera
        }
        
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseExceptionHandler();  
        app.MapControllers();
        app.Run();
    }
}