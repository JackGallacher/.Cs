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
    class AStar
    {
        //path variables.
        public bool[,] closed;
        public float[,] cost;
        public Coord2[,] link;
        public bool[,] inPath;
        public bool astar_built = false;

        //path nodes and heuristic.
        public float[,] heuristic;
        public List<Coord2> astar_path;
        public List<Coord2> open_locations;

        //timer variable.
        public float elapsedMS;

        //stores node count.
        public int isClosed;

        //bools for algorithm toggling.
        public bool astar_is_built = false;
        public bool dijkstra_is_built = false;

        //position of the closed node.
        Coord2 nextclosed;

        //sets map size;
        public int map_size = 160;
       
        //constructor.
        public AStar()
        {
            closed = new bool[map_size, map_size];
            cost = new float[map_size, map_size];
            link = new Coord2[map_size, map_size];
            inPath = new bool[map_size, map_size];
            heuristic = new float[map_size, map_size];
            astar_path = new List<Coord2>();
            open_locations = new List<Coord2>();
        }

        //generates path.
        public void Build(Level level, AiBotBase bot, Player plr)
        {
            //starts timing the algorithm.
            var watch = Stopwatch.StartNew();

            astar_is_built = true;
            astar_built = true;

            //fill nodes with default value.
            for (int i = 0; i < map_size; i++)
            {
                for (int j = 0; j < map_size; j++)
                {
                    closed[i, j] = false;
                    cost[i, j] = 999999;
                    link[i, j] = new Coord2(-1, -1);
                    inPath[i, j] = false;
                }
            }
            open_locations.Add(bot.GridPosition);

            Coord2 lowest_location = new Coord2(-1, -1);
            float lowest_cost = 999999;
            float newcost = 999999;

            closed[bot.GridPosition.X, bot.GridPosition.Y] = false;
            cost[bot.GridPosition.X, bot.GridPosition.Y] = 0;

            //heuristic calculation
            for (int i = 0; i < map_size; i++)
            {
                for (int j = 0; j < map_size; j++)
                {
                    if (level.manhatten == true)
                    {
                        heuristic[i, j] = Math.Abs(i - plr.GridPosition.X) + Math.Abs(j - plr.GridPosition.Y);//manhatten.                       
                    }
                    else if (level.diagonal == true)
                    {
                        heuristic[i, j] = Math.Max(Math.Abs(i - plr.GridPosition.X), Math.Abs(j - plr.GridPosition.Y));//diagonal.
                    }
                    else if (level.euclidian == true)
                    {
                        heuristic[i, j] = (int)Math.Sqrt(Math.Pow(i - plr.GridPosition.X, 2) + Math.Pow(j - plr.GridPosition.Y, 2));//euclidian.
                    }
                    else
                    {
                        heuristic[i, j] = Math.Abs(i - plr.GridPosition.X) + Math.Abs(j - plr.GridPosition.Y);//manhatten is the default heuristic.
                    }
                }
            }
            while (!closed[plr.GridPosition.X, plr.GridPosition.Y])
            {
                lowest_cost = 999999;

                //unoptomised astar.
                if (level.astar_unoptomised_chosen == true)
                {
                    for (int x = 0; x < map_size; x++)
                    {
                        for (int y = 0; y < map_size; y++)
                        {
                            if (cost[x, y] + heuristic[x, y] < lowest_cost && !closed[x, y])
                            {
                                lowest_cost = cost[x, y] + heuristic[x, y];
                                lowest_location = new Coord2(x, y);
                            }
                        }
                    }
                }
                else
                {
                    //optomised astar, only checks open locations.
                    for (int x = 0; x < open_locations.Count(); x++)
                    {
                        if ((cost[open_locations[x].X, open_locations[x].Y] + heuristic[open_locations[x].X, open_locations[x].Y]) < lowest_cost && !closed[open_locations[x].X, open_locations[x].Y])
                        {
                            lowest_cost = cost[open_locations[x].X, open_locations[x].Y] + heuristic[open_locations[x].X, open_locations[x].Y];
                            lowest_location = new Coord2(open_locations[x].X, open_locations[x].Y);
                        }
                    }
                }
                closed[lowest_location.X, lowest_location.Y] = true;
                open_locations.Remove(lowest_location);

                //counts nodes closed.
                if (closed[lowest_location.X, lowest_location.Y] == true)
                {
                    isClosed ++;
                }

                for (int f = -1; f <= 1; f++)
                {
                    for (int g = -1; g <= 1; g++)
                    {
                        //assigns a cost to each location surrounding the current path node.
                        if (level.ValidPosition(new Coord2(lowest_location.X + f, lowest_location.Y + g)))
                        {
                            newcost = 999999;

                            Coord2 neighbour_location;
                            neighbour_location = new Coord2(lowest_location.X + f, lowest_location.Y + g);
                          
                            if (f != 0 ^ g != 0)
                            {
                                //horizontal cost.
                                newcost = 1f;
                            }                            
                            else if (f != 0 && g != 0)
                            {
                                //diagonal cost.
                                newcost = 1.4f;
                            }
                            if (!open_locations.Contains(neighbour_location) && neighbour_location != bot.GridPosition)
                            {
                                open_locations.Add(neighbour_location);
                            }

                            if (newcost < cost[lowest_location.X + f, lowest_location.Y + g] && level.ValidPosition(new Coord2(lowest_location.X + f, lowest_location.Y + g)) && !closed[lowest_location.X + f, lowest_location.Y + g])
                            {
                                cost[lowest_location.X + f, lowest_location.Y + g] = newcost;//assigns current cost to the new cost.
                                link[lowest_location.X + f, lowest_location.Y + g] = new Coord2(lowest_location.X, lowest_location.Y);//assigns current link to lowest location.
                            }
                        }
                    }
                }
            }

            bool done = false;
            Coord2 nextclosed = plr.GridPosition;
            while (done == false)
            {
                //adds next step to path.
                astar_path.Add(nextclosed);
                inPath[nextclosed.X, nextclosed.Y] = true;
                nextclosed = link[nextclosed.X, nextclosed.Y];
                if (nextclosed == bot.GridPosition)
                {
                    astar_path.Add(bot.GridPosition);//adds path to the bot.
                    done = true;
                }
            }
            astar_path.Reverse();//reverse path for reading.
            watch.Stop();

            elapsedMS = watch.ElapsedMilliseconds;
        }

        //resets algorithm.
        public void reset_path()
        {
            nextclosed = (new Coord2(0, 0));
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
