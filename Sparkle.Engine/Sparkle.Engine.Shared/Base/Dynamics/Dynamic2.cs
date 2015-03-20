namespace Sparkle.Engine.Base.Dynamics
{
	using System;
	using Sparkle.Engine.Core;
	using Microsoft.Xna.Framework;

	/// <summary>
	/// A basic vector with two dimensions dynamic value implementation.
	/// </summary>
    public class Dynamic2 : DynamicBase<Vector2>
    {
        public Dynamic2(Vector2 value)
            : base(value)
        {

        }

        public Dynamic2() : this(Vector2.Zero)
        {

        }

        public Dynamic2 (float x, float y) : this (new Vector2 (x, y))
        {

        }
        
        protected override Vector2 AddValues(Vector2 value, Vector2 other)
        {
            return value + other;
        }

        protected override Vector2 SubstractValues(Vector2 value, Vector2 other)
        {
            return value - other;
        }

        protected override Vector2 MultiplyValues(Vector2 value, Vector2 other)
        {
            return value * other;
        }

        protected override Vector2 MultiplyValues(Vector2 value, double v)
        {
            return value * (float)v;
        }

        protected override Vector2 GetZeroValue()
        {
            return Vector2.Zero;
        }

        protected override Vector2 GetMinValue(Vector2 value, Vector2 other)
        {
            return Vector2.Min(value, other);
        }

        protected override Vector2 GetMaxValue(Vector2 value, Vector2 other)
        {
            return Vector2.Max(value, other);
        }
    }
}

