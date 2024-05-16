using E_CommerceFurnitureBackend.DbCo;
using E_CommerceFurnitureBackend.Mapper;
using E_CommerceFurnitureBackend.Services.ProductServices;
using E_CommerceFurnitureBackend.Services.UserServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IProductServices, ProductServices>();
builder.Services.AddSingleton<IUserServices,UserServices>();
builder.Services.AddSingleton<UserDbContext>();
builder.Services.AddAutoMapper(typeof(Mappers));
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

app.UseAuthorization();

app.MapControllers();

app.Run();
