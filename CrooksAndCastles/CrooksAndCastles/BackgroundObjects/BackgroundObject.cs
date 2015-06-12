using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrooksAndCastles.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CrooksAndCastles.BackgroundObjects
{
    abstract class BackgroundObject:IDraw
    {
        private Texture2D objectTexture;
        private Rectangle baseRectangle;

        public BackgroundObject(ContentManager content, string asset, Rectangle baseRectangle)
        {
            this.Object = content.Load<Texture2D>(asset);
            this.BaseRectangle = baseRectangle;
        }

        public Texture2D Object { get; set; }
        public Rectangle BaseRectangle { get; set; }
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
