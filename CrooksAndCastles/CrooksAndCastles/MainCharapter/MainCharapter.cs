using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrooksAndCastles.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CrooksAndCastles.AnimatedCharapters
{
    public class MainCharapter:IDraw,IAnimation,IMovabble
    {

        /////// ANIMATION VARIABLES ///////
        
        private Texture2D charapter; //Charapter textures
        private Rectangle sourceRectangle; //Base Bounderies
        private Vector2 position = new Vector2(0f, 175f); // Start possition
        private float elapsed; //elapse time
        private float frametime; // frame time
        private int numberOfFrames; // number of animations
        private int currentFrame; // current frame
        private int wigdth;
        private int height;
        private int frameWigdht;
        private int frameHeight;
        private bool looping; //Looping Animation
        private string asset; // asset

        public MainCharapter(ContentManager content, string asset, float frameSpeed, int numberOfFrames, bool looping)
        {
            this.FrameTime = frameSpeed; // frame speed
            this.NumberOfFrames = numberOfFrames; // numbers of frames in sprite animation
            this.Looping = looping; //loopin bool
            this.Charapter = content.Load<Texture2D>(asset); // load texture
            this.FrameWigdht = (Charapter.Width / numberOfFrames); // calculate frame in asset
            this.frameHeight = (Charapter.Height); // frame hight base on charapter hight
        }

        ////////// PROPERTIS //////////
        public int FrameWigdht
        {
            get { return this.frameWigdht; }
            set { this.frameWigdht = value; }
        }
        public float FrameTime { get; set; }
        public int NumberOfFrames { get; set; }
        public bool Looping { get; set; }
        public Texture2D Charapter { get; set; }

        ///////////// METHODS /////////////
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Charapter, this.position, this.sourceRectangle, Color.White);
        }
        public void playCharapterAnimation(GameTime gameTime)
        {
            this.elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            this.sourceRectangle = new Rectangle(currentFrame * frameWigdht, 0, frameWigdht, frameHeight);
            if (elapsed >= FrameTime)
            {
                if (currentFrame >= NumberOfFrames - 1)
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
            this.Charapter = content.Load<Texture2D>(asset);
            this.NumberOfFrames = numberOfFrames;
        }
        public void MoveUp()
        {
            float x = this.position.X;
            float y = this.position.Y;
            y -= 2f;
            if (y < 50)
            {
                y = 50;
            }
            this.position = new Vector2(x, y);
        }
        public void MoveDown()
        {
            float x = this.position.X;
            float y = this.position.Y;
            y += 2f;
            if (y > 410)
            {
                y = 410;
            }
            this.position = new Vector2(x, y);
        }
        public void MoveRight()
        {
            float x = this.position.X;
            float y = this.position.Y;
            x += 2f;
            if (x > 750)
            {
                x = 750;
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
