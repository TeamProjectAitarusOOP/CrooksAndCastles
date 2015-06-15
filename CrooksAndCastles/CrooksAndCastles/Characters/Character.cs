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
    public abstract class Character : IDraw, IAnimation, ISkills
    {
        ////////// FIELDS //////////
        private Rectangle sourceRectangle; //Base Bounderies
        protected float elapsed; //elapse time
        private int currentFrame; // current frame
        protected readonly string AssetMoveLeft;
        protected readonly string AssetMoveRight;
        protected readonly string AssetHitLeft;
        protected readonly string AssetHitRight;
        protected const float HitDistance = 32.0f;

        public Character(ContentManager content, string assetMoveLeft, string assetMoveRight, string assetHitLeft, string assetHitRight, float frameSpeed, int numberOfFrames, bool looping, int level)
        {
            this.Content = content;
            this.FrameTime = frameSpeed; // frame speed
            this.NumberOfFrames = numberOfFrames; // numbers of frames in sprite animation
            this.Looping = looping; //loopin bool
            this.CharacterTexture = content.Load<Texture2D>(assetMoveRight); // load texture
            this.FrameWidth = (CharacterTexture.Width / this.NumberOfFrames); // calculate frame in asset
            this.FrameHeight = (CharacterTexture.Height); // frame hight base on charapter hight 
            this.Level = level;
            this.Damage = 1 * Level;
            this.Health = 1000 * level; 
            this.IsAlive = true;
            this.AssetMoveRight = assetMoveRight;
            this.AssetMoveLeft = assetMoveLeft;
            this.AssetHitLeft = assetHitLeft;
            this.AssetHitRight = assetHitRight;

        }

        ////////// PROPERTIS //////////
        public abstract Vector2 Position { get; set; }
        public int FrameWidth { get; set; }
        public int FrameHeight { get; set; }
        public float FrameTime { get; set; }
        public int NumberOfFrames { get; set; }
        public bool Looping { get; set; }
        public Texture2D CharacterTexture { get; set; }
        public ContentManager Content { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public bool IsAlive { get; set; }

        ///////////// METHODS /////////////
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.CharacterTexture, this.Position, this.sourceRectangle, Color.White);
        }
        public void playCharacterAnimation(GameTime gameTime)
        {
            this.elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            this.sourceRectangle = new Rectangle(currentFrame * this.FrameWidth, 0, this.FrameWidth, this.FrameHeight);
            if (elapsed >= this.FrameTime)
            {
                if (this.currentFrame >= this.NumberOfFrames - 1)
                {
                    if (Looping)
                    {
                        this.currentFrame = 0;
                    }
                }
                else
                {
                    this.currentFrame++;
                }
                this.elapsed = 0;
            }
        }
        public void ChangeAsset(ContentManager content, string asset, int numberOfFrames)
        {
            this.CharacterTexture = content.Load<Texture2D>(asset);
            this.NumberOfFrames = numberOfFrames;
        }
        public abstract void Awareness();
        
    }
}