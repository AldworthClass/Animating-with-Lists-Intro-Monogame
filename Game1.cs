using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Animating_with_Lists_Intro
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D snowTexture;
        Rectangle window;

        List<Rectangle> snowFlakes;
        Vector2 fallSpeed;

        Random generator;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            generator = new Random();

            window = new Rectangle(0, 0, 800, 600);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();
            
            snowFlakes = new List<Rectangle>();
            Rectangle tempSnowflake;
            // Creates a List of 50 snowflakes at random locations within the window
            for (int i = 0; i < 50; i++)
            {
                tempSnowflake = new Rectangle(
                    generator.Next(window.Width),
                    generator.Next(window.Height),
                    6,
                    6);
                snowFlakes.Add(tempSnowflake);
            }
            fallSpeed = new Vector2(0, 2);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            snowTexture = Content.Load<Texture2D>("flake");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // Iterated through each snowflake
            for (int i = 0; i < snowFlakes.Count; i++)
            {
                // Moves the snowflake down
                snowFlakes[i] = new Rectangle(
                    snowFlakes[i].X + (int)fallSpeed.X,
                    snowFlakes[i].Y + (int)fallSpeed.Y,
                    snowFlakes[i].Width, 
                    snowFlakes[i].Height);

                // Detects a snowflake falling off the screen and randomply puts it back on top
                if (snowFlakes[i].Y > window.Height)
                {
                    snowFlakes[i] = new Rectangle(
                    generator.Next(window.Width),
                    generator.Next(-10, 0),
                    6,
                    6);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            foreach(Rectangle snowFlake in snowFlakes)
                _spriteBatch.Draw(snowTexture, snowFlake, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
