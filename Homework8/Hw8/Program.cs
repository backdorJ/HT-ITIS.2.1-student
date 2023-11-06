using System.Diagnostics.CodeAnalysis;
using Hw8.Calculator;
using Hw8.ExceptionHandler;
using Hw8.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hw8;

[ExcludeFromCodeCoverage]
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddScoped<ICalculatorParser, CalculatorParserService>();
        builder.Services.AddScoped<ICalculator, CalculatorService>();

        var app = builder.Build();


        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseMiddleware<CustomExceptionMiddleware>();
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthorization();

        
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Calculator}/{action=Index}");

        app.Run();
    }
}