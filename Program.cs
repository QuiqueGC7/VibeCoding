using WeatherAPI.Services;
using WeatherAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar HttpClient
builder.Services.AddHttpClient<IWeatherService, WeatherService>();

// Inyección de dependencias
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddSingleton<ICacheRepository, CacheRepository>();

// Configuración CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configurar pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
