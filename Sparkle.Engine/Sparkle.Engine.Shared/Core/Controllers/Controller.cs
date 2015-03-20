namespace Sparkle.Engine.Core.Controllers
{
	using Sparkle.Engine.Base;
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// A controller is responsible for updating the world with a list of commands.
	/// </summary>
	public class Controller : UpdatableBase, IController
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Sparkle.Engine.Core.Controllers.Controller"/> class.
		/// </summary>
		public Controller ()
		{
			this.commands = new List<ICommand> ();
		}

		private List<ICommand> commands;

		/// <summary>
		/// Gets all the commands.
		/// </summary>
		/// <value>The commands.</value>
		public IEnumerable<ICommand> Commands { get { return commands; } }

		/// <summary>
		/// Adds the given command.
		/// </summary>
		/// <param name="command">Command.</param>
		public void AddCommand (ICommand command)
		{
			this.commands.Add (command);
		}

		protected override void DoUpdate (Microsoft.Xna.Framework.GameTime gameTime)
		{
			foreach (var command in commands) {
				command.Update (gameTime);
			}
		}

	}
}
