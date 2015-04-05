namespace Sparkle.Engine.Core.Components
{
    using Microsoft.Xna.Framework;
    using Sparkle.Engine.Base.Animation;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Sparkle.Engine.Core.Resources;

    public class TransformAnimation : Component
    {

        /// <summary>
        /// The repeat mode for the animation.
        /// </summary>
        public Repeat.Mode Repeat { get; set; }

        /// <summary>
        /// Indicates whether the animation is running.
        /// </summary>
        public bool IsStarted { get; set; }

        public TimeSpan CurrentTime { get; set; }

        public string CurrentAnimation { get; set; }

        private Transforms sheet;

        public Transforms Sheet
        {
            get { return sheet; }
            set
            {
                sheet = value;
                if (this.CurrentAnimation == null && sheet.Animations.Keys.Count > 0)
                    this.CurrentAnimation = sheet.Animations.Keys.ElementAt(0);
            }
        }


        public Sparkle.Engine.Core.Resources.Transforms.Animation Animation
        {
            get
            {
                if (this.CurrentAnimation == null || !this.Sheet.Animations.ContainsKey(this.CurrentAnimation))
                {
                    return null;
                }

                return this.Sheet.Animations[this.CurrentAnimation];
            }
        }

        /// <summary>
        /// The last keyframe time value.
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                var max = TimeSpan.Zero;

                var animation = this.Animation;

                if (animation == null)
                    return max;

                if (animation.Positions.Count > 0)
                {
                    var kf = animation.Positions.Last();

                    max = kf == null || kf.Time < max ? max : kf.Time;
                }

                if (animation.Scales.Count > 0)
                {
                    var kf = animation.Scales.Last();

                    max = kf == null || kf.Time < max ? max : kf.Time;
                }

                if (animation.Colors.Count > 0)
                {
                    var kfc = animation.Colors.Last();

                    max = kfc == null || kfc.Time < max ? max : kfc.Time;
                }

                if (animation.Rotations.Count > 0)
                {
                    var kfr = animation.Rotations.Last();

                    max = kfr == null || kfr.Time < max ? max : kfr.Time;
                }
                return max;
            }
        }

        public void Stop()
        {
            this.CurrentTime = TimeSpan.Zero;
            this.IsStarted = false;
        }

        public void Play(string name, Repeat.Mode mode = Base.Animation.Repeat.Mode.Once)
        {
            if (this.CurrentAnimation != name || !this.IsStarted)
            {
                this.CurrentAnimation = name;
                this.CurrentTime = TimeSpan.Zero;
                this.Repeat = mode;
                this.IsStarted = true;
            }
        }

        public void Play(Repeat.Mode mode = Base.Animation.Repeat.Mode.Once)
        {
            if (this.IsStarted)
                return;

            this.CurrentTime = TimeSpan.Zero;
            this.Repeat = mode;
            this.IsStarted = true;
        }
    }
}
