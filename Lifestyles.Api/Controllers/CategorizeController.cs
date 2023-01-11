using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lifestyles.Domain.Live.Services;
using Lifestyles.Domain.Node.Entities;
using Lifestyles.Infrastructure.Session.Categorize.Models;
using Lifestyles.Infrastructure.Session.Live.Models;
using CategoryMap = Lifestyles.Infrastructure.Session.Categorize.Map.Category;
using Category = Lifestyles.Service.Categorize.Map.Category;
using Newtonsoft.Json;

namespace Lifestyles.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategorizeController : ControllerBase
    {
        private readonly ILiveService<Category> _categorySvc;

        public CategorizeController(ILiveService<Category> categorySvc)
        {
            _categorySvc = categorySvc;
        }

        public class DefaultParams
        {
            public JsonLifestyle Lifestyle { get; set; }
        }

        public static IList<JsonCategory> DefaultCategories(DefaultParams dfParams)
        {
            var filePath = $"Categorize/Defaults/categories.{dfParams.Lifestyle.Alias}.json";
            using (StreamReader srCategories = System.IO.File.OpenText(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), filePath))))
            {
                var dfCategories = (JsonConvert.DeserializeObject<List<JsonCategory>>(srCategories.ReadToEnd()) ?? new List<JsonCategory>());

                return dfCategories;
            }
        }

        [HttpPost]
        [Route("upsert")]
        public IList<JsonCategory> Upsert(List<JsonCategory> jsonCategories)
        {
            return _categorySvc
                .Upsert(
                    jsonCategories
                        .Select(jsonCategory => new CategoryMap(jsonCategory) as Category)
                        .ToList())
                .Select(category => new JsonCategory(category))
                .ToList();
        }

        [HttpPost]
        [Route("delete")]
        public void Delete(List<JsonCategory> jsonCategories)
        {
            _categorySvc
                .Delete(
                    jsonCategories
                        .Select(jsonCategory => new CategoryMap(jsonCategory) as Category)
                        .ToList());
        }

        [HttpPost]
        [Route("group")]
        public IList<INode<JsonCategory>> Group(List<Grouped<JsonCategory>> groupings)
        {
            return _categorySvc
                .Group(
                    groupings
                        .Select(grouping =>
                            new Grouped<Category>
                            {
                                AsEntity = new CategoryMap(grouping.AsEntity) as Category,
                                Entities = grouping.Entities.Select(entity => new CategoryMap(entity) as Category).ToList()
                            } as IGrouped<Category>)
                        .ToList())
                .Select(n => n.SelectDownRecursive(node => new JsonCategory(node)))
                .ToList();
        }
    }
}