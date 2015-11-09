//animated sprite class.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mortal_Kombat
{
    public class AnimatedSprite
    {
        Dictionary<AnimationKey, Animation> animations;
        AnimationKey currentAnimation;
        bool isAnimating;//checks if animating or not.

        Texture2D texture;//sets texture.
        public Vector2 position;//sets position.
        Vector2 velocity;//sets velocity.
        float speed = 4.0f;//sets speed.

        float width;
        float height;

        public Rectangle boundingbox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)width - 30, (int)height);//minus the values in here to make the bounding box smaller.
            }
        }

        public Rectangle attackbox //create another bounding box which checks if characters are close enough attack eather other, make larger than original box (make bounding box smaller to make them overlap.) 
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)width - 10, (int)height);
            }
        }
        
        

        public AnimationKey CurrentAnimation//checks current animation.
        {
            get { return currentAnimation; }
            set { currentAnimation = value; }
        }
        public bool IsAnimating//checks if animating.
        {
            get { return isAnimating; }
            set { isAnimating = value;}
        }
        public int Width//checks frame width.
        {
            get { return animations[currentAnimation].FrameWidth; }
        }
        public int Height//checks frame height.
        {
            get { return animations[currentAnimation].FrameHeight; }
        }
        public float Speed//checks sprite speed.
        {
            get { return speed; }
            set { speed = MathHelper.Clamp(speed, 1.0f, 16.0f); }
        }
        public Vector2 Position//checks sprite position.
        {
            get { return position; }
            set
            {
                position.X = MathHelper.Clamp(position.X, 0, (900 - (width * 2)));//value; 
                position.Y = MathHelper.Clamp(position.Y, 0, (600 - (height * 2)));//value;
            }          
        }
        public Vector2 Velocity//checks sprite velocity.
        {
            get { return velocity; }
            set
            {
                velocity = value;
                if (velocity != Vector2.Zero)
                    velocity.Normalize();
            }
        }
        public AnimatedSprite(Texture2D sprite, Dictionary<AnimationKey, Animation> animation, float width, float height)//sets sprite animation to frame.
        {
            this.width = width;
            this.height = height;

            texture = sprite;
            animations = new Dictionary<AnimationKey, Animation>();

            foreach (AnimationKey key in animation.Keys)
                animations.Add(key, (Animation)animation[key].Clone());
        }
        public void Update(GameTime gameTime)//update sprite animation.
        {
            if (isAnimating)
                animations[currentAnimation].Update(gameTime);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)//draws sprite animation.
        {
            spriteBatch.Draw(texture, position, animations[currentAnimation].CurrentFrameRect, Color.White);
        }
    }
}
