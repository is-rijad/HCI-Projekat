using Backend.Data;
using Backend.Servisi;
using Microsoft.EntityFrameworkCore;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false)
    .Build();

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDbContext<HCIDBContext>(options =>
    options.UseSqlServer(config.GetConnectionString("HCI_Database")));
builder.Services.AddTransient<ProvjeriRezervaciju>();
builder.Services.AddTransient<AuthServis>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
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

app.UseCors(
    options => options
        .SetIsOriginAllowed(x => _ = true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
); //This needs to set everything allowed

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();