using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Core.Components
{
    public class Rotation : Component
    {
        public float Value { get; set; }

        public float Velocity { get; set; }

        public float Acceleration { get; set; }

        public float GetAbsolute()
        {
            if (this.Entity.Parent == null)
            {
                return this.Value;
            }

            float result = this.Value;

            // 1. Parent's rotation

            var parentRotation = this.Entity.Parent.GetComponent<Rotation>();

            if (parentRotation != null)
            {
                var parentAbsoluteRotation = parentRotation.GetAbsolute();
                result += parentAbsoluteRotation;
            }

            return result;
        }
    }
}
