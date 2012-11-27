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
        List<Block> blocks;

        public Level(Texture2D block_cloud, Texture2D block_grass, Texture2D green_block, string[] s)
        {
            
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
                        Block block = new Block(rect, block_grass);
                        blocks.Add(block);
                    }
                    if (c == 'G')
                    {
                        Block block = new Block(rect, green_block);
                        blocks.Add(block);
                    }
                    x += 40;
                }
                x = 0;
                y += 20;
            }
        }


        public List<Block> Blocks
        {
            get { return blocks; }
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
