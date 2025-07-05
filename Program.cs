using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebSocket_API.Data;
using WebSocket_API.Interfaces;
using WebSocket_API.Repository;
using WebSocket_API.Repository.RepositoryInterfaces;
using WebSocket_API.Seeders;
using WebSocket_API.Services;
using WebSocket_API.Services.WebSocket;
using Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// 🔷 Додаємо Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WebSocket API",
        Version = "v1",
        Description = "API for WebSocket financial data processing"
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddHttpClient<ClientService>();

builder.Services.AddScoped<IExchangeRepository, ExchangeRepository>();
builder.Services.AddScoped<IGicsClassificationRepository, GicsClassificationRepository>();
builder.Services.AddScoped<IGicsItemRepository, GicsItemRepository>();
builder.Services.AddScoped<IInstrumentRepository, InstrumentRepository>();
builder.Services.AddScoped<IInstrumentMappingRepository, InstrumentMappingRepository>();
builder.Services.AddScoped<IInstrumentProfileRepository, InstrumentProfileRepository>();
builder.Services.AddScoped<IKindRepository, KindRepository>();
builder.Services.AddScoped<IProviderRepository, ProviderRepository>();
builder.Services.AddScoped<ITradingHoursRepository, TradingHoursRepository>();
builder.Services.AddScoped<IPriceInfoRepository, PriceInfoRepository>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<WebSocketPriceProccesorService>();
builder.Services.AddScoped<IMessageProcessorService, WebSocketMessageProcessorService>();
builder.Services.AddScoped<WebSocketSubscriptionService>();
builder.Services.AddScoped<IAssetService, AssetService>();

builder.Services.AddTransient<FintachartsInstrumentService>();
builder.Services.AddTransient<FintachartsExchangeService>();
builder.Services.AddTransient<FintachartsGicsService>();
builder.Services.AddTransient<FintachartsKindService>();
builder.Services.AddTransient<FintachartsProviderService>();

builder.Services.AddTransient<ExchangeDataSeeder>();
builder.Services.AddTransient<GicsDataSeeder>();
builder.Services.AddTransient<InstrumentDataSeeder>();
builder.Services.AddTransient<KindDataSeeder>();
builder.Services.AddTransient<ProviderDataSeeder>();
builder.Services.AddTransient<DataSeeder>();

builder.Services.AddSingleton<FintachartsAuthService>();
builder.Services.AddSingleton<IWebSocketConnectionAccessor, WebSocketConnectionAccessor>();

builder.Services.AddHostedService<WebSocketClientService>();

var app = builder.Build();

// 🔷 Включаємо Swagger у Development середовищі
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebSocket API V1");
        c.RoutePrefix = string.Empty; // 👉 відкривати Swagger на корені (localhost:5000/)
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

var retryPolicy = Policy
    .Handle<SqlException>()
    .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
        (exception, timeSpan, retryCount, context) =>
        {
            app.Logger.LogWarning("Retry {RetryCount} of database connection failed: {ExceptionMessage}", retryCount, exception.Message);
        });

await retryPolicy.ExecuteAsync(async () =>
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();

    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    await seeder.Seed();
});

app.Run();
