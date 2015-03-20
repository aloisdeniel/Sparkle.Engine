namespace Sparkle.Engine.Base.Triggers
{
	using Sparkle.Engine.Base;
	using System;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// Represents any trigger that could be activated, maintained and released inside the game.
	/// </summary>
	public interface ITrigger
	{
		/// <summary>
		/// Gets the current state of the trigger.
		/// </summary>
		/// <value>The state.</value>
		TriggerState State { get; }
	}
}
