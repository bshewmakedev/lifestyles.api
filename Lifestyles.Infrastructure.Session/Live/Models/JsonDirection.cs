using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Infrastructure.Session.Live.Models
{
    public static class JsonDirection
    {
        public static int Map(this Direction direction)
        {
            switch (direction)
            {
                case Direction.In: return 1;
                case Direction.Out: return -1;
                default: return 0;
            }
        }
    }
}