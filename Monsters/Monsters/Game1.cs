using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;


namespace Monsters
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D idle;
        Texture2D run;
        Texture2D creep;
        Texture2D creep_idle;

        Texture2D background;
        Texture2D cloud;
        Texture2D grass;
        Texture2D green_block;
        Nemo nemo;
        Level levels;

        int windowWidth;
        int windowHeight;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            windowWidth = graphics.PreferredBackBufferWidth = 1366;
            windowHeight = graphics.PreferredBackBufferHeight = 768;

            graphics.IsFullScreen = true;
        }

        public int WindowWidth
        {
            get { return windowWidth; }
        }

        public int WindowHeight
        {
            get { return windowHeight; }
        }
        
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);

            creep = Content.Load<Texture2D>("nemoSprites/nemo_creep");
            run = Content.Load<Texture2D>("nemoSprites/nemo_run");
            idle = Content.Load<Texture2D>("nemoSprites/nemo_idle");
            creep_idle = Content.Load<Texture2D>("nemoSprites/nemo_creep_idle");

            background = Content.Load<Texture2D>("fon");

            cloud = Content.Load<Texture2D>("textures/cloud");
            grass = Content.Load<Texture2D>("textures/grass");
            green_block = Content.Load<Texture2D>("textures/green_block");

            string[] s = File.ReadAllLines("content/levels/level1.txt");

            Rectangle rect = new Rectangle(100, 200, 60, 60);
            nemo = new Nemo(rect, idle, run, creep, creep_idle, this);
            levels = new Level (cloud, grass, green_block, s);
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            nemo.Update(gameTime);

            nemo.KeyControl(Keyboard.GetState());

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            spriteBatch.End();
            
            nemo.Draw(spriteBatch);
            levels.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
