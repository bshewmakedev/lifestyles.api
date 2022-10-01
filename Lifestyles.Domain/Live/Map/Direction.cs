using Lifestyles.Domain.Live.Constants;
using DirectionEnum = Lifestyles.Domain.Live.Constants.Direction;

namespace Lifestyles.Domain.Live.Map
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