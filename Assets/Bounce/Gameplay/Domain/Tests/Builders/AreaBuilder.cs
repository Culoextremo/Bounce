using Bounce.Gameplay.Domain.Runtime;
using JunityEngine.Maths.Runtime;

namespace Bounce.Gameplay.Domain.Tests.Builders
{
    public class AreaBuilder
    {
        Bounds2D bounds = Bounds2D.Infinite;
        int maxTrampolineLength = int.MaxValue;
        float minTrampolineLength = 0;
        float speedBoost;

        public static AreaBuilder Area() => new AreaBuilder();


        public AreaBuilder WithBounds(Bounds2D bounds)
        {
            this.bounds = bounds;
            return this;
        }

        public AreaBuilder WithMaxTrampolineLength(int maxTrampolineLength)
        {
            this.maxTrampolineLength = maxTrampolineLength;
            return this;
        }

        public AreaBuilder WithMinTrampolineLength(int minTrampolineLength)
        {
            this.minTrampolineLength = minTrampolineLength;
            return this;
        }

        public Area Build() => new(bounds, minTrampolineLength, maxTrampolineLength, speedBoost);

        public AreaBuilder WithSpeedBoost(float boost)
        {
            speedBoost = boost;
            return this;
        }
    }
}