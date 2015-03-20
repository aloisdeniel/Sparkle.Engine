namespace Sparkle.Engine.Base.Triggers
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Linq;

	/// <summary>
	/// A composite trigger that observes the state of a set of existing triggers to update its current state.
	/// </summary>
	public class MultiTrigger : ITrigger
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Sparkle.Engine.Base.Triggers.MultiTrigger"/> class.
		/// </summary>
		/// <param name="triggers">The observed triggers.</param>
		public MultiTrigger (params ITrigger[] triggers)
		{
			this.Children = triggers.ToList ();
		}

		/// <summary>
		/// Gets the children triggers.
		/// </summary>
		/// <value>The children.</value>
		public List<ITrigger> Children { get; private set; }

		/// <summary>
		/// Gets the current state of the trigger.
		/// </summary>
		/// <value>The state.</value>
		public TriggerState State {
			get {

				var states = this.Children.Select ((trigger) => trigger.State);

				var inactives = states.Contains (TriggerState.Inactive);
				var started = states.Contains (TriggerState.Started);
				var actives = states.Contains (TriggerState.Active);
				var stopped = states.Contains (TriggerState.Stopped);

				if (!actives && started) //FIXME
                    return TriggerState.Started;

				if (!inactives && stopped) //FIXME
                    return TriggerState.Stopped;

				if (actives)
					return TriggerState.Active;

				return TriggerState.Inactive;
			}
		}
	}
}
