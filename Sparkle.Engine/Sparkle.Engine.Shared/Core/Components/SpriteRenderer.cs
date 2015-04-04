using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sparkle.Engine.Base;
using Sparkle.Engine.Base.Geometry;
using Sparkle.Engine.Core.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Core.Components
{
    public class SpriteRenderer : Component, IQuadStorable
    {
        private float? height;

        private float? width;

        private Vector2? center;

        public Rectangle? sourceArea;

        /// <summary>
        /// The drawing order of the sprite.
        /// </summary>
        public float Order { get; set; }

        /// <summary>
        /// Bounding box of the instance. It can be used to optimize drawn areas.
        /// </summary>
        public Frame Bounds
        {
            get
            {
                return this.DestinationArea;
            }
        }

        /// <summary>
        /// Rendering color of the sprite.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// The rendered sprite.
        /// </summary>
        public Sprite Sprite { get; set; }
                
        /// <summary>
        /// Sub area of the screen where the sprite is drawn.
        /// </summary>
        public Frame DestinationArea { get; set; }

        /// <summary>
        /// Rotation value on screen of the sprite.
        /// </summary>
        public float Angle { get; set; }

        /// <summary>
        /// Sub area from the texture that will be rendered.
        /// </summary>
        public Rectangle SourceArea
        {
            get
            {
                if (sourceArea == null)
                    this.InitializeSourceArea();

                return (Rectangle)sourceArea;
            }
            set { sourceArea = value; }
        }

        /// <summary>
        /// Rendering base width of the sprite.
        /// </summary>
        public Vector2 Center
        {
            get
            {
                if (center == null)
                    this.InitializeCenter();

                return (Vector2)center;
            }
            set { center = value; }
        }

        /// <summary>
        /// Rendering base width of the sprite.
        /// </summary>
        public float Width
        {
            get
            {
                if (width == null)
                    this.InitializeSize();

                return (float)width;
            }
            set { width = value; }
        }

        /// <summary>
        /// Rendering base height of the sprite.
        /// </summary>
        public float Height
        {
            get
            {
                if (height == null)
                    this.InitializeSize();

                return (float)height;
            }
            set { height = value; }
        }

        /// <summary>
        /// Auto-initializes size from texture size.
        /// </summary>
        private void InitializeSize()
        {
            if (this.Sprite != null && this.Sprite.Texture != null)
            {
                this.Width = this.Sprite.Texture.Width;
                this.Height = this.Sprite.Texture.Height;
            }
            else
            {
                this.Width = 0;
                this.Height = 0;
            }
        }

        /// <summary>
        /// Auto-initializes center from size.
        /// </summary>
        private void InitializeCenter()
        {
            this.Center = new Vector2(this.Width / 2, this.Height / 2);
        }

        /// <summary>
        /// Auto-initializes source from texture size.
        /// </summary>
        private void InitializeSourceArea()
        {
            if (this.Sprite != null && this.Sprite.Texture != null)
            {
                this.SourceArea = new Rectangle(0, 0, this.Sprite.Texture.Width, this.Sprite.Texture.Height);
            }
            else
            {
                this.SourceArea = new Rectangle();
            }
        }
    }
}
