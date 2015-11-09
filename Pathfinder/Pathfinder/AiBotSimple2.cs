using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pathfinder
{
    class AiBotSimple2 : AiBotBase
    {
        public AiBotSimple2(int x, int y) : base(x, y)
        {
        }

        protected override void ChooseNextGridLocation(Level level, Player plr)
        {
            Coord2 position;
            position = GridPosition;

            if (plr.GridPosition.X < position.X)
            {
                position.X -= 1;
            }
            if (plr.GridPosition.X > position.X)
            {
                position.X += 1;
            }
            if (plr.GridPosition.Y < position.Y)
            {
                position.Y -= 1;
            }
            if (plr.GridPosition.Y > position.Y)
            {
                position.Y += 1;
            }

            if (!level.ValidPosition(position))
            {
                if (plr.GridPosition.Y < position.Y)
                {
                    position = GridPosition;
                    position.Y -= 1;
                    SetNextGridPosition(position, level);
                }
                if (plr.GridPosition.Y > position.Y)
                {
                    position = GridPosition;
                    position.Y += 1;
                    SetNextGridPosition(position, level);
                }
                if (plr.GridPosition.X > position.X)
                {
                    position = GridPosition;
                    position.X -= 1;
                    SetNextGridPosition(position, level);
                }
                if (plr.GridPosition.X < position.X)
                {
                    position = GridPosition;
                    position.X += 1;
                    SetNextGridPosition(position, level);
                }
            }
            SetNextGridPosition(position, level);//sets position of bot.
        }
    }
}
