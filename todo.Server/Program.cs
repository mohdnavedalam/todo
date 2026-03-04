using Microsoft.EntityFrameworkCore;
using todo.Server.Data;
using todo.Server.Services.Contracts;
using todo.Server.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to use HTTPS
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(7015, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});

// Add services to the container.
//builder.Services.AddDbContext<AppDbContext>(options => {
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDbConnection"));
//});
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureDbConnection"), builder =>
    {
        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    });
});
builder.Services.AddScoped<ITodoActions, TodoActions>();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnetcore/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        corsBuilder =>
        {
            corsBuilder.WithOrigins("https://localhost:61207")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseDefaultFiles();
app.UseStaticFiles();

// HTTPS redirection is disabled in development
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
