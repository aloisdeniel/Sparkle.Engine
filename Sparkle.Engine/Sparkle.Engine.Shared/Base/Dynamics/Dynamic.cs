namespace Sparkle.Engine.Base.Dynamics
{
	using Microsoft.Xna.Framework;
	using System;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// A basic float dynamic value implementation.
	/// </summary>
	public class Dynamic : DynamicBase<float>
	{
        public Dynamic() : base(0.0f)
        {

        }
        public Dynamic(float value)
            : base(value)
        {

        }
        
        protected override float AddValues(float value, float other)
        {
            return value + other;
        }

        protected override float SubstractValues(float value, float other)
        {
            return value - other;
        }

        protected override float MultiplyValues(float value, float other)
        {
            return value * other;
        }

        protected override float MultiplyValues(float value, double v)
        {
            return (float)(value * v);
        }

        protected override float GetZeroValue()
        {
            return 0.0f;
        }

        protected override float GetMinValue(float value, float other)
        {
            return Math.Min(value, other);
        }

        protected override float GetMaxValue(float value, float other)
        {
            return Math.Max(value, other);
        }
    }
}