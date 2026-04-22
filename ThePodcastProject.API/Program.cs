using Microsoft.EntityFrameworkCore;
using ThePodcastProject.Application.Services;
using ThePodcastProject.Infraestructure.Repositories;
using ThePodcastProject.Persistence.DataContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ThePodcastProjectApplicationContext>(o=>o.
    UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<CabinRepository>();
builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<EquipmentRepository>();
builder.Services.AddScoped<ReservationRepository>();

builder.Services.AddScoped<CabinService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<EquipmentService>();
builder.Services.AddScoped<ReservationService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();        // ← agregar
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();

