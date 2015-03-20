namespace Sparkle.Engine.UI
{
	using System;
	using Microsoft.Xna.Framework;
	using Sparkle.Engine.Base;
    using Sparkle.Engine.Base.Shapes;

	public class UIElement : Sparkle.Engine.Base.IDrawable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Sparkle.Engine.UI.UIElement"/> class.
		/// </summary>
		public UIElement ()
		{
		}

		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the position into a grid if parent is a UIGrid.
		/// </summary>
		/// <value>The grid position.</value>
		public UIGrid.Position GridPosition { get; set; }

		/// <summary>
		/// Gets or sets the parent of this element.
		/// </summary>
		/// <value>The parent.</value>
		public UILayout Parent { get; set; }

		/// <summary>
		/// Gets or sets the vertical alignment into parent.
		/// </summary>
		/// <value>The vertical alignment.</value>
		public VerticalAlignment VerticalAlignment { get; set; }

		/// <summary>
		/// Gets or sets the horizontal alignment into parent.
		/// </summary>
		/// <value>The vertical alignment.</value>
		public HorizontalAlignment HorizontalAlignment { get; set; }

		/// <summary>
		/// Gets or sets the visibility.
		/// </summary>
		/// <value>The visibility.</value>
		public Visibility Visibility { get; set; }

		/// <summary>
		/// Gets or sets the defined height (not the rendered one).
		/// </summary>
		/// <value>The height.</value>
		public float? Height { get; set; }

		/// <summary>
		/// Gets or sets the defined width (not the rendered one).
		/// </summary>
		/// <value>The width.</value>
		public float? Width { get; set; }

		/// <summary>
		/// Gets or sets the actual rendered frame.
		/// </summary>
		/// <value>The actual frame.</value>
		public Frame ActualFrame {
			get {
			
				var container = this.Parent != null ? this.Parent.GetChildArea (this) : new Frame (0, 0, (float)Width, (float)Height);

				container.X += this.Margin.Left;
				container.Y += this.Margin.Top;
				container.Width -= this.Margin.Left + this.Margin.Right;
				container.Height -= this.Margin.Top + this.Margin.Bottom;
			
				//TODO use alignements

				return container;
			
			}
		}

		/// <summary>
		/// Gets or sets the margin.
		/// </summary>
		/// <value>The margin.</value>
		public Margin Margin { get; set; }

		/// <summary>
		/// Gets or sets the background color.
		/// </summary>
		/// <value>The background.</value>
		public Color Background { get; set; }

		/// <summary>
		/// Gets or sets the foreground color.
		/// </summary>
		/// <value>The foreground.</value>
		public Color Foreground { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has the focus from the user.
		/// </summary>
		/// <value><c>true</c> if this instance has focus; otherwise, <c>false</c>.</value>
		public bool HasFocus { get; set; }

		/// <summary>
		/// Gets or sets the opacity level from 0 to 1.
		/// </summary>
		/// <value>The opacity.</value>
		public float Opacity { get; set; }

		#region IDrawable implementation

		public virtual void Draw (Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
		{

		}

		public bool IsVisible {
			get ;
			set;
		}

		public int DrawOrder {
			get ;
			set;
		}

		#endregion
	}
}

