using Microsoft.AspNetCore.Mvc;
using Lifestyles.Domain.Node.Entities;
using Lifestyles.Domain.Live.Services;
using Lifestyles.Infrastructure.Session.Budget.Models;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace Lifestyles.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LiveController : ControllerBase
    {
        private readonly ILiveService<Lifestyles.Service.Budget.Map.Budget> _liveService;

        public LiveController(ILiveService<Lifestyles.Service.Budget.Map.Budget> liveService)
        {
            _liveService = liveService;
        }

        [HttpGet]
        [Route("default")]
        public INode<JsonBudget> Default()
        {
            using (StreamReader srLifestyles = System.IO.File.OpenText(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), $"Live/Defaults/lifestyles.json"))))
            {
                var dfLifestyles = (JsonConvert.DeserializeObject<List<JsonBudget>>(srLifestyles.ReadToEnd()) ?? new List<JsonBudget>());
                var lifestyles = Upsert(dfLifestyles).ToList();
                lifestyles.ForEach(lifestyle =>
                {
                    using (StreamReader srCategories = System.IO.File.OpenText(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), $"Categorize/Defaults/categories.{lifestyle.Alias}.json"))))
                    {
                        var dfCategories = (JsonConvert.DeserializeObject<List<JsonBudget>>(srCategories.ReadToEnd()) ?? new List<JsonBudget>());
                        var categories = Upsert(dfCategories).ToList();
                        Group(new List<Grouped<JsonBudget>>() {
                            new Grouped<JsonBudget>
                            {
                                AsEntity = lifestyle,
                                Entities = categories
                            }
                        });
                        categories.ForEach(category =>
                        {
                            using (StreamReader srBudgets = System.IO.File.OpenText(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), $"Budget/Defaults/budgets.{lifestyle.Alias}.{category.Alias}.json"))))
                            {
                                var dfBudgets = (JsonConvert.DeserializeObject<List<JsonBudget>>(srBudgets.ReadToEnd()) ?? new List<JsonBudget>());
                                var budgets = Upsert(dfBudgets).ToList();
                                Group(new List<Grouped<JsonBudget>>() {
                                    new Grouped<JsonBudget>
                                    {
                                        AsEntity = category,
                                        Entities = budgets
                                    }
                                });
                            }
                        });
                    }
                });
            }

            return Find();
        }

        [HttpGet]
        [Route("find")]
        public INode<JsonBudget> Find()
        {
            return _liveService
                .Find()
                .SelectDownRecursive(node => new JsonBudget(node));
        }

        [HttpPost]
        [Route("findgroupedas")]
        public INode<JsonBudget> FindGroupedAs(JsonBudget jsonBudget)
        {
            return _liveService
                .FindGroupedAs(new Lifestyles.Infrastructure.Session.Budget.Map.Budget(jsonBudget) as Lifestyles.Service.Budget.Map.Budget)
                .SelectDownRecursive(node => new JsonBudget(node));
        }

        [HttpPost]
        [Route("upsert")]
        public IList<JsonBudget> Upsert(List<JsonBudget> jsonBudgets)
        {
            return _liveService
                .Upsert(
                    jsonBudgets
                        .Select(jsonBudget => new Lifestyles.Infrastructure.Session.Budget.Map.Budget(jsonBudget) as Lifestyles.Service.Budget.Map.Budget)
                        .ToList())
                .Select(budget => new JsonBudget(budget))
                .ToList();
        }

        [HttpPost]
        [Route("delete")]
        public void Delete(List<JsonBudget> jsonBudgets)
        {
            _liveService
                .Delete(
                    jsonBudgets
                        .Select(jsonBudget => new Lifestyles.Infrastructure.Session.Budget.Map.Budget(jsonBudget) as Lifestyles.Service.Budget.Map.Budget)
                        .ToList());
        }

        [HttpPost]
        [Route("group")]
        public IList<INode<JsonBudget>> Group(List<Grouped<JsonBudget>> groupings)
        {
            return _liveService
                .Group(
                    groupings
                        .Select(grouping =>
                            new Grouped<Lifestyles.Service.Budget.Map.Budget>
                            {
                                AsEntity = new Lifestyles.Infrastructure.Session.Budget.Map.Budget(grouping.AsEntity) as Lifestyles.Service.Budget.Map.Budget,
                                Entities = grouping.Entities.Select(entity => new Lifestyles.Infrastructure.Session.Budget.Map.Budget(entity) as Lifestyles.Service.Budget.Map.Budget).ToList()
                            } as IGrouped<Lifestyles.Service.Budget.Map.Budget>)
                        .ToList())
                .Select(n => n.SelectDownRecursive(node => new JsonBudget(node)))
                .ToList();
        }
    }

}