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
    public class MainCharacter : Character, IMovabble,IAttack
    {
        public MainCharacter(ContentManager content, string asset, float frameSpeed, int numberOfFrames, bool looping)
            : base(content, asset, frameSpeed, numberOfFrames, looping)
        {
            this.Position = new Vector2(CrooksAndCastles.WindowWidth / 2, CrooksAndCastles.WindowHeight / 2);
            this.Health = 1000;
            this.Damage = 30;
            this.IsAlive = true;
        }
        
        ////////// PROPERTIS //////////
        public override Vector2 Position { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public bool IsAlive { get; set; }
       
        ///////////// METHODS /////////////
        public void MoveUp()
        {
            if (IsAlive)
            {
                float x = this.Position.X;
                float y = this.Position.Y;
                y -= 2f;
                if (y < CrooksAndCastles.MenuHeight)
                {
                    y = CrooksAndCastles.MenuHeight;
                }
                this.Position = new Vector2(x, y);
            }
        }
        public void MoveDown()
        {
            if (IsAlive)
            {
                float x = this.Position.X;
                float y = this.Position.Y;
                y += 2f;
                if (y > CrooksAndCastles.WindowHeight - CrooksAndCastles.MenuHeight)
                {
                    y = CrooksAndCastles.WindowHeight - CrooksAndCastles.MenuHeight;
                }
                this.Position = new Vector2(x, y);
            }
        }
        public void MoveRight()
        {
            if (IsAlive)
            {
                float x = this.Position.X;
                float y = this.Position.Y;
                x += 2f;
                if (x > CrooksAndCastles.WindowWidth - CrooksAndCastles.MenuHeight)
                {
                    x = CrooksAndCastles.WindowWidth - CrooksAndCastles.MenuHeight;
                }
                this.Position = new Vector2(x, y);
            }
        }
        public void MoveLeft()
        {
            if (IsAlive)
            {
                float x = this.Position.X;
                float y = this.Position.Y;
                x -= 2f;
                if (x < 0)
                {
                    x = 0;
                }
                this.Position = new Vector2(x, y);  
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
            {
                base.Draw(spriteBatch);
            }
        }

        public void Attack(Character unit)
        {
            throw new NotImplementedException();
        }
    }
}
