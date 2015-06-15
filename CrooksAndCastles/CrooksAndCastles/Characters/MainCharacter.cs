using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrooksAndCastles.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CrooksAndCastles.Characters
{
    public class MainCharacter : Character, IMovabble, ISkills
    {
        private readonly string AssetMoveUp;
        private readonly string AssetMoveDown;
        private const int baseDamage = 20;

        public MainCharacter(ContentManager content, string assetMoveLeft, string assetMoveRight, string assetHitLeft, string assetHitRight, float frameSpeed, int numberOfFrames, bool looping, int level, string assetMoveDown, string assetMoveUp)
            : base(content, assetMoveLeft, assetMoveRight, assetHitLeft, assetHitRight, frameSpeed, numberOfFrames, looping, level)
        {
            this.Position = new Vector2(CrooksAndCastles.WindowWidth / 2, CrooksAndCastles.WindowHeight / 2);
            this.Damage = baseDamage * level;
            this.IsAlive = true;
            this.AssetMoveUp = assetMoveUp;
            this.AssetMoveDown = assetMoveDown;
        }
        
        ////////// PROPERTIS //////////
        public override Vector2 Position { get; set; }
       
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
        public override void Awareness()
        {
            for (int index = 1; index < CrooksAndCastles.Units.Count; index++)
            {
                float distanceFromEnemy = Vector2.Distance(this.Position, CrooksAndCastles.Units[index].Position);
                if (distanceFromEnemy < HitDistance+20 && CrooksAndCastles.keyBoard.IsKeyDown(Keys.Space))
                {
                    if (this.Position.X > CrooksAndCastles.Units[index].Position.X)
                    {
                        this.ChangeAsset(this.Content, this.AssetHitLeft, 3);
                    }
                    else
                    {
                        this.ChangeAsset(this.Content, this.AssetHitRight, 3);
                    }
                    Attack(index);
                }
            }
        }
        protected void Attack(int index)
        {
            Enemy enemy = (Enemy)CrooksAndCastles.Units[index];
            enemy.Health -= this.Damage;
            if (enemy.Health <= 0)
            {
                this.ChangeAsset(this.Content, this.AssetMoveDown, 1);
                CrooksAndCastles.Units.Remove(enemy);
            }
        }
    }
}
