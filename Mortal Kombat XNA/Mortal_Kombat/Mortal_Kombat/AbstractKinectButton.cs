using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Kinect;

namespace Mortal_Kombat
{
    abstract class AbstractKinectButton
    {
        private bool enteredHotArea;

        public delegate void KinectButtonClickedHandler();

        public event KinectButtonClickedHandler KinectButtonClicked;

        protected Texture2D buttonImage;
        protected SelectAnimation selectAnimation;

        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        protected Rectangle hotArea;
        public Rectangle HotArea
        {
            get { return hotArea; }
            set { hotArea = value; }
        }

        protected JointType clientJoint;
        public JointType ClientJoint
        {
            get { return clientJoint; }
            set { clientJoint = value; }
        }


        public AbstractKinectButton(Texture2D buttonImage, Vector2 position, SelectAnimation selectAnimation)
        {
            this.buttonImage = buttonImage;
            this.position = position;
            this.selectAnimation = selectAnimation;

            hotArea = new Rectangle((int)position.X, (int)position.Y, buttonImage.Width, buttonImage.Height);
        }

        public void RegisterClientJoint(JointType clientJoint)
        {
            this.clientJoint = clientJoint;
        }

        public virtual void Update(GameTime gameTime, Skeleton skeleton)
        {
            if (skeleton == null)
                return;

            if (hotArea.Contains((int)skeleton.Joints[clientJoint].ScaleToScreen().Position.X, (int)skeleton.Joints[clientJoint].ScaleToScreen().Position.Y))
            {
                if (selectAnimation.AnimationState == AnimationStates.Ready && !enteredHotArea)
                {
                    selectAnimation.AnimationState = AnimationStates.Active;
                    enteredHotArea = true;
                }

                selectAnimation.Update(gameTime);                

                if (selectAnimation.AnimationState == AnimationStates.Done)
                {
                    if (KinectButtonClicked != null)
                        KinectButtonClicked();

                    selectAnimation.AnimationState = AnimationStates.Ready;
                }
            }
            else
            {
                if (selectAnimation.AnimationState == AnimationStates.Active)
                    selectAnimation.AnimationState = AnimationStates.Ready;

                enteredHotArea = false;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(buttonImage, position, Color.White);

            if (selectAnimation.AnimationState == AnimationStates.Active)
                selectAnimation.Draw(spriteBatch);
        }
    }
}
