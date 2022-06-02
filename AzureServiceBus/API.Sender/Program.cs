using API.Sender.Domain.Models;
using API.Sender.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SqlDBContext>(options => options
    .UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlDb")));

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IAzureServiceBusService, AzureServiceBusService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options
    .CustomSchemaIds(type => $"{type.Name}_{Guid.NewGuid()}")
);

builder.Services.AddMediatR(typeof(Program));

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
