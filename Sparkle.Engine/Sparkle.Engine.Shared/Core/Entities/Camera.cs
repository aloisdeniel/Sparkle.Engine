using Microsoft.Xna.Framework;
using Sparkle.Engine.Base.Geometry;
using Sparkle.Engine.Core.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Core.Entities
{
    public class Camera : Entity
    {
        public Camera()
        {
            this.AddComponent<Transform>();
        }

        public float Zoom
        {
            get
            {
                var transform = this.GetComponent<Transform>();

                return 1.0f / transform.Position.Z;
            }
        }

        public Frame Bounds
        {
            get
            {
                var transform = this.GetComponent<Transform>();

                var baseBounds = new Frame(
                    (transform.Position.X - this.Width / 2),
                    (transform.Position.Y - this.Height / 2),
                    this.Width,
                    this.Height);

                var center = baseBounds.Center;
                var w2 = (baseBounds.Width / 2.0f) / this.Zoom;
                var h2 = (baseBounds.Height / 2.0f) / this.Zoom;

                return new Frame(center.X - w2, center.Y - h2, 2 * w2, 2 * h2);
            }
        }

        public float Width { get; set; }

        public float Height { get; set; }

        /// <summary>
        /// Gets the world transform matrix for this camera.
        /// </summary>
        /// <value>The transform.</value>
        public Matrix CreateTransform()
        {
            var transform = this.GetComponent<Transform>();

            return Matrix.CreateTranslation(new Vector3(-1 * transform.Position.X, -1 * transform.Position.Y, 0))
            * Matrix.CreateRotationZ(transform.Rotation)
            * Matrix.CreateScale(new Vector3(this.Zoom, this.Zoom, 0))
            * Matrix.CreateTranslation(new Vector3(this.Width * 0.5f, this.Height * 0.5f, 0));
        }
    }
}
