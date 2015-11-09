using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pathfinder
{
    class ScentmapBot : AiBotBase
    {

        public ScentmapBot(int x, int y)
            : base(x, y)
        {

        }
        protected override void ChooseNextGridLocation(Level level, Player plr)
        {
            Coord2 CurrentPos;
            CurrentPos = GridPosition;

            //checks bot surrounding nodes.
            if(GridPosition != plr.GridPosition)
            {
                for (int f = -1; f <= 1; f++)
                {
                    for (int g = -1; g <= 1; g++)
                    {
                        //changes bot position.
                        if (level.scentmap.buffer1[GridPosition.X + f, GridPosition.Y + g] > level.scentmap.buffer1[GridPosition.X, GridPosition.Y])
                        {
                            CurrentPos = new Coord2(GridPosition.X + f, GridPosition.Y + g);
                            level.path_count ++;
                        }
                        SetNextGridPosition(CurrentPos, level);
                    }
                }
            }
        }
    }
}
