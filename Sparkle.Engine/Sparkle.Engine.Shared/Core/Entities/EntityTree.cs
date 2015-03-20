namespace Sparkle.Engine.Core.Entities
{
    using Microsoft.Xna.Framework;
    using Sparkle.Engine.Base;
    using Sparkle.Engine.Base.Shapes;
    using System.Collections.Generic;

	/// <summary>
	/// A quad tree containing all the entities.
	/// </summary>
	public class EntityTree : QuadTree<Entity>
	{
		public EntityTree (Frame bounds) : base (bounds)
		{

		}

	}
}

