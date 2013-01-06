﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monsters
{
    public class Block
    {
        public Rectangle rect;
        Texture2D texture;

        public Block(Rectangle rect, Texture2D texture)
        {
            this.rect = rect;
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle screenRect = Level.GetScreenRect(rect);
            spriteBatch.Draw(texture, screenRect, Color.White);
        }
    }
}
