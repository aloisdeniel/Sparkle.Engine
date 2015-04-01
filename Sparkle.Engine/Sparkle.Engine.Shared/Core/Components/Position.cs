using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Core.Components
{
    public class Position : Component
    {
        public Vector3 Value { get; set; }

        public Vector3 Velocity { get; set; }

        public Vector3 Acceleration { get; set; }
        
        public Vector3 GetAbsolute()
        {
            if (this.Entity.Parent == null)
            {
                return this.Value;
            }

            Vector3 result = Vector3.Zero;

            // 1. Parent's position

            var parentPosition = this.Entity.Parent.GetComponent<Position>();

            if (parentPosition != null)
            {
                var parentAbsolutePosition = parentPosition.GetAbsolute();

                result += parentAbsolutePosition;
            }

            // 2. Parent's rotation

            var parentRotation = this.Entity.Parent.GetComponent<Rotation>();

            if (parentRotation != null)
            {
                var parentAbsoluteRotation = parentRotation.GetAbsolute();

                var cos = Math.Cos(parentAbsoluteRotation);
                var sin = Math.Sin(parentAbsoluteRotation);

                result.X += (float)(this.Value.X * cos - this.Value.Y * sin);
                result.Y += (float)(this.Value.Y * sin + this.Value.X * cos);
                result.Z += this.Value.Z;
            }
            else
            {
                result += this.Value;
            }

            return result;
        }

    }
}
