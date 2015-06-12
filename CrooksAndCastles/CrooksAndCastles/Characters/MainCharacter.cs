using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrooksAndCastles.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CrooksAndCastles.Characters
{
    public class MainCharacter: Character, IMovabble
    {
        public MainCharacter(ContentManager content, string asset, float frameSpeed, int numberOfFrames, bool looping)
            : base(content, asset, frameSpeed, numberOfFrames, looping)
        {
            
        }
        public void MoveUp()
        {
            float x = this.position.X;
            float y = this.position.Y;
            y -= 2f;
            if (y < CrooksAndCastles.MenuHeight)
            {
                y = CrooksAndCastles.MenuHeight;
            }
            this.position = new Vector2(x, y);
        }
        public void MoveDown()
        {
            float x = this.position.X;
            float y = this.position.Y;
            y += 2f;
            if (y > CrooksAndCastles.WindowHeight - CrooksAndCastles.MenuHeight)
            {
                y = CrooksAndCastles.WindowHeight - CrooksAndCastles.MenuHeight;
            }
            this.position = new Vector2(x, y);
        }
        public void MoveRight()
        {
            float x = this.position.X;
            float y = this.position.Y;
            x += 2f;
            if (x > CrooksAndCastles.WindowWidth - CrooksAndCastles.MenuHeight)
            {
                x = CrooksAndCastles.WindowWidth - CrooksAndCastles.MenuHeight;
            }
            this.position = new Vector2(x, y);
        }
        public void MoveLeft()
        {
            float x = this.position.X;
            float y = this.position.Y;
            x -= 2f;
            if (x < 0)
            {
                x = 0;
            }
            this.position = new Vector2(x, y);
        }
    }
}
