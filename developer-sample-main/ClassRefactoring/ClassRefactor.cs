using System;
using System.Collections.Generic;

namespace DeveloperSample.ClassRefactoring
{
    public enum SwallowType
    {
        African, European
    }

    public enum SwallowLoad
    {
        None, Coconut
    }

    public class SwallowFactory
    {
        public Swallow GetSwallow(SwallowType swallowType) => new Swallow(swallowType);
    }

    public class Swallow
    {
        private static readonly Dictionary<(SwallowType, SwallowLoad), int> _airspeedVelocities =
            new()
            {
                {(SwallowType.African, SwallowLoad.None), 22},
                {(SwallowType.African, SwallowLoad.Coconut), 18},
                {(SwallowType.European, SwallowLoad.None), 20},
                {(SwallowType.European, SwallowLoad.Coconut), 16}
            };

        public SwallowType Type { get; }
        public SwallowLoad Load { get; private set; }

        public Swallow(SwallowType swallowType)
        {
            Type = swallowType;
        }

        public void ApplyLoad(SwallowLoad load)
        {
            Load = load;
        }

        public double GetAirspeedVelocity()
        {
            if (_airspeedVelocities.TryGetValue((Type, Load), out int velocity))
                return velocity;

            throw new InvalidOperationException();
        }
    }
}