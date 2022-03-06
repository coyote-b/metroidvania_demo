using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MetroidvaniaDemo.Managers
{
    // a class for managing animations
    public class AnimationManager
    {
        private Animation _animation;
        private int _delayCounter;
        private List<Rectangle> _frames;
        private SpriteEffects _flip;

        // this is public for future collision manager 
        public Vector2 Position { get; set; }

        public AnimationManager(Animation animation)
        {
            _animation = animation;
        }

        // a method that starts an animation from the first frame
        public void Play(Animation animation, bool flip)
        {
            if (_animation == animation)
                return;

            _animation = animation;
            _animation.FrameIndex = 0;
            _delayCounter = 0;

            if (flip)
                _flip = SpriteEffects.FlipHorizontally;
            else
                _flip = SpriteEffects.None;
        }

        // a method that stops an animation
        public void Stop()
        {
            _delayCounter = 0;
            _animation.FrameIndex = 0;
        }

        // overload of base draw method 
        public void Draw(GameTime gameTime)
        {
            Shared.spriteBatch.Begin();
            Shared.spriteBatch.Draw(_animation.SpriteSheet, Position, _animation.Frames[_animation.FrameIndex], Color.White, 0f, _animation.Origin, 1f, _flip, 0f);
            Shared.spriteBatch.End();
        }

        // overload of base update method
        public void Update(GameTime gameTime)
        {
            // update the delay counter every update
            _delayCounter++;

            // only advance to the next frame once the delay counter runs out
            if (_delayCounter > _animation.Delay)
            {
                // advance to next frame
                _animation.FrameIndex++;
                _delayCounter = 0;

                // start animation over once it reaches the end
                if (_animation.FrameIndex > _animation.Frames.Count - 1)
                {
                    _animation.FrameIndex = 0;
                }
            }
        }
    }
}
