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
    public class MainCharacter:IDraw,IAnimation,IMovabble
    {

        /////// ANIMATION VARIABLES ///////

        private Rectangle sourceRectangle; //Base Bounderies
        private Vector2 position = new Vector2(0f, 175f); // Start possition
        private float elapsed; //elapse time
        private int currentFrame; // current frame
        private int wigdth;
        private int height;
        private string asset; // asset

        public MainCharacter(ContentManager content, string asset, float frameSpeed, int numberOfFrames, bool looping)
        {
            this.FrameTime = frameSpeed; // frame speed
            this.NumberOfFrames = numberOfFrames; // numbers of frames in sprite animation
            this.Looping = looping; //loopin bool
            this.Character = content.Load<Texture2D>(asset); // load texture
            this.FrameWidth = (Character.Width / this.NumberOfFrames); // calculate frame in asset
            this.FrameHeight = (Character.Height); // frame hight base on charapter hight
        }

        ////////// PROPERTIS //////////
        public int FrameWidth { get; set; }
        public int FrameHeight { get; set; }
        public float FrameTime { get; set; }
        public int NumberOfFrames { get; set; }
        public bool Looping { get; set; }
        public Texture2D Character { get; set; }

        ///////////// METHODS /////////////
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Character, this.position, this.sourceRectangle, Color.White);
        }
        public void playCharapterAnimation(GameTime gameTime)
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
            this.Character = content.Load<Texture2D>(asset);
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
            if (y > CrooksAndCastles.WindowHeight - 50)
            {
                y = CrooksAndCastles.WindowHeight - 50;
            }
            this.position = new Vector2(x, y);
        }
        public void MoveRight()
        {
            float x = this.position.X;
            float y = this.position.Y;
            x += 2f;
            if (x > CrooksAndCastles.WindowWidth - 50)
            {
                x = CrooksAndCastles.WindowWidth - 50;
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
