using System;
using AutoMapper;
using FluentValidation;
using HR.Application;
using HR.Application.Employees.DTOs;
using HR.Application.GeneralSettings.Dtos;
using HR.Application.OfficialHoliday.Dtos;
using HR.Application.Services;
using HR.Application.Validation;
using HR.Domain.Repositories;
using HR.Domain.Services;
using HR.Infrastructure.Persistence;
using HR.Infrastructure.Repositories;
using HR.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HrDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAutoMapper(cfg => { }, typeof(EmployeeProfile).Assembly);
builder.Services.AddAutoMapper(cfg => { }, typeof(GeneralSettingsProfile).Assembly);
builder.Services.AddAutoMapper(cfg => { }, typeof(OfficialHolidayProfile).Assembly);


builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IGeneralSettingsRepository, GeneralSettingsRepository>();
builder.Services.AddScoped<IOfficialHolidayRepository, OfficialHolidayRepository>();


builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IGeneralSettingsService, GeneralSettingsService>();
builder.Services.AddScoped<IOfficialHolidayService, OfficialHolidayService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateEmployeeValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateEmployeeValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateGeneralSettingsValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateOfficialHolidayValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateOfficialHolidayValidator>(); 


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();