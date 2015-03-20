namespace Sparkle.Engine
{
	using System;
	using Microsoft.Xna.Framework;
	using System.Collections.Generic;

	public static class ColorExtensions
	{
		/// <summary>
		/// Interpolate a color from one color to another
		/// </summary>
		/// <param name="c1">The starting color.</param>
		/// <param name="c2">The ending color.</param>
		/// <param name="pamount">The amount from 0 to 1.</param>
		public static Color Interpolate (this Color c1, Color c2, float amount)
		{
			amount = amount.Clamp (0, 1);

			Color result = new Color ();

			result.R = (byte)(c1.R * (1 - amount) + c2.R * amount);
			result.G = (byte)(c1.G * (1 - amount) + c2.G * amount);
			result.B = (byte)(c1.B * (1 - amount) + c2.B * amount);
			result.A = (byte)(c1.A * (1 - amount) + c2.A * amount);

			return result;
		}

		/// <summary>
		/// A collection of named colors.
		/// </summary>
		public static Dictionary<string,Color> NamedColors = new Dictionary<string, Color> () {
			{ "white",Color.White },
			{ "black",Color.Black },
			{ "red",Color.Red },
			{ "green",Color.Green },
			{ "blue",Color.Blue },
			{ "cyan",Color.Cyan },
			{ "yellow",Color.Yellow },
			{ "pink",Color.Pink },
		};

		/// <summary>
		/// Gets the color from the given string. The provided string can be a named color, '#RRGGBB', '#AARRGGBB', 'r g b', 'a r g b' 
		/// where ARGB are hexadecimal bytes (ex: '#FF66AAFF'), and argb are float values (ex: '0.4 0.5 0.7')
		/// </summary>
		/// <returns>The color.</returns>
		/// <param name="color">Color.</param>
		public static Color GetColor (this String color)
		{
			if (String.IsNullOrWhiteSpace (color))
				return Color.Transparent;

			if (color.StartsWith ("#")) {
				if (color.Length > 1) {
					byte[] bytes = color.Substring (1).GetColorBytes ();
					if (bytes == null)
						return Color.Transparent;

					if (bytes.Length == 3)
						return new Color (bytes [0], bytes [1], bytes [2]);

					if (bytes.Length == 4)
						return new Color (bytes [1], bytes [2], bytes [3], bytes [0]);
				}

				return Color.Transparent;
			}

			String[] colors = color.Split (' ');

			if (colors.Length == 3)
				return new Color (
					(byte)(double.Parse (colors [0]) * 255), 
					(byte)(double.Parse (colors [1]) * 255), 
					(byte)(double.Parse (colors [2]) * 255));

			if (colors.Length == 4)
				return new Color (
					(byte)(double.Parse (colors [1]) * 255), 
					(byte)(double.Parse (colors [2]) * 255), 
					(byte)(double.Parse (colors [3]) * 255), 
					(byte)(double.Parse (colors [0]) * 255));


			color = color.ToLower ();

			if (NamedColors.ContainsKey (color)) {
				return NamedColors [color];
			}

			return Color.Transparent;

		}

		public static Vector4 ToVector (this Color color)
		{
			return new Vector4 ((color.R) / 255f, (color.G) / 255f, (color.B) / 255f, (color.A) / 255f);
		}

        public static Color Min(this Color value, Color other)
        {
            value.A = Math.Min(other.A, value.A);
            value.R = Math.Min(other.R, value.R);
            value.G = Math.Min(other.G, value.G);
            value.B = Math.Min(other.B, value.B);

            return value;
        }

        public static Color Max(this Color value, Color other)
        {
            value.A = Math.Max(other.A, value.A);
            value.R = Math.Max(other.R, value.R);
            value.G = Math.Max(other.G, value.G);
            value.B = Math.Max(other.B, value.B);

            return value;
        }

        public static Color Clamp(this Color value, Color min, Color max)
        {
            value.A = Math.Min(max.A, Math.Max(min.A, value.A));
            value.R = Math.Min(max.R, Math.Max(min.R, value.R));
            value.G = Math.Min(max.G, Math.Max(min.G, value.G));
            value.B = Math.Min(max.B, Math.Max(min.B, value.B));

            return value;
        }

        public static Color Add(this Color color1, Color color2)
        {
            color1.A += color2.A;
            color1.R += color2.R;
            color1.G += color2.G;
            color1.B += color2.B;

            return color1;
        }

        public static Color Substract(this Color color1, Color color2)
        {
            color1.A -= color2.A;
            color1.R -= color2.R;
            color1.G -= color2.G;
            color1.B -= color2.B;

            return color1;
        }

        public static Color Multiply(this Color color1, Color color2)
        {
            color1.A *= color2.A;
            color1.R *= color2.R;
            color1.G *= color2.G;
            color1.B *= color2.B;

            return color1;
        }

        public static Color Multiply(this Color color1, double d)
        {
            color1.A = (byte)(((int)color1.A) * d);
            color1.R = (byte)(((int)color1.R) * d);
            color1.G = (byte)(((int)color1.G) * d);
            color1.B = (byte)(((int)color1.B) * d);

            return color1;
        }

		private static byte[] GetColorBytes (this String hexString)
		{

			if (hexString == null)
				return null;
			int len = hexString.Length;
			if (len % 2 == 1)
				return null;

			byte[] bs = new byte[len / 2];
			try {
				//convert the hexstring to bytes
				for (int i = 0; i != bs.Length; i++) {
					bs [i] = (byte)Int32.Parse (hexString.Substring (i * 2, 2), System.Globalization.NumberStyles.HexNumber);
				}
			} catch (Exception ex) {
			}
			//return the byte array
			return bs;
		}
	}
}

