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
    class Scentmap
    {
        //path variables.
        int gridsize;
        public int[,] buffer1;
        public int[,] buffer2;

        public int maxvalue;
        public int minvalue;

        int sourcevalue;

        //timer variable.
        public float elapsedMS;

        //sets map size.
        public int map_size = 160;

        public int test;

        //constructor.
        public Scentmap()
        {
            map_size = 160;
            maxvalue = 0;
            minvalue = 0;
            buffer1 = new int[map_size, map_size];
            buffer2 = new int[map_size, map_size];
            sourcevalue = 0;
            for (int x = 0; x < map_size; x++)
            {
                for (int y = 0; y < map_size; y++)
                {
                    buffer1[x, y] = 0;
                    buffer2[x, y] = 0;                   
                }
            }
        }
        //constantly updates scent path.
        public void Update(Level level, Player plr)
        {
            //starts timing the algorithm.
            var watch = Stopwatch.StartNew();

            //copy buffer 1 to buffer 2.
            Array.Copy(buffer1, buffer2, map_size * map_size);

            sourcevalue = sourcevalue + 1;
            minvalue = sourcevalue;

            //scans each grid node.
            for (int i = 0; i < map_size; i++)
            {
                for (int j = 0; j < map_size; j++)
                {
                    Coord2 tempposition;
                    tempposition = new Coord2(i,j);

                    //scans current node surrounding cells.
                    for (int f = -1; f <= 1; f++)
                    {
                        for (int g = -1; g <= 1; g++)
                        {
                            if (level.ValidPosition(new Coord2(tempposition.X + f, tempposition.Y + g)) && buffer2[tempposition.X + f, tempposition.Y + g] > buffer1[tempposition.X, tempposition.Y])
                            {
                                // buffer 1 = buffer 2 temp + values of f and g.
                                buffer1[i, j] = buffer2[tempposition.X + f, tempposition.Y + g] - 1;
                            }
                        }
                    }
                }
            }

            //resets buffers and values.
            buffer1[plr.GridPosition.X, plr.GridPosition.Y] = sourcevalue;
            buffer2[plr.GridPosition.X, plr.GridPosition.Y] = sourcevalue;

            maxvalue = buffer1.Cast<int>().Max();
            minvalue = buffer1.Cast<int>().Min();

            watch.Stop();
            elapsedMS = watch.ElapsedMilliseconds;
        }
        public void reset_path()
        {
            map_size = 160;
            maxvalue = 0;
            minvalue = 0;
            buffer1 = new int[map_size, map_size];
            buffer2 = new int[map_size, map_size];
            sourcevalue = 0;
            for (int i = 0; i < map_size; i++)
            {
                for (int j = 0; j < map_size; j++)
                {
                    buffer1[i, j] = 0;
                    buffer2[i, j] = 0;   
                }
            }
        }
    }
}
