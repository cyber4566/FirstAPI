using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using LoginLib.Login.Implementation;
using LoginLib.Login.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILoginService,LoginService>();



var keyVaultUrl = builder.Configuration["KeyVaultUri"];
if (!string.IsNullOrEmpty(keyVaultUrl))
{
    var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
    // Load all secrets from Key Vault into the application's configuration
    builder.Configuration.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());
}

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
