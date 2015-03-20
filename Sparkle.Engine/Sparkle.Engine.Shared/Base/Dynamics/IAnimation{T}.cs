namespace Sparkle.Engine.Base.Dynamics
{
    using Microsoft.Xna.Framework;
    using System;

    public interface IAnimation<T> : IUpdateable
    {
        RepeatMode Mode { get; }

        TimeSpan Duration { get; }

        bool IsFinished { get; }

        Curve Curve { get; }

        void Start();

        void Pause();

        void Stop();
    }
}
