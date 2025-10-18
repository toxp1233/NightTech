using gizmogeo.API.Middlewares;
using NightTech.Application.Extensions;
using NightTech.Infrastructure.Extensions;
using NightTech.Infrastructure.Seeders;
using NightTect.API.Extensions;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Middlewares & Logging
    builder.Services.AddTransient<ErrorHandlingMiddleware>();

    // Infrastructure
    builder.Services.AddInfrastructure(builder.Configuration);

    // Application (DI order matters)
    builder.Services.AddHttpContextAccessor(); 
    builder.Services.AddApplication(builder.Configuration);        

    // Presentation
    builder.AddPresentation();

    var app = builder.Build();

    // Seed data
    try
    {
        using var scope = app.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<INightTechSeeder>();
        await seeder.Seed();
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Seeder failed during startup");
        throw;
    }

    // Middleware pipeline
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseSerilogRequestLogging();

    // Swagger in dev
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    if (ex is AggregateException agg)
        Console.WriteLine(agg.Flatten().InnerException);
    else
        Console.WriteLine(ex);

    Log.Fatal(ex.Message, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }
