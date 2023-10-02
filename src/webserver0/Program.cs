using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var otlpEndpoint = builder.Configuration.GetValue<string>("OtlpExporter:Endpoint");
var applicationName = builder.Environment.ApplicationName;

builder.Services.AddOpenTelemetry()
    .ConfigureResource(b => b
        .AddService(serviceName: applicationName))
    .WithTracing(b => b
        .AddAspNetCoreInstrumentation()
        .AddEntityFrameworkCoreInstrumentation()
        .AddGrpcClientInstrumentation()
        .AddHttpClientInstrumentation()
        .AddOtlpExporter(opt =>
        {
            if (otlpEndpoint != null)
                opt.Endpoint = new Uri(otlpEndpoint);
        }))
    .WithMetrics(b => b
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddOtlpExporter());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
