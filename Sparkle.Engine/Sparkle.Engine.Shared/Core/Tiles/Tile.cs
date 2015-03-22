namespace Sparkle.Engine.Core.Tiles
{
    using System;
    using Microsoft.Xna.Framework;
    using Sparkle.Engine.Base;
    using Sparkle.Engine.Base.Shapes;
    using Microsoft.Xna.Framework.Graphics;

    public class Tile : Sparkle.Engine.Base.IDrawable, IQuadStorable
	{
		public Tile (TileSheet parent,int x, int y, int width, int height)
		{
            this.IsVisible = true;
            this.Parent = parent;
			this.SourceArea = new Rectangle (x, y, width, height);
		}

        public TileSheet Parent { get; private set; }

        #region Coordinates

        private Rectangle destinationCoordinates;

        public Rectangle DestinationCoordinates
        {
			get { return this.destinationCoordinates; }
			set { 
				this.destinationCoordinates = value; 
				this.DestinationArea = new Rectangle (
					this.SourceArea.Width * value.X, 
					this.SourceArea.Height * value.Y,
					this.SourceArea.Width * value.Width,
					this.SourceArea.Height * value.Height);
			}
		}

		/// <summary>
		/// The subarea from the texture that will be drawn into destination.
		/// </summary>
		public Rectangle DestinationArea { get; private set; }

		/// <summary>
		/// The subarea from the texture that will be drawn into destination.
		/// </summary>
		public Rectangle SourceArea { get; private set; }

        #endregion

        public Base.Shapes.Frame Bounds
        {
            get { return new Frame(DestinationArea.X, DestinationArea.Y, DestinationArea.Width, DestinationArea.Height); }
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

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            if (this.IsVisible)
            {
                sb.Draw(this.Parent.Texture,
                    this.DestinationArea,
                    this.SourceArea,
                    Color.White,
                    0,
                    Vector2.Zero,
                    SpriteEffects.None,
                    this.DrawOrder);
            }
        }
    }
}

