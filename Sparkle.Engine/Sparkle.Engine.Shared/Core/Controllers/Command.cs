namespace Sparkle.Engine.Core.Controllers
{
    using Microsoft.Xna.Framework;
    using Sparkle.Engine.Base;
    using Sparkle.Engine.Base.Triggers;
    using System;
    using System.Collections.Generic;
    using System.Text;

	/// <summary>
	/// Represents a set actions that are executed according to the state of a trigger.
	/// </summary>
	public abstract class Command : UpdatableBase, ICommand
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Sparkle.Engine.Core.Controllers.Command"/> class.
		/// </summary>
		/// <param name="name">The name of the command.</param>
		/// <param name="trigger">The trigger.</param>
		public Command (String name, ITrigger trigger)
		{
			this.Trigger = trigger;
			this.Name = name;
		}

		/// <summary>
		/// Gets the trigger of the command.
		/// </summary>
		/// <value>The trigger.</value>
		public ITrigger Trigger {
			get;
			private set;
		}

		/// <summary>
		/// Gets the name of the command.
		/// </summary>
		/// <value>The name.</value>
		public string Name {
			get;
			private set;
		}

		protected override void DoUpdate (Microsoft.Xna.Framework.GameTime gameTime)
		{
            if(this.Trigger is IUpdateable)
            {
                ((IUpdateable)this.Trigger).Update(gameTime);
            }

			var state = this.Trigger.State;

			switch (state) {
			case TriggerState.Inactive: 
				this.OnInactive ();
				break;
			case TriggerState.Started:
				this.OnStart ();
				this.OnActive ();
				break;
			case TriggerState.Active:
				this.OnActive ();
				break;
			case TriggerState.Stopped:
				this.OnStop ();
				this.OnInactive ();
				break;
			default:
				break;
			}
		}

		/// <summary>
		/// The action that will be executed in case of an "Inactive" or "Stopped" trigger state.
		/// </summary>
		public abstract void OnInactive ();

		/// <summary>
		/// The action that will be executed in case of a "Started" trigger state.
		/// </summary>
		public abstract void OnStart ();

		/// <summary>
		/// The action that will be executed in case of an "Active" or "Started" trigger state.
		/// </summary>
		public abstract void OnActive ();

		/// <summary>
		/// The action that will be executed in case of a "Stopped" trigger state.
		/// </summary>
		public abstract void OnStop ();

		/// <summary>
		/// The relay is an helper class for building a command from lambda expressions.
		/// </summary>
		public class Relay : Command
		{
			private Relay (String name, ITrigger trigger, Action onStart, Action onStopped, Action onActive, Action onInactive)
				: base (name, trigger)
			{
				this.onStart = onStart;
				this.onStopped = onStopped;
				this.onActive = onActive;
				this.onInactive = onInactive;
			}

			private Action onStart, onStopped, onActive, onInactive;

			public override void OnInactive ()
			{
				if (this.onInactive != null)
					this.onInactive ();
			}

			public override void OnStart ()
			{
				if (this.onStart != null)
					this.onStart ();
			}

			public override void OnActive ()
			{
				if (this.onActive != null)
					this.onActive ();
			}

			public override void OnStop ()
			{
				if (this.onStopped != null)
					this.onStopped ();
			}

			public static Relay Create (string name, ITrigger trigger, Action onStart, Action onStopped, Action onActive, Action onInactive)
			{
				return new Relay (name, trigger, onStart, onStopped, onActive, onInactive);
			}

			public static Relay OnStartAndStop (string name, ITrigger trigger, Action<bool> onStart)
			{
				return new Relay (name, trigger, () => onStart (true), () => onStart (false), null, null);
			}

			public static Relay OnActiveAndInactive (string name, ITrigger trigger, Action<bool> onActive)
			{
				return new Relay (name, trigger, null, null, () => onActive (true), () => onActive (false));
			}

			public static Relay OnStart (string name, ITrigger trigger, Action onStart)
			{
				return new Relay (name, trigger, onStart, null, null, null);
			}

			public static Relay OnStop (string name, ITrigger trigger, Action onStop)
			{
				return new Relay (name, trigger, null, onStop, null, null);
			}

			public static Relay OnActive (string name, ITrigger trigger, Action onActive)
			{
				return new Relay (name, trigger, null, null, onActive, null);
			}

			public static Relay OnInactive (string name, ITrigger trigger, Action onInactive)
			{
				return new Relay (name, trigger, null, null, null, onInactive);
			}
		}
	}
}