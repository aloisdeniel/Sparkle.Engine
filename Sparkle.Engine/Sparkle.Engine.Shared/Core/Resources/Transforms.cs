using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace Sparkle.Engine.Core.Resources
{
    public class Transforms
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

        public class Animation
        {
            public Animation()
            {
                this.positions = new List<Keyframe<Vector3>>();
                this.scales = new List<Keyframe<Vector3>>();
                this.rotations = new List<Keyframe<float>>();
                this.colors = new List<Keyframe<Color>>();
            }

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
        }

        public Transforms()
        {
            this.Animations = new Dictionary<string, Animation>();
        }

        /// <summary>
        /// All the animation steps.
        /// </summary>
        public Dictionary<string, Animation> Animations { get; set; }

        public Animation Add(string name)
        {
            return this.Add(name,new Animation());
        }

        public Animation Add(string name, Animation animation)
        {
            this.Animations[name] = animation;
            return animation;
        }

    }
}
