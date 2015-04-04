namespace Sparkle.Engine.Core.Components
{
    using Microsoft.Xna.Framework;
    using Sparkle.Engine.Base.Animation;
    using Sparkle.Engine.Base.Geometry;
    using Sparkle.Engine.Core.Resources;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// A component that represents the current animation state of a sprite.
    /// </summary>
    public class SpriteAnimation : Component
    {
        public SpriteAnimation()
        {

        }

        public string CurrentAnimation { get; set; }
        
        private Spritesheet sheet;

        public Spritesheet Sheet
        {
            get { return sheet; }
            set { 
                sheet = value;
                if (this.CurrentAnimation == null && sheet.Animations.Keys.Count > 0)
                    this.CurrentAnimation = sheet.Animations.Keys.ElementAt(0);
            }
        }


        public List<Rectangle> Steps { 
            get
            {
                if(this.CurrentAnimation == null || !this.Sheet.Animations.ContainsKey(this.CurrentAnimation))
                {
                    return new List<Rectangle>();
                }

                return this.Sheet.Animations[this.CurrentAnimation];
            }
        }

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
    }
}
