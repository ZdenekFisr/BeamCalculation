using Application.Services.AssignForce;
using Application.Services.AssignMoment;
using Application.Services.AssignStress;
using Application.Services.Calculation;
using Application.Services.CreatePoints;
using BeamCalc.Localization.Service;
using BeamCalc.Plot;
using BeamCalc.Services.EmbeddedCsv;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

namespace BeamCalc
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddRadzenComponents();

            builder.Services.AddScoped<IEmbeddedCsvService, EmbeddedCsvService>();
            builder.Services.AddScoped<ILanguageService, LanguageService>();

            builder.Services.AddScoped<ICreatePointsService, CreatePointsService>();
            builder.Services.AddScoped<IAssignForceService, AssignForceService>();
            builder.Services.AddScoped<IAssignMomentService, AssignMomentService>();
            builder.Services.AddScoped<IAssignStressService, AssignStressService>();
            builder.Services.AddScoped<ICalculationService, CalculationService>();
            builder.Services.AddScoped<IPointTransformationService, PointTransformationService>();

            await builder.Build().RunAsync();
        }
    }
}
