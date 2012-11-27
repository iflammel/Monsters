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
        Game1 game;

        bool isRunning = false;
        bool isRunningRight = false;
        bool isCreep = false;

        float yVelocity;
        float maxYVelocity = 10;
        float g = 0.2f;

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

        public Nemo(Rectangle rect, Texture2D idle, Texture2D run, Texture2D creep, Texture2D creep_idle, Game1 game)
        {
            this.rect = rect;
            this.idle = idle;
            this.creep = creep;
            this.run = run;
            this.creep_idle = creep_idle;
            this.game = game;

            frameWidth = frameHeight = creep.Height;
        }

        public void StartRun(bool isRight)
        {         
            isRunning = true;
            isRunningRight = isRight;
        }

        public void StopRun()
        {
            currentFrame = 0;
            timeElapsed = 0;
            isRunning = false;
        }

        public void ApplyGravity(GameTime gameTime)
        {
            yVelocity = yVelocity - g * gameTime.ElapsedGameTime.Milliseconds / 10;
            float dy = yVelocity * gameTime.ElapsedGameTime.Milliseconds / 10;

            Rectangle nextPosition = rect;
            nextPosition.Offset(0, -(int)dy);

            Rectangle boudingRect = GetBoundingRect(nextPosition);

            if (boudingRect.Top > 0 && boudingRect.Bottom < game.WindowHeight)
            {
                rect = nextPosition;
            }

            if (boudingRect.Bottom > game.WindowHeight)
            {
                yVelocity = 0;
            }
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

        private Rectangle GetBoundingRect(Rectangle rectangle) //reduce the size of the frame, to control borders
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
               
                Rectangle boudingRect = GetBoundingRect(nextPosition);

                if (boudingRect.Left > 0 && boudingRect.Right < game.WindowWidth) //border control
                {
                    rect = nextPosition;
                }

                
            }
            ApplyGravity(gameTime);
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
                    effects = SpriteEffects.FlipHorizontally;  //flip image to change the way


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
