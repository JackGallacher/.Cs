using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mortal_Kombat
{
    class KinectButton : AbstractKinectButton
    {
        public KinectButton(Texture2D buttonImage, Vector2 position, SelectAnimation selectAnimation)
            : base(buttonImage, position, selectAnimation)
        {
        }
    }
}
