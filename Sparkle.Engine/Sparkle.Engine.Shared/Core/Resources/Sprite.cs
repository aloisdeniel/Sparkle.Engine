using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Core.Resources
{
    public class Sprite : Resource, Base.ILoadable
    {
        public Sprite(String texture)
        {
            this.TextureName = texture;
        }

        /// <summary>
        /// Path to the texture.
        /// </summary>
        public String TextureName { get; set; }

        /// <summary>
        /// The texture object if loaded.
        /// </summary>
        public Texture2D Texture { get; set; }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            this.Texture = content.Load<Texture2D>(this.TextureName);
        }

    }
}
