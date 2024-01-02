using CoreLayer.Configuration;
using CoreLayer.Models;
using CoreLayer.Repository;
using CoreLayer.Services;
using CoreLayer.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer;
using RepositoryLayer.Repositories;
using RepositoryLayer.UnitOfWork;
using ServiceLayer.Services;
using SharedLibrary.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();

//DI Register
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped(typeof(IServiceGeneric<,>),typeof(ServiceGeneric<,>));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("RepositoryLayer");
    });

});

builder.Services.AddIdentity<UserApp,IdentityRole>(Opt =>
{
    Opt.User.RequireUniqueEmail = true;
    Opt.Password.RequireNonAlphanumeric = false;

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();



builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));
builder.Services.Configure<List<Client>>(builder.Configuration.GetSection("Clients"));



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

//Token'ýn ömrünü ve hangi verilere istek yapabileceðini belirliyoruz.//
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, ops =>
{
    var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
    ops.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience[0],
        IssuerSigningKey = SignService.GetSymmmetriSecurityKey(tokenOptions.SecurityKey),

        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero

    };
});

builder.Services.AddSwaggerGen();

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

app.Run();
