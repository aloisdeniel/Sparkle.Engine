namespace Sparkle.Engine.Base.Triggers
{
	using Microsoft.Xna.Framework.Input.Touch;
	using Sparkle.Engine.Base;
	using Sparkle.Engine.Base.Shapes;
	using System;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// A trigger when the user touch the screen.
	/// </summary>
	public class TapTrigger : ITrigger
	{
		private bool prec;

		/// <summary>
		/// Gets or sets the area of the screen that must be touched to changed current state.
		/// </summary>
		/// <value>The frame.</value>
		public Frame Frame { get; set; }

		public TriggerState State {
			get {

				var state = TouchPanel.GetState ();

				bool containsTap = false;
				foreach (TouchLocation tl in state) {
					if (this.Frame.Contains (tl.Position)) {
						containsTap = true;
						break;
					}
				}

				TriggerState result = TriggerState.Inactive;

				if (containsTap && !prec)
					result = TriggerState.Started;

				if (!containsTap && prec)
					result = TriggerState.Stopped;

				if (containsTap)
					result = TriggerState.Active;

				prec = containsTap;

				return result;
            
			}
		}
	}
}
