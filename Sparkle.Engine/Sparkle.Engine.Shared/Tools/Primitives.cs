using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sparkle.Engine.Base.Shapes;

namespace Sparkle.Engine.Tools
{
	public static class Primitives
	{
		public static Texture2D Pixel {
			get;
			private set;
		}

		private static bool isLoaded;

		public static void LoadContent (GraphicsDevice graphicDevice)
		{
			if (!isLoaded) {
				Pixel = new Texture2D (graphicDevice, 1, 1);
				Pixel.SetData<Color> (new Color[] { Color.White });
				isLoaded = true;
			}
		}

		public static void DrawSegment (this SpriteBatch sb, Color color, Segment segment)
		{
			Vector2 edge = segment.End - segment.Start;
			// calculate angle to rotate line
			float angle =
				(float)Math.Atan2 (edge.Y, edge.X);


			sb.Draw (Pixel,
				new Rectangle ((int)segment.Start.X, (int)segment.Start.Y, (int)edge.Length (), 1),
				null,
				color,
				angle,
				new Vector2 (0, 0),
				SpriteEffects.None,
				0);

		}

		public static void DrawVector (this SpriteBatch sb, Color color, Vector2 start, Vector2 end)
		{
			var segment = new Segment (start, end);
			sb.DrawSegment (color, segment);
			sb.DrawDot (color, segment.Start);

		}

		public static void DrawDot (this SpriteBatch sb, Color color, Vector2 position)
		{
			const float width = 4;
			const float height = 2;

			sb.Draw (Pixel,
				new Rectangle ((int)(position.X - width), (int)(position.Y - height), (int)(2 * width), (int)(2 * height)),
				null,
				color,
				0,
				new Vector2 (0, 0),
				SpriteEffects.None,
				0);

			sb.Draw (Pixel,
				new Rectangle ((int)(position.X - height), (int)(position.Y - width), (int)(2 * height), (int)(2 * width)),
				null,
				color,
				0,
				new Vector2 (0, 0),
				SpriteEffects.None,
				0);

		}

		public static void DrawFrame (this SpriteBatch sb, Color color, Frame frame)
		{
			sb.DrawSegment (color, frame.LeftEdge);
			sb.DrawSegment (color, frame.RightEdge);
			sb.DrawSegment (color, frame.TopEdge);
			sb.DrawSegment (color, frame.BottomEdge);
		}
	}
}

