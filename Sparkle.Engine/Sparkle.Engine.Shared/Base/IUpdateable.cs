namespace Sparkle.Engine.Base
{
    using Microsoft.Xna.Framework;

    public interface IUpdateable
    {
        /// <summary>
        /// Indicates whether the instance should be updated.
        /// </summary>
        bool IsEnabled { get; }

        /// <summary>
        /// Updates the instance.
        /// </summary>
        /// <param name="time">Game time info</param>
        void Update(GameTime time);
    }
}
