using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;

namespace RADMonoGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>dfg
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Map map;

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

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            map = new Map();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //load music
            Song song = Content.Load<Song>("8bit");
            MediaPlayer.Play(song);


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

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();


            map.Draw(spriteBatch);





            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

