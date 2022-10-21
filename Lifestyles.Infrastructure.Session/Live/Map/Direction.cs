using DirectionEnum = Lifestyles.Domain.Live.Entities.Direction;

namespace Lifestyles.Infrastructure.Session.Live.Map
{
    public static class Direction
    {
        public static DirectionEnum Map(this int direction)
        {
            switch (direction)
            {
                case 1: return DirectionEnum.In;
                case -1: return DirectionEnum.Out;
                default: return DirectionEnum.Neutral;
            }
        }
    }
}