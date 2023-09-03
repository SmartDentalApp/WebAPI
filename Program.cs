using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Interfaces;
using smart_dental_webapi.Boot;
using smart_dental_webapi.Data;
using smart_dental_webapi.Repositories.Customer;
using smart_dental_webapi.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddTransient<SeedDb>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddDbContext<SmartDentalDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.ConfigureAuthentication();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.ConfigureSwagger();

builder.Services.ConfigureCors();

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedDb.SeedData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAny");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
