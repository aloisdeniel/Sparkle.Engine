namespace Sparkle.Engine
{
	using System;
	using Microsoft.Xna.Framework;

	public static class MathExtensions
	{
		public static float Quadratic (this float t, float p0, float p1, float p2)
		{
			t = t.Clamp (0.0f, 1.0f);
			var r = (1 - t);
			return r * r * p0 + 2 * t * r * p1 + t * t * p2;
		}

		public static long Clamp (this long value, long min, long max)
		{
			return Math.Min (max, Math.Max (min, value));
		}

		public static int Clamp (this int value, int min, int max)
		{
			return Math.Min (max, Math.Max (min, value));
		}

		public static double Clamp (this double value, double min, double max)
		{
			return Math.Min (max, Math.Max (min, value));
		}

		public static float Clamp (this float value, float min, float max)
		{
			return Math.Min (max, Math.Max (min, value));
		}

		public static Vector2 ToVector2 (this Vector3 v)
		{
			return new Vector2 (v.X, v.Y);
		}

		public static Vector2 Clamp (this Vector2 value, Vector2 min, Vector2 max)
		{
			return new Vector2 (value.X.Clamp (min.X, max.X), value.Y.Clamp (min.Y, max.Y));
		}

		public static Vector3 Clamp (this Vector3 value, Vector3 min, Vector3 max)
		{
			return new Vector3 (value.X.Clamp (min.X, max.X), value.Y.Clamp (min.Y, max.Y), value.Z.Clamp (min.Z, max.Z));
		}

		public static Vector4 Clamp (this Vector4 value, Vector4 min, Vector4 max)
		{
			return new Vector4 (value.X.Clamp (min.X, max.X), value.Y.Clamp (min.Y, max.Y), value.Z.Clamp (min.Z, max.Z), value.W.Clamp (min.W, max.W));
		}

		public static Vector2 Rotate (Vector2 v, double angle)
		{
			return new Vector2 ((float)(Math.Cos (angle) * v.X - Math.Sin (angle) * v.Y), (float)(Math.Sin (angle) * v.X + Math.Cos (angle) * v.Y));
		}

	}
}

