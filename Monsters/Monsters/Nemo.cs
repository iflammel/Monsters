using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monsters
{
    class Nemo
    {
        Rectangle rect;
        Texture2D run;
        Texture2D idle;

        bool isRunning = true;

        int frameWidth;
        int frameHeight;

        public int Frames
        {
            get
            {
                return run.Width / frameWidth;
            }
        }

        int currentFrame;
        int timeElapsed;
        int timeForFrame = 100;

        public Nemo(Rectangle rect, Texture2D idle, Texture2D run)
        {
            this.rect = rect;
            this.idle = idle;
            this.run = run;

            frameWidth = frameHeight = run.Height;
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
            }
            else
            {
                spriteBatch.Draw(idle, rect, Color.White);
            }
            spriteBatch.End();
        }
    }
}
