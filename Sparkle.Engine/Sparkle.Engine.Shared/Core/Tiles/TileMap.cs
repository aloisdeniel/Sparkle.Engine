using Microsoft.Xna.Framework;
using Sparkle.Engine.Base;
using Sparkle.Engine.Base.Shapes;
using System;
using System.Collections.Generic;

namespace Sparkle.Engine.Core.Tiles
{
    public class TileMap : Sparkle.Engine.Base.IDrawable, ILoadable, IQuadStorable
	{
		public TileMap (World parent, Frame bounds, string texture, int layers, int tileWidth, int tileHeight)
		{
            this.World = parent;
            this.Layers = new TileLayer[Math.Max(1,layers)];
            this.TileSize = new Size(tileWidth, tileHeight);
            for (int i = 0; i < this.Layers.Length; i++)
            {
                this.Layers[i] = new TileLayer(this, bounds, texture);
            }
		}

        public TileLayer[] Layers { get; private set; }

        public World World { get; set; }

        public Size TileSize { get; set; }

        public bool IsVisible
        {
            get;
            set;
        }

        public int DrawOrder
        {
            get;
            set;
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            if(this.IsVisible)
            {
                foreach (var layer in this.Layers)
                {
                    layer.Draw(sb);
                }
            }
        }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            foreach (var layer in this.Layers)
            {
                layer.LoadContent(content);
            }
        }

        public void UnloadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {

        }


        
        public Frame Bounds
        {
            get { return this.Layers[0].Bounds; }
        }
    }
}

