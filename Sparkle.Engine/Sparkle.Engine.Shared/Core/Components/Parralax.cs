using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Core.Components
{
    public class Parralax : Component
    {
        public Parralax()
        {
            this.Power = Vector2.One;
            this.Scaling = 0;
        }

        public Vector2 Power { get; set; }

        public float Scaling { get; set; }
    }
}
