using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sparkle.Engine.Base;
using Sparkle.Engine.Base.Shapes;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Sparkle.Engine.Core.Tiles
{
    public class TileLayer : Sparkle.Engine.Base.IDrawable, ILoadable, IQuadStorable
	{
		public TileLayer (TileMap parent, Frame bounds,string texture)
		{
            this.Tiles = new QuadTree<Tile>(bounds);
            this.tileSources = new Dictionary<string, Rectangle>();
            this.TextureName = texture;
            this.Parent = parent;
		}

        public TileMap Parent { get; private set; }

        public QuadTree<Tile> Tiles { get; set; }

        #region Texture

        /// <summary>
        /// The name of the texture.
        /// </summary>
        public String TextureName { get; private set; }

        /// <summary>
        /// Texture of the sprite.
        /// </summary>
        public Texture2D Texture { get; private set; }

        #endregion

        #region Tile creation

        private Dictionary<string, Rectangle> tileSources;

        /// <summary>
        /// Registers a tile sources coordinates with an identier.
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void RegisterTile(string identifier, int x, int y, int width = 1, int height = 1)
        {
            this.tileSources[identifier] = new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// Create the tile with the identifier at the coordinates.
        /// </summary>
        /// <param name="indentifier"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void CreateTile(string indentifier, int x, int y, int width = 1, int height = 1)
        {
            this.CreateTile(this.tileSources[indentifier], x, y,width, height);
        }

        /// <summary>
        /// Create the tile with the source area at the coordinates.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void CreateTile(Rectangle source, int x, int y, int width = 1, int height = 1)
        {
            var tile = new Tile(
                this,
                (int)(source.X * this.Parent.TileSize.Width),
                (int)(source.Y * this.Parent.TileSize.Height),
                (int)(source.Width * this.Parent.TileSize.Width),
                (int)(source.Height * this.Parent.TileSize.Height));

            tile.DestinationCoordinates = new Rectangle(x, y, width, height);

            this.Tiles.Add(tile);
        }

        #endregion

        #region Tile requests

        /// <summary>
        /// Finds all the tiles that intersects the given frame.
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        public List<Tile> FindTiles(Frame frame)
        {
            return this.Tiles.GetObjects(frame);
        }

        /// <summary>
        /// Finds the tile that contains the given point.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Tile FindTile(Vector2 point)
        {
            return this.Tiles.GetObjects(new Frame(point.X, point.Y, 1, 1)).FirstOrDefault();
        }

        #endregion

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            this.Texture = content.Load<Texture2D>(TextureName);
        }
        
        public void UnloadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            
        }

        public Frame Bounds
        {
            get { return this.Tiles.QuadRect; }
        }

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

        public void Draw(SpriteBatch sb)
        {
            if(this.IsVisible)
            {
                sb.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.LinearWrap, null, null, null, this.Parent.World.Camera.Transform);

                var drawn = this.Tiles.GetObjects(this.Parent.World.Camera.Bounds);

                foreach (var tile in drawn)
                {
                    tile.Draw(sb);
                }

                sb.End();
            }
        }
    }
}

