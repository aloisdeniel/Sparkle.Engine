using Microsoft.Xna.Framework;
using Sparkle.Engine.Base.Animation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Base.Animation
{
    public static class Curve
    {
        /// <summary>
        /// Simple curve modes.
        /// </summary>
        public enum Mode
        {
            Linear,
            EaseIn,
            EaseOut,
            EaseInOut,
        }

        /// <summary>
        /// Calculate the current value for a time between zero and one, and a curve mode.
        /// </summary>
        /// <param name="mode">Curve mode.</param>
        /// <param name="time">Current time (1.0f represents the one way total duration).</param>
        public static float Calculate(Mode mode, float time)
        {
            time = MathHelper.Clamp(time, 0.0f, 1.0f);

            var easeIn = (mode == Mode.EaseIn || mode == Mode.EaseInOut) && time <= 0.5f;
            var easeOut = (mode == Mode.EaseOut || mode == Mode.EaseInOut) && time > 0.5f;

            if (easeIn)
            {
                return (time * time * 2);
            }
            else if (easeOut)
            {
                var t = (1.0f - time);
                return 1.0f - (t * t * 2);
            }

            return time;
        }

        public static Vector4 Calculate(Mode mode, float time, Vector4 start, Vector4 end)
        {
            time = Curve.Calculate(mode, time);

            end -= start;
            end *= time;
            end += start;

            return end;
        }

        public static Vector3 Calculate(Mode mode, float time, Vector3 start, Vector3 end)
        {
            time = Curve.Calculate(mode, time);

            end -= start;
            end *= time;
            end += start;

            return end;
        }

        public static Vector2 Calculate(Mode mode, float time, Vector2 start, Vector2 end)
        {
            time = Curve.Calculate(mode, time);

            end -= start;
            end *= time;
            end += start;

            return end;
        }

        public static float Calculate(Mode mode, float time, float start, float end)
        {
            time = Curve.Calculate(mode, time);

            end -= start;
            end *= time;
            end += start;

            return end;
        }

        public static Color Calculate(Mode mode, float time, Color start, Color end)
        {
            var startVector = start.ToVector4();
            var endVector = end.ToVector4();

            return new Color(Curve.Calculate(mode,time,startVector,endVector));
        }
    }
}
