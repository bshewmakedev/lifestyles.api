using Lifestyles.Api.Controllers;
using Lifestyles.Domain.Live.Services;
using Lifestyles.Domain.Node.Repositories;
using Lifestyles.Service.Live.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Budget = Lifestyles.Service.Budget.Map.Budget;
using Category = Lifestyles.Service.Categorize.Map.Category;
using Lifestyle = Lifestyles.Service.Live.Map.Lifestyle;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddTransient<BudgetController>();
builder.Services.AddTransient<CategorizeController>();
builder.Services.AddTransient<LiveController>();
builder.Services.AddScoped<ILiveService<Budget>, LiveService<Budget>>();
builder.Services.AddScoped<ILiveService<Category>, LiveService<Category>>();
builder.Services.AddScoped<ILiveService<Lifestyle>, LiveService<Lifestyle>>();
builder.Services.AddScoped<INodeRepo<Budget>, NodeRepo<Budget>>();
builder.Services.AddScoped<INodeRepo<Category>, NodeRepo<Category>>();
builder.Services.AddScoped<INodeRepo<Lifestyle>, NodeRepo<Lifestyle>>();

builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// todo : replace session w/ relational db
builder.Services.AddDistributedMemoryCache(); // Adds default in-memory implementation of IDistributedCache.
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseSession();
// app.UseAuthorization();

app.MapControllers();

app.Run();