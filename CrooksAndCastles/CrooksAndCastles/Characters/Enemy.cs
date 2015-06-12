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
        private Vector2 enemyWanderDirection;
        public const float EnemyHitDistance = 10.0f;
        public const float EnemyChaseDistance = 100.0f;
        public const float EnemyTurnSpeed = 2.0f;
        public const float MaxEnemySpeed = 0.7f;
        public const float enemyHysteresis = 15.0f;

        public Enemy(ContentManager content, string asset, float frameSpeed, int numberOfFrames, bool looping, float x, float y)
            : base(content, asset, frameSpeed, numberOfFrames, looping)
        {
            this.Position = new Vector2(x, y);
            this.startPositionX = x;
            this.startPositionY = y;
        }

        public override Vector2 Position { get; set; }

        public void Awareness()
        {
            float enemyChaseTreshold = EnemyChaseDistance;
            float enemyHitDistance = EnemyHitDistance;
            float distanceFromMainCharacter = Vector2.Distance(this.Position, CrooksAndCastles.Hero.Position);

            if (this.enemyState == EnemyState.Chill)
            {
                enemyChaseTreshold += enemyHysteresis / 2;
                enemyHitDistance -= enemyHysteresis / 2;
            }
            else if (enemyState == EnemyState.Chasing)
            {              
                enemyChaseTreshold -= enemyHysteresis / 2;
            }
            else if (this.enemyState == EnemyState.Caught)
            {
                enemyChaseTreshold += enemyHysteresis / 2;
            }

            //Changing states according to distance between enemy and hero
            if (distanceFromMainCharacter < enemyChaseTreshold)
            {
                this.enemyState = EnemyState.Chasing;
            }
            else if (distanceFromMainCharacter < enemyHitDistance)
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
                enemyOrientation = TurnToFace(this.Position, CrooksAndCastles.Hero.Position, enemyOrientation, EnemyTurnSpeed);
                currentEnemySpeed = MaxEnemySpeed;
            }
            else if (this.enemyState == EnemyState.Chill)
            {
                LookingForHero(this.Position, ref enemyWanderDirection, ref enemyOrientation, EnemyTurnSpeed);
                currentEnemySpeed = MaxEnemySpeed / 4.0f;
                this.Position = new Vector2(startPositionX, startPositionY);
                
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

        private void LookingForHero(Vector2 position, ref Vector2 wanderDirection, ref float orientation, float turnSpeed)
        {
            Random rand = new Random();
            wanderDirection.X += MathHelper.Lerp(-.25f, .25f, (float)rand.NextDouble());
            wanderDirection.Y += MathHelper.Lerp(-.25f, .25f, (float)rand.NextDouble());
            if (wanderDirection != Vector2.Zero)
            {
                wanderDirection.Normalize();
            }
            orientation = TurnToFace(position, position + wanderDirection, orientation, .15f * turnSpeed);

            Vector2 screenCenter = Vector2.Zero;
            screenCenter.X = CrooksAndCastles.Graphics.GraphicsDevice.Viewport.Width / 2;
            screenCenter.Y = CrooksAndCastles.Graphics.GraphicsDevice.Viewport.Height / 2;

            float distanceFromScreenCenter = Vector2.Distance(screenCenter, position);
            float MaxDistanceFromScreenCenter = Math.Min(screenCenter.Y, screenCenter.X);
            float normalizedDistance = distanceFromScreenCenter / MaxDistanceFromScreenCenter;
            float turnToCenterSpeed = .3f * normalizedDistance * normalizedDistance * turnSpeed;
            orientation = TurnToFace(position, screenCenter, orientation, turnToCenterSpeed);
        }
    }
}
