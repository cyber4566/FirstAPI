using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using LoginLib.Login.Implementation;
using LoginLib.Login.Interface;
using Serilog;
using Serilog.Sinks.ApplicationInsights.TelemetryConverters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILoginService,LoginService>();



//var keyVaultUrl = builder.Configuration["KeyVaultUri"];
//if (!string.IsNullOrEmpty(keyVaultUrl))
//{
//    var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
//    // Load all secrets from Key Vault into the application's configuration
//    builder.Configuration.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());
//}
builder.Services.AddApplicationInsightsTelemetry();

IConfiguration configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration().WriteTo.ApplicationInsights(connectionString: configuration["ApplicationInsights:ConnectionString"], telemetryConverter: new TraceTelemetryConverter()).CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
