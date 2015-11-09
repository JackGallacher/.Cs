using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using System.Diagnostics;

namespace Pathfinder
{
    class Dijkstra
    {
        //path variables.
        public bool[,] closed;
        public float[,] cost;
        public Coord2[,] link;
        public bool[,] inPath;
        public bool dijkstra_built = false;

        //path nodes
        public List<Coord2> dijkstra_path;

        //timer variable.
        public float elapsedMS;

        //stores node count.
        public int isClosed;

        //bools for algorithm toggling.
        public bool astar_is_built = false;
        public bool dijkstra_is_built = false;

        //position of the closed node.
        Coord2 nextclosed;

        //sets map size.
        public int map_size = 160;

        //constructor.
        public Dijkstra()
        {
            closed = new bool[map_size, map_size];
            cost = new float[map_size, map_size];
            link = new Coord2[map_size, map_size];
            inPath = new bool[map_size, map_size];
            dijkstra_path = new List<Coord2>();
        }

        //generates path.
        public void Build(Level level, AiBotBase bot, Player plr)
        {
            //starts timing the algorithm.
            var watch = Stopwatch.StartNew();

            dijkstra_is_built = true;
            dijkstra_built = true;

            //fill nodes with default value.
            for (int i = 0; i < map_size; i++)
            {
                for (int j = 0; j < map_size; j++)
                {
                    closed[i, j] = false;
                    cost[i, j] = float.MaxValue;
                    link[i, j] = new Coord2(-1, -1);
                    inPath[i, j] = false;
                }
            }

            Coord2 lowest_location = new Coord2 (-1, -1);
            float lowest_cost = float.MaxValue;

            closed[bot.GridPosition.X, bot.GridPosition.Y] = false;
            cost[bot.GridPosition.X, bot.GridPosition.Y] = 0;

            while (!closed[plr.GridPosition.X, plr.GridPosition.Y])
            {
                lowest_cost = float.MaxValue;
                for (int x = 0; x < map_size; x++)
                {
                    for (int y = 0; y < map_size; y++)
                    {
                        //sets new lowest cost.
                        if(cost[x,y] <= lowest_cost && !closed[x, y])
                        {
                            lowest_cost = cost[x, y];
                            lowest_location = new Coord2(x, y);                   
                        }
                    }
                }
                //closes that node.
                closed[lowest_location.X, lowest_location.Y] = true;

                //counts nodes closed.
                if(closed[lowest_location.X, lowest_location.Y] == true)
                {
                    isClosed ++;
                }

                for (int f = -1; f <= 1; f++)
                {
                    for (int g = -1; g <= 1; g++)
                    {
                        //assigns a cost to each location surrounding the current path node.
                        if(level.ValidPosition(new Coord2(lowest_location.X + f, lowest_location.Y + g)))
                        {
                            float newcost = float.MaxValue;

                            if(f != 0 ^ g != 0)
                            {
                                //horizontal cost.
                                newcost = lowest_cost + 1f;
                            }
                            else if (f != 0 && g != 0)
                            {
                                //diagonal cost.
                                newcost = lowest_cost + 1.4f;
                            }
                            if(newcost < cost[lowest_location.X + f, lowest_location.Y + g] && level.ValidPosition(new Coord2(lowest_location.X + f, lowest_location.Y + g)))
                            {
                                cost[lowest_location.X + f, lowest_location.Y + g] = newcost;//assigns current cost to the new cost.
                                link[lowest_location.X + f, lowest_location.Y + g] = new Coord2(lowest_location.X, lowest_location.Y);//assigns current link to lowest location.
                            }
                        }
                    }
                }
            }

            bool done = false;
            nextclosed = plr.GridPosition;
            while (done == false)
            {
                //adds next step to path.
                dijkstra_path.Add(nextclosed);
                inPath[nextclosed.X, nextclosed.Y] = true;
                nextclosed = link[nextclosed.X, nextclosed.Y];
                if (nextclosed == bot.GridPosition)
                {
                    dijkstra_path.Add(bot.GridPosition);//adds path to the bot.
                    done = true;
                }                   
            }
            dijkstra_path.Reverse();//reverse path for reading.

            elapsedMS = watch.ElapsedMilliseconds;
        }

        //resets algorithm.
        public void reset_path()
        {
            nextclosed = (new Coord2(0,0));
            for (int i = 0; i < map_size; i++)
            {
                for (int j = 0; j < map_size; j++)
                {
                    closed[i, j] = false;
                    cost[i, j] = 999999;
                    link[i, j] = new Coord2(-1, -1);
                    inPath[i, j] = false;
                    isClosed = 0;
                }
            }
        }
    }
}
