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
		public TileLayer (TileMap parent, Frame bounds,TileSheet sheet)
        {
            this.IsVisible = true;
            this.Tiles = new QuadTree<Tile>(bounds);
            this.Parent = parent;
            this.Sheet = sheet;
		}

        public TileSheet Sheet { get; set; }

        public TileMap Parent { get; private set; }

        public QuadTree<Tile> Tiles { get; set; }

        #region Tile creation

        /// <summary>
        /// Create the tile with the identifier at the coordinates.
        /// </summary>
        /// <param name="indentifier"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void AddTile(string identifier, int x, int y, int width = 1, int height = 1)
        {
            var tile = this.Sheet.CreateTile(identifier);

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
            this.Sheet.LoadContent(content);
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
                var drawn = this.Tiles.GetObjects(this.Parent.World.Camera.Bounds);

                foreach (var tile in drawn)
                {
                    tile.Draw(sb);
                }

            }
        }
    }
}

