using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.IO;


namespace Pathfinder
{
    class AiBotSimple: AiBotBase
    {
        public AiBotSimple(int x, int y) : base(x, y)
        {
        }

        protected override void ChooseNextGridLocation(Level level, Player plr)
        {
            //your code will go here.
            Coord2 position;
            position = GridPosition;

            if(plr.GridPosition.X < position.X)//if player position X less than bot position X move back on the X axis
            {
                //position.X -= 1;
            }
            if (plr.GridPosition.X > position.X)//if player position X greater then bot position X move forward on the X axis.
            {
                //position.X += 1;
            }
            if (plr.GridPosition.Y < position.Y)//if player position Y less than bot position Y move down on the Y axis.
            {
                //position.Y -= 1;
            }
            if (plr.GridPosition.Y > position.Y)//if player position Y greater than bot position Y move up on the Y axis.
            {
                //position.Y += 1;
            }
            SetNextGridPosition(position, level);//sets position of bot.
            level.ValidPosition(position);//checks for walls.
        }
    }
}
