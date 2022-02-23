using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MetroidvaniaDemo
{
    class Sprite : DrawableGameComponent
    {
        private AnimationManager _animationManager;
        private Dictionary<string, Animation> _animations;
        private Texture2D _tex;
        private Vector2 _position;
        private string lastDirection;

        private bool _duck;
        private bool _pointUp;
        private bool _shoot;

        public float Speed = 2f;
        public Vector2 Velocity;
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }

        public Sprite(Game game, Dictionary<string, Animation> animations) : base(game)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }

        protected virtual void Move()
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Left))
                Velocity.X -= Speed;
            if (ks.IsKeyDown(Keys.Right))
                Velocity.X += Speed;

            if (ks.IsKeyDown(Keys.Down))
                _duck = true;
            else
                _duck = false;

            if (ks.IsKeyDown(Keys.Up))
                _pointUp = true;
            else
                _pointUp = false;

            if (ks.IsKeyDown(Keys.Z))
                _shoot = true;
            else
                _shoot = false;
        }

        protected virtual void SetAnimation()
        {
            if (Velocity.X > 0)
            {
                if (_shoot)
                    _animationManager.Play(_animations["WalkShootRight"]);
                else
                    _animationManager.Play(_animations["WalkRight"]);

                lastDirection = "RIGHT";
            }
            else if (Velocity.X < 0)
            {
                if (_shoot)
                    _animationManager.Play(_animations["WalkShootLeft"]);
                else
                    _animationManager.Play(_animations["WalkLeft"]);

                lastDirection = "LEFT";
            }
            else if (Velocity.X == 0)
            {
                if (_duck)
                {
                    if (lastDirection == "RIGHT")
                        _animationManager.Play(_animations["DuckRight"]);
                    else if (lastDirection == "LEFT")
                        _animationManager.Play(_animations["DuckLeft"]);
                }
                else if (_pointUp)
                {
                    if (lastDirection == "RIGHT")
                        _animationManager.Play(_animations["ShootUpRight"]);
                    else if (lastDirection == "LEFT")
                        _animationManager.Play(_animations["ShootUpLeft"]);
                }
                else if (_shoot)
                {
                    if (lastDirection == "RIGHT")
                        _animationManager.Play(_animations["ShootRight"]);
                    else if (lastDirection == "LEFT")
                        _animationManager.Play(_animations["ShootLeft"]);
                }
                else
                {
                    if (lastDirection == "RIGHT")
                        _animationManager.Play(_animations["IdleRight"]);
                    else if (lastDirection == "LEFT")
                        _animationManager.Play(_animations["IdleLeft"]);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (_tex != null)
            {
                Shared.spriteBatch.Begin();
                Shared.spriteBatch.Draw(_tex, Position, Color.White);
                Shared.spriteBatch.End();
            }  
            else if (_animationManager != null)
                _animationManager.Draw(gameTime);
            
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            Move();
            SetAnimation();

            _animationManager.Update(gameTime);

            Position += Velocity;
            Velocity = Vector2.Zero;
            
            base.Update(gameTime);
        }
    }
}
