namespace Sparkle.Engine.Core.Components
{
    using Microsoft.Xna.Framework;
    using Sparkle.Engine.Base.Geometry;

    public class Body : Component
    {
        public Body()
        {
            this.GravityScale = 1;
            this.Mass = 1;
        }

        /// <summary>
        /// Bounding box of the instance. It can be used to optimize updated areas.
        /// </summary>
        public Frame Bounds { get; set; }

        /// <summary>
        /// Gets or sets the position of the body.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets the velocity of the position.
        /// </summary>
        public Vector3 Velocity { get; set; }

        /// <summary>
        /// Gets or sets the angle of rotation of the body.
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// Gets or sets the velocity of the rotation.
        /// </summary>
        public float Inertia { get; set; }

        /// <summary>
        /// The mass of the body.
        /// </summary>
        public float Mass { get; set; }
        
        /// <summary>
        /// A scale applied to the gravity forces applied on this body.
        /// </summary>
        public float GravityScale { get; set; }

        /// <summary>
        /// All summed forces applied to the position of the body.
        /// </summary>
        public Vector3 Forces { get; set; }

        /// <summary>
        /// All summed forces applied to the rotation of the body.
        /// </summary>
        public float Torques { get; set; }

        public void Impulse(Vector3 force)
        {
            this.Forces += force;
        }

        public void Torque(float torque)
        {
            this.Torques += torque;
        }

        public void Impulse(Vector3 position, Vector3 force)
        {
            //Calculate Force & Torque
        }
    }
}
