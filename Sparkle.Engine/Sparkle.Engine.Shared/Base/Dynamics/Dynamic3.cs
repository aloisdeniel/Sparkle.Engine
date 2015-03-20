namespace Sparkle.Engine.Base.Dynamics
{
	using Microsoft.Xna.Framework;
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Diagnostics;

	/// <summary>
	/// A basic vector with three dimensions dynamic value implementation.
	/// </summary>
    public class Dynamic3 : DynamicBase<Vector3>
    {
        public Dynamic3(Vector3 value)
            : base(value)
        {

        }
        public Dynamic3() : this(Vector3.Zero)
        {

        }

        public Dynamic3(float x, float y)
            : this(x, y, 0)
        {

        }

        public Dynamic3(float x, float y, float z)
            : this(new Vector3(x, y, z))
        {

        }
        
        protected override Vector3 AddValues(Vector3 value, Vector3 other)
        {
            return value + other;
        }

        protected override Vector3 SubstractValues(Vector3 value, Vector3 other)
        {
            return value - other;
        }

        protected override Vector3 MultiplyValues(Vector3 value, Vector3 other)
        {
            return value * other;
        }

        protected override Vector3 MultiplyValues(Vector3 value, double v)
        {
            return value * (float)v;
        }

        protected override Vector3 GetZeroValue()
        {
            return Vector3.Zero;
        }

        protected override Vector3 GetMinValue(Vector3 value, Vector3 other)
        {
            return Vector3.Min(value,other);
        }

        protected override Vector3 GetMaxValue(Vector3 value, Vector3 other)
        {
            return Vector3.Max(value, other);
        }
    }
}
