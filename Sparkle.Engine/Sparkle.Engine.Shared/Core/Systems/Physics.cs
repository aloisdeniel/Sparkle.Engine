using Sparkle.Engine.Core.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework;
using Sparkle.Engine.Core.Resources;

namespace Sparkle.Engine.Core.Systems
{
    public class Physics : System, Base.IUpdateable
    {
        public Physics(SparkleGame game)
            : base(game)
        {

        }

        public void Update(Microsoft.Xna.Framework.GameTime time)
        {
            // 1. Physics

            var components = this.Game.Scene.GetComponents<Body>();

            var dt = (time.ElapsedGameTime.Milliseconds / 2000.0f);

            foreach (var body in components)
            {
                body.Inertia += dt * body.Torques / body.Mass;
                body.Rotation += dt * body.Inertia;

                body.Velocity += dt * body.Forces / body.Mass;
                body.Position += dt * body.Velocity;
            }

            // 2. Transform animations

            var animations = this.Game.Scene.GetComponents<TransformAnimation>();

            foreach (var animation in animations)
            {
                if(animation.IsStarted)
                {
                    var transform = animation.Owner.GetComponent<Transform>();

                    animation.CurrentTime += time.ElapsedGameTime;

                    if(transform != null)
                    {
                        this.updateTransform(transform, animation);
                    }
                }
            }
        }

        #region Calculating current transform from animation

        private void updateTransform(Transform transform, TransformAnimation animation)
        {
            var duration = animation.Duration;
            var progress = (float)(animation.CurrentTime.TotalMilliseconds / duration.TotalMilliseconds);

            animation.IsStarted = !Base.Animation.Repeat.IsFinished(animation.Repeat, progress);

            progress = Base.Animation.Repeat.Calculate(animation.Repeat, progress);
            var animationTime = progress * duration.TotalMilliseconds;


            var current = animation.Animation;

            // Calculating current values
            if(current != null)
            {
                this.updateValue(animationTime, current.Positions, (v) => { if (transform.Parent != null) transform.LocalPosition = v; else transform.Position = v; });
                this.updateValue(animationTime, current.Scales, (v) => { if (transform.Parent != null) transform.LocalScale = v; else transform.Scale = v; });
                this.updateValue(animationTime, current.Colors, (v) => { if (transform.Parent != null) transform.LocalColor = v; else transform.Color = v; });
                this.updateValue(animationTime, current.Rotations, (v) => { if (transform.Parent != null) transform.LocalRotation = v; else transform.Rotation = v; });
            }
        }

        private bool updateValue(double time,  IList<Transforms.Keyframe<Vector3>> keyframes, Action<Vector3> setter)
        {
            if (keyframes.Count == 0)
                return false;

            var interval = this.getKeyframes(time, keyframes);

            if (interval.Item1 != null && interval.Item2 != null)
            {
                var progress = (time - interval.Item1.Time.TotalMilliseconds) / (interval.Item2.Time.TotalMilliseconds - interval.Item1.Time.TotalMilliseconds);

                setter(Base.Animation.Curve.Calculate(interval.Item2.Curve, (float)progress, interval.Item1.Value, interval.Item2.Value));
                return true;
            }
            else if (interval.Item1 != null)
            {
                setter(interval.Item1.Value);
                return true;
            }

            return false;
        }

        private bool updateValue(double time, IList<Transforms.Keyframe<float>> keyframes, Action<float> setter)
        {
            if (keyframes.Count == 0)
                return false;

            var interval = this.getKeyframes(time, keyframes);

            if (interval.Item1 != null && interval.Item2 != null)
            {
                var progress = (time - interval.Item1.Time.TotalMilliseconds) / (interval.Item2.Time.TotalMilliseconds - interval.Item1.Time.TotalMilliseconds);

                setter(Base.Animation.Curve.Calculate(interval.Item2.Curve, (float)progress, interval.Item1.Value, interval.Item2.Value));
                return true;
            }
            else if (interval.Item1 != null)
            {
                setter(interval.Item1.Value);
                return true;
            }

            return false;
        }

        private bool updateValue(double time, IList<Transforms.Keyframe<Color>> keyframes, Action<Color> setter)
        {
            if (keyframes.Count == 0)
                return false;

            var interval = this.getKeyframes(time, keyframes);

            if (interval.Item1 != null && interval.Item2 != null)
            {
                var progress = (time - interval.Item1.Time.TotalMilliseconds) / (interval.Item2.Time.TotalMilliseconds - interval.Item1.Time.TotalMilliseconds);

                setter(Base.Animation.Curve.Calculate(interval.Item2.Curve, (float)progress, interval.Item1.Value, interval.Item2.Value));
                return true;
            }
            else if (interval.Item1 != null)
            {
                setter(interval.Item1.Value);
                return true;
            }

            return false;
        }

        private Tuple<Transforms.Keyframe<T>, Transforms.Keyframe<T>> getKeyframes<T>(double time, IList<Transforms.Keyframe<T>> keyframes)
        {
            Transforms.Keyframe<T> positionStart = null, positionEnd = null;
            positionStart = keyframes.LastOrDefault((k) => k.Time.TotalMilliseconds <= time);
            if (positionStart != null)
            {
                var positionStartIndex = keyframes.IndexOf(positionStart);

                if (positionStartIndex < keyframes.Count - 1)
                {
                    positionEnd = keyframes.ElementAt(positionStartIndex + 1);
                }
            }

            return new Tuple<Transforms.Keyframe<T>, Transforms.Keyframe<T>>(positionStart, positionEnd);
        }

        #endregion

    }
}
