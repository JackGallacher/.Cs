using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Pathfinder
{
    class DijkstraBot : AiBotBase
    {
        int index;

        public DijkstraBot(int x, int y)
            : base(x, y)
        {
            index = 0;
        }
        protected override void ChooseNextGridLocation(Level level, Player plr)
        {
            Coord2 CurrentPos;
            if(level.build_dijkstra == true)
            {
                CurrentPos = GridPosition;

                //sets new position for the bot.
                if(GridPosition != plr.GridPosition)
                {
                    CurrentPos = level.dijkstra.dijkstra_path[index];
                }
                SetNextGridPosition(CurrentPos, level);
                index++;
            }


        }
    }
}

