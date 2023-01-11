using System;
using Lifestyles.Domain.Categorize.Entities;

namespace Lifestyles.Infrastructure.Session.Categorize.Models
{
    public class JsonCategory
    {
        public Guid? Id { get; set; }
        public string Alias { get; set; }
        public string Label { get; set; }

        public JsonCategory() { }

        public JsonCategory(ICategory category)
        {
            Id = category.Id;
            Alias = category.Alias;
            Label = category.Label;
        }
    }
}