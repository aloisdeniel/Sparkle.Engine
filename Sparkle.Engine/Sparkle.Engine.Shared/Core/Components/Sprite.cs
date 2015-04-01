using Microsoft.Xna.Framework;
using Sparkle.Engine.Base.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Core.Components
{
    public class Sprite : Component
    {
        public Frame Bounds { get; set; }

        public String Texture { get; set; }

        public Vector2 Center { get; set; }

        public Rectangle SourceArea { get; set; }

        public Frame DestinationArea { get; set; }
    }
}
