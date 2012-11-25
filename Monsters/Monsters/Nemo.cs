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
        Texture2D creep_idle;

        bool isRunning = false;
        bool isRunningRight = false;
        bool isCreep = false;

        int frameWidth;
        int frameHeight;

        KeyboardState oldKeyboardState; 

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

        public Nemo(Rectangle rect, Texture2D idle, Texture2D run, Texture2D creep, Texture2D creep_idle)
        {
            this.rect = rect;
            this.idle = idle;
            this.creep = creep;
            this.run = run;
            this.creep_idle = creep_idle;

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
            if (keyboardState.IsKeyDown(Keys.Left) && oldKeyboardState.IsKeyDown(Keys.Left))
            {
                StartRun(true);
                isRunningRight = false;
            }else
                if (keyboardState.IsKeyDown(Keys.Right) && oldKeyboardState.IsKeyDown(Keys.Right))
                {
                    StartRun(true);
                    isRunningRight = true;
                }
                    else
                    {
                        StopRun();
                    }

            

            if (keyboardState.IsKeyDown(Keys.Down) && oldKeyboardState.IsKeyDown(Keys.Down))
            {
                isCreep = true;
                
            }
            else
            {
                isCreep = false;
            }

            oldKeyboardState = keyboardState;
        }

        private Rectangle GetBoundingRect(Rectangle rectangle)
        {
            int width = (int)(frameWidth * 0.4f);
            int x = rectangle.Left + (int)(frameWidth * 0.2f);

            return new Rectangle(x, rectangle.Top, width, rectangle.Height);
        }

        public void Update(GameTime gameTime)
        {
            timeElapsed += gameTime.ElapsedGameTime.Milliseconds;
            int tempTime = timeForFrame;
            if (timeElapsed > timeForFrame)
            {
                currentFrame = (currentFrame + 1) % Frames;
                timeElapsed = 0;
            }
            if (isRunning)
            {
                int dx = 1 * gameTime.ElapsedGameTime.Milliseconds / 10;
                if (!isRunningRight)
                {
                    dx = -dx;
                }

                Rectangle nextPosition = rect;
                nextPosition.Offset(dx, 0);
                rect = nextPosition;
               // Rectangle boudingRect = GetBoundingRect(nextPosition); 

                
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            Rectangle r = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            SpriteEffects effects = SpriteEffects.None;
            if (isRunning)
            {
                if (!isRunningRight)
                {
                    effects = SpriteEffects.FlipHorizontally;
                }
                
                if(isCreep)
                {
                    spriteBatch.Draw(creep, rect, r, Color.White, 0, Vector2.Zero, effects, 0);
                }
                if (!isCreep)
                {
                    spriteBatch.Draw(run, rect, r, Color.White, 0, Vector2.Zero, effects, 0);
                }

                
            }
            else
                if (isCreep && !isRunning)
                {
                    spriteBatch.Draw(creep_idle, rect, r, Color.White, 0, Vector2.Zero, effects, 0);
                }else
                    if (!isRunning)
                    {
                         spriteBatch.Draw(idle, rect, Color.White);
                    }
            spriteBatch.End();
        }
    }
}
