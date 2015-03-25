namespace Sparkle.Engine.Base
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// Helping functions for calculating basic curves values.
	/// </summary>
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
		/// <param name="time">Current time (from 0.0f to 1.0f).</param>
		public static float Calculate (Mode mode, float time)
		{
			var easeIn = (mode == Mode.EaseIn || mode == Mode.EaseInOut) && time <= 0.5f;
			var easeOut = (mode == Mode.EaseOut || mode == Mode.EaseInOut) && time > 0.5f;

			if (easeIn) {
                return (time * time * 2);
			}
            else if (easeOut)
            {
                var t = (1.0f - time);
                return 1.0f - (t * t * 2);
            }

			return time.Clamp (0.0f, 1.0f);
		}
	}
}
