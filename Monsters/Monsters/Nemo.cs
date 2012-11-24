using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monsters
{
    class Nemo
    {
        Rectangle rect;
        Texture2D run;
        Texture2D idle;
        Texture2D creep;

        bool isRunning;
        bool isRunningRight;
        bool isCreep;

        int frameWidth;
        int frameHeight;

        public int Frames
        {
            get
            {
                return creep.Width / frameWidth;
            }
        }

        int currentFrame;
        int timeElapsed;
        int timeForFrame = 100;

        public Nemo(Rectangle rect, Texture2D idle, Texture2D run, Texture2D creep)
        {
            this.rect = rect;
            this.idle = idle;
            this.creep = creep;
            this.run = run;
            

            frameWidth = frameHeight = creep.Height;
        }

        public void StartRun(bool isRight)
        {
            isRunning = true;
            isRunningRight = isRight;
        }

        public void StopRun()
        {
            isRunning = false;
            currentFrame = 0;
            timeElapsed = 0;

        }

        public void KeyControl(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                StartRun(false);

            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                StartRun(true);
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                isCreep = true;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (isRunning)
            {
                timeElapsed += gameTime.ElapsedGameTime.Milliseconds;
                if (timeElapsed > timeForFrame)
                {
                    currentFrame = (currentFrame + 1) % Frames;
                    timeElapsed = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (isRunning)
            {
                Rectangle r = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
                SpriteEffects effects = SpriteEffects.None;
                if (!isRunningRight)
                {
                    effects = SpriteEffects.FlipHorizontally;
                }
                if (isCreep)
                {
                    spriteBatch.Draw(creep, rect, r, Color.White, 0, Vector2.Zero, effects, 0);
                }
                else
                {
                    spriteBatch.Draw(run, rect, r, Color.White, 0, Vector2.Zero, effects, 0);
                }            
                
                
            }
            else
            if (!isRunning)
            {
                spriteBatch.Draw(idle, rect, Color.White);
            }
            spriteBatch.End();
        }
    }
}
