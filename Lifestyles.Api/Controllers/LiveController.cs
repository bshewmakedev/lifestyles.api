using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lifestyles.Domain.Live.Services;
using Lifestyles.Infrastructure.Session.Live.Models;
using LifestyleMap = Lifestyles.Infrastructure.Session.Live.Map.Lifestyle;
using Category = Lifestyles.Service.Categorize.Map.Category;
using Lifestyle = Lifestyles.Service.Live.Map.Lifestyle;

namespace Lifestyles.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LiveController : CategorizeController
    {
        private readonly ILiveService<Category> _categorySvc;
        private readonly ILiveService<Lifestyle> _lifestyleSvc;

        public LiveController(
            ILiveService<Category> categorySvc,
            ILiveService<Lifestyle> lifestyleSvc) : base(categorySvc)
        {
            _lifestyleSvc = lifestyleSvc;
        }

        public static IList<JsonLifestyle> DefaultLifestyles()
        {
            var filePath = $"Live/Defaults/lifestyles.json";
            using (StreamReader srLifestyles = System.IO.File.OpenText(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), filePath))))
            {
                var dfLifestyles = (JsonConvert.DeserializeObject<List<JsonLifestyle>>(srLifestyles.ReadToEnd()) ?? new List<JsonLifestyle>());

                return dfLifestyles;
            }
        }

        [HttpPost]
        [Route("upsert")]
        public IList<JsonLifestyle> Upsert(List<JsonLifestyle> jsonLifestyles)
        {
            return _lifestyleSvc
                .Upsert(
                    jsonLifestyles
                        .Select(jsonLifestyle => new LifestyleMap(jsonLifestyle) as Lifestyle)
                        .ToList())
                .Select(lifestyle => new JsonLifestyle(lifestyle))
                .ToList();
        }

        [HttpPost]
        [Route("delete")]
        public void Delete(List<JsonLifestyle> jsonLifestyles)
        {
            _lifestyleSvc
                .Delete(
                    jsonLifestyles
                        .Select(jsonLifestyle => new LifestyleMap(jsonLifestyle) as Lifestyle)
                        .ToList());
        }
    }

}