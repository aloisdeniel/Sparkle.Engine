namespace Sparkle.Engine.Core.Components
{
    using Microsoft.Xna.Framework;
    using Sparkle.Engine.Base.Animation;
    using Sparkle.Engine.Base.Geometry;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A component that represents the current animation state of a sprite.
    /// </summary>
    public class SpriteAnimation : Component
    {
        /// <summary>
        /// All the animation steps.
        /// </summary>
        public List<Rectangle> Steps { get; set; }

        /// <summary>
        /// The repeat mode for the animation.
        /// </summary>
        public RepeatMode Mode { get; set; }

        /// <summary>
        /// Indicates whether the animation is running.
        /// </summary>
        public bool IsStarted { get; private set; }

        /// <summary>
        /// Current time from begin.
        /// </summary>
        public double CurrentTime { get; set; }

        /// <summary>
        /// The time interval between two frames.
        /// </summary>
        public TimeSpan Interval { get; set; }
    }
}
