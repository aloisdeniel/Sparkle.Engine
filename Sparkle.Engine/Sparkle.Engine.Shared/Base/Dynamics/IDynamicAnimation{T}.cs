using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Base.Dynamics
{
    public interface IDynamicAnimation<T> : IUpdateable
    {
        T StartValue { get; }

        T EndValue { get; }

        T Value { get; }

        bool IsFinished { get; }

        bool IsStarted { get; }

        Curve.Mode Curve { get; }

        TimeSpan Duration { get; }

        RepeatMode Repeat { get; set; }

        void Start(RepeatMode repeat);

        void Stop();
    }
}
