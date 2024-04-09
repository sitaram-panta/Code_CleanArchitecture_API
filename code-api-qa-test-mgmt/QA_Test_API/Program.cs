using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QA_Test_Log.AutoMigration;
using QA_Test_Log.Data;
using QA_Test_Log.Interface;
using QA_Test_Log.Models;
using QA_Test_Log.Services;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();  //enabling cors 
    });
});



Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

builder.Host.UseSerilog(Log.Logger);

Log.Information("Getting the motors running...");
try
{

    // Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddScoped<IModuleRepo, ModuleRepo>();
    builder.Services.AddScoped<IDatabaseHelper, DatabaseHelperService>();
    builder.Services.AddScoped<IProjectRepo, ProjectRepo>();
    builder.Services.AddScoped<ITestCaseRepo, TestCaseRepo>();
    builder.Services.AddScoped<ITestResultRepo, TestResultRepo>();
    builder.Services.AddScoped<ITaskRepo, TaskRepo>();

    //builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

    builder.Services.AddScoped<IDbConnection>(c => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddDbContext<AppDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
       );

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

    // Adding Authentication
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    // Adding Jwt Bearer
        .AddJwtBearer(options =>
 {
     options.SaveToken = true;
     options.RequireHttpsMetadata = false;
     options.TokenValidationParameters = new TokenValidationParameters()
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidAudience = configuration["JWT:ValidAudience"],
         ValidIssuer = configuration["JWT:ValidIssuer"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
     };
 });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
        

    });


    var app = builder.Build();
    app.UseCors("MyPolicy");

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Auto_Mig.Initialize(dbContext);
    }


    // Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment())
    //{
    app.UseSwagger();
    app.UseSwaggerUI();
    //}

    app.UseHttpsRedirection();
    app.UseAuthentication();

    app.UseAuthorization();
    app.UseStaticFiles();
    app.UseDefaultFiles();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
