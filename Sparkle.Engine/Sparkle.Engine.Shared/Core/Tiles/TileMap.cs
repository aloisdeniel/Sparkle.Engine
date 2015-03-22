using Microsoft.Xna.Framework;
using Sparkle.Engine.Base;
using Sparkle.Engine.Base.Shapes;
using System;
using System.Collections.Generic;

namespace Sparkle.Engine.Core.Tiles
{
    public class TileMap : Sparkle.Engine.Base.IDrawable, ILoadable, IQuadStorable
	{
		public TileMap (World parent, Frame bounds)
        {
            this.IsVisible = true;
            this.World = parent;
            this.Layers = new List<TileLayer>();
		}

        public List<TileLayer> Layers { get; private set; }

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

        public TileLayer CreateLayer(TileSheet sheet)
        {
            var layer = new TileLayer(this, this.Bounds, sheet);
            this.Layers.Add(layer);
            return layer;
        }

        public TileLayer GetLayer(int index)
        {
            return this.Layers[index];
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
            get { return this.Layers.Count == 0 ? new Frame() : this.Layers[0].Bounds; } // TODO : get max of all layers
        }
    }
}

