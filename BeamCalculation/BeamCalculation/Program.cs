using Application.Services.AssignForce;
using Application.Services.Calculation;
using Application.Services.CreateLoads;
using Application.Services.CreatePoints;
using Application.Services.Reactions;
using BeamCalculation.Components;

namespace BeamCalculation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();

            builder.Services.AddScoped<IReactionsService, ReactionsService>();
            builder.Services.AddScoped<ICreateLoadsService, CreateLoadsService>();
            builder.Services.AddScoped<ICreatePointsService, CreatePointsService>();
            builder.Services.AddScoped<IAssignForceService, AssignForceService>();
            builder.Services.AddScoped<ICalculationService, CalculationService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.Run();
        }
    }
}
