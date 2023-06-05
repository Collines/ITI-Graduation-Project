using GraduationProject;
using GraduationProject.Middlewares;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add All Our needed Services here
        builder.Services.AddHospitalServices(builder.Configuration);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //Enable Cors
        builder.Services.AddCors(o =>
        {
            o.AddDefaultPolicy(builder =>
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

        // app.UseHttpsRedirection();

        //Use Cors
        app.UseCors();

        app.UseMiddleware<TokenAuthenticationMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();



        app.Run();
    }
}