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

        private double currentTime;

        /// <summary>
        /// Current time from begin.
        /// </summary>
        public double CurrentTime
        {
            get { return currentTime; }
            set 
            { 
                currentTime = value; 
                if(this.IsFinished())
                {
                    this.IsStarted = false;
                }
            }
        }

        /// <summary>
        /// The time interval between two frames.
        /// </summary>
        public double Interval { get; set; }
        
        private bool IsFinished()
        {
            var index = (int)(CurrentTime / this.Interval);

            return ((this.Mode == RepeatMode.Once && index >= this.Steps.Count) ||
                    (this.Mode == RepeatMode.Reverse && index <= 0) ||
                    (this.Mode == RepeatMode.OnceWithReverse && index >= 2 * this.Steps.Count));
        }

        public Rectangle GetSource()
        {
            if (this.Steps.Count > 0 && this.IsStarted)
            {
                var index = (int)(CurrentTime / this.Interval);

                if (this.Mode == RepeatMode.Once)
                    index = Math.Min(this.Steps.Count-1,index);
                else if (this.Mode == RepeatMode.Reverse)
                    index = this.Steps.Count - index - 1;
                else if (this.Mode == RepeatMode.Loop)
                    index %= this.Steps.Count;
                else if (this.Mode == RepeatMode.LoopWithReverse)
                    index %= this.Steps.Count * 2;

                if ((this.Mode == RepeatMode.OnceWithReverse || this.Mode == RepeatMode.LoopWithReverse) && index >= this.Steps.Count)
                {
                    index = this.Steps.Count * 2 - index - 1;
                }

                return this.Steps[index];
            }

            return new Rectangle();
        }
    }
}
