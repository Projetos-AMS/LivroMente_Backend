using System.Reflection;
using LivroMente.API.Handlers.CategoryBookHandler;
using LivroMente.Data.Context;
using LivroMente.Domain.Requests;
using LivroMente.Domain.ViewModels;
using LivroMente.Service.Interfaces;
using LivroMente.Service.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddScoped<IRequestHandler<CreateCategoryBookRequest,bool>,CreateCategoryBookHandler>();



builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
// builder.Services.AddTransient<CreateCategoryBookHandler>();
// builder.Services.AddTransient<GetAllCategoryBookHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
       options.UseSqlite(builder.Configuration.GetConnectionString("DefaultContext")));

builder.Services.AddScoped(typeof(IBaseService<>),typeof(BaseService<>));
// builder.Services.AddAutoMapper(typeof(DataContext));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();



app.Run();


