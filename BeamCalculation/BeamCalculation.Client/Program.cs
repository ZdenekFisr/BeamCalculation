using Application.Services.AssignForce;
using Application.Services.AssignMoment;
using Application.Services.Calculation;
using Application.Services.CreatePoints;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BeamCalculation.Client
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddScoped<ICreatePointsService, CreatePointsService>();
            builder.Services.AddScoped<IAssignForceService, AssignForceService>();
            builder.Services.AddScoped<IAssignMomentService, AssignMomentService>();
            builder.Services.AddScoped<ICalculationService, CalculationService>();

            await builder.Build().RunAsync();
        }
    }
}
