using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Interfaces;
using GraduationProject_DAL.Interfaces.Authentication;
using GraduationProject_DAL.Repositories.Authentication;
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
        builder.Services.AddDbContext<HospitalBDContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnectionString")
                ),contextLifetime:ServiceLifetime.Singleton);

        //Add Repo Services
        builder.Services.AddScoped<IDoctorRepo, DoctorRepo>();
        builder.Services.AddScoped<IPatientRepo, PatientRepo>();

		// Adding Authentication using JWT
		builder.Services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(o =>
		{
			var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
			o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                ValidAudience = builder.Configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
			};
            
		});
		builder.Services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();
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

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        
        //Use Cors
        app.UseCors(CorsPolicy);

        app.Run();
    }
}