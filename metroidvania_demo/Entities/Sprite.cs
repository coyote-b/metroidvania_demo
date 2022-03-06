using MetroidvaniaDemo.Managers;
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
    // a class for entity sprites
    // TODO: make this more flexible. add children classes for entities that inherit from this
    // right now it only functions for player (since its the only existing entity)
    class Sprite : DrawableGameComponent
    {
        private AnimationManager _animationManager;
        private Dictionary<string, Animation> _animations;
        private Texture2D _tex;
        private Vector2 _position;
        private string _lastDirection;

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

        // TODO: fix directions
        protected virtual void SetAnimation()
        {
            bool flip = false;

            if (Velocity.X != 0)
            {
                if (Velocity.X < 0)
                {
                    flip = true;
                    _lastDirection = "LEFT";
                }
                else
                {
                    _lastDirection = "RIGHT";
                }
                    

                if (_shoot)
                    _animationManager.Play(_animations["PlayerRunShoot"], flip);
                else
                    _animationManager.Play(_animations["PlayerRun"], flip);
            }
            else if (Velocity.X == 0)
            {
                if (_lastDirection == "LEFT")
                    flip = true;

                if (_duck)
                    _animationManager.Play(_animations["PlayerDuck"], flip);
                else if (_pointUp)
                    _animationManager.Play(_animations["PlayerUpShoot"], flip);
                else if (_shoot)
                    _animationManager.Play(_animations["PlayerStandShoot"], flip);
                else
                    _animationManager.Play(_animations["PlayerIdle"], flip);
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
