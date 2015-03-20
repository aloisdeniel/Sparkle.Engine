namespace Sparkle.Engine.UI
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
    using Sparkle.Engine.Base;
    using Sparkle.Engine.Base.Shapes;

	public abstract class UILayout : UIElement
	{
		public UILayout ()
		{
			this.children = new List<UIElement> ();
		}

		protected List<UIElement> children;

		public UIElement[] Children { get { return this.Children.ToArray (); } }

		public void Add (UIElement child)
		{
			this.children.Add (child);
		}

		public void Remove (UIElement child)
		{
			this.children.Remove (child);
		}

		/// <summary>
		/// Find children with the specified id, in this layout and all its children layouts.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T Find<T> (string id) where T : UIElement
		{
			foreach (var child in this.Children) {
				if (child.Id == id)
					return (T)child;
				var layout = child as UILayout;
				if (child != null) {
					var found = layout.Find<T> (id);
					if (found != null)
						return found;
				}
			}

			return null;
		}

		public abstract Frame GetChildArea (UIElement child);
	}
}

