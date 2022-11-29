using NodeTypeEnum = Lifestyles.Domain.Tree.Entities.NodeType;

namespace Lifestyles.Domain.Tree.Map
{
    public static class NodeType
    {
        public const string Budget = "budget";
        public const string Category = "category";
        public const string Lifestyle = "lifestyle";

        public static NodeTypeEnum Map(this string nodeType)
        {
            switch (nodeType)
            {
                case Budget: return NodeTypeEnum.Budget;
                case Category: return NodeTypeEnum.Category;
                default: return NodeTypeEnum.Lifestyle;
            }
        }

        public static string Map(this NodeTypeEnum nodeType)
        {
            switch (nodeType)
            {
                case NodeTypeEnum.Budget: return Budget;
                case NodeTypeEnum.Category: return Category;
                default: return Lifestyle;
            }
        }
    }
}