using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sparkle.Engine.Base.Geometry;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Core.Components
{
    public class Sprite : Component
    {
        /// <summary>
        /// Path to the texture.
        /// </summary>
        public String TextureName { get; set; }

        /// <summary>
        /// The texture object if loaded.
        /// </summary>
        public Texture2D Texture { get; set; }
        
        /// <summary>
        /// Sub area from the texture that will be rendered.
        /// </summary>
        public Rectangle SourceArea { get; set; }

        /// <summary>
        /// Sub area of the screen where the sprite is drawn.
        /// </summary>
        public Frame DestinationArea { get; set; }

    }
}
