using Lifestyles.Api.Live.Repositories;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Domain.Budget.Entities; 
using Lifestyles.Infrastructure.Database.Budget.Repositories;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IKeyValueStorage, SessionStorage>();
builder.Services.AddScoped<IRepository<IBudget>, BudgetRepo>();
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
