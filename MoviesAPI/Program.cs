using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoviesAPI.Data;
using MoviesAPI.Models;
using MoviesAPI.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));
var jwtSettings = builder.Configuration.GetSection("JWTSettings").Get<JWTSettings>();
var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);

builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

string connectionString = builder.Configuration.GetConnectionString("SaudeConnection");
builder.Services.AddDbContext<APIContext>(opts => opts.UseSqlServer(connectionString));

builder.Services.AddScoped<SpecialtyService>();
builder.Services.AddScoped<DoctorService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<UserInfoService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<LoginService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalhost3000");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
