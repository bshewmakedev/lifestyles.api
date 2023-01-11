using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lifestyles.Domain.Live.Services;
using Lifestyles.Domain.Node.Entities;
using Lifestyles.Infrastructure.Session.Budget.Models;
using Lifestyles.Infrastructure.Session.Categorize.Models;
using Lifestyles.Infrastructure.Session.Live.Models;
using BudgetMap = Lifestyles.Infrastructure.Session.Budget.Map.Budget;
using Budget = Lifestyles.Service.Budget.Map.Budget;
using Category = Lifestyles.Service.Categorize.Map.Category;
using Lifestyle = Lifestyles.Service.Live.Map.Lifestyle;

namespace Lifestyles.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BudgetController : LiveController
    {
        private readonly ILiveService<Budget> _budgetSvc;
        private readonly ILiveService<Category> _categorySvc;
        private readonly ILiveService<Lifestyle> _lifestyleSvc;

        public BudgetController(
            ILiveService<Budget> budgetSvc,
            ILiveService<Category> categorySvc,
            ILiveService<Lifestyle> lifestyleSvc) : base(
                categorySvc,
                lifestyleSvc)
        {
            _budgetSvc = budgetSvc;
        }

        public new class DefaultParams
        {
            public JsonLifestyle Lifestyle { get; set; }
            public JsonCategory Category { get; set; }
        }

        public static IList<JsonBudget> DefaultBudgets(DefaultParams dfParams)
        {
            var filePath = $"Budget/Defaults/budgets.{dfParams.Lifestyle.Alias}.{dfParams.Category.Alias}.json";
            using (StreamReader srBudgets = System.IO.File.OpenText(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), filePath))))
            {
                var dfBudgets = (JsonConvert.DeserializeObject<List<JsonBudget>>(srBudgets.ReadToEnd()) ?? new List<JsonBudget>());

                return dfBudgets;
            }
        }

        [HttpGet]
        [Route("default")]
        public INode<JsonBudget> Default()
        {
            var lifestyles = Upsert(DefaultLifestyles().ToList()).ToList();
            lifestyles.ForEach(lifestyle =>
            {
                var categories = Upsert(DefaultCategories(new CategorizeController.DefaultParams
                {
                    Lifestyle = lifestyle
                }).ToList()).ToList();
                Group(new List<Grouped<JsonCategory>>() {
                    new Grouped<JsonCategory>
                    {
                        AsEntity = lifestyle,
                        Entities = categories
                    }
                });
                categories.ForEach(category =>
                {
                    var budgets = Upsert(DefaultBudgets(new BudgetController.DefaultParams
                    {
                        Lifestyle = lifestyle,
                        Category = category
                    }).ToList()).ToList();
                    Group(new List<Grouped<JsonCategory>>() {
                        new Grouped<JsonCategory>
                        {
                            AsEntity = category,
                            Entities = budgets.Select(b => b as JsonCategory).ToList()
                        }
                    });
                });
            });

            return Find();
        }

        [HttpGet]
        [Route("find")]
        public INode<JsonBudget> Find()
        {
            return _budgetSvc
                .Find()
                .SelectDownRecursive(node => new JsonBudget(node));
        }

        [HttpGet]
        [Route("findgroupedas")]
        public INode<JsonBudget> FindGroupedAs(JsonBudget jsonBudget)
        {
            return _budgetSvc
                .FindGroupedAs(new BudgetMap(jsonBudget) as Budget)
                .SelectDownRecursive(node => new JsonBudget(node));
        }

        [HttpPost]
        [Route("upsert")]
        public IList<JsonBudget> Upsert(List<JsonBudget> jsonBudgets)
        {
            return _budgetSvc
                .Upsert(
                    jsonBudgets
                        .Select(jsonBudget => new BudgetMap(jsonBudget) as Budget)
                        .ToList())
                .Select(budget => new JsonBudget(budget))
                .ToList();
        }

        [HttpPost]
        [Route("delete")]
        public void Delete(List<JsonBudget> jsonBudgets)
        {
            _budgetSvc
                .Delete(
                    jsonBudgets
                        .Select(jsonBudget => new BudgetMap(jsonBudget) as Budget)
                        .ToList());
        }
    }
}