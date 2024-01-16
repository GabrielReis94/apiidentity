using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace MicroserviceIdentityAPI.CrossCutting.IOC.ConfigSerilog
{
    public static class SerilogConfiguration
    {
        public static void AddSerilogApi(IConfiguration configuration)
        {
            var esUri = configuration.GetSection("ElasticSearch:Uri").Value?.ToString() ?? string.Empty;

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(
                    options:
                        new ElasticsearchSinkOptions(
                                new Uri(esUri))
                        {
                            AutoRegisterTemplate = true,
                            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                            IndexFormat = "MicroserviceIdentityapi-{0:yyyy.MM}",
                            MinimumLogEventLevel = LogEventLevel.Error,
                            CustomFormatter = new ElasticsearchJsonFormatter()
                        })
                        .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Error)
                        .CreateLogger();
        }
    }
}