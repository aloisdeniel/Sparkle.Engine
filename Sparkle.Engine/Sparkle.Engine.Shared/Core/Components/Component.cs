namespace Sparkle.Engine.Core.Components
{
    using Sparkle.Engine.Base;
    using Sparkle.Engine.Core;
    using Sparkle.Engine.Core.Entities;

    /// <summary>
    /// A component represents the behavior of a world entity. A component can also be seen has a "state" data
    /// that can be easily saved and restored.
    /// </summary>
    public abstract class Component
    {
        /// <summary>
        /// Owner entity of the component.
        /// </summary>
        public Entity Owner { get; set; }

        /// <summary>
        /// Called when the component is attached to its owner entity.
        /// </summary>
        public virtual void Attached()
        {

        }

        /// <summary>
        /// Called when the component is detached from its owner entity.
        /// </summary>
        public virtual void Detached()
        {

        }
    }
}
