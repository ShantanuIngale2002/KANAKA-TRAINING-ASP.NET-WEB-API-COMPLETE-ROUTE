

using JWT_RoleBasedAuthorization_WebApiApp.Repository.EFCore;
using JWT_RoleBasedAuthorization_WebApiApp.Repository.Interface;
using JWT_RoleBasedAuthorization_WebApiApp.Repository.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container before container.

// db context service
builder.Services.AddDbContext<RoleBasedAppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Jwt_RoleBasedAuthorization_ProjectDB")));

// added this runtime object creation services
builder.Services.AddTransient<IEmployeeRepository, EmployeeService>();
builder.Services.AddTransient<IAuthRepository, AuthService>();



builder.Services.AddControllers();



// adding authorizatoin to add token authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });





builder.Services.AddEndpointsApiExplorer();


// builder.Services.AddSwaggerGen(); // this is updated to below code in order to associate app with token use.
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyApi", Version = "v1" }); // documentation used for api versioning
    // this method used to define the security scheme to be passed
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please Enter Token ",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JMT",
        Scheme = "bearer"
    });
    // this applies the security globally to all api's
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[]{}
        }
    });
});



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
