using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using System.Threading;
using RADMonoGame.GameObjects;

namespace RADMonoGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Map map;

        SpriteFont font;
        float Time = 0;

        Texture2D simpleSprite;
        PlayerSprite player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = false;

            //fixed screen resolution
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 700;

            graphics.ApplyChanges();      
        }

        protected override void Initialize()
        {
            map = new Map();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //load music
            Song song = Content.Load<Song>("8bit");
            MediaPlayer.Play(song);

            font = Content.Load<SpriteFont>("Message");

            Tile.Content = Content;//t
            map.Generate(new int[,] {
                {3,3,3,3,3,3,3,3,3,3,3,3,2,2,3,3,3,3,3,3},
                {2,2,2,2,3,3,3,3,3,3,3,3,2,2,3,3,3,3,3,3},
                {2,2,2,2,3,3,3,3,3,3,3,3,2,2,3,3,3,3,3,3},
                {3,3,2,2,3,3,3,3,3,3,3,3,2,2,3,3,3,3,3,3},
                {3,3,2,2,3,3,3,3,3,3,3,3,2,2,2,2,2,2,2,2},
                {3,3,2,2,3,3,3,3,3,3,3,3,2,2,2,2,2,2,2,2},
                {3,3,2,2,2,2,2,2,2,2,2,2,2,2,3,3,3,3,3,3},
                {3,3,2,2,2,2,2,2,2,2,2,2,2,2,3,3,3,3,3,3},
                {3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3},
                {3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3},
                {3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3},
            }, 64);

            simpleSprite = Content.Load<Texture2D>("sprite");

            player = new PlayerSprite(this, simpleSprite, new Point(600, 350));
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            map.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Time Passed: " + Time.ToString("0.00"), new Vector2(300, 50), Color.Black);
            spriteBatch.End();
            player.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}

