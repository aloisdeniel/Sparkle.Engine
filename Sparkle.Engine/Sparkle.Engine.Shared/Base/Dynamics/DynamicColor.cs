using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Base.Dynamics
{
    public class DynamicColor : DynamicBase<Color>
    {
        public DynamicColor(Color value)
            : base(value)
        {

        }
        public DynamicColor()
            : this(Color.White)
        {

        }

        protected override Color AddValues(Color value, Color other)
        {
            return value.Add(other);
        }

        protected override Color SubstractValues(Color value, Color other)
        {
            return value.Substract(other);
        }

        protected override Color MultiplyValues(Color value, Color other)
        {
            return value.Multiply(other);
        }

        protected override Color MultiplyValues(Color value, double v)
        {
            return value.Multiply(v);
        }

        protected override Color GetZeroValue()
        {
            return Color.TransparentBlack;
        }
        
        protected override Color GetMinValue(Color value, Color other)
        {
            return value.Min(other);
        }

        protected override Color GetMaxValue(Color value, Color other)
        {
            return value.Max(other);
        }
    }
}
