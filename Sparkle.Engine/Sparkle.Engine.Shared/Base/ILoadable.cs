
namespace Sparkle.Engine.Base
{
	using System;
	using Microsoft.Xna.Framework.Content;

	/// <summary>
	/// An instance that have loadable content.
	/// </summary>
	public interface ILoadable
	{
		/// <summary>
		/// Loads the content.
		/// </summary>
		/// <param name="content">Content manager.</param>
		void LoadContent (ContentManager content);

		/// <summary>
		/// Unloads the content.
		/// </summary>
		/// <param name="content">Content manager.</param>
		void UnloadContent (ContentManager content);
	}
}

