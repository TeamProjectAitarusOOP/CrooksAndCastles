using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using CrooksAndCastles.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using CrooksAndCastles.Characters.AI;
using CrooksAndCastles;
using CrooksAndCastles.Characters;
using System.Timers;

namespace CrooksAndCastles.Characters
{
    public class Enemy : Character
    {
        ////////// FIELDS //////////
        private static Random rand = new Random();
        private static Vector2 randomPosition;
        private float startPositionX;
        private float startPositionY;
        private EnemyState enemyState = EnemyState.Chill;
        private float enemyOrientation;
        public const float EnemyChaseDistance = 150.0f;
        public const float EnemyTurnSpeed = 2.0f;
        public const float MaxEnemySpeed = 0.7f;

        public Enemy(ContentManager content, string assetMoveLeft, string assetMoveRight, string assetHitLeft, string assetHitRight, float frameSpeed, int numberOfFrames, bool looping, Vector2 position, int level)
            : base(content, assetMoveLeft, assetMoveRight, assetHitLeft, assetHitRight, frameSpeed, numberOfFrames, looping, level)
        {
            this.Position = position;
            this.startPositionX = position.X;
            this.startPositionY = position.Y;
        }

        ////////// PROPERTIS //////////
        public override Vector2 Position { get; set; }
                
        ///////////// METHODS /////////////
        public override void Awareness()
        {
            float distanceFromMainCharacter = Vector2.Distance(this.Position, CrooksAndCastles.Hero.Position);

            //Changing states according to distance between enemy and hero
            if (distanceFromMainCharacter < EnemyChaseDistance && distanceFromMainCharacter > HitDistance)
            {
                this.enemyState = EnemyState.Chasing;
            }
            else if (distanceFromMainCharacter < HitDistance)
            {
                this.enemyState = EnemyState.Caught;
            }
            else
            {
                this.enemyState = EnemyState.Chill;
            }

            float currentEnemySpeed;
            if (this.enemyState == EnemyState.Chasing)
            {
                if (CrooksAndCastles.Hero.Position.X > this.Position.X)
                {
                    this.ChangeAsset(this.Content, this.AssetMoveRight, 4);
                }
                else
                {
                    this.ChangeAsset(this.Content, this.AssetMoveLeft, 4);
                }
                currentEnemySpeed = MaxEnemySpeed;
                enemyOrientation = TurnToFace(this.Position, CrooksAndCastles.Hero.Position, enemyOrientation, EnemyTurnSpeed);
            }
            else if (this.enemyState == EnemyState.Chill)
            {
                currentEnemySpeed = MaxEnemySpeed;
                enemyOrientation = TurnToFace(this.Position, new Vector2(this.startPositionX, this.startPositionY), enemyOrientation, EnemyTurnSpeed);
                if (CrooksAndCastles.Hero.Position.X > this.startPositionX)
                {
                    this.ChangeAsset(this.Content, this.AssetMoveLeft, 4);
                }
                else
                {
                    this.ChangeAsset(this.Content, this.AssetMoveRight, 4);
                }
                if (((this.startPositionX - this.Position.X) * (this.startPositionX - this.Position.X) +
                    (this.startPositionY - this.Position.Y) * (this.startPositionY - this.Position.Y)) <=
                    0.2 * 0.2)
                {
                    this.ChangeAsset(this.Content, this.AssetMoveRight, 1);
                    currentEnemySpeed = 0;
                }
            }
            else
            {
                if (CrooksAndCastles.Hero.Position.X > this.Position.X)
                {
                    this.ChangeAsset(this.Content, this.AssetHitRight, 2);
                }
                else
                {
                    this.ChangeAsset(this.Content, this.AssetHitLeft, 2);
                }
                currentEnemySpeed = 0;
                this.Attack();                
                
            }
            Vector2 heading = new Vector2((float)Math.Cos(enemyOrientation), (float)Math.Sin(enemyOrientation));
            this.Position += heading * currentEnemySpeed;
        }
        private static float TurnToFace(Vector2 position, Vector2 faceThis, float currentAngle, float turnSpeed)
        {
            float x = faceThis.X - position.X;
            float y = faceThis.Y - position.Y;
            float desiredAngle = (float)Math.Atan2(y, x);
            float difference = WrapAngle(desiredAngle - currentAngle);
            difference = MathHelper.Clamp(difference, -turnSpeed, turnSpeed);
            return WrapAngle(currentAngle + difference);
        }
        private static float WrapAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
            {
                base.Draw(spriteBatch);
            }
        }
        protected void Attack()
        {
            CrooksAndCastles.Hero.Health -= this.Damage;
            if (CrooksAndCastles.Hero.Health <= 0)
            {
                CrooksAndCastles.Hero.IsAlive = false;
            }
        }       
        internal static Vector2 GetRandomPosition()
        {
            float randomX = rand.Next(0, CrooksAndCastles.WindowWidth - CrooksAndCastles.MenuHeight);
            float randomY = rand.Next(CrooksAndCastles.MenuHeight, CrooksAndCastles.WindowHeight - CrooksAndCastles.MenuHeight);
            randomPosition = new Vector2(randomX, randomY);
            foreach (var unit in CrooksAndCastles.Units)
            {
                float distance = Vector2.Distance(unit.Position, randomPosition);
                if (distance <= EnemyChaseDistance)
                {
                    GetRandomPosition();
                }
            }
            return randomPosition;
        }     
    }
}
