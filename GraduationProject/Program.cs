using GraduationProject.Middlewares;
using GraduationProject_BL.Interfaces;
using GraduationProject_BL.Managers;
using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using GraduationProject_DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        string CorsPolicy = "";
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.
        builder.Services.AddDbContext<HospitalDBContext>(options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnectionString")
        ));

        //Add Repo Services
        builder.Services.AddScoped<IRepository<Department>, DepartmentRepository>();
        builder.Services.AddScoped<ITranslations<DepartmentTranslations>, DepartmentTranslationsRepository>();
        builder.Services.AddScoped<IDepartmentManager, DepartmentManager>();
        builder.Services.AddScoped<IRepository<Doctor>, DoctorRepository>();
        builder.Services.AddScoped<IRepository<Patient>, PatientRepository>();
        builder.Services.AddScoped<IRepository<PatientsLogins>, PatientsLoginsRepository>();
        builder.Services.AddScoped<ITranslations<PatientTranslations>, PatientTranslationsRepository>();
        builder.Services.AddScoped<IPatientManager, PatientManager>();
        builder.Services.AddScoped<IPatientLoginManager, PatientLoginManager>();
        builder.Services.AddScoped<IRepository<Reservation>, ReservationRepository>();

        // Adding Authentication using JWT

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                };
            });

        // End of JWT Authentication

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

        // To Access Headers
        builder.Services.AddHttpContextAccessor();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<TokenAuthenticationMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        //Use Cors
        app.UseCors(CorsPolicy);

        app.Run();
    }
}