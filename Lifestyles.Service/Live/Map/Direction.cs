using DirectionEnum = Lifestyles.Domain.Live.Entities.Direction;

namespace Lifestyles.Service.Live.Map
{
    public static class Direction
    {
        public static DirectionEnum Map(this int direction)
        {
            return (DirectionEnum)direction;
        }

        public static int Map(this DirectionEnum direction)
        {
            return (int)direction;
        }
    }
}