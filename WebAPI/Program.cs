using AppCore.Interfaces;
using AppCore.Module;
using FluentValidation.AspNetCore;
using Infrastructure.Memory;

namespace WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddAuthorization();
        builder.Services.AddContactsModule(builder.Configuration);
        builder.Services.AddMemoryCache();
        
        builder.Services.AddScoped<ICompanyRepository, MemoryCompanyRepository>();
        builder.Services.AddScoped<IContactRepository, MemoryContactRepository>();
        builder.Services.AddScoped<IContactUnitOfWork, MemoryContactUnitOfWork>();
        builder.Services.AddScoped<IPersonRepository, MemoryPersonRepository>();
        builder.Services.AddScoped<IOrganizationRepository, MemoryOrganizationRepository>();
        builder.Services.AddScoped<IPersonService, MemoryPersonService>();
        
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
        }
        
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseExceptionHandler();  
        app.MapControllers();
        app.Run();
    }
}