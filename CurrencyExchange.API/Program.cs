using CurrencyExchange.API.HttpClients;
using CurrencyExchange.API.Middlewares;
using CurrencyExchange.API.ServiceContracts;
using CurrencyExchange.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICurrencyConverterService, CurrencyConverterService>();

// Add http client
builder.Services.AddHttpClient<IConversionProvider, FastExchangeHttpClient>((serviceProvider, client) =>
{
    var baseUrl = builder.Configuration["FastExchangeApi:BaseUrl"];
    client.BaseAddress = new Uri(baseUrl!);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandlingMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { } 
