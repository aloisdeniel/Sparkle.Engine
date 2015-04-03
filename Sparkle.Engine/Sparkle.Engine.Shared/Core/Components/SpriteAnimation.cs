namespace Sparkle.Engine.Core.Components
{
    using Microsoft.Xna.Framework;
    using Sparkle.Engine.Base.Animation;
    using Sparkle.Engine.Base.Geometry;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// A component that represents the current animation state of a sprite.
    /// </summary>
    public class SpriteAnimation : Component
    {
        public SpriteAnimation()
        {
            this.Animations = new Dictionary<string, List<Rectangle>>();
        }

        public string CurrentAnimation { get; set; }

        public List<Rectangle> Steps { 
            get
            {
                if(!this.Animations.ContainsKey(this.CurrentAnimation))
                {
                    return new List<Rectangle>();
                }

                return this.Animations[this.CurrentAnimation];
            }
        }

        /// <summary>
        /// All the animation steps.
        /// </summary>
        public Dictionary<string, List<Rectangle>> Animations { get; set; }

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
            var steps = this.Steps;

            if (steps.Count == 0)
                return new Rectangle();

            var index = (int)(CurrentTime / this.Interval);

            if (this.Mode == RepeatMode.Once)
                index = Math.Min(steps.Count - 1, index);
            else if (this.Mode == RepeatMode.Reverse)
                index = steps.Count - index - 1;
            else if (this.Mode == RepeatMode.Loop)
                index %= steps.Count;
            else if (this.Mode == RepeatMode.LoopWithReverse)
                index %= steps.Count * 2;

            if ((this.Mode == RepeatMode.OnceWithReverse || this.Mode == RepeatMode.LoopWithReverse) && index >= steps.Count)
            {
                index = steps.Count * 2 - index - 1;
            }
            
            return steps[index];
        }

        public void Stop()
        {
            this.currentTime = 0;
            this.IsStarted = false;
        }

        public void Play(string name, RepeatMode mode = RepeatMode.Once)
        {
            if (this.CurrentAnimation != name || !this.IsStarted)
            {
                this.CurrentAnimation = name;
                this.currentTime = 0;
                this.Mode = mode;
                this.IsStarted = true;
            }
        }

        public void Play(RepeatMode mode = RepeatMode.Once)
        {
            if (this.IsStarted)
                return;

            this.currentTime = 0;
            this.Mode = mode;
            this.IsStarted = true;
        }

        public void Add(string name, int width, int height, params Point[] steps)
        {
            var stepRects = new List<Rectangle>();

            foreach (var point in steps)
            {
                stepRects.Add(new Rectangle(point.X * width, point.Y * height, width, height));
            }

            this.Animations[name] = stepRects;

            if(String.IsNullOrEmpty(this.CurrentAnimation))
            {
                this.CurrentAnimation = name;
            }
        }
    }
}
