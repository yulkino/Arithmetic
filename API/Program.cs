using Application;
using Domain.Entity.SettingsEntities;
using FluentValidation;
using Infrastructure;
using Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
services.AddApplication();
services.AddInfrastructure(builder.Configuration);

ValidatorOptions.Global.LanguageManager.Enabled = false;

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
dbContext.AttachRange(Operation.Addition, Operation.Subtraction, Operation.Division, Operation.Multiplication);
dbContext.AttachRange(Difficulty.Easy, Difficulty.Hard, Difficulty.Medium);

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
