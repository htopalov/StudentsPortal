using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StudentsPortal.Data;
using StudentsPortal.Data.Repositories.Image;
using StudentsPortal.Data.Repositories.Student;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddCors((opt) =>
    {
        opt.AddPolicy("angularApp", (policyBuilder) =>
        {
            policyBuilder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .WithMethods("GET", "POST", "PUT", "DELETE")
                .WithExposedHeaders("*");
        });
    });

builder
    .Services
    .AddControllers();

builder
    .Services
    .AddDbContext<StudentsPortalDbContext>(opt =>
        opt.UseSqlServer(
            builder
                .Configuration
                .GetConnectionString("StudentsPortalDbConnection"))
        );

builder
    .Services
    .AddScoped<IStudentRepository, StudentRepository>();

builder
    .Services
    .AddScoped<IImageRepository, ImageRepository>();

builder
    .Services
    .AddAutoMapper(typeof(Program).Assembly);

builder
    .Services
    .AddEndpointsApiExplorer();

builder
    .Services
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentsPortalAPI", Version = "v1" });
    });

builder
    .Services
    .Configure<RouteOptions>(opt =>
    {
        opt.LowercaseUrls = true;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("angularApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
