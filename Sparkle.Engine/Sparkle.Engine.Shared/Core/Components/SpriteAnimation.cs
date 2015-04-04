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
        public Repeat.Mode Mode { get; set; }

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
            var time = (float)(CurrentTime / this.Interval);
            return Repeat.IsFinished(this.Mode,time);
        }

        public Rectangle GetSource()
        {
            var steps = this.Steps;

            if (steps.Count == 0)
                return new Rectangle();

            var time = (float)(CurrentTime / (this.Interval * steps.Count));
            time = Repeat.Calculate(this.Mode, time);

            var index = (int)(time * steps.Count);
                        
            return steps[index];
        }

        public void Stop()
        {
            this.currentTime = 0;
            this.IsStarted = false;
        }

        public void Play(string name, Repeat.Mode mode = Repeat.Mode.Once)
        {
            if (this.CurrentAnimation != name || !this.IsStarted)
            {
                this.CurrentAnimation = name;
                this.currentTime = 0;
                this.Mode = mode;
                this.IsStarted = true;
            }
        }

        public void Play(Repeat.Mode mode = Repeat.Mode.Once)
        {
            if (this.IsStarted)
                return;

            this.currentTime = 0;
            this.Mode = mode;
            this.IsStarted = true;
        }
    }
}
