using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpanJson.WebBenchmark.Infrastructure;

namespace SpanJson.WebBenchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services
            builder.Services.AddMvcCore().AddSerializers();

            var app = builder.Build();

            app.MapControllers();

            app.Run();
        }
    }
}