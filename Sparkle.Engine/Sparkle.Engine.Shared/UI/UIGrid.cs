namespace Sparkle.Engine.UI
{
    using Sparkle.Engine.Base.Shapes;
    using System;

	public class UIGrid : UILayout
	{
		public UIGrid (int rows, int columns)
		{
			this.Rows = new int[rows];
			this.Columns = new int[columns];
		}

		/// <summary>
		/// Gets or sets the row definitions. A zero indicates that the available space must be divided to let the
		/// row use as space as possible.
		/// </summary>
		/// <value>The rows.</value>
		public int[] Rows { get; set; }

		/// <summary>
		/// Gets or sets the column definition. A zero indicates that the available space must be divided to let the
		/// column use as space as possible.
		/// </summary>
		/// <value>The columns.</value>
		public int[] Columns { get; set; }

		public override Frame GetChildArea (UIElement child)
		{
			throw new NotImplementedException ();
		}

		public struct Position
		{
			public int Row { get; set; }

			public int Column { get; set; }

			public int RowSpan { get; set; }

			public int ColumnSpan { get; set; }
		}

	}
}

