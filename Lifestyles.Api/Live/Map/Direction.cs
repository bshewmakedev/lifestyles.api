using DirectionEnum = Lifestyles.Domain.Live.Entities.Direction;

namespace Lifestyles.Api.Live.Map
{
    public static class Direction
    {
        public static DirectionEnum Map(this decimal amount)
        {
            if (amount > 0)
            {
                return DirectionEnum.In;
            }

            if (amount < 0)
            {
                return DirectionEnum.Out;
            }

            return DirectionEnum.Neutral;
        }
    }
}