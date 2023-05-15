using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Interfaces;
using GraduationProject_DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        string CorsPolicy = "";
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.
        builder.Services.AddDbContext<HospitalBDContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnectionString")
                ));

        //Add Repo Services
        builder.Services.AddScoped<IDoctorRepo, DoctorRepo>();
        builder.Services.AddScoped<IPatientRepo, PatientRepo>();
        builder.Services.AddScoped<IReservationRepo, ReservationRepo>();

        // Add reference loop handling
        builder.Services.AddControllers().AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //Enable Cors
        builder.Services.AddCors(o =>
        {
            o.AddPolicy(CorsPolicy, builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });
        });

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
        
        //Use Cors
        app.UseCors(CorsPolicy);

        app.Run();
    }
}