using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using System.Threading;
using RADMonoGame.GameObjects;
using System.Linq;

namespace RADMonoGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Map map;

        SpriteFont font;
        float Time = 0;

        int Score = 0;

        Texture2D playerSprite;
        PlayerSprite player;

        Texture2D collectableSprite;
        List<Collectable> collectablesList;

        Random randomNumber = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = false;

            //fixed the screen resolution
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

            //loads the music
            Song song = Content.Load<Song>("8bit");
            MediaPlayer.Play(song);

            font = Content.Load<SpriteFont>("Message");

            Tile.Content = Content;//sets which tiles are place where within the game
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

            //Loads player and collectables
            playerSprite = Content.Load<Texture2D>("sprite");
            collectableSprite = Content.Load<Texture2D>("collectableSprite");

            //Sets the players spawnpoint
            player = new PlayerSprite(this, playerSprite, new Point(100, 300));

            //Sets the boundaries for where the collectables can spawn within
            collectablesList = new List<Collectable>();
            for (int i = 0; i < 6; i++)
            {
                collectablesList.Add(new Collectable(this, collectableSprite, new Point(randomNumber.Next(0, 1200), randomNumber.Next(0, 620))));
            }
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Counts the number of collectables that you have gotten
            for (int i = 0; i < collectablesList.Count; i++)
            {
                if(collectablesList[i].BoundingRect.Intersects(player.BoundingRect))
                {
                    collectablesList[i].Visible = false;
                    collectablesList.RemoveAt(i);
                    Score += 1;
                    i--;
                }
            }

            if(collectablesList.Count == 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    collectablesList.Add(new Collectable(this, collectableSprite, new Point(randomNumber.Next(0, 1200), randomNumber.Next(0, 620))));
                }
            }

            Time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            map.Draw(spriteBatch);
            player.Draw(gameTime);
            foreach(Collectable i in collectablesList)
            {
                i.Draw(gameTime);
            }
            //Displays time and score
            spriteBatch.DrawString(font, "Time Passed: " + Time.ToString("0.00"), new Vector2(140, 20), Color.Black);
            spriteBatch.DrawString(font, "Score: " + Score.ToString(), new Vector2(20, 20), Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

