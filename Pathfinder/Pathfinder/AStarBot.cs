using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pathfinder
{
    class AStarBot : AiBotBase
    {
        int index;
              
        public AStarBot(int x, int y)
            : base(x, y)
        {
            index = 0; 
        }
        protected override void ChooseNextGridLocation(Level level, Player plr)
        {              
            Coord2 CurrentPos;
            if(level.build_astar == true)
            {
                CurrentPos = GridPosition;

                //sets new position for the bot.
                if (GridPosition != plr.GridPosition)
                {
                    CurrentPos = level.astar.astar_path[index];
                }
                SetNextGridPosition(CurrentPos, level);
                index++;
            }
        }
    }
}
