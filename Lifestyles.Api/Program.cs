using Lifestyles.Domain.Budget.Repositories;
using Lifestyles.Domain.Categorize.Repositories;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Infrastructure.Session.Budget.Repositories;
using Lifestyles.Infrastructure.Session.Categorize.Repositories;
using Lifestyles.Infrastructure.Session.Live.Repositories;
using DefaultBudgetRepo = Lifestyles.Infrastructure.Default.Budget.Repositories.BudgetRepo;
using DefaultCategoryRepo = Lifestyles.Infrastructure.Default.Categorize.Repositories.CategoryRepo;
using DefaultLifestyleRepo = Lifestyles.Infrastructure.Default.Live.Repositories.LifestyleRepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IKeyValueRepo, SessionRepo>();
builder.Services.AddScoped<IBudgetTypeRepo, BudgetTypeRepo>();
builder.Services.AddScoped<IRecurrenceRepo, RecurrenceRepo>();
builder.Services.AddScoped<IExistenceRepo, ExistenceRepo>();
builder.Services.AddScoped<IBudgetRepo, BudgetRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<ILifestyleRepo, LifestyleRepo>();
builder.Services.AddScoped<DefaultBudgetRepo, DefaultBudgetRepo>();
builder.Services.AddScoped<DefaultCategoryRepo, DefaultCategoryRepo>();
builder.Services.AddScoped<DefaultLifestyleRepo, DefaultLifestyleRepo>();
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