using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Core.Components
{
    public class Color : Component
    {
        public Microsoft.Xna.Framework.Color Value { get; set; }

        public Microsoft.Xna.Framework.Color Velocity { get; set; }

        public Microsoft.Xna.Framework.Color Acceleration { get; set; }

        public Microsoft.Xna.Framework.Color GetAbsolute()
        {
            if (this.Entity.Parent == null)
            {
                return this.Value;
            }

            var result = this.Value;

            // 1. Parent's color

            var parentColor = this.Entity.Parent.GetComponent<Color>();

            if (parentColor != null)
            {
                var parentAbsoluteColor = parentColor.GetAbsolute();

                result.R = (byte)(((int)result.R * (int)parentAbsoluteColor.R) / 255);
                result.G = (byte)(((int)result.G * (int)parentAbsoluteColor.G) / 255);
                result.B = (byte)(((int)result.B * (int)parentAbsoluteColor.B) / 255);
                result.A = (byte)(((int)result.A * (int)parentAbsoluteColor.A) / 255);
            }

            return result;
        }
    }
}
