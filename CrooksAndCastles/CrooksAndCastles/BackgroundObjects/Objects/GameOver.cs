using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CrooksAndCastles.BackgroundObjects.Objects
{
    class GameOver:BackgroundObject
    {
        public GameOver(ContentManager content, string asset, Rectangle baseRectangle) : base(content, asset, baseRectangle)
        {
        }
    }
}
