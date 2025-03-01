using Application.Services.AssignForce;
using Application.Services.AssignMoment;
using Application.Services.AssignStress;
using Application.Services.Calculation;
using Application.Services.CreatePoints;
using BeamCalculation.Client.Localization.Service;
using BeamCalculation.Client.Plot;
using BeamCalculation.Client.Services.EmbeddedCsv;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

namespace BeamCalculation.Client
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

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
