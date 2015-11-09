#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Diagnostics;

#endregion

namespace Pathfinder
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        //to player time.
        Stopwatch stopwatch = new Stopwatch();       

        //program font and position.
        SpriteFont font;

        //program information.
        Vector2 title_position;
        Vector2 controls_position;
        Vector2 folder_info_position;
        Vector2 press_escape_position;
        Vector2 player_controls_position;

        //nodes in path.
        Vector2 astar_pathcount_position;
        Vector2 dijkstra_pathcount_position;
        Vector2 scentmap_pathcount_position;
        
        //time to execute.
        Vector2 astar_execution_position;
        Vector2 dijkstra_execution_position;
        Vector2 scentmap_execution_position;

        //nodes searched.
        Vector2 astar_nodecount_position;
        Vector2 dijkstra_nodecount_position;

        //timer draw position.
        Vector2 astar_timer_position;
        Vector2 dijkstra_timer_position;
        Vector2 scentmap_timer_position;

        //algorithm title position;
        Vector2 astar_title_position;
        Vector2 astar_unoptomised_title_position;
        Vector2 dijkstra_title_position;
        Vector2 scentmap_title_position;

        //astar controls position
        Vector2 astar_controls_position;

        //astar heuristic chosen position
        Vector2 manhatten_chosen_position;
        Vector2 euclidian_chosen_position;
        Vector2 diagonal_chosen_position;

        //sprite texture for tiles, player, and ai bot
        Texture2D tile1Texture;
        Texture2D tile2Texture;
        Texture2D aiTexture;
        Texture2D playerTexture;

        //objects representing the level map, bot, and player 
        private Level level;
        private AiBotBase astar_bot;
        private AiBotBase dijkstra_bot;
        private AiBotBase scentmap_bot;
        private Player player;

        //screen size and frame rate
        private const int TargetFrameRate = 50;
        private const int BackBufferWidth = 1920;//600
        private const int BackBufferHeight = 1080;//600

        //toggles drawing of the algorithms.
        public bool astar_draw = false;
        public bool astar_unoptomised_draw = false;
        public bool dijkstra_draw = false;
        public bool scentmap_draw = false;

        public int scentmap_path_count;

        //for recording key press.
        KeyboardState oldState;

        //file writing location.
        StreamWriter astar_writetext = new StreamWriter("../../../Analysis Data/AstarAnalysis.txt");
        StreamWriter dijkstra_writetext = new StreamWriter("../../../Analysis Data/DijkstraAnalysis.txt");
        StreamWriter scentmap_writetext = new StreamWriter("../../../Analysis Data/ScentmapAnalysis.txt");
        StreamWriter astar_unoptomised_writetext = new StreamWriter("../../../Analysis Data/AstarUnoptomisedAnalysis.txt");

        //stopwatch bools.
        public bool astar_path_completed = false;
        public bool astar_unoptomised_path_completed = false;
        public bool dijkstra_path_completed = false;
        public bool scentmap_path_completed = false;

        //timer start bools.
        public bool astar_timer_start = false;
        public bool dijkstra_timer_start= false;
        public bool scentmap_timer_start = false;

    
        public Game1()
        {
            //program settings.
            Window.Title = "GAL11266257_CGP3001M_Assignment 2";
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = BackBufferHeight;
            graphics.PreferredBackBufferWidth = BackBufferWidth;          
            Content.RootDirectory = "../../../Content";
            TargetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / TargetFrameRate);
           
            //loads map.
            level = new Level();
            level.Loadmap("../../../Content/0.txt");

            //loads player.
            player = new Player(125, 67);          
        }

        protected override void Initialize()
        {
            base.Initialize();           
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //font data.
            font = Content.Load<SpriteFont>("Font");

            //screen positions for data.
            title_position = new Vector2(920.0f, 20.0f);
            controls_position = new Vector2(920.0f, 1030.0f);
            folder_info_position = new Vector2(920.0f, 55.0f);
            press_escape_position = new Vector2(920.0f, 85.0f);
            player_controls_position = new Vector2(920.0f, 990.0f);

            astar_pathcount_position = new Vector2(1700.0f, 520.0f);
            dijkstra_pathcount_position = new Vector2(1700.0f, 520.0f);
            scentmap_pathcount_position = new Vector2(1700.0f, 520.0f);

            astar_execution_position = new Vector2(1700.0f, 500.0f);
            dijkstra_execution_position = new Vector2(1700.0f, 500.0f);
            scentmap_execution_position = new Vector2(1700.0f, 500.0f);

            astar_nodecount_position = new Vector2(1700.0f, 540.0f);
            dijkstra_nodecount_position = new Vector2(1700.0f, 540.0f);

            astar_timer_position = new Vector2(1700.0f, 480.0f);
            dijkstra_timer_position = new Vector2(1700.0f, 480.0f);
            scentmap_timer_position = new Vector2(1700.0f, 480.0f);

            astar_title_position = new Vector2(1700.0f, 460.0f);
            astar_unoptomised_title_position = new Vector2(1700.0f, 460.0f);
            dijkstra_title_position = new Vector2(1700.0f, 460.0f);
            scentmap_title_position = new Vector2(1700.0f, 460.0f);

            astar_controls_position = new Vector2(920.0f, 950.0f);

            manhatten_chosen_position = new Vector2(1700.0f, 440.0f);
            euclidian_chosen_position = new Vector2(1700.0f, 440.0f);
            diagonal_chosen_position = new Vector2(1700.0f, 440.0f);

            //loads the sprite textures.
            Content.RootDirectory = "../../../Content";
            tile1Texture = Content.Load<Texture2D>("tile1");
            tile2Texture = Content.Load<Texture2D>("tile2");
            aiTexture = Content.Load<Texture2D>("ai");
            playerTexture = Content.Load<Texture2D>("target");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            //initialize keyboard controls.
            KeyboardState keyState = Keyboard.GetState();
            Coord2 currentPos = new Coord2();
            currentPos = player.GridPosition;

            //player movement.
            if (keyState.IsKeyDown(Keys.Escape))//exit.
            { 
                //writes data to text files when the escape button is pressed, is blank otherwise.

                astar_writetext.Close();
                astar_unoptomised_writetext.Close();
                dijkstra_writetext.Close();
                scentmap_writetext.Close();
                
                this.Exit();
            }   
            if (keyState.IsKeyDown(Keys.W))//up.
            {
                currentPos.Y -= 1;
                player.SetNextLocation(currentPos, level);
            }
            else if (keyState.IsKeyDown(Keys.S))//down.
            {
                currentPos.Y += 1;
                player.SetNextLocation(currentPos, level);               
            }
            else if (keyState.IsKeyDown(Keys.A))//left
            {
                currentPos.X -= 1;
                player.SetNextLocation(currentPos, level);
            }
            else if (keyState.IsKeyDown(Keys.D))//right.
            {
                currentPos.X += 1;
                player.SetNextLocation(currentPos, level);
            }

            //select astar.
            if (keyState.IsKeyDown(Keys.D1) && !oldState.IsKeyDown(Keys.D1))
            {
                //sets other algorithms to false and resets current path.
                level.dijkstra_chosen = false;
                level.scentmap_chosen = false;
                level.astar_unoptomised_chosen = false;
                level.astar_chosen = true;
                level.astar.reset_path();

                //resets heuristic
                level.manhatten = false;
                level.euclidian = false;
                level.diagonal = false;

                //initilizes a new bot and clears the current path.
                astar_bot = new AStarBot(1, 1);
                level.astar.astar_path.Clear();

                //resets and restarts the path timer.
                astar_timer_start = true;
                stopwatch.Reset();
                stopwatch.Start();

                //builds current selection.
                level.build_astar = true;
                level.astar.Build(level, astar_bot, player);
                astar_draw = true;
                astar_unoptomised_draw = false;
                dijkstra_draw = false;
                scentmap_draw = false;

                //writes analysis data to the text file.
                astar_writetext.WriteLine("Heuristic Chosen: Manhatten");
                astar_writetext.WriteLine("Path Count: " + level.astar.astar_path.Count());
                astar_writetext.WriteLine("Execution Time: " + level.astar.elapsedMS + "ms");
                astar_writetext.WriteLine("Closed Nodes: " + level.astar.isClosed);
            }            
            //select dijkstra.
            else if (keyState.IsKeyDown(Keys.D2) && !oldState.IsKeyDown(Keys.D2))
            {
                //sets other algorithms to false and resets current path.
                level.astar_chosen = false;
                level.scentmap_chosen = false;
                level.astar_unoptomised_chosen = false;
                level.dijkstra_chosen = true;
                level.dijkstra.reset_path();

                //initilizes a new bot and clears the current path.
                dijkstra_bot = new DijkstraBot(1, 1);
                level.dijkstra.dijkstra_path.Clear();

                //resets and restarts the path timer.
                dijkstra_timer_start = true;
                stopwatch.Reset();
                stopwatch.Start();

                //builds current selection.
                level.build_dijkstra = true;
                level.dijkstra.Build(level, dijkstra_bot, player);
                dijkstra_draw = true;
                astar_draw = false;
                scentmap_draw = false;

                //writes analysis data to the text file.
                dijkstra_writetext.WriteLine("Path Count: " + level.dijkstra.dijkstra_path.Count());
                dijkstra_writetext.WriteLine("Execution Time: " + level.dijkstra.elapsedMS + "ms");
                dijkstra_writetext.WriteLine("Closed Nodes: " + level.dijkstra.isClosed);
            }
            
            //select scentmap.
            else if (keyState.IsKeyDown(Keys.D3) && !oldState.IsKeyDown(Keys.D3))
            {
                //sets other algorithms to false and resets current path.
                level.astar_chosen = false;
                level.dijkstra_chosen = false;
                level.astar_unoptomised_chosen = false;
                level.scentmap_chosen = true;
                level.scentmap.reset_path();
                level.path_count = 0;

                //initilizes a new bot.
                scentmap_bot = new ScentmapBot(1,1);

                //resets and restarts the path timer.
                scentmap_timer_start = true;
                stopwatch.Reset();
                stopwatch.Start();

                //builds current selection.
                scentmap_draw = true;
                astar_draw = false;
                dijkstra_draw = false;                  
            }

            //select unoptomised astar.
            else if (keyState.IsKeyDown(Keys.D4) && !oldState.IsKeyDown(Keys.D4))
            {
                //sets other algorithms to false and resets current path.
                level.astar_chosen = false;
                level.dijkstra_chosen = false;                
                level.scentmap_chosen = false;
                level.astar_unoptomised_chosen = true;
                level.astar.reset_path();

                //resets heuristic
                level.manhatten = false;
                level.euclidian = false;
                level.diagonal = false;

                //initilizes a new bot and clears the current path.
                astar_bot = new AStarBot(1, 1);
                level.astar.astar_path.Clear();

                //resets and restarts the path timer.
                astar_timer_start = true;
                stopwatch.Reset();
                stopwatch.Start();

                //builds current selection.
                level.build_astar = true;
                level.astar.Build(level, astar_bot, player);
                astar_draw = true;
                astar_unoptomised_draw = true;
                dijkstra_draw = false;
                scentmap_draw = false;

                //writes analysis data to the text file.
                astar_unoptomised_writetext.WriteLine("Heuristic Choson: Manhatten");
                astar_unoptomised_writetext.WriteLine("Path Count: " + level.astar.astar_path.Count());
                astar_unoptomised_writetext.WriteLine("Execution Time: " + level.astar.elapsedMS + "ms");
                astar_unoptomised_writetext.WriteLine("Closed Nodes: " + level.astar.isClosed);
            }

            //heuristic changing for astar and unoptomised astar.
            if (level.astar_chosen || level.astar_unoptomised_chosen == true)
            {
                //Euclidian.
                if (keyState.IsKeyDown(Keys.E) && !oldState.IsKeyDown(Keys.E))
                {
                    level.manhatten = false;
                    level.euclidian = true;
                    level.diagonal = false;
                    level.astar.reset_path();

                    //initilizes a new bot and clears the current path.
                    astar_bot = new AStarBot(1, 1);
                    level.astar.astar_path.Clear();

                    //resets and restarts the path timer.
                    astar_timer_start = true;
                    stopwatch.Reset();
                    stopwatch.Start();

                    //builds current selection.
                    level.build_astar = true;
                    level.astar.Build(level, astar_bot, player);
                    astar_draw = true;
                    dijkstra_draw = false;
                    scentmap_draw = false;

                    if(level.astar_chosen == true)
                    {
                        astar_writetext.WriteLine("Heuristic Chosen: Euclidian");
                        astar_writetext.WriteLine("Path Count: " + level.astar.astar_path.Count());
                        astar_writetext.WriteLine("Execution Time: " + level.astar.elapsedMS + "ms");
                        astar_writetext.WriteLine("Closed Nodes: " + level.astar.isClosed);
                    }
                    if(level.astar_unoptomised_chosen == true)
                    {
                        astar_unoptomised_writetext.WriteLine("Heuristic Choson: Euclidian");
                        astar_unoptomised_writetext.WriteLine("Path Count: " + level.astar.astar_path.Count());
                        astar_unoptomised_writetext.WriteLine("Execution Time: " + level.astar.elapsedMS + "ms");
                        astar_unoptomised_writetext.WriteLine("Closed Nodes: " + level.astar.isClosed);
                    }
                }
                //Diagonal.
                if (keyState.IsKeyDown(Keys.R) && !oldState.IsKeyDown(Keys.R))
                {
                    level.manhatten = false;
                    level.euclidian = false;
                    level.diagonal = true;
                    level.astar.reset_path();

                    //initilizes a new bot and clears the current path.
                    astar_bot = new AStarBot(1, 1);
                    level.astar.astar_path.Clear();

                    //resets and restarts the path timer.
                    astar_timer_start = true;
                    stopwatch.Reset();
                    stopwatch.Start();

                    //builds current selection.
                    level.build_astar = true;
                    level.astar.Build(level, astar_bot, player);
                    astar_draw = true;
                    dijkstra_draw = false;
                    scentmap_draw = false;

                    if (level.astar_chosen == true)
                    {
                        astar_writetext.WriteLine("Heuristic Chosen: Diagonal");
                        astar_writetext.WriteLine("Path Count: " + level.astar.astar_path.Count());
                        astar_writetext.WriteLine("Execution Time: " + level.astar.elapsedMS + "ms");
                        astar_writetext.WriteLine("Closed Nodes: " + level.astar.isClosed);
                    }
                    if (level.astar_unoptomised_chosen == true)
                    {
                        astar_unoptomised_writetext.WriteLine("Heuristic Choson: Diagonal");
                        astar_unoptomised_writetext.WriteLine("Path Count: " + level.astar.astar_path.Count());
                        astar_unoptomised_writetext.WriteLine("Execution Time: " + level.astar.elapsedMS + "ms");
                        astar_unoptomised_writetext.WriteLine("Closed Nodes: " + level.astar.isClosed);
                    }
                }
                //Manhatten.
                if (keyState.IsKeyDown(Keys.T) && !oldState.IsKeyDown(Keys.T))
                {
                    level.manhatten = true;
                    level.euclidian = false;
                    level.diagonal = false;
                    level.astar.reset_path();

                    //initilizes a new bot and clears the current path.
                    astar_bot = new AStarBot(1, 1);
                    level.astar.astar_path.Clear();

                    //resets and restarts the path timer.
                    astar_timer_start = true;
                    stopwatch.Reset();
                    stopwatch.Start();

                    //builds current selection.
                    level.build_astar = true;
                    level.astar.Build(level, astar_bot, player);
                    astar_draw = true;
                    dijkstra_draw = false;
                    scentmap_draw = false;

                    if (level.astar_chosen == true)
                    {
                        astar_writetext.WriteLine("Heuristic Chosen: Manhatten");
                        astar_writetext.WriteLine("Path Count: " + level.astar.astar_path.Count());
                        astar_writetext.WriteLine("Execution Time: " + level.astar.elapsedMS + "ms");
                        astar_writetext.WriteLine("Closed Nodes: " + level.astar.isClosed);
                    }
                    if (level.astar_unoptomised_chosen == true)
                    {
                        astar_unoptomised_writetext.WriteLine("Heuristic Choson: Manhatten");
                        astar_unoptomised_writetext.WriteLine("Path Count: " + level.astar.astar_path.Count());
                        astar_unoptomised_writetext.WriteLine("Execution Time: " + level.astar.elapsedMS + "ms");
                        astar_unoptomised_writetext.WriteLine("Closed Nodes: " + level.astar.isClosed);
                    }
                }
            }



            oldState = keyState;

            //when the bot has reached its target, write how long it has taken to the text file.
            if(level.astar_chosen == true)
            {
                astar_bot.Update(gameTime, level, player);
                if (astar_bot.GridPosition == player.GridPosition && astar_timer_start == true)
                {
                    stopwatch.Stop();
                    astar_path_completed = true;
                    astar_timer_start = false;
                    astar_writetext.WriteLine("Time to reach player: " + stopwatch.ElapsedMilliseconds / 1000 + " seconds");
                    astar_writetext.WriteLine("--------------------------------------------------------------------------");
                }
            }

            //when the bot has reached its target, write how long it has taken to the text file.
            if (level.astar_unoptomised_chosen == true)
            {
                astar_bot.Update(gameTime, level, player);
                if (astar_bot.GridPosition == player.GridPosition && astar_timer_start == true)
                {
                    stopwatch.Stop();
                    astar_path_completed = true;
                    astar_timer_start = false;
                    astar_unoptomised_writetext.WriteLine("Time to reach player: " + stopwatch.ElapsedMilliseconds / 1000 + " seconds");
                    astar_unoptomised_writetext.WriteLine("--------------------------------------------------------------------------");
                }
            }

            //when the bot has reached its target, write how long it has taken to the text file.
            if(level.dijkstra_chosen == true)
            {
                dijkstra_bot.Update(gameTime, level, player);
                if (dijkstra_bot.GridPosition == player.GridPosition && dijkstra_timer_start == true)
                {
                    stopwatch.Stop();
                    dijkstra_path_completed = true;
                    dijkstra_timer_start = false;
                    dijkstra_writetext.WriteLine("Time to reach player: " + stopwatch.ElapsedMilliseconds / 1000 + " seconds");
                    dijkstra_writetext.WriteLine("--------------------------------------------------------------------------");
                }
            }

            //when the bot has reached its target, write how long it has taken to the text file.
            if(level.scentmap_chosen == true)
            {
                scentmap_bot.Update(gameTime, level, player);
                level.scentmap.Update(level, player);
                if (scentmap_bot.GridPosition == player.GridPosition && scentmap_timer_start == true)
                {
                    stopwatch.Stop();
                    scentmap_path_completed = true;
                    scentmap_timer_start = false;
                    scentmap_writetext.WriteLine("Path Count: " + level.path_count);
                    scentmap_writetext.WriteLine("Execution Time: " + level.scentmap.elapsedMS + "ms");  
                    scentmap_writetext.WriteLine("Time to reach player: " + stopwatch.ElapsedMilliseconds / 1000 + " seconds");
                    scentmap_writetext.WriteLine("--------------------------------------------------------------------------");
                }
            }           
            player.Update(gameTime, level);           
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
                      
            //draws map.
            DrawGrid();

            //draws the bot corresponding to the selected algroithm.
            if (astar_draw == true)
            {
                spriteBatch.Draw(aiTexture, astar_bot.ScreenPosition, Color.White);
            }
            else if(dijkstra_draw == true)
            {
                spriteBatch.Draw(aiTexture, dijkstra_bot.ScreenPosition, Color.White);
            }
            else if(scentmap_draw == true)
            {
                spriteBatch.Draw(aiTexture, scentmap_bot.ScreenPosition, Color.White);
            }

            //draws the player.
            spriteBatch.Draw(playerTexture, player.ScreenPosition, Color.White);

            //title.
            string title = "Welcome to the Pathfinding comparison!";
            Vector2 title_origin = font.MeasureString(title) / 2;
            spriteBatch.DrawString(font, title, title_position, Color.Navy, 0, title_origin, 2.0f, SpriteEffects.None, 0.5f);

            //folder information.
            string folder_info = "Results - ../../Pathfinder/Analysis Data/";
            Vector2 folder_info_origin = font.MeasureString(folder_info) / 2;
            spriteBatch.DrawString(font, folder_info, folder_info_position, Color.Black, 0, folder_info_origin, 1.75f, SpriteEffects.None, 0.5f);

            //tells player to press escape to write data to text files.
            string press_escape = "Please press escape to close the program and save data to text file.";
            Vector2 press_escape_origin = font.MeasureString(press_escape) / 2;
            spriteBatch.DrawString(font, press_escape, press_escape_position, Color.Black, 0, press_escape_origin, 1.75f, SpriteEffects.None, 0.5f);

            //displays controls.
            string output = "1 - AStar, 2 - Dijkstra, 3 - Scentmap, 4 - Astar Unoptomised, Esc - Close";
            Vector2 controls_origin = font.MeasureString(output) / 2;
            spriteBatch.DrawString(font, output, controls_position, Color.Black, 0, controls_origin, 2.0f, SpriteEffects.None, 0.5f);

            //displays player controls.
            string player_controls_output = "W - Up, S - Down, A, Left, D - Right";
            Vector2 player_controls_origin = font.MeasureString(player_controls_output) / 2;
            spriteBatch.DrawString(font, player_controls_output, player_controls_position, Color.Black, 0, player_controls_origin, 2.0f, SpriteEffects.None, 0.5f);

            //displays data for astar/unoptomised astar.
            if(astar_draw == true)
            {
                string astar_pathcount_output = "Pathcount: " + level.astar.astar_path.Count().ToString();
                Vector2 astar_pathcount_origin = font.MeasureString(astar_pathcount_output) / 2;
                spriteBatch.DrawString(font, astar_pathcount_output, astar_pathcount_position, Color.Black, 0, astar_pathcount_origin, 1.5f, SpriteEffects.None, 0.5f);

                string astar_execution_output = "Execution Time: " + level.astar.elapsedMS.ToString() + "ms";
                Vector2 astar_execution_origin = font.MeasureString(astar_execution_output) / 2;
                spriteBatch.DrawString(font, astar_execution_output, astar_execution_position, Color.Black, 0, astar_execution_origin, 1.5f, SpriteEffects.None, 0.5f);

                string astar_nodecount_output = "Node Count: " + level.astar.isClosed.ToString();
                Vector2 astar_nodecount_origin = font.MeasureString(astar_nodecount_output) / 2;
                spriteBatch.DrawString(font, astar_nodecount_output, astar_nodecount_position, Color.Black, 0, astar_nodecount_origin, 1.5f, SpriteEffects.None, 0.5f);

                string astar_timer_output = "Time Taken: " + (stopwatch.ElapsedMilliseconds / 1000).ToString();
                Vector2 astar_timer_origin = font.MeasureString(astar_timer_output) / 2;
                spriteBatch.DrawString(font, astar_timer_output, astar_timer_position, Color.Black, 0, astar_timer_origin, 1.5f, SpriteEffects.None, 0.5f);
                
                string astar_controls_output = "Heuristics: E - Euclidian, R - Diagonal, T - Manhatten";
                Vector2 astar_controls_origin = font.MeasureString(astar_controls_output) / 2;
                spriteBatch.DrawString(font, astar_controls_output, astar_controls_position, Color.Black, 0, astar_controls_origin, 1.75f, SpriteEffects.None, 0.5f);

                if (astar_unoptomised_draw == false)
                {
                    string astar_title_output = "Astar";
                    Vector2 astar_title_origin = font.MeasureString(astar_title_output) / 2;
                    spriteBatch.DrawString(font, astar_title_output, astar_title_position, Color.Black, 0, astar_title_origin, 1.5f, SpriteEffects.None, 0.5f);
                }
                else
                {
                    string astar_unoptomised_title_output = "Unoptomised Astar";
                    Vector2 astar_unoptomised_title_origin = font.MeasureString(astar_unoptomised_title_output) / 2;
                    spriteBatch.DrawString(font, astar_unoptomised_title_output, astar_unoptomised_title_position, Color.Black, 0, astar_unoptomised_title_origin, 1.5f, SpriteEffects.None, 0.5f);
                }
                if (level.manhatten == true)
                {
                    string manhatten_chosen_output = "Manhatten";
                    Vector2 manhatten_chosen_origin = font.MeasureString(manhatten_chosen_output) / 2;
                    spriteBatch.DrawString(font, manhatten_chosen_output, manhatten_chosen_position, Color.Black, 0, manhatten_chosen_origin, 1.5f, SpriteEffects.None, 0.5f);
                }
                else if (level.euclidian == true)
                {
                    string euclidian_chosen_output = "Euclidian";
                    Vector2 euclidian_chosen_origin = font.MeasureString(euclidian_chosen_output) / 2;
                    spriteBatch.DrawString(font, euclidian_chosen_output, euclidian_chosen_position, Color.Black, 0, euclidian_chosen_origin, 1.5f, SpriteEffects.None, 0.5f);
                }
                else if (level.diagonal == true)
                {
                    string diagonal_chosen_output = "Diagonal";
                    Vector2 diagonal_chosen_origin = font.MeasureString(diagonal_chosen_output) / 2;
                    spriteBatch.DrawString(font, diagonal_chosen_output, diagonal_chosen_position, Color.Black, 0, diagonal_chosen_origin, 1.5f, SpriteEffects.None, 0.5f);
                }
                else
                {
                    string manhatten_chosen_output = "Manhatten";
                    Vector2 manhatten_chosen_origin = font.MeasureString(manhatten_chosen_output) / 2;
                    spriteBatch.DrawString(font, manhatten_chosen_output, manhatten_chosen_position, Color.Black, 0, manhatten_chosen_origin, 1.5f, SpriteEffects.None, 0.5f);
                }



            }

            //displays data for dijkstra.
            if(dijkstra_draw == true)
            {
                string dijkstra_output = "Pathcount: " + level.dijkstra.dijkstra_path.Count().ToString();
                Vector2 dijkstra_origin = font.MeasureString(dijkstra_output) / 2;
                spriteBatch.DrawString(font, dijkstra_output, dijkstra_pathcount_position, Color.Black, 0, dijkstra_origin, 1.5f, SpriteEffects.None, 0.5f);

                string dijkstra_execution_output = "Execution Time: " + level.dijkstra.elapsedMS.ToString() + "ms";
                Vector2 dijkstra_execution_origin = font.MeasureString(dijkstra_execution_output) / 2;
                spriteBatch.DrawString(font, dijkstra_execution_output, dijkstra_execution_position, Color.Black, 0, dijkstra_execution_origin, 1.5f, SpriteEffects.None, 0.5f);

                string dijkstra_nodecount_output = "Node Count: " + level.dijkstra.isClosed.ToString();
                Vector2 dijkstra_nodecount_origin = font.MeasureString(dijkstra_nodecount_output) / 2;
                spriteBatch.DrawString(font, dijkstra_nodecount_output, dijkstra_nodecount_position, Color.Black, 0, dijkstra_nodecount_origin, 1.5f, SpriteEffects.None, 0.5f);

                string dijkstra_timer_output = "Time Taken: " + (stopwatch.ElapsedMilliseconds / 1000).ToString();
                Vector2 dijkstra_timer_origin = font.MeasureString(dijkstra_timer_output) / 2;
                spriteBatch.DrawString(font, dijkstra_timer_output, dijkstra_timer_position, Color.Black, 0, dijkstra_timer_origin, 1.5f, SpriteEffects.None, 0.5f);

                string dijkstra_title_output = "Dijkstra";
                Vector2 dijkstra_title_origin = font.MeasureString(dijkstra_title_output) / 2;
                spriteBatch.DrawString(font, dijkstra_title_output, dijkstra_title_position, Color.Black, 0, dijkstra_title_origin, 1.5f, SpriteEffects.None, 0.5f);
            }

            //displays data for scentmap.
            if(scentmap_draw == true)
            {
                string scentmap_pathcount_output = "Pathcount: " + level.path_count.ToString();
                Vector2 scentmap_pathcount_origin = font.MeasureString(scentmap_pathcount_output) / 2;
                spriteBatch.DrawString(font, scentmap_pathcount_output, scentmap_pathcount_position, Color.Black, 0, scentmap_pathcount_origin, 1.5f, SpriteEffects.None, 0.5f);

                string scentmap_execution_output = "Execution Time: " + level.scentmap.elapsedMS.ToString();
                Vector2 scentmap_execution_origin = font.MeasureString(scentmap_execution_output) / 2;
                spriteBatch.DrawString(font, scentmap_execution_output, scentmap_execution_position, Color.Black, 0, scentmap_execution_origin, 1.5f, SpriteEffects.None, 0.5f);

                string scentmap_timer_output = "Time Taken: " + (stopwatch.ElapsedMilliseconds / 1000).ToString();
                Vector2 scentmap_timer_origin = font.MeasureString(scentmap_timer_output) / 2;
                spriteBatch.DrawString(font, scentmap_timer_output, dijkstra_timer_position, Color.Black, 0, scentmap_timer_origin, 1.5f, SpriteEffects.None, 0.5f);

                string scentmap_title_output = "Scentmap";
                Vector2 scentmap_title_origin = font.MeasureString(scentmap_title_output) / 2;
                spriteBatch.DrawString(font, scentmap_title_output, scentmap_title_position, Color.Black, 0, scentmap_title_origin, 1.5f, SpriteEffects.None, 0.5f);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        //draws the map and paths.
        private void DrawGrid()
        {
            int sz = level.GridSize;
            for (int x = 0; x < sz; x++)
            {
                for (int y = 0; y < sz; y++)
                {
                    Coord2 pos = new Coord2((x * 15), (y * 15));
                    if (level.tiles[x, y] == 0)
                    {
                        //scentmap draw.
                        if(scentmap_draw == true)//draws scentmap path.
                        {
                            float lowestvalue = (float)level.scentmap.minvalue;
                            float highestvalue = (float)level.scentmap.maxvalue;
                            float gridvalue = (level.scentmap.buffer1[x, y] - lowestvalue) / (highestvalue - lowestvalue);

                            if (gridvalue <= 0.25)
                            {
                                spriteBatch.Draw(tile1Texture, pos, Color.Yellow);
                            }
                            else if (gridvalue <= 0.50)
                            {
                                spriteBatch.Draw(tile1Texture, pos, Color.Orange);
                            }
                            else if (gridvalue <= 0.75)
                            {
                                spriteBatch.Draw(tile1Texture, pos, Color.OrangeRed);
                            }
                            else if (gridvalue <= 1.0)
                            {
                                spriteBatch.Draw(tile1Texture, pos, Color.Red);
                            }
                        }
                        else
                        {
                            spriteBatch.Draw(tile1Texture, pos, Color.White);
                        } 
  
                        //astar draw.
                        if (astar_draw == true)
                        {
                            if (level.astar.inPath[x, y])
                            {
                                spriteBatch.Draw(tile1Texture, pos, Color.Red);
                            }
                            else if (level.astar.closed[x, y])
                            {
                                spriteBatch.Draw(tile1Texture, pos, Color.Blue);
                            }
                            else if (level.astar.astar_built && level.astar.cost[x, y] < 999999)
                            {
                                spriteBatch.Draw(tile1Texture, pos, Color.LightBlue);
                            }
                            else
                            {
                                spriteBatch.Draw(tile1Texture, pos, Color.White);
                            }

                        }

                        //dijkstra draw.
                        if (dijkstra_draw == true)//draws dijkstra path.
                        {
                            if (level.dijkstra.inPath[x, y])
                            {
                                spriteBatch.Draw(tile1Texture, pos, Color.Red);
                            }
                            else if (level.dijkstra.closed[x, y])
                            {
                                spriteBatch.Draw(tile1Texture, pos, Color.Blue);
                            }
                            else if (!level.dijkstra.closed[x, y] && level.dijkstra.dijkstra_built && level.dijkstra.cost[x, y] < 999999)
                            {
                                spriteBatch.Draw(tile1Texture, pos, Color.LightBlue);
                            }
                            else
                            {
                                spriteBatch.Draw(tile1Texture, pos, Color.White);
                            }
                        }
                    }
                    
                    //wall colour.
                    else
                    {
                        spriteBatch.Draw(tile2Texture, pos, Color.White);
                    }                  
                }
            }
        }       
    }
    
}
