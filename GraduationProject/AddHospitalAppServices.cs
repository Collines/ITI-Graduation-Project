﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using GraduationProject_BL.Interfaces;
using GraduationProject_BL.Managers;
using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using GraduationProject_DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace GraduationProject
{
    public static class AddHospitalAppServices
    {
        public static IServiceCollection AddHospitalServices(this IServiceCollection services, IConfiguration config)
        {
            // Add services to the container.
            services.AddDbContext<HospitalDBContext>(options => options.UseSqlServer(
                config.GetConnectionString("DefaultConnectionString")
            ));

            //Add Repo Services
            services.AddScoped<IRepository<Department>, DepartmentRepository>();
            services.AddScoped<ITranslations<DepartmentTranslations>, DepartmentTranslationsRepository>();
            services.AddScoped<IDepartmentManager, DepartmentManager>();
            services.AddScoped<IRepository<Doctor>, DoctorRepository>();
            services.AddScoped<IRepository<Patient>, PatientRepository>();
            services.AddScoped<IRepository<PatientsLogins>, PatientsLoginsRepository>();
            services.AddScoped<ITranslations<PatientTranslations>, PatientTranslationsRepository>();
            services.AddScoped<IPatientManager, PatientManager>();
            services.AddScoped<IPatientLoginManager, PatientLoginManager>();
            services.AddScoped<IRepository<Reservation>, ReservationRepository>();

            // Adding Authentication using JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = config["JWT:Issuer"],
                        ValidAudience = config["JWT:Audience"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]))
                    };
                });

            // End of JWT Authentication

            // Add reference loop handling
            services.AddControllers().AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            return services;
        }
    }
}
