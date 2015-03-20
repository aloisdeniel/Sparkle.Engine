namespace Sparkle.Engine.Core.Controllers
{
	using Microsoft.Xna.Framework;
	using System;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// A controller is responsible for updating the world with a list of commands.
	/// </summary>
	public interface IController : IUpdateable
	{
		/// <summary>
		/// Gets all the commands.
		/// </summary>
		/// <value>The commands.</value>
		IEnumerable<ICommand> Commands { get; }
	}
}
