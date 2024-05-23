using E_CommerceFurnitureBackend.DbCo;
using E_CommerceFurnitureBackend.Mapper;
using E_CommerceFurnitureBackend.Services.CartServices;
using E_CommerceFurnitureBackend.Services.JwtServices;
using E_CommerceFurnitureBackend.Services.OrderServices;
using E_CommerceFurnitureBackend.Services.ProductServices;
using E_CommerceFurnitureBackend.Services.UserServices;
using E_CommerceFurnitureBackend.Services.WishListServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IProductServices, ProductServices>();
builder.Services.AddSingleton<IWishListServices, WishListServices>();
builder.Services.AddSingleton<ICartServices,CartServices>();
builder.Services.AddSingleton<IUserServices,UserServices>();
builder.Services.AddSingleton<UserDbContext>();
builder.Services.AddAutoMapper(typeof(Mappers));
builder.Services.AddSingleton<IOrderServices,OrderServices>();
builder.Services.AddSingleton<IJwtServices,JwtServices>();
builder.Services.AddHttpContextAccessor();

//--------------
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
