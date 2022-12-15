using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PYPJ.Interfaces;
using PYPJ.Models;
using PYPJ.Models.CustomModels;
using PYPJ.Repository;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



var connectionString = builder.Configuration.GetConnectionString("dbConnection");
builder.Services.AddDbContext<PypjContext>(option => { option.UseSqlServer(connectionString); });
var _dbcontext = builder.Services.BuildServiceProvider().GetService<PypjContext>();

builder.Services.AddSingleton<IRefreshTokenGenerate>(provider => new RefreshTokenGenerate(_dbcontext));

//var _jwtsetting = Microsoft.Extensions.Logging.Configuration.GetSection("JWTSetting");
//builder.Services.Configure<JWTSetting>(_jwtsetting);

//var authkey = Configuration.GetValue<string>("JWTSetting:securitykey");

builder.Services.AddAuthentication(item =>
{
    item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(item =>
{

    item.RequireHttpsMetadata = true;
    item.SaveToken = true;
    item.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisismysecretkey")),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("api", new OpenApiInfo()
//    {
//        Description = "Customer API with curd operations",
//        Title = "Customer",
//        Version = "1"
//    });
//});
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
