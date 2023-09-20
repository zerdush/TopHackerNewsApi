using Microsoft.Extensions.Caching.Memory;
using TopHackerNewsApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IHackerRankService, HackerRankService>();
builder.Services.AddScoped<IHackerRankClient>(provider => new CachedHackerRankClient(
    new HackerRankClient(provider.GetRequiredService<HttpClient>()),
    provider.GetRequiredService<IMemoryCache>()));

builder.Services.AddScoped(_ =>
{
    var httpClient = new HttpClient();
    return httpClient;
});

var memoryCache = new MemoryCache(new MemoryCacheOptions());
builder.Services.AddSingleton<IMemoryCache>(_ => memoryCache);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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