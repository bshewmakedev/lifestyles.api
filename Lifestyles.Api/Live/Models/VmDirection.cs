using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Api.Live.Models
{
    public static class VmDirection
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