using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monsters
{
    public class Nemo
    {
        public Rectangle rect;
        Texture2D run;
        Texture2D idle;
        Texture2D jumpInPlace;
        Texture2D shord;

        Game1 game;

        bool isRunning = false;
        bool isRunningRight = false;
        bool isJumping = false;
        bool fightWithShord = false;

        float yVelocity;
        float maxYVelocity = 8;
        float g = 0.2f;

        int frameWidth;
        int frameHeight;
                    

        KeyboardState oldKeyboardState; 

        public int Frames(int _width)
        {           
                return _width / frameWidth;
        }

        int currentFrame;
        int timeElapsed;
        int timeForFrame = 100;

        public Nemo(Rectangle rect, Texture2D idle, Texture2D run, Texture2D jumpInPlace, Texture2D shord, Game1 game)
        {
            this.rect = rect;
            this.idle = idle;
            this.run = run;
            this.jumpInPlace = jumpInPlace;
            this.shord = shord;
            this.game = game;
            

            frameHeight = frameWidth = run.Height;

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

        public void Jump()
        {
            if (!isJumping && yVelocity == 0.0f)
            {
                isJumping = true;
                currentFrame = 0;
                timeElapsed = 0;
                yVelocity = maxYVelocity;
            }
        }

        public void ApplyGravity(GameTime gameTime)
        {
            yVelocity = yVelocity - g * gameTime.ElapsedGameTime.Milliseconds / 10;
            float dy = yVelocity * gameTime.ElapsedGameTime.Milliseconds / 10;

            Rectangle nextPosition = rect;
            nextPosition.Offset(0, -(int)dy);

            Rectangle boudingRect = GetBoundingRect(nextPosition);

            if (boudingRect.Top > 0 && boudingRect.Bottom < game.WindowHeight && !game.CollidesWithLevel(boudingRect))
            {
                rect = nextPosition;
            }

            bool collideOnFallDown = (game.CollidesWithLevel(boudingRect) && yVelocity < 0);
            if (boudingRect.Bottom > game.WindowHeight || collideOnFallDown)
            {
                yVelocity = 0;
                isJumping = false;
            }
        }

        public void KeyControl(KeyboardState keyboardState)
        {

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                Jump();
            }

            if (keyboardState.IsKeyDown(Keys.S) && oldKeyboardState.IsKeyDown(Keys.S))
            {
                fightWithShord = true;
                isRunning = false;
            }


            if (keyboardState.IsKeyDown(Keys.Left) && oldKeyboardState.IsKeyDown(Keys.Left))
            {
                StartRun(true);
                isRunningRight = false;
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Right) && oldKeyboardState.IsKeyDown(Keys.Right))
                {
                    StartRun(true);
                    isRunningRight = true;
                }
                else
                {
                    StopRun();
                }
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
                currentFrame = (currentFrame + 1) % Frames(run.Width);
                timeElapsed = 0;
            }
            if (isRunning)
            {
                int dx = 1 * gameTime.ElapsedGameTime.Milliseconds / 7;
                if (!isRunningRight)
                {
                    dx = -dx;
                }

                Rectangle nextPosition = rect;
                nextPosition.Offset(dx, 0);
               
                Rectangle boudingRect = GetBoundingRect(nextPosition);
                Rectangle screenRect = Level.GetScreenRect(boudingRect);

                if (screenRect.Left > 0 && screenRect.Right < game.WindowWidth) //border control
                {
                    rect = nextPosition;
                }

                
            }
            ApplyGravity(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle r = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            SpriteEffects effects = SpriteEffects.None;
            if (!isRunningRight)
            {
                effects = SpriteEffects.FlipHorizontally;  //flip image to change the way
            }
            Rectangle screenRect = Level.GetScreenRect(rect);
            spriteBatch.Begin();
           
            if (isJumping)
            {
                spriteBatch.Draw(jumpInPlace, screenRect, r, Color.White, 0, Vector2.Zero, effects, 0);
            }
            else
            {
                if (fightWithShord)
                {
                    frameHeight = shord.Height;
                    Rectangle rs = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
                    spriteBatch.Draw(jumpInPlace, screenRect, rs, Color.White, 0, Vector2.Zero, effects, 0);
                    fightWithShord = false;
                }
                else
                {

                    {
                        if (isRunning)
                        {
                            frameHeight = run.Height;
                            spriteBatch.Draw(run, screenRect, r, Color.White, 0, Vector2.Zero, effects, 0);
                        }
                        else
                        {
                            spriteBatch.Draw(idle, screenRect, Color.White);
                        }
                    }
                }
            }
            spriteBatch.End();
        }
    }
}
