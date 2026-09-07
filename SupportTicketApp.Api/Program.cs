using Microsoft.EntityFrameworkCore;
using SupportTicketApp.Api.Data;
using SupportTicketApp.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//DBContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//Services
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ILookupService, LookupService>();

//Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowBlazorClient",
        policy =>
        {
            policy.WithOrigins("https://localhost:7118").AllowAnyHeader().AllowAnyMethod();
        }
    );
});

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowBlazorClient");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
