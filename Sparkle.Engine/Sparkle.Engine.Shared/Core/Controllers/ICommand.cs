namespace Sparkle.Engine.Core.Controllers
{
	using Microsoft.Xna.Framework;
	using System;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// Represents a set actions that are executed according to the state of a trigger.
	/// </summary>
	public interface ICommand : IUpdateable
	{
		/// <summary>
		/// Gets the name of the command.
		/// </summary>
		/// <value>The name.</value>
		string Name { get; }

		/// <summary>
		/// The action that will be executed in case of an "Inactive" or "Stopped" trigger state.
		/// </summary>
		void OnInactive ();

		/// <summary>
		/// The action that will be executed in case of a "Started" trigger state.
		/// </summary>
		void OnStart ();

		/// <summary>
		/// The action that will be executed in case of an "Active" or "Started" trigger state.
		/// </summary>
		void OnActive ();

		/// <summary>
		/// The action that will be executed in case of a "Stopped" trigger state.
		/// </summary>
		void OnStop ();
	}
}
