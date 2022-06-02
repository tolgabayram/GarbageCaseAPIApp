using GarbageCaseAPI.Authentication.Implemantation;
using GarbageCaseAPI.Authentication.Interface;
using GarbageCaseAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var myAllowSpesificOrigins = "_myAllowSpesificOrigins";
// Add services to the container.

builder.Services.AddControllers();

var key = "This is my first Test Key";
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key))
    };
});

builder.Services.AddSingleton<IJwtAuth>(new Auth(key));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))

);
//Enable CORS
builder.Services.AddCors(options =>
options.AddPolicy(name: myAllowSpesificOrigins,
builder => builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader()
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(myAllowSpesificOrigins);
app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
