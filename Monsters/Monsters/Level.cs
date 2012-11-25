using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monsters
{
    class Level
    {
        int currentLevel = 1;
        List<Block> blocks;
        Texture2D block_cloud;


        public Level(Texture2D block_cloud, string[] s)
        {
            currentLevel++;
            if (currentLevel > 3)
                currentLevel = 1;
            blocks = new List<Block>();
            

            int x = 0;
            int y = 0;
            foreach (string str in s)
            {
                foreach (char c in str)
                {
                    Rectangle rect = new Rectangle(x, y, 40, 40);
                    if (c == 'X')
                    {

                        Block block = new Block(rect, block_cloud);
                        blocks.Add(block);
                    }
                    if (c == 'Y')
                    {

                    }
                    x += 40;
                }
                x = 0;
                y += 40;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (Block block in blocks)
            {
                block.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
