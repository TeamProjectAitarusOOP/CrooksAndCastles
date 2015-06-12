using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrooksAndCastles.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using CrooksAndCastles.Characters.AI;
using CrooksAndCastles;
using CrooksAndCastles.Characters;

namespace CrooksAndCastles.Characters
{
    public class Enemy : Character
    {
        private float startPositionX;
        private float startPositionY;
        private Vector2 EnemyTextureCenter;
        private EnemyState enemyState = EnemyState.Chill;
        private float enemyOrientation;
        public const float EnemyHitDistance = 10.0f;
        public const float EnemyChaseDistance = 100.0f;
        public const float EnemyTurnSpeed = 2.0f;
        public const float MaxEnemySpeed = 0.7f;

        public Enemy(ContentManager content, string asset, float frameSpeed, int numberOfFrames, bool looping, Vector2 position, int level)
            : base(content, asset, frameSpeed, numberOfFrames, looping, level)
        {
            this.Position = position;
            this.startPositionX = position.X;
            this.startPositionY = position.Y;
        }

        public override Vector2 Position { get; set; }

        public void Awareness()
        {
            float distanceFromMainCharacter = Vector2.Distance(this.Position, CrooksAndCastles.Hero.Position);

            //Changing states according to distance between enemy and hero
            if (distanceFromMainCharacter < EnemyChaseDistance)
            {
                this.enemyState = EnemyState.Chasing;
            }
            else if (distanceFromMainCharacter < EnemyHitDistance)
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
                if (CrooksAndCastles.Hero.Position.X > this.startPositionX)
                {
                    this.ChangeAsset(this.Content, "EnemyOneRight", 4);
                }
                else
                {
                    this.ChangeAsset(this.Content, "EnemyOneLeft", 4);
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
                    this.ChangeAsset(this.Content, "EnemyOneLeft", 4);
                }
                else
                {
                    this.ChangeAsset(this.Content, "EnemyOneRight", 4);
                }
                if (((this.startPositionX - this.Position.X) * (this.startPositionX - this.Position.X) +
                    (this.startPositionY - this.Position.Y) * (this.startPositionY - this.Position.Y)) <=
                    0.2 * 0.2)
                {
                    this.ChangeAsset(this.Content, "EnemyOneRight", 1);
                    currentEnemySpeed = 0;
                }
            }
            else
            {
                //TODO HIT CHARAPTER
                currentEnemySpeed = 0;
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
    }
}
