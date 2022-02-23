using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MetroidvaniaDemo
{
    public class AnimationManager
    {
        private Animation _animation;
        private int _delayCounter;
        private List<Rectangle> _frames;
        private SpriteEffects _flip;

        public Vector2 Position { get; set; }

        public AnimationManager(Animation animation)
        {
            _animation = animation;
            CreateFrames();
        }

        public void Play(Animation animation)
        {
            if (_animation == animation)
                return;

            _animation = animation;
            _animation.FrameIndex = 0;
            _delayCounter = 0;
            CreateFrames();

            if (animation.Flip)
                _flip = SpriteEffects.FlipHorizontally;
            else if (!animation.Flip)
                _flip = SpriteEffects.None;
        }

        public void Stop()
        {
            _delayCounter = 0;
            _animation.FrameIndex = 0;
        }

        public void CreateFrames()
        {
            _frames = new List<Rectangle>();
            for (int i = 0; i < _animation.FrameCount; i++)
            {
                int x = i * (int)_animation.Dimension.X;

                Rectangle frame = new Rectangle(x, 0, (int)_animation.Dimension.X, (int)_animation.Dimension.Y);

                _frames.Add(frame);
            }
        }

        public void Draw(GameTime gameTime)
        {
            Shared.spriteBatch.Begin();
            Shared.spriteBatch.Draw(_animation.SpriteSheet, Position, _frames[_animation.FrameIndex], Color.White, 0f, _animation.Origin, 1f, _flip, 0f);
            Shared.spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            _delayCounter++;

            if (_delayCounter > _animation.Delay)
            {
                _animation.FrameIndex++;
                _delayCounter = 0;

                if (_animation.FrameIndex > _animation.FrameCount - 1)
                {
                    _animation.FrameIndex = 0;
                }
            }
        }
    }
}
