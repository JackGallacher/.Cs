//animation class.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Mortal_Kombat
{
    public enum AnimationKey {Left, Right, Duck, Idle, Punch, Kick, Block }//sets different movement type names.

    public class Animation : ICloneable
    {
        Rectangle[] frames;//rectangle class called frames.
        int framesPerSecond;//sets fps.
        TimeSpan frameLength;//sets length of frames.
        TimeSpan frameTimer;//sets time of each frame.
        int currentFrame;//sets current frame.
        int frameWidth;//sets frame width.
        int frameHeight;//sets frame height.

        //public int boundingheight;
        //public int boundingwidth;
           
        public int FramesPerSecond//allocates game fps.
        {
            get { return framesPerSecond; }
            set
            {
                if (value < 1)
                    framesPerSecond = 1;
                else if (value > 60)
                    framesPerSecond = 60;
                else
                    framesPerSecond = value;
                frameLength = TimeSpan.FromSeconds(1 / (double)framesPerSecond);
            }
        }
        public Rectangle CurrentFrameRect//sets current rectangle in the frame.
        {
            get { return frames[currentFrame]; }
        }
        public int CurrentFrame//sets current frame.
        {
            get { return currentFrame; }
            set
            {
                currentFrame = (int)MathHelper.Clamp(value, 0, frames.Length - 1);
            }
        }
        public int FrameWidth//sets frame width.
        {
            get { return frameWidth; }
        }
        public int FrameHeight//sets frame height.
        {
            get { return frameHeight; }
        }
        public Animation(int frameCount, int frameWidth, int frameHeight, int xOffset, int yOffset)//sets animation properties.
        {
            frames = new Rectangle[frameCount];
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;

            for (int i = 0; i < frameCount; i++)
            {
                frames[i] = new Rectangle(xOffset + (frameWidth * i), yOffset, frameWidth, frameHeight);
            }
            FramesPerSecond = 5;
            Reset();
        }
        private Animation(Animation animation)//set running speed of animations.
        {
            this.frames = animation.frames;
            FramesPerSecond = 15;
        }
        public void Update(GameTime gameTime)//update function for each animation.
        {
            frameTimer += gameTime.ElapsedGameTime;

            if (frameTimer >= frameLength)
            {
                frameTimer = TimeSpan.Zero;
                currentFrame = (currentFrame + 1) % frames.Length;
            }
        }
        public void Reset()//resets game timer.
        {
            currentFrame = 0;
            frameTimer = TimeSpan.Zero;
        }
        public object Clone()//allows cloning of sprite rectangle.
        {
            Animation animationClone = new Animation(this);

            animationClone.frameWidth = this.frameWidth;
            animationClone.frameHeight = this.frameHeight;
            animationClone.Reset();

            return animationClone;
        }
    }

}
