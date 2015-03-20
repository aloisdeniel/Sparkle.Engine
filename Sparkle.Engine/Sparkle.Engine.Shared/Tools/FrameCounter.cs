using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Sparkle.Engine.Tools
{
	public class FrameCounter
	{
		public FrameCounter ()
		{
		}

		public long TotalFrames { get; private set; }

		public float TotalSeconds { get; private set; }

		public float AverageFramesPerSecond { get; private set; }

		public float CurrentFramesPerSecond { get; private set; }

		public const int MAXIMUM_SAMPLES = 100;

		private Queue<float> _sampleBuffer = new Queue<float> ();

		public void Draw (SpriteBatch sb, GameTime gameTime)
		{
			var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

			CurrentFramesPerSecond = 1.0f / deltaTime;

			_sampleBuffer.Enqueue (CurrentFramesPerSecond);

			if (_sampleBuffer.Count > MAXIMUM_SAMPLES) {
				_sampleBuffer.Dequeue ();
				AverageFramesPerSecond = _sampleBuffer.Average (i => i);
			} else {
				AverageFramesPerSecond = CurrentFramesPerSecond;
			}

			TotalFrames++;
			TotalSeconds += deltaTime;

			var fps = string.Format ("FPS: {0}", this.AverageFramesPerSecond);

			//sb.DrawString ( _spriteFont, fps, new Vector2 (1, 1), Color.Black);
			Debug.WriteLine (fps);
		}
	}
}

