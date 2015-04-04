namespace Sparkle.Engine.Core.Components
{
    using Microsoft.Xna.Framework;
    using Sparkle.Engine.Base.Animation;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class TransformAnimation : Component
    {
        public class Keyframe<T>
        {
            public Keyframe(TimeSpan time, T value, Base.Animation.Curve.Mode valueMode = Base.Animation.Curve.Mode.Linear)
            {
                this.Time = time;
                this.Curve = valueMode;
                this.Value = value;
            }

            public TimeSpan Time { get; set; }

            public Base.Animation.Curve.Mode Curve { get; set; }

            public T Value { get; set; }
        }

        public TransformAnimation()
        {
            this.CurrentTime = TimeSpan.Zero;
            this.positions = new List<Keyframe<Vector3>>();
            this.scales = new List<Keyframe<Vector3>>();
            this.rotations = new List<Keyframe<float>>();
            this.colors = new List<Keyframe<Color>>();
        }

        /// <summary>
        /// The repeat mode for the animation.
        /// </summary>
        public Repeat.Mode Repeat { get; set; }
        
        /// <summary>
        /// Indicates whether the animation is running.
        /// </summary>
        public bool IsStarted { get; set; }

        public TimeSpan CurrentTime { get; set; }

        private List<Keyframe<Vector3>> positions;

        private List<Keyframe<Vector3>> scales;

        public List<Keyframe<float>> rotations;

        public List<Keyframe<Color>> colors;

        /// <summary>
        /// All position keyframes ordered by Time.
        /// </summary>
        public IList<Keyframe<Vector3>> Positions
        {
            get { return new ReadOnlyCollection<Keyframe<Vector3>>(positions); }
        }

        /// <summary>
        /// All scale keyframes ordered by Time.
        /// </summary>
        public IList<Keyframe<Vector3>> Scales
        {
            get { return new ReadOnlyCollection<Keyframe<Vector3>>(scales); }
        }

        /// <summary>
        /// All rotation keyframes ordered by Time.
        /// </summary>
        public IList<Keyframe<float>> Rotations
        {
            get { return new ReadOnlyCollection<Keyframe<float>>(rotations); }
        }

        /// <summary>
        /// All color keyframes ordered by Time.
        /// </summary>
        public IList<Keyframe<Color>> Colors
        {
            get { return new ReadOnlyCollection<Keyframe<Color>>(colors); }
        }

        #region Adding keyframes

        public void AddPosition(TimeSpan time, Vector3 value, Base.Animation.Curve.Mode valueMode = Base.Animation.Curve.Mode.Linear)
        {
            this.addKeyframe(ref this.positions,new Keyframe<Vector3>(time,value,valueMode));
        }

        public void AddScale(TimeSpan time, Vector3 value, Base.Animation.Curve.Mode valueMode = Base.Animation.Curve.Mode.Linear)
        {
            this.addKeyframe(ref this.scales, new Keyframe<Vector3>(time, value, valueMode));
        }

        public void AddRotation(TimeSpan time, float value, Base.Animation.Curve.Mode valueMode = Base.Animation.Curve.Mode.Linear)
        {
            this.addKeyframe(ref this.rotations, new Keyframe<float>(time, value, valueMode));
        }

        public void AddColor(TimeSpan time, Color value, Base.Animation.Curve.Mode valueMode = Base.Animation.Curve.Mode.Linear)
        {
            this.addKeyframe(ref this.colors, new Keyframe<Color>(time, value, valueMode));
        }

        private void addKeyframe<T>(ref List<Keyframe<T>> list, Keyframe<T> keyframe)
        {
            list.Add(keyframe);
            list = list.OrderBy(item => item.Time).ToList();
        }

        #endregion



        /// <summary>
        /// The last keyframe time value.
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                var max = TimeSpan.Zero;
                
                if(this.positions.Count > 0)
                {
                    var kf = this.positions.Last();

                    max = kf == null || kf.Time < max ? max : kf.Time;
                }

                if (this.scales.Count > 0)
                {
                    var kf = this.scales.Last();

                    max = kf == null || kf.Time < max ? max : kf.Time;
                }

                if (this.colors.Count > 0)
                {
                    var kfc = this.colors.Last();

                    max = kfc == null || kfc.Time < max ? max : kfc.Time;
                }

                if (this.rotations.Count > 0)
                {
                    var kfr = this.rotations.Last();

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

        public void Play(Repeat.Mode mode = Base.Animation.Repeat.Mode.Once)
        {
            this.CurrentTime = TimeSpan.Zero;
            this.Repeat = mode;
            this.IsStarted = true;
        }
    }
}
