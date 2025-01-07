using Application.Services.Reactions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BeamCalculation.Client
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddScoped<IReactionsService, ReactionsService>();

            await builder.Build().RunAsync();
        }
    }
}
