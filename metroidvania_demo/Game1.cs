using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace MetroidvaniaDemo
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<Sprite> _sprites;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Shared.stage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            Shared.spriteBatch = new SpriteBatch(GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            var animations = new Dictionary<string, Animation>()
            {
                { "IdleLeft", new Animation(Content.Load<Texture2D>("assets/player-idle"), 4, true) },
                { "IdleRight", new Animation(Content.Load<Texture2D>("assets/player-idle"), 4, false) },
                { "WalkLeft", new Animation(Content.Load<Texture2D>("assets/player-run"), 10, true) },
                { "WalkRight", new Animation(Content.Load<Texture2D>("assets/player-run"), 10, false) },
                { "DuckLeft", new Animation(Content.Load<Texture2D>("assets/player-duck"), 1, true) },
                { "DuckRight", new Animation(Content.Load<Texture2D>("assets/player-duck"), 1, false) },
                { "ShootLeft", new Animation(Content.Load<Texture2D>("assets/player-stand"), 3, true) },
                { "ShootRight", new Animation(Content.Load<Texture2D>("assets/player-stand"), 3, false) },
                { "ShootUpLeft", new Animation(Content.Load<Texture2D>("assets/player-shoot-up"), 1, true) },
                { "ShootUpRight", new Animation(Content.Load<Texture2D>("assets/player-shoot-up"), 1, false) },
                { "WalkShootLeft", new Animation(Content.Load<Texture2D>("assets/player-run-shoot"), 10, true) },
                { "WalkShootRight", new Animation(Content.Load<Texture2D>("assets/player-run-shoot"), 10, false) },
            };
            _sprites = new List<Sprite>()
            {
                new Sprite(this, animations)
                {
                    Position = new Vector2(Shared.stage.X / 2 - animations.First().Value.Dimension.X / 2,
                        Shared.stage.Y - animations.First().Value.Dimension.Y)
                }
            };

            foreach (Sprite sprite in _sprites)
                Components.Add(sprite);
        }

        protected override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            base.Draw(gameTime);
        }
    }
}
