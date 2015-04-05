using Microsoft.Xna.Framework;
using Sparkle.Engine.Base.Geometry;
using Sparkle.Engine.Core.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Core.Components
{
    public class ParticleEmitter : Renderer
    {
        public class Particle
        {
            public float Rotation { get; set; }

            public float RotationVelocity { get; set; }

            public Vector4 Color { get; set; }

            public Vector4 ColorVelocity { get; set; }

            public Vector3 Position { get; set; }

            public Vector3 PositionVelocity { get; set; }

            public Vector3 PositionAcceleration { get; set; }

            public float Scale { get; set; }

            public float ScaleVelocity { get; set; }
            
            public TimeSpan Lifetime { get; set; }

            public TimeSpan TotalLifetime { get; set; }

            public Rectangle DestinationArea { get; set; }

        }
        
        public Color StartColor { get; set; }

        public Color EndColor { get; set; }
                
        public Frame StartArea { get; set; }

        public Frame EndArea { get; set; }

        public Vector3 MinAcceleration { get; set; }

        public Vector3 MaxAcceleration { get; set; }

        public Rectangle SourceArea { get; set; }

        public float MinStartScale { get; set; }

        public float MaxStartScale { get; set; }

        public float MinEndScale { get; set; }

        public float MaxEndScale { get; set; }

        public float MinStartRotation { get; set; }

        public float MaxStartRotation { get; set; }

        public float MinEndRotation { get; set; }

        public float MaxEndRotation { get; set; }

        public TimeSpan MinLifetime { get; set; }

        public TimeSpan MaxLifetime { get; set; }

        public static Random random = new Random();

        /// <summary>
        /// The drawing order of the sprite.
        /// </summary>
        public override float Order { get; set; }

        public Sprite Sprite { get; set; }

        private List<Particle> particles = new List<Particle>();

        public IList<Particle> Particles
        {
            get { return particles; }
        }

        public void Emit(int number)
        {
            for (int i = 0; i < number; i++)
            {
                this.Emit();
            }
        }

        public Particle Emit()
        {
            var particle = new Particle();

            // Lifetime

            particle.TotalLifetime = MinLifetime + TimeSpan.FromMilliseconds((MaxLifetime.TotalMilliseconds - MinLifetime.TotalMilliseconds) * random.NextDouble());

            // Color

            particle.Color = this.StartColor.ToVector4();
            particle.ColorVelocity = (this.EndColor.ToVector4() - this.StartColor.ToVector4()) / (float)(particle.TotalLifetime.TotalMilliseconds);

            // Position

            var startPos = new Vector2();
            startPos.X = this.StartArea.X + (float)(random.NextDouble() * this.StartArea.Width);
            startPos.Y = this.StartArea.Y + (float)(random.NextDouble() * this.StartArea.Height);

            var endPos = new Vector2();
            endPos.X = this.EndArea.X + (float)(random.NextDouble() * this.EndArea.Width);
            endPos.Y = this.EndArea.Y + (float)(random.NextDouble() * this.EndArea.Height);

            particle.Position = new Vector3(startPos,0);
            particle.PositionVelocity = new Vector3((endPos - startPos) / (float)(particle.TotalLifetime.TotalMilliseconds),0);
            particle.PositionAcceleration = this.MinAcceleration + (float)random.NextDouble() * (this.MaxAcceleration - this.MinAcceleration);

            var transform = this.Owner.GetComponent<Transform>();

            // Rotation

            var startRot = this.MinStartRotation + random.NextDouble() * (this.MaxStartRotation - this.MinStartRotation);
            var endRot = this.MinEndRotation + random.NextDouble() * (this.MaxEndRotation - this.MinEndRotation);
            particle.Rotation = (float)startRot;
            particle.RotationVelocity = (float)(endRot - startRot) / (float)(particle.TotalLifetime.TotalMilliseconds);

            // Scale

            var startSca = this.MinStartScale + random.NextDouble() * (this.MaxStartScale - this.MinStartScale);
            var endSca = this.MinEndScale + random.NextDouble() * (this.MaxEndScale - this.MinEndScale);
            particle.Scale = (float)startSca;
            particle.ScaleVelocity = (float)(endSca - startSca) / (float)(particle.TotalLifetime.TotalMilliseconds);

            // Adding owner's transform 

            if(transform != null)
            {
                particle.Position += transform.Position - new Vector3(this.StartArea.Width / 2,this.StartArea.Height / 2,0);
            }

            this.particles.Add(particle);

            return particle;
            
        }

        public override Frame Bounds
        {
            get {

                var result = new Frame();
                bool first = true;
                foreach (var particle in this.Particles)
                {
                    if (first)
                    {
                        result = new Frame(particle.DestinationArea);
                    }
                    else
                    {
                        var right = result.Right;
                        var bottom = result.Bottom;

                        result.X = Math.Min(result.X, particle.DestinationArea.X);
                        result.Y = Math.Min(result.X, particle.DestinationArea.X);

                        right = Math.Max(particle.DestinationArea.Right, right);
                        bottom = Math.Max(particle.DestinationArea.Bottom, bottom);

                        result.Width = right - result.X;
                        result.Height = bottom - result.Y;
                    }
                   
                }

                return result;
            }
        }
    }
}
