namespace Sparkle.Engine.Base.Triggers
{
	using Microsoft.Xna.Framework.Input;
	using Sparkle.Engine.Base;
	using System;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// A keyboard input trigger.
	/// </summary>
	public class KeyboardTrigger : ITrigger
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Sparkle.Engine.Base.Triggers.KeyboardTrigger"/> class.
		/// </summary>
		/// <param name="key">Key.</param>
		public KeyboardTrigger (Keys key)
		{
			this.Key = key;
		}

		/// <summary>
		/// The last update state
		/// </summary>
		private KeyboardState prec;

		/// <summary>
		/// Gets or sets the key that will be observed.
		/// </summary>
		/// <value>The key.</value>
		public Keys Key { get; set; }

		public TriggerState State {
			get {
				KeyboardState state = Keyboard.GetState ();

				TriggerState result = TriggerState.Inactive;

				if (state.IsKeyDown (Key) && !prec.IsKeyDown (Key))
					result = TriggerState.Started;
				else if (!state.IsKeyDown (Key) && prec.IsKeyDown (Key))
					result = TriggerState.Stopped;
				else if (state.IsKeyDown (Key))
					result = TriggerState.Active;

				prec = state;


				return result;
			}
		}

	}
}
