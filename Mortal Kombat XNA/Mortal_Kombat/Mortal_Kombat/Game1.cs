using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Kinect;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;

namespace Mortal_Kombat
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;//sets graphics.
        SpriteBatch spriteBatch;//sets sprites.

        KinectSensor kinect = null;
        Skeleton[] skeletons;
        Texture2D rightHandImage;

        Skeleton trackedSkeleton;
        KinectButton startbutton;//start button on first screen.
        KinectButton exitbutton;//quit button on first screen.
        KinectButton pauseresumebutton;//resume button on the pause screen
        KinectButton pauseexitbutton;//quit button on pause screen.
        KinectButton pauserestartbutton;//restart button on the pause screen

        SpriteFont font;
        int count;

        KeyboardState keyboardState;//sets keyboard state for Player 1 (Scorpion).
        KeyboardState keyboardState2;//sets keyboard state for Player 2 (Liu Kang).

        GamePadState gamePadState;//sets keyboard state for Player 1 (Scorpion).
        GamePadState gamePadState2;//sets keyboard state for Player 2 (Liu Kang).

        AnimatedSprite sprite;//initialises sprite (Scorpion).
        AnimatedSprite sprite2;//initilises sprite2 (Liu Kang).

        int groundheight = 270;//sets game ground height.
        Vector2 motion = new Vector2();//sets Scorpion motion.
        Vector2 motion2 = new Vector2();//sets Liu Kang motion.

        int attacktime;
        int attacktime2;

        bool scorpionpunch = false;
        bool scorpionkick = false;
        bool scorpionblock = false;
        bool scorpionduck = false;

        bool liukangpunch = false;
        bool liukangkick = false;
        bool liukangblock = false;
        bool liukangduck = false;

        SpriteBatch mBatch;
        Texture2D mHealthBar;

        int LiuKang_health = 100;
        int Scorpion_health = 100;

        Texture2D backgroundTexture;
        Texture2D startscreenTexture;
        Texture2D pausescreenTexture;

        SoundEffectInstance LKPunchInstance;
        SoundEffectInstance LKKickInstance;
        SoundEffectInstance SPunchInstance;
        SoundEffectInstance SKickInstance;
        SoundEffectInstance MenuInstance;
        SoundEffectInstance FightInstance;

        SoundEffect LKPunch;
        SoundEffect LKKick;

        SoundEffect SPunch;
        SoundEffect SKick;
        SoundEffect MenuMusic;
        SoundEffect FightMusic;
   
        Song Fight;
        Song LKWins;
        Song SWins;
       
        SpriteBatch zbatch;
        Texture2D LKnameplate;
        Texture2D Snameplate;

        int screenWidth = 800;
        int screenHeight = 480;

        bool mIsGameScreenShown;
        bool mIsControllerDetectScreenShown;
        bool mIsPauseScreenShown;

        PlayerIndex mPlayerOne;

        Vector2 bodyPositionPixels = new Vector2();
        RecognizerInfo ri;
        KinectAudioSource source;
        SpeechRecognitionEngine sre;
        Stream s;

        string connectedStatus = "Not connected";

        Texture2D kinectRGBVideo;
        Texture2D body;

        const float maxVal = 5.0f;

        bool Sleft = false;
        bool Sright = false;
        bool Sidle = false;

        bool screenshot = false;

        private void UpdateControllerDetectScreen()
        {
            for (int aPlayer = 0; aPlayer < 2; aPlayer++)
            {
                if(mIsControllerDetectScreenShown == true)
                {
                    if (GamePad.GetState((PlayerIndex)aPlayer).Buttons.Start == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Z) == true)//starts the game if the start button or "Z" has been pressed.
                    {
                        mPlayerOne = (PlayerIndex)aPlayer;
                        mIsGameScreenShown = true;
                        mIsControllerDetectScreenShown = false;
                        return;
                    }
                    if (GamePad.GetState((PlayerIndex)aPlayer).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.X) == true)//exits the game if the back button or "X" has been pressed.
                    {
                        Exit();
                    }
                }
            }
        }
        private void UpdateGameScreen()
        {
            if (GamePad.GetState(mPlayerOne).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.P) == true)//pauses the game if the back button or "P" has been pressed.
                {
                    mIsGameScreenShown = false;
                    mIsPauseScreenShown = true;
                    return;
                }
        }
        private void UpdatePauseScreen()
        {
            if (GamePad.GetState(mPlayerOne).Buttons.B == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Z) == true)
            {
                mIsGameScreenShown = true;
                mIsControllerDetectScreenShown = false;
                mIsPauseScreenShown = false;
            }
            if (GamePad.GetState(mPlayerOne).Buttons.Y == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.C) == true)
            {
                Scorpion_health = 100;
                LiuKang_health = 100;
                sprite.position = new Vector2(100, groundheight);
                sprite2.position = new Vector2(600, groundheight);
                mIsGameScreenShown = true;
                mIsControllerDetectScreenShown = false;
                mIsPauseScreenShown = false;
            }
            if (GamePad.GetState(mPlayerOne).Buttons.X == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.X) == true)
            {
                Exit();
            }
        }
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            this.graphics.IsFullScreen = true;
        }
        void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
       {
           if (this.kinect == e.Sensor)
           {
               if (e.Status == KinectStatus.Disconnected ||
                   e.Status == KinectStatus.NotPowered)
               {
                   this.kinect = null;
                   this.DiscoverKinectSensor();
               }
           }
       }
        private bool InitializeKinect()
       {
           kinect.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
           kinect.ColorFrameReady += new EventHandler<ColorImageFrameReadyEventArgs>(kinectSensor_ColorFrameReady);
           kinect = KinectSensor.KinectSensors.FirstOrDefault();


           try
           {
               kinect.Start();

               source = kinect.AudioSource;
               source.EchoCancellationMode = EchoCancellationMode.None;
               source.AutomaticGainControlEnabled = false;

               ri = GetKinectRecognizer();

               if (ri == null)
               {
                   Console.WriteLine("ri = 0, Kinect audio not working.");
               }
               else
               {
                   Console.WriteLine("*** using speech");

                   int wait = 4;
                   while (wait > 0)
                   {
                       wait--;
                       Console.WriteLine("Device will be ready for speech recognition in " + wait + " seconds.");
                       Thread.Sleep(1000);
                   }
                   kinect.Start();
               }
               kinect.SkeletonStream.Enable(new TransformSmoothParameters()
               {
                   Smoothing = 0.5f,
                   Correction = 0.5f,
                   Prediction = 0.5f,
                   JitterRadius = 0.05f,
                   MaxDeviationRadius = 0.04f
               });
               kinect.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(kinect_SkeletonFrameReady);
           }
           catch
           {
               connectedStatus = "Unable to start the Kinect Sensor";
               return false;
           }
           return true; 

       }
        void kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    Skeleton[] skeletonData = new Skeleton[skeletonFrame.SkeletonArrayLength];

                    //skeletonFrame.CopySkeletonDataTo(skeletons);
                    skeletonFrame.CopySkeletonDataTo(skeletonData);

                    trackedSkeleton = skeletonData.Where(s => s.TrackingState == SkeletonTrackingState.Tracked).FirstOrDefault();
                    Skeleton playerSkeleton = (from s in skeletonData where s.TrackingState == SkeletonTrackingState.Tracked select s).FirstOrDefault();
                    if (skeletonData == null)
                    {
                        skeletonData = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    }
                    if (playerSkeleton != null)
                    {
                        Joint body = playerSkeleton.Joints[JointType.Spine];
                        bodyPositionPixels = new Vector2((((0.5f * body.Position.X) + 0.5f) * (640)), (((-0.5f * body.Position.Y) + 0.5f) * (480)));

                        if (body.Position.X > 0.1)
                        {
                            Console.WriteLine("Moving Right");
                            scorpionduck = false;
                            Sleft = false;
                            Sright = true;
                        }
                        if (body.Position.X < -0.1)
                        {
                            Console.WriteLine("Moving Left");
                            scorpionduck = false;
                            Sright = false;
                            Sleft = true;
                        }
                        if (body.Position.Y < 0)
                        {
                            Console.WriteLine("Ducking");
                            Sright = false;
                            Sleft = false;
                            scorpionduck = true;
                        }
                        if (body.Position.X > -0.1 && body.Position.X < 0.1)
                        {
                            Sright = false;
                            Sleft = false;
                            scorpionduck = false;
                            Sidle = true;
                        }
                    }  
                }
            }
        }
        public float Scale(float value, int max)
        {
            return MathHelper.Clamp((max >> 1) + (value * (max >> 1)), 0, max);
        }
        void kinectSensor_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame colorImageFrame = e.OpenColorImageFrame())
            {
                if (colorImageFrame != null)
                {
                    byte[] pixelsFromFrame = new byte[colorImageFrame.PixelDataLength];

                    colorImageFrame.CopyPixelDataTo(pixelsFromFrame);

                    Color[] color = new Color[colorImageFrame.Height * colorImageFrame.Width];
                    kinectRGBVideo = new Texture2D(graphics.GraphicsDevice, colorImageFrame.Width, colorImageFrame.Height);
                    int index = 0;
                    for (int y = 0; y < colorImageFrame.Height; y++)
                    {
                        for (int x = 0; x < colorImageFrame.Width; x++, index += 4)
                        {
                            color[y * colorImageFrame.Width + x] = new Color(pixelsFromFrame[index + 2], pixelsFromFrame[index + 1], pixelsFromFrame[index + 0]);
                        }
                    }
                    kinectRGBVideo.SetData(color);
                }
            }
        }
        private void DiscoverKinectSensor()
        {
            foreach (KinectSensor sensor in KinectSensor.KinectSensors)
            {
                if (sensor.Status == KinectStatus.Connected)
                {
                    // Found one, set our sensor to this
                    kinect = sensor;
                    break;
                }
            }

            if (this.kinect == null)
            {
                connectedStatus = "Found none Kinect Sensors connected to USB";
                Console.WriteLine("Cannot find microphone");
                return;
            }
            switch (kinect.Status)
            {
                case KinectStatus.Connected:
                    {
                        connectedStatus = "Status: Connected";
                        break;
                    }
                case KinectStatus.Disconnected:
                    {
                        connectedStatus = "Status: Disconnected";
                        break;
                    }
                case KinectStatus.NotPowered:
                    {
                        connectedStatus = "Status: Connect the power";
                        break;
                    }
                default:
                    {
                        connectedStatus = "Status: Error";
                        break;
                    }
            }

            // Init the found and connected device
            if (kinect.Status == KinectStatus.Connected)
            {
                InitializeKinect();
            }

            sre = new SpeechRecognitionEngine(ri.Id);

            var command = new Choices();
            command.Add("start");
            command.Add("pause");
            command.Add("exit");
            command.Add("resume");
            command.Add("restart");
            command.Add("menu");

            var gb = new GrammarBuilder { Culture = ri.Culture };
            gb.Culture = ri.Culture;
            gb.Append(command);

            var g = new Grammar(gb);

            sre.LoadGrammar(g);
            sre.SpeechRecognized += SreSpeechRecognized;
            sre.SpeechHypothesized += SreSpeechHypothesized;
            sre.SpeechRecognitionRejected += SreSpeechRecognitionRejected;
            sre.SpeechDetected += SreSpeechDetected;

            s = source.Start();

            sre.SetInputToAudioStream(s, new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
            sre.RecognizeAsync(RecognizeMode.Multiple);
        }
        protected override void Initialize()
        {
            KinectSensor.KinectSensors.StatusChanged += new EventHandler<StatusChangedEventArgs>(KinectSensors_StatusChanged);
            DiscoverKinectSensor();

            base.Initialize();
        }
        private static RecognizerInfo GetKinectRecognizer()
        {
            Func<RecognizerInfo, bool> matchingFunc = r =>
            {
                string value;
                r.AdditionalInfo.TryGetValue("Kinect", out value);
                return "True".Equals(value, StringComparison.InvariantCultureIgnoreCase) && "en-US".Equals(r.Culture.Name, StringComparison.InvariantCultureIgnoreCase);
            };
            return SpeechRecognitionEngine.InstalledRecognizers().Where(matchingFunc).FirstOrDefault();
        }
        private static void SreSpeechDetected(object sender, SpeechDetectedEventArgs e)
        {
            Console.WriteLine("*** speech SreSpeechDetected");
        }
        private static void SreSpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            if (e.Result != null)
            {
                Console.WriteLine("*** speech SreSpeechRecognitionRejected");
            }
        }
        private static void SreSpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            Console.WriteLine("*** speech SreSpeechHypothesized");
        }
        private void SreSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("*** speech recog");

            if (e.Result.Confidence >= 0.65)
            {
                switch (e.Result.Text)
                {
                    case "pause":
                        Console.WriteLine("YOU SAID PAUSE GAME");
                        mIsGameScreenShown = false;
                        mIsPauseScreenShown = true;
                        break;
                    case "exit":
                        Console.WriteLine("YOU SAID EXIT GAME");
                        Exit();
                        break;
                    case "resume":
                        Console.WriteLine("YOU SAID RESUME GAME");
                        mIsGameScreenShown = true;
                        mIsControllerDetectScreenShown = false;
                        mIsPauseScreenShown = false;
                        break;
                    case "restart":
                        Console.WriteLine("YOU SAID RESTART GAME");
                        Scorpion_health = 100;
                        LiuKang_health = 100;
                        sprite.position = new Vector2(100, groundheight);
                        sprite2.position = new Vector2(600, groundheight);
                        mIsGameScreenShown = true;
                        mIsControllerDetectScreenShown = false;
                        mIsPauseScreenShown = false;
                        break;
                    case "start":
                        Console.WriteLine("YOU SAID START GAME");
                        mIsGameScreenShown = true;
                        mIsControllerDetectScreenShown = false;
                        break;
                    case "menu":
                        Console.WriteLine("YOU SAID MENU");
                        Scorpion_health = 100;
                        LiuKang_health = 100;
                        sprite.position = new Vector2(100, groundheight);
                        sprite2.position = new Vector2(600, groundheight);
                        mIsPauseScreenShown = false;
                        mIsGameScreenShown = false;
                        mIsControllerDetectScreenShown = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine("Heard something, but the confidence is too low: " + e.Result.Confidence.ToString());
            }
        }
        protected override void LoadContent()
        {
            IsMouseVisible = true;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            kinectRGBVideo = new Texture2D(GraphicsDevice, 1337, 1337);
            body = Content.Load<Texture2D>("Space_Invader");

            SelectAnimation selectAnimation = new SelectAnimation(1500, new Vector2(100, 100), Content.Load<Texture2D>("AnimationStrip"));
            SelectAnimation selectAnimation2 = new SelectAnimation(1500, new Vector2(100, 100), Content.Load<Texture2D>("AnimationStrip"));
            startbutton = new KinectButton(Content.Load<Texture2D>("Start_Button"), new Vector2(90, 250), selectAnimation);
            exitbutton = new KinectButton(Content.Load<Texture2D>("Exit_Button_StartScreen"), new Vector2(380, 250), selectAnimation2);
            startbutton.RegisterClientJoint(JointType.HandLeft);//sets joint to track!
            exitbutton.RegisterClientJoint(JointType.HandLeft);//sets joint to track!
            startbutton.KinectButtonClicked += new AbstractKinectButton.KinectButtonClickedHandler(startbutton_KinectButtonClicked);
            exitbutton.KinectButtonClicked += new AbstractKinectButton.KinectButtonClickedHandler(exitbutton_KinectButtonClicked);

            SelectAnimation selectAnimation3 = new SelectAnimation(1500, new Vector2(100, 100), Content.Load<Texture2D>("AnimationStrip"));
            SelectAnimation selectAnimation4 = new SelectAnimation(1500, new Vector2(100, 100), Content.Load<Texture2D>("AnimationStrip"));
            SelectAnimation selectAnimation5 = new SelectAnimation(1500, new Vector2(100, 100), Content.Load<Texture2D>("AnimationStrip"));
            pauseresumebutton = new KinectButton(Content.Load<Texture2D>("Resume_Button"), new Vector2(90, 150), selectAnimation3);
            pauseexitbutton = new KinectButton(Content.Load<Texture2D>("Exit_Button_PauseScreen"), new Vector2(380, 150), selectAnimation4);
            pauserestartbutton = new KinectButton(Content.Load<Texture2D>("Restart_Button"), new Vector2(230, 300), selectAnimation5);
            pauseresumebutton.RegisterClientJoint(JointType.HandLeft);//sets joint to track!
            pauseexitbutton.RegisterClientJoint(JointType.HandLeft);//sets joint to track!
            pauserestartbutton.RegisterClientJoint(JointType.HandLeft);//sets joint to track!
            pauseresumebutton.KinectButtonClicked += new AbstractKinectButton.KinectButtonClickedHandler(pauseresumebutton_KinectButtonClicked);
            pauseexitbutton.KinectButtonClicked += new AbstractKinectButton.KinectButtonClickedHandler(pauseexitbutton_KinectButtonClicked);
            pauserestartbutton.KinectButtonClicked += new AbstractKinectButton.KinectButtonClickedHandler(pauserestartbutton_KinectButtonClicked);
          
           rightHandImage = Content.Load<Texture2D>("Kinect_Hand");
           font = Content.Load<SpriteFont>("SpriteFont1");

            LKPunch = Content.Load<SoundEffect>("LKPunch");
            LKPunchInstance = LKPunch.CreateInstance();
            LKKick = Content.Load<SoundEffect>("LKKick");
            LKKickInstance = LKKick.CreateInstance();
            SPunch = Content.Load<SoundEffect>("SPunch");
            SPunchInstance = SPunch.CreateInstance();
            SKick = Content.Load<SoundEffect>("SKick");
            SKickInstance = SKick.CreateInstance();

            SWins = Content.Load<Song>("SWins");
            LKWins = Content.Load<Song>("LKWins");
            Fight = Content.Load<Song>("Fight");

            MenuMusic = Content.Load<SoundEffect>("Menu_Music");
            MenuInstance = MenuMusic.CreateInstance();
            MenuInstance.Volume = 0.3f;
            MenuInstance.IsLooped = true;

            FightMusic = Content.Load<SoundEffect>("Fight_Music");
            FightInstance = FightMusic.CreateInstance();
            FightInstance.Volume = 0.1f;
            FightInstance.IsLooped = true;

            MediaPlayer.Volume = 10f;

            backgroundTexture = Content.Load<Texture2D>("Background");
            startscreenTexture = Content.Load<Texture2D>("title");
            pausescreenTexture = Content.Load<Texture2D>("Pause_Screen");

            mIsGameScreenShown = false;
            mIsPauseScreenShown = false;
            mIsControllerDetectScreenShown = true;

            spriteBatch = new SpriteBatch(GraphicsDevice);

            Dictionary<AnimationKey, Animation> animations = new Dictionary<AnimationKey, Animation>();//loads in player 1 (Scorpion)
            Dictionary<AnimationKey, Animation> animations2 = new Dictionary<AnimationKey, Animation>();//loads in player 2 (Liu Kang)

            Animation animation = new Animation(0, 0, 0, 0, 0);//no idea what this is, program doesn't work without it!
            Animation animation2 = new Animation(0, 0, 0, 0, 0);//does the same for animation2!.

            mBatch = new SpriteBatch(this.graphics.GraphicsDevice);//initialises the spritebatch.
            ContentManager aLoader = new ContentManager(this.Services);//creates a content manager to help load the image.
            mHealthBar = Content.Load<Texture2D>("HealthBar") as Texture2D;//loads in the healthbar texture.

            zbatch = new SpriteBatch(this.GraphicsDevice);
            ContentManager zLoader = new ContentManager(this.Services);
            LKnameplate = Content.Load<Texture2D>("LKNameplate") as Texture2D;
            Snameplate = Content.Load<Texture2D>("SNameplate") as Texture2D;

            animation = new Animation(12, 100, 135, 0, 0);//sets Scorpion idle
            animations.Add(AnimationKey.Idle, animation);//defines Scorpion idle animation.
            animation2 = new Animation(8, 100, 135, 0, 0);//sets Liu Kang idle.
            animations2.Add(AnimationKey.Idle, animation2);//defines Liu Kang idle animation.

            animation = new Animation(9, 100, 165, 0, 285);//sets Scorpion left.
            animations.Add(AnimationKey.Left, animation);//defines Scorpion left animation.
            animation2 = new Animation(9, 100, 165, 0, 135);//sets Liu Kang left.
            animations2.Add(AnimationKey.Left, animation2);//defines Liu Kang left animation.

            animation = new Animation(9, 100, 150, 0, 135);//sets Scorpion right.
            animations.Add(AnimationKey.Right, animation);//defines Scorpion right animation
            animation2 = new Animation(9, 100, 165, 0, 285);//sets Liu Kang right.
            animations2.Add(AnimationKey.Right, animation2);//defines Liu Kang right animation

            animation = new Animation(5, 150, 140, 0, 450);//sets Scorpion Punch
            animations.Add(AnimationKey.Punch, animation);//defines Scorpion punch animation.
            animation2 = new Animation(5, 150, 140, 0, 450);//sets Liu Kang Punch
            animations2.Add(AnimationKey.Punch, animation2);//defines Liu Kang punch animation.

            animation = new Animation(9, 150, 140, 0, 600);//sets Scorpion Kick
            animations.Add(AnimationKey.Kick, animation);//defines Scorpion Kick animation.
            animation2 = new Animation(9, 150, 140, 0, 600);//sets Liu Kang Kick
            animations2.Add(AnimationKey.Kick, animation2);//defines Liu Kang Kick animation.

            animation = new Animation(5, 100, 150, 0, 750);//sets Scorpion Block
            animations.Add(AnimationKey.Block, animation);//defines Scorpion Block animation.
            animation2 = new Animation(5, 100, 150, 0, 750);//sets Liu Kang Block
            animations2.Add(AnimationKey.Block, animation2);//defines Liu Kang Block animation.

            animation = new Animation(5, 100, 150, 0, 890);//sets Scorpion Duck
            animations.Add(AnimationKey.Duck, animation);//defines Scorpion Duck animation.
            animation2 = new Animation(5, 100, 150, 0, 890);//sets Liu Kang Duck
            animations2.Add(AnimationKey.Duck, animation2);//defines Liu Kang Duck animation.

            sprite = new AnimatedSprite(Content.Load<Texture2D>("Scorpion"), animations, animation.FrameWidth, animation.FrameHeight);//loads Scorpion sprite texture.
            sprite2 = new AnimatedSprite(Content.Load<Texture2D>("LiuKang"), animations2, animation2.FrameWidth, animation.FrameHeight);//loads Liu Kang sprite texture.

            sprite.position = new Vector2(100, groundheight);//sets sprite (Scorpion) start position.
            sprite2.position = new Vector2(560, groundheight);//sets sprite2 (Liu Kang) start position.

        }
        void startbutton_KinectButtonClicked()
        {
            count++;
            mIsGameScreenShown = true;
            mIsControllerDetectScreenShown = false;
            mIsPauseScreenShown = false;
        }
        void exitbutton_KinectButtonClicked()
        {
            count++;
            mIsGameScreenShown = true;
            mIsControllerDetectScreenShown = false;
            mIsPauseScreenShown = false;
            Exit();
        }
        void pauseresumebutton_KinectButtonClicked()
        {
            count++;
            mIsGameScreenShown = true;
            mIsControllerDetectScreenShown = false;
            mIsPauseScreenShown = false;
        }
        void pauseexitbutton_KinectButtonClicked()
        {
            count++;
            mIsGameScreenShown = false;
            mIsControllerDetectScreenShown = false;
            mIsPauseScreenShown = true;
            Exit();
        }
        void pauserestartbutton_KinectButtonClicked()
        {
            Scorpion_health = 100;
            LiuKang_health = 100;
            sprite.position = new Vector2(100, groundheight);
            sprite2.position = new Vector2(600, groundheight);
            count++;
            mIsGameScreenShown = true;
            mIsControllerDetectScreenShown = false;
            mIsPauseScreenShown = false;
        }
        protected override void UnloadContent()
        {
            kinect.Stop();
            kinect.Dispose();
        }
        protected override void Update(GameTime gameTime)
        {

            keyboardState = Keyboard.GetState();//gets state of keyboard for Player 1 (Scorpion).
            keyboardState2 = Keyboard.GetState();//gets state of keyboard for Player 2 (Liu Kang).

            gamePadState = GamePad.GetState(PlayerIndex.One);//gets state of keyboard for Player 1 (Scorpion).
            gamePadState2 = GamePad.GetState(PlayerIndex.Two);//gets state of keyboard for Player 2 (Liu Kang).

            bool movementpressed = false;//checks movment for Player 1 (Scorpion).
            bool movementpressed2 = false;//checks movment for Player 2 (Liu Kang).
          
            motion.X = 0;//sets motion for sprite (Scorpion).
            motion2.X = 0;//sets motion for sprite2 (Lui Kang).

            //Left and Right movement for Scorpion.
            if(Sidle == true)
            {
                sprite.IsAnimating = true;
                sprite.CurrentAnimation = AnimationKey.Idle;
            }
            if (scorpionduck == true)
            {
                attacktime += gameTime.ElapsedGameTime.Milliseconds;
                if (attacktime > 1 && attacktime < 100)
                {
                    sprite.IsAnimating = true;
                    sprite.CurrentAnimation = AnimationKey.Duck;
                }
                if (attacktime < 200 && attacktime > 100)
                {
                    sprite.IsAnimating = true;
                    sprite.CurrentAnimation = AnimationKey.Duck;
                }
                if (attacktime > 200 && attacktime < 300)
                {
                    sprite.IsAnimating = true;
                    sprite.CurrentAnimation = AnimationKey.Duck;
                }
                else if (attacktime > 300 && keyboardState.IsKeyUp(Keys.A))
                {
                    attacktime = 0;
                    scorpionduck = false;
                }
            }  
            if (Sleft == true)
            {
                Vector2 lastposition = sprite.position;
                sprite.IsAnimating = true;
                sprite.CurrentAnimation = AnimationKey.Left;
                motion.X = -5;
                sprite.position.X += motion.X;
                if (collision(sprite.boundingbox, sprite2.boundingbox))
                {
                    sprite.position = lastposition;
                }
                movementpressed = true;
            }
            if (Sright == true)
            {
                Vector2 lastposition = sprite.position;
                sprite.IsAnimating = true;
                sprite.CurrentAnimation = AnimationKey.Right;
                motion.X = 5;
                sprite.position.X += motion.X;
                if (collision(sprite.boundingbox, sprite2.boundingbox))
                {
                    sprite.position = lastposition;
                }
                movementpressed = true;
            }
            if (keyboardState.IsKeyDown(Keys.A) || gamePadState.IsButtonDown(Buttons.LeftThumbstickLeft))//checks to see if Linked button is being pressed (Scorpion).
            {
                    Vector2 lastposition = sprite.position;
                    sprite.IsAnimating = true;
                    sprite.CurrentAnimation = AnimationKey.Left;
                    motion.X = -5;
                    sprite.position.X += motion.X;
                    if(collision(sprite.boundingbox, sprite2.boundingbox))                   
                    {
                        sprite.position = lastposition;
                    }
                    movementpressed = true;
            }
            else if (keyboardState.IsKeyDown(Keys.D) || gamePadState.IsButtonDown(Buttons.LeftThumbstickRight))//checks to see if Linked button is being pressed (Scorpion).
            {
                    Vector2 lastposition = sprite.position;
                    sprite.IsAnimating = true;
                    sprite.CurrentAnimation = AnimationKey.Right;
                    motion.X = 5;
                    sprite.position.X += motion.X;
                    if(collision(sprite.boundingbox, sprite2.boundingbox))                  
                    {
                        sprite.position = lastposition;
                    }
                    movementpressed = true;               
            }

            //Left and Right movment for Liu Kang
            if (keyboardState2.IsKeyDown(Keys.J) || gamePadState2.IsButtonDown(Buttons.LeftThumbstickLeft))//checks to see if Linked button is being pressed (Liu Kang).
            {
                Vector2 lastposition = sprite2.position;
                sprite2.IsAnimating = true;
                sprite2.CurrentAnimation = AnimationKey.Left;
                motion2.X = -5;
                sprite2.position.X += motion2.X;
                if (collision(sprite2.boundingbox, sprite.boundingbox))
                {
                    sprite2.position = lastposition;
                }
                movementpressed2 = true;
            }
            else if (keyboardState2.IsKeyDown(Keys.L) || gamePadState2.IsButtonDown(Buttons.LeftThumbstickRight))//checks to see if Linked button is being pressed (Liu Kang).
            {
                Vector2 lastposition = sprite2.position;
                sprite2.IsAnimating = true;
                sprite2.CurrentAnimation = AnimationKey.Right;
                motion2.X = 5;
                sprite2.position.X += motion2.X;
                if (collision(sprite2.boundingbox, sprite.boundingbox))
                {
                    sprite2.position = lastposition;
                }
                movementpressed2 = true;
            }

            //Idle movement for Scorpion
            if (movementpressed == false)
            {
                sprite.IsAnimating = true;
                sprite.CurrentAnimation = AnimationKey.Idle;
            }

            //Idle movment for Liu Kang
            if (movementpressed2 == false)
            {
                sprite2.IsAnimating = true;
                sprite2.CurrentAnimation = AnimationKey.Idle;
            }

            //Motion for Scorpion
            if (motion != Vector2.Zero)
            {
                sprite.IsAnimating = true;
                sprite.Position += motion * sprite.Speed;
            }
            else
            {
                sprite.IsAnimating = true;
            }

            //Motion for Liu Kang
            if (motion2 != Vector2.Zero)
            {
                sprite2.IsAnimating = true;
                sprite2.Position += motion2 * sprite2.Speed;
            }
            else
            {
                sprite2.IsAnimating = true;
            }
            //Punch, Kick, Duck and Block for Scorpion.
            if (gamePadState.IsButtonDown(Buttons.X) || keyboardState.IsKeyDown(Keys.Q))
            {
                scorpionpunch = true;
                if (attack(sprite.attackbox, sprite2.attackbox) && liukangblock == false)
                {
                    LiuKang_health -= 1;
                }
            }
            if (scorpionpunch == true)
            {
                attacktime += gameTime.ElapsedGameTime.Milliseconds;
                if (attacktime > 1 && attacktime < 100)
                {
                    sprite.IsAnimating = true;
                    sprite.CurrentAnimation = AnimationKey.Punch;
                }
                if (attacktime < 200 && attacktime > 100)
                {
                    sprite.IsAnimating = true;
                    sprite.CurrentAnimation = AnimationKey.Punch;
                }
                if (attacktime > 200 && attacktime < 300)
                {
                    sprite.IsAnimating = true;
                    sprite.CurrentAnimation = AnimationKey.Punch;
                }
                else if (attacktime > 300)
                {
                    attacktime = 0;
                    scorpionpunch = false;
                    SPunchInstance.Play();
                }
            }
            if (gamePadState.IsButtonDown(Buttons.Y) || keyboardState.IsKeyDown(Keys.E))
            {
                scorpionkick = true;
                if (attack(sprite2.attackbox, sprite.attackbox) && liukangblock == false)
                {
                    LiuKang_health -= 1;
                }
            }
            if (scorpionkick == true)
            {
                attacktime += gameTime.ElapsedGameTime.Milliseconds;
                if (attacktime > 1 && attacktime < 400)
                {
                    sprite.IsAnimating = true;
                    sprite.CurrentAnimation = AnimationKey.Kick;
                }
                if (attacktime < 500 && attacktime > 400)
                {
                    sprite.IsAnimating = true;
                    sprite.CurrentAnimation = AnimationKey.Kick;
                }
                if (attacktime > 500 && attacktime < 600)
                {
                    sprite.IsAnimating = true;
                    sprite.CurrentAnimation = AnimationKey.Kick;
                }
                else if (attacktime > 600)
                {
                    attacktime = 0;
                    scorpionkick = false;
                    SKickInstance.Play();
                }
            }
            if (gamePadState.IsButtonDown(Buttons.B) || keyboardState.IsKeyDown(Keys.W))
            {
                scorpionblock = true;
            }
            if (scorpionblock == true)
            {
                attacktime += gameTime.ElapsedGameTime.Milliseconds;
                if (attacktime > 1 && attacktime < 100)
                {
                    sprite.IsAnimating = true;
                    sprite.CurrentAnimation = AnimationKey.Block;
                }
                if (attacktime < 200 && attacktime > 100)
                {
                    sprite.IsAnimating = true;
                    sprite.CurrentAnimation = AnimationKey.Block;
                }
                if (attacktime > 200 && attacktime < 300)
                {
                    sprite.IsAnimating = true;
                    sprite.CurrentAnimation = AnimationKey.Block;
                }
                else if (gamePadState.IsButtonUp(Buttons.B))//attacktime > 300 // && gamePadState.IsButtonDown(Buttons.B) || keyboardState.IsKeyDown(Keys.W))
                {
                    attacktime = 0;
                    scorpionblock = false;
                }
            }
            if (gamePadState.IsButtonDown(Buttons.A) || keyboardState.IsKeyDown(Keys.S))
            {
                scorpionduck = true;
            }
            if (scorpionduck == true)
            {
                attacktime += gameTime.ElapsedGameTime.Milliseconds;
                if (attacktime > 1 && attacktime < 100)
                {
                    sprite.IsAnimating = true;
                    sprite.CurrentAnimation = AnimationKey.Duck;
                }
                if (attacktime < 200 && attacktime > 100)
                {
                    sprite.IsAnimating = true;
                    sprite.CurrentAnimation = AnimationKey.Duck;
                }
                if (attacktime > 200 && attacktime < 300)
                {
                    sprite.IsAnimating = true;
                    sprite.CurrentAnimation = AnimationKey.Duck;
                }
                else if (attacktime > 300 && gamePadState.IsButtonDown(Buttons.A) || keyboardState.IsKeyDown(Keys.S))
                {
                    attacktime = 0;
                    scorpionduck = false;
                }
            }  
            //Punch, Kick and Block for Liu Kang.
            if (gamePadState2.IsButtonDown(Buttons.X) || keyboardState2.IsKeyDown(Keys.U))
            {                
                liukangpunch = true;
                if (attack(sprite2.attackbox, sprite.attackbox) && scorpionblock == false)
                {
                    Scorpion_health -= 1;
                }
            }
            if (liukangpunch == true)
            {
                attacktime2 += gameTime.ElapsedGameTime.Milliseconds;
                if (attacktime2 > 1 && attacktime2 < 100)
                {
                    sprite2.IsAnimating = true;
                    sprite2.CurrentAnimation = AnimationKey.Punch;
                }
                if (attacktime2 < 200 && attacktime2 > 100)
                {
                    sprite2.IsAnimating = true;
                    sprite2.CurrentAnimation = AnimationKey.Punch;
                }
                if (attacktime2 > 200 && attacktime2 < 300)
                {
                    sprite2.IsAnimating = true;
                    sprite2.CurrentAnimation = AnimationKey.Punch;
                }
                else if (attacktime2 > 300)
                {                   
                    attacktime2 = 0;
                    liukangpunch = false;
                    LKPunchInstance.Play();
                }
            }
            if (gamePadState2.IsButtonDown(Buttons.Y) || keyboardState2.IsKeyDown(Keys.O))
            {
                liukangkick = true;
                if (attack(sprite2.attackbox, sprite.attackbox) && scorpionblock == false)
                {
                    Scorpion_health -= 1;
                }
            }
            if (liukangkick == true)
            {
                attacktime2 += gameTime.ElapsedGameTime.Milliseconds;
                if (attacktime2 > 1 && attacktime2 < 400)
                {
                    sprite2.IsAnimating = true;
                    sprite2.CurrentAnimation = AnimationKey.Kick;
                }
                if (attacktime2 < 500 && attacktime2 > 400)
                {
                    sprite2.IsAnimating = true;
                    sprite2.CurrentAnimation = AnimationKey.Kick;
                }
                if (attacktime2 > 500 && attacktime2 < 600)
                {
                    sprite2.IsAnimating = true;
                    sprite2.CurrentAnimation = AnimationKey.Kick;
                }
                else if (attacktime2 > 600)
                {
                    attacktime2 = 0;
                    liukangkick = false;
                    LKKickInstance.Play();
                }
            }
            if (gamePadState2.IsButtonDown(Buttons.B) || keyboardState2.IsKeyDown(Keys.I))
            {
                liukangblock = true;
            }
            if (liukangblock == true)
            {
                attacktime2 += gameTime.ElapsedGameTime.Milliseconds;
                if (attacktime2 > 1 && attacktime2 < 100)
                {
                    sprite2.IsAnimating = true;
                    sprite2.CurrentAnimation = AnimationKey.Block;
                }
                if (attacktime2 < 200 && attacktime2 > 100)
                {
                    sprite2.IsAnimating = true;
                    sprite2.CurrentAnimation = AnimationKey.Block;
                }
                if (attacktime2 > 200 && attacktime2 < 300)
                {
                    sprite2.IsAnimating = true;
                    sprite2.CurrentAnimation = AnimationKey.Block;
                }
                else if (gamePadState2.IsButtonUp(Buttons.B))
                {
                    attacktime2 = 0;
                    liukangblock = false;
                }
            }
            if (gamePadState2.IsButtonDown(Buttons.A) || keyboardState2.IsKeyDown(Keys.K))
            {
                liukangduck = true;
            }
            if (liukangduck == true)
            {
                attacktime2 += gameTime.ElapsedGameTime.Milliseconds;
                if (attacktime2 > 1 && attacktime2 < 100)
                {
                    sprite2.IsAnimating = true;
                    sprite2.CurrentAnimation = AnimationKey.Duck;
                }
                if (attacktime2 < 200 && attacktime2 > 100)
                {
                    sprite2.IsAnimating = true;
                    sprite2.CurrentAnimation = AnimationKey.Duck;
                }
                if (attacktime2 > 200 && attacktime2 < 300)
                {
                    sprite2.IsAnimating = true;
                    sprite2.CurrentAnimation = AnimationKey.Duck;
                }
                else if (attacktime2 > 300 && gamePadState2.IsButtonDown(Buttons.A) || keyboardState2.IsKeyUp(Keys.J))
                {
                    attacktime2 = 0;
                    liukangduck = false;
                }
            } 
            if(LiuKang_health == 0)
            {
                MediaPlayer.Play(SWins);
                LiuKang_health = 100;
                Scorpion_health = 100;
                sprite.position = new Vector2(100, groundheight);
                sprite2.position = new Vector2(600, groundheight);
            }
            if (Scorpion_health == 0)
            {
                MediaPlayer.Play(LKWins);
                Scorpion_health = 100;
                LiuKang_health = 100;
                sprite.position = new Vector2(100, groundheight);
                sprite2.position = new Vector2(600, groundheight);
            }
            if(mIsControllerDetectScreenShown)
            {
                startbutton.Update(gameTime, trackedSkeleton);
                exitbutton.Update(gameTime, trackedSkeleton);
                FightInstance.Stop();
                MenuInstance.Play();
                UpdateControllerDetectScreen();
            }
            else if(mIsGameScreenShown)
            {
                MenuInstance.Stop();
                FightInstance.Play();
                UpdateGameScreen();
            }
            if (mIsPauseScreenShown)
            {
                FightInstance.Stop();
                pauseexitbutton.Update(gameTime, trackedSkeleton);
                pauserestartbutton.Update(gameTime, trackedSkeleton);
                pauseresumebutton.Update(gameTime, trackedSkeleton);
                UpdatePauseScreen();
                
            }

            if (keyboardState2.IsKeyDown(Keys.PrintScreen))
            {
                screenshot = true;
            }
            
            sprite.Update(gameTime);//updates sprite (Scorpion).
            sprite2.Update(gameTime);//updates sprite2 (Liu Kang).

            base.Update(gameTime);//updates game world.
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (screenshot)
            {
                GraphicsDevice.PrepareScreenShot();//prepares for screenshot.
            }

            spriteBatch.Begin();
            spriteBatch.Draw(kinectRGBVideo, new Rectangle(0, 0, 640, 480), Color.White);
            spriteBatch.Draw(body, bodyPositionPixels, null, Color.White, 0, new Vector2(body.Width / 2, body.Height / 2), 1, SpriteEffects.None, 0);
            spriteBatch.End();

            if(mIsControllerDetectScreenShown)
            {
                spriteBatch.Begin();              
                spriteBatch.Draw(startscreenTexture, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
                startbutton.Draw(spriteBatch);
                exitbutton.Draw(spriteBatch);
                if (trackedSkeleton != null)
                spriteBatch.Draw(rightHandImage, new Vector2(trackedSkeleton.Joints[JointType.HandLeft].ScaleToScreen().Position.X, trackedSkeleton.Joints[JointType.HandLeft].ScaleToScreen().Position.Y), Color.White);

                spriteBatch.End();
            }
            else if (mIsGameScreenShown)
            {
                GraphicsDevice.Clear(Color.Blue);//clears graphics device.

                spriteBatch.Begin();
                spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
                sprite.Draw(gameTime, spriteBatch);//draws sprite on the screen.
                sprite2.Draw(gameTime, spriteBatch);//draws sprite2 on the screen.
                spriteBatch.End();
              
                mBatch.Begin();//draws Scorpion health bar.
                mBatch.Draw(mHealthBar, new Rectangle(this.Window.ClientBounds.Width / 3 - mHealthBar.Width / 2, 30, 250, 20), new Rectangle(0, 45, mHealthBar.Width, 44), Color.Red);//draws missing health.
                mBatch.Draw(mHealthBar, new Rectangle(this.Window.ClientBounds.Width / 3 - mHealthBar.Width / 2, 30, (int)(250 * ((double)Scorpion_health / 100)), 20),
                new Rectangle(0, 45, mHealthBar.Width, 44), Color.Lime);//draws red for health, grey for missin health.
                mBatch.Draw(mHealthBar, new Rectangle(this.Window.ClientBounds.Width / 3 - mHealthBar.Width / 2, 30, 250, 20), new Rectangle(0, 0, mHealthBar.Width, 44), Color.White);//draws border.

                mBatch.Draw(mHealthBar, new Rectangle(this.Window.ClientBounds.Width - mHealthBar.Width + 210, 30, 250, 20), new Rectangle(0, 45, mHealthBar.Width, 44), Color.Red);//draws missing health.
                mBatch.Draw(mHealthBar, new Rectangle(this.Window.ClientBounds.Width - mHealthBar.Width + 210, 30, (int)(250 * ((double)LiuKang_health / 100)), 20),
                new Rectangle(0, 45, mHealthBar.Width, 44), Color.Lime);//draws red for health, grey for missin health.
                mBatch.Draw(mHealthBar, new Rectangle(this.Window.ClientBounds.Width - mHealthBar.Width + 210, 30, 250, 20), new Rectangle(0, 0, mHealthBar.Width, 44), Color.White);//draws border.
                mBatch.End();

                zbatch.Begin();
                zbatch.Draw(LKnameplate, new Rectangle(this.Window.ClientBounds.Width - mHealthBar.Width + 355, 30, 400, 70), new Rectangle(0, 0, mHealthBar.Width, 44), Color.White);
                zbatch.Draw(Snameplate, new Rectangle(this.Window.ClientBounds.Width / 3 - mHealthBar.Width / 2 + 15, 30, 400, 70), new Rectangle(0, 0, mHealthBar.Width, 44), Color.White);
                zbatch.End();
            }
            else if (mIsPauseScreenShown)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(pausescreenTexture, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
                pauseexitbutton.Draw(spriteBatch);
                pauserestartbutton.Draw(spriteBatch);
                pauseresumebutton.Draw(spriteBatch);
                if (trackedSkeleton != null)
                spriteBatch.Draw(rightHandImage, new Vector2(trackedSkeleton.Joints[JointType.HandLeft].ScaleToScreen().Position.X, trackedSkeleton.Joints[JointType.HandLeft].ScaleToScreen().Position.Y), Color.White);
                spriteBatch.End();
            }
            base.Draw(gameTime);

            if (screenshot)
            {
                GraphicsDevice.SaveScreenshot();
                screenshot = false;
            }
        }
        bool collision(Rectangle A, Rectangle B)
        {
            if (A.Intersects(B))
            {
                return true;
            }
            return false;
        }
        bool attack(Rectangle A, Rectangle B)// intersection for attacks.
        {
            if (A.Intersects(B))
            {
                return true;
            }
            return false;
        }      
    }
}
