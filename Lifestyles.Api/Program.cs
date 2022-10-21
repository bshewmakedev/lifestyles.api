using Lifestyles.Domain.Budget.Repositories;
using Lifestyles.Domain.Budget.Services;
using Lifestyles.Domain.Categorize.Repositories;
using Lifestyles.Domain.Categorize.Services;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Domain.Live.Services;
using Lifestyles.Infrastructure.Session.Budget.Repositories;
using Lifestyles.Infrastructure.Session.Categorize.Repositories;
using Lifestyles.Infrastructure.Session.Live.Repositories;
using Lifestyles.Service.Budget.Services;
using Lifestyles.Service.Categorize.Services;
using Lifestyles.Service.Live.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IBudgetService, BudgetService>();
builder.Services.AddScoped<ICategorizeService, CategorizeService>();
builder.Services.AddScoped<ILiveService, LiveService>();
builder.Services.AddScoped<IKeyValueRepo, SessionRepo>();
builder.Services.AddScoped<IBudgetRepo, BudgetRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<ILifestyleRepo, LifestyleRepo>();
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