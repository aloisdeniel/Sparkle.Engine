namespace Sparkle.Engine.Core.Tiles
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Sparkle.Engine.Base;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TileSheet : ILoadable
    {
        public TileSheet(string texture, int tileWidth, int tileHeight)
        {
            this.tileSources = new Dictionary<int, Rectangle>();
            this.TextureName = texture;
            this.TileSize = new Size(tileWidth, tileHeight);
		}


        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            this.Texture = content.Load<Texture2D>(TextureName);
        }

        public void UnloadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {

        }

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

        public Size TileSize { get; set; }

        private Dictionary<int, Rectangle> tileSources;

        /// <summary>
        /// Registers a tile sources coordinates with an identier.
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void RegisterTile(int identifier, int x, int y, int width = 1, int height = 1)
        {
            this.tileSources[identifier] = new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// Create the tile with the identifier at the coordinates.
        /// </summary>
        /// <param name="indentifier"></param>
        public Tile CreateTile(int indentifier)
        {
            return this.CreateTile(this.tileSources[indentifier]);
        }

        /// <summary>
        /// Create the tile from the coordinates.
        /// </summary>
        /// <param name="source"></param>
        public Tile CreateTile(int x, int y)
        {
            return this.CreateTile(new Rectangle(x, y, 1, 1));
        }

        /// <summary>
        /// Create the tile with the source area at the coordinates.
        /// </summary>
        /// <param name="source"></param>
        public Tile CreateTile(Rectangle source)
        {
            return new Tile(
                this,
                (int)(source.X * this.TileSize.Width),
                (int)(source.Y * this.TileSize.Height),
                (int)(source.Width * this.TileSize.Width),
                (int)(source.Height * this.TileSize.Height));
        }

        #endregion
    }
}
