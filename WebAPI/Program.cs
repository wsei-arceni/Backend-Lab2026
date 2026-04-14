using AppCore.Interfaces;
using AppCore.Module;
using Infrastructure.Memory;

namespace WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddAuthorization();
        builder.Services.AddContactsModule(builder.Configuration);
        builder.Services.AddControllers();
        
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
        
        app.UseExceptionHandler();  
        app.MapControllers();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}