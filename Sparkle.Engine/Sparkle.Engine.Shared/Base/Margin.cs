namespace Sparkle.Engine.Base
{
    using System;

    /// <summary>
    /// Represents left, top, bottom and right spaces between an element and its parent.
    /// </summary>
	public struct Margin
	{

		public Margin (float left, float top, float right, float bottom)
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}

		public Margin (float leftRight, float topBottom) : this (leftRight, topBottom, leftRight, topBottom)
		{

		}

		public Margin (float margin) : this (margin, margin)
		{

		}
        
        /// <summary>
        /// Instanciate a margin from a string value formated like 'L,T,B,R'(ex:'4,2,5,6'), 'LR,TB'(ex:'4,2') or 'LTBR'(ex:'4').
        /// </summary>
        /// <param name="value"></param>
        public Margin(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                Left = 0;
                Top = 0;
                Right = 0;
                Bottom = 0;
            }
            else
            {
                var values = value.Split(',');
                Left = float.Parse(values[0].Trim());
                Top =  values.Length > 1 ? float.Parse(values[1].Trim()) : Left;
                Right = values.Length > 3 ? float.Parse(values[3].Trim()) : Left;
                Bottom = values.Length > 3 ? float.Parse(values[4].Trim()) : Top;
            }
        }

        /// <summary>
        /// The top space between this element and its parent.
        /// </summary>
		public float Top;

        /// <summary>
        /// The bottom space between this element and its parent.
        /// </summary>
		public float Bottom;

        /// <summary>
        /// The left space between this element and its parent.
        /// </summary>
		public float Left;

        /// <summary>
        /// The right space between this element and its parent.
        /// </summary>
		public float Right;
	}
}

