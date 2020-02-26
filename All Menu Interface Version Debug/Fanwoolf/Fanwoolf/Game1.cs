using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
//using Microsoft.Xna.Framework.Input.Touch;

namespace Fanwoolf
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 

    enum FanwoolfGameState
    {
        fIntro, 
        fPlay, 
        fContinue, 
        fCredits, 
        fEnd
    }; 


    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player; // Represents the player

        // Keyboard states used to determine key presses
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        // Gamepad states used to determine button presses
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        float playerMoveSpeed; // A movement speed for the player

        Texture2D mainBackground; // Image used to display the static background

        // basic enemy related variables 
        Texture2D eMiG_straightTex;
        List<eMiG_straight> list_eMig_straight; 
        TimeSpan eMiG_straight_SpawnTime;
        TimeSpan prev_eMiG_straight_SpawnTime;
        Random rnd_eMiG_straight;

        int basicMiGCount; // counter for how many basic MiG have been spawned

        // diagonal moving enemy related variables 
        Texture2D eMiG_diagTex;
        List<eMiG_diag> list_eMig_diag;
        TimeSpan eMiG_diag_SpawnTime;
        TimeSpan prev_eMiG_diag_SpawnTime;
        Random rnd_eMiG_diag;

        // shooting and straight moving enemy related variables 
        Texture2D eMiG_straight_basicShooter_Tex;
        List<eMiG_straight_basicShooter> list_eMig_straight_basicShooter;
        TimeSpan eMiG_straight_basicShooter_SpawnTime;
        TimeSpan prev_eMiG_straight_basicShooter_SpawnTime;
        Random rnd_eMiG_straight_basicShooter;

        // wave moving enemy related variables 
        Texture2D eMiG_wave_Tex;
        List<eMiG_wave> list_eMig_wave;
        TimeSpan eMiG_wave_SpawnTime;
        TimeSpan prev_eMiG_wave_SpawnTime;
        Random rnd_eMiG_wave;

        Random random; // A random number generator

        // hero bullets 
        Texture2D hbTex;
        List<heroBullet> list_heroBullet; 

        // enemy bullets
        Texture2D ebbTex;
        List<enemyBulletBasic> list_enemyBulletBasic; 

        // The rate of fire of the player guns 
        TimeSpan fireTime;
        TimeSpan previousFireTime;

        Texture2D explosionTexture;
        List<Animation> explosions;

        Texture2D biTex;
        List<Animation> list_bulletImpacts; 

        SoundEffect gunFireSound;      // The sound that is played when a gun fires one round
        SoundEffect bulletImpactSound;      // The sound that is played when a single bullet hits a metal surface
        SoundEffect enemyCannonSound; 

        SoundEffect explosionSound;  // The sound used when the player or an enemy dies
        Song gameplayMusic;          // The music played during gameplay

        int score;                   // Number that holds the player score
        int maxLives;
        int currentLives; 
        SpriteFont font;             // The font used to display UI elements

        FanwoolfGameState myGameState;

        Boolean isGamePaused;

        Texture2D fIntroTex;
        Texture2D fCreditsTex;
        Texture2D fPauseTex;
        Texture2D fContinueTex;
        Texture2D fEndTex;

        Texture2D uiBackground;
        Texture2D uiTitle; 
        Texture2D healthBarBackground;
        Texture2D awLogo; 

        Texture2D uiLife3;
        Texture2D uiLife2;
        Texture2D uiLife1;

        // green lights 
        Texture2D iL_G_4_UI_Tex;
        Texture2D iL_G_3_UI_Tex;
        Texture2D iL_G_2_UI_Tex;
        Texture2D iL_G_1_UI_Tex;
        Texture2D iL_G_0_UI_Tex;

        // yellow lights 
        Texture2D iL_Y_4_UI_Tex;
        Texture2D iL_Y_3_UI_Tex;
        Texture2D iL_Y_2_UI_Tex;
        Texture2D iL_Y_1_UI_Tex;
        Texture2D iL_Y_0_UI_Tex;

        // orange lights 
        Texture2D iL_O_4_UI_Tex;
        Texture2D iL_O_3_UI_Tex;
        Texture2D iL_O_2_UI_Tex;
        Texture2D iL_O_1_UI_Tex;
        Texture2D iL_O_0_UI_Tex;

        // red lights 
        Texture2D iL_R_4_UI_Tex;
        Texture2D iL_R_3_UI_Tex;
        Texture2D iL_R_2_UI_Tex;
        Texture2D iL_R_1_UI_Tex;
        Texture2D iL_R_0_UI_Tex;

        //clouds

        Texture2D   clouds01op025_Tex;
        List<cloud> clouds01op025_List;
        cloud       clouds01op025_Start; 
        TimeSpan    clouds01op025_nowTime;
        TimeSpan    clouds01op025_prevTime;

        Texture2D   clouds01op050_Tex;
        List<cloud> clouds01op050_List;
        cloud       clouds01op050_Start;
        TimeSpan    clouds01op050_nowTime;
        TimeSpan    clouds01op050_prevTime;

        Texture2D   clouds01op075_Tex;
        List<cloud> clouds01op075_List;
        cloud       clouds01op075_Start;
        TimeSpan    clouds01op075_nowTime;
        TimeSpan    clouds01op075_prevTime;

        Texture2D   clouds01op100_Tex;
        List<cloud> clouds01op100_List;
        cloud       clouds01op100_Start;
        TimeSpan    clouds01op100_nowTime;
        TimeSpan    clouds01op100_prevTime;

        Random      clouds01_rnd;
        Random      clouds01_050_rnd;
        Random      clouds01_075_rnd;
        Random      clouds01_100_rnd;

        Texture2D   clouds02op025_Tex;
        List<cloud> clouds02op025_List;
        cloud       clouds02op025_Start;
        TimeSpan    clouds02op025_nowTime;
        TimeSpan    clouds02op025_prevTime;

        Texture2D   clouds02op050_Tex;
        List<cloud> clouds02op050_List;
        cloud       clouds02op050_Start;
        TimeSpan    clouds02op050_nowTime;
        TimeSpan    clouds02op050_prevTime;

        Texture2D   clouds02op075_Tex;
        List<cloud> clouds02op075_List;
        cloud       clouds02op075_Start;
        TimeSpan    clouds02op075_nowTime;
        TimeSpan    clouds02op075_prevTime;

        Texture2D   clouds02op100_Tex;
        List<cloud> clouds02op100_List;
        cloud       clouds02op100_Start;
        TimeSpan    clouds02op100_nowTime;
        TimeSpan    clouds02op100_prevTime;

        Random      clouds02_rnd;
        Random      clouds02_050_rnd;
        Random      clouds02_075_rnd;
        Random      clouds02_100_rnd;

        Texture2D   clouds03op025_Tex;
        List<cloud> clouds03op025_List;
        cloud       clouds03op025_Start;
        TimeSpan    clouds03op025_nowTime;
        TimeSpan    clouds03op025_prevTime;

        Texture2D   clouds03op050_Tex;
        List<cloud> clouds03op050_List;
        cloud       clouds03op050_Start;
        TimeSpan    clouds03op050_nowTime;
        TimeSpan    clouds03op050_prevTime;

        Texture2D   clouds03op075_Tex;
        List<cloud> clouds03op075_List;
        cloud       clouds03op075_Start;
        TimeSpan    clouds03op075_nowTime;
        TimeSpan    clouds03op075_prevTime;

        Texture2D   clouds03op100_Tex;
        List<cloud> clouds03op100_List;
        cloud       clouds03op100_Start;
        TimeSpan    clouds03op100_nowTime;
        TimeSpan    clouds03op100_prevTime;

        Random      clouds03_rnd;
        Random      clouds03_050_rnd;
        Random      clouds03_075_rnd;
        Random      clouds03_100_rnd;

        Texture2D   clouds04op025_Tex;
        List<cloud> clouds04op025_List;
        cloud       clouds04op025_Start;
        TimeSpan    clouds04op025_nowTime;
        TimeSpan    clouds04op025_prevTime;

        Texture2D   clouds04op050_Tex;
        List<cloud> clouds04op050_List;
        cloud       clouds04op050_Start;
        TimeSpan    clouds04op050_nowTime;
        TimeSpan    clouds04op050_prevTime;

        Texture2D   clouds04op075_Tex;
        List<cloud> clouds04op075_List;
        cloud       clouds04op075_Start;
        TimeSpan    clouds04op075_nowTime;
        TimeSpan    clouds04op075_prevTime;

        Texture2D   clouds04op100_Tex;
        List<cloud> clouds04op100_List;
        cloud       clouds04op100_Start;
        TimeSpan    clouds04op100_nowTime;
        TimeSpan    clouds04op100_prevTime;

        Random      clouds04_rnd;
        Random      clouds04_050_rnd;
        Random      clouds04_075_rnd;
        Random      clouds04_100_rnd;


        Boolean alreadyRanOnce_CloudStarter; 

        // the two background tiles
        Texture2D bMyTex;
        bkVertScrollTile northTile;
        bkVertScrollTile southTile; 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            player = new Player(); // Initialize the player class
            playerMoveSpeed = 8.0f; // Set a constant player move speed

            //Enable the FreeDrag gesture.
            //TouchPanel.EnabledGestures = GestureType.FreeDrag;

            //bgLayer1 = new ParallaxingBackground();
            //bgLayer2 = new ParallaxingBackground();

            random                                     = new Random(); // Initialize our random number generator

            list_eMig_straight           = new List<eMiG_straight>(); 
            eMiG_straight_SpawnTime      = TimeSpan.FromSeconds(2.0f);
            prev_eMiG_straight_SpawnTime = TimeSpan.Zero;
            rnd_eMiG_straight            = new Random(); 

            list_eMig_diag           = new List<eMiG_diag>();
            eMiG_diag_SpawnTime      = TimeSpan.FromSeconds(2.0f);
            prev_eMiG_diag_SpawnTime = TimeSpan.Zero;
            rnd_eMiG_diag            = new Random(); 

            list_eMig_straight_basicShooter            = new List<eMiG_straight_basicShooter>();
            eMiG_straight_basicShooter_SpawnTime       = TimeSpan.FromSeconds(3.0f);
            prev_eMiG_straight_basicShooter_SpawnTime  = TimeSpan.Zero;
            rnd_eMiG_straight_basicShooter             = new Random();

            list_eMig_wave           = new List<eMiG_wave>();
            eMiG_wave_SpawnTime      = TimeSpan.FromSeconds(3.0f);
            prev_eMiG_wave_SpawnTime = TimeSpan.Zero;
            rnd_eMiG_wave            = new Random();

            list_heroBullet       = new List<heroBullet>(); 
            fireTime              = TimeSpan.FromSeconds(.15f);  // Set the laser to fire every quarter second

            list_enemyBulletBasic = new List<enemyBulletBasic>(); 

            explosions            = new List<Animation>();
            list_bulletImpacts    = new List<Animation>();

            clouds01op025_List = new List<cloud>();
            clouds01op025_Start = new cloud();

            clouds01op050_List = new List<cloud>();
            clouds01op050_Start = new cloud();

            clouds01op075_List = new List<cloud>();
            clouds01op075_Start = new cloud();

            clouds01op100_List = new List<cloud>();
            clouds01op100_Start = new cloud();

            clouds02op025_List = new List<cloud>();
            clouds02op025_Start = new cloud();

            clouds02op050_List = new List<cloud>();
            clouds02op050_Start = new cloud();

            clouds02op075_List = new List<cloud>();
            clouds02op075_Start = new cloud();

            clouds02op100_List = new List<cloud>();
            clouds02op100_Start = new cloud();

            clouds03op025_List = new List<cloud>();
            clouds03op025_Start = new cloud();

            clouds03op050_List = new List<cloud>();
            clouds03op050_Start = new cloud();

            clouds03op075_List = new List<cloud>();
            clouds03op075_Start = new cloud();

            clouds03op100_List = new List<cloud>();
            clouds03op100_Start = new cloud();

            clouds04op025_List = new List<cloud>();
            clouds04op025_Start = new cloud();

            clouds04op050_List = new List<cloud>();
            clouds04op050_Start = new cloud();

            clouds04op075_List = new List<cloud>();
            clouds04op075_Start = new cloud();

            clouds04op100_List = new List<cloud>();
            clouds04op100_Start = new cloud();

            clouds01op025_nowTime  = TimeSpan.FromSeconds(9.0f);
            clouds01op025_prevTime = TimeSpan.Zero;
            clouds01op050_nowTime  = TimeSpan.FromSeconds(18.0f);
            clouds01op050_prevTime = TimeSpan.Zero;
            clouds01op075_nowTime  = TimeSpan.FromSeconds(27.0f);
            clouds01op075_prevTime = TimeSpan.Zero;
            clouds01op100_nowTime  = TimeSpan.FromSeconds(45.0f);
            clouds01op100_prevTime = TimeSpan.Zero;

            clouds02op025_nowTime  = TimeSpan.FromSeconds(7.0f);
            clouds02op025_prevTime = TimeSpan.Zero;
            clouds02op050_nowTime  = TimeSpan.FromSeconds(14.0f);
            clouds02op050_prevTime = TimeSpan.Zero;
            clouds02op075_nowTime  = TimeSpan.FromSeconds(21.0f);
            clouds02op075_prevTime = TimeSpan.Zero;
            clouds02op100_nowTime  = TimeSpan.FromSeconds(35.0f);
            clouds02op100_prevTime = TimeSpan.Zero;

            clouds03op025_nowTime  = TimeSpan.FromSeconds(11.0f);
            clouds03op025_prevTime = TimeSpan.Zero;
            clouds03op050_nowTime  = TimeSpan.FromSeconds(22.0f);
            clouds03op050_prevTime = TimeSpan.Zero;
            clouds03op075_nowTime  = TimeSpan.FromSeconds(33.5f);
            clouds03op075_prevTime = TimeSpan.Zero;
            clouds03op100_nowTime  = TimeSpan.FromSeconds(55.0f);
            clouds03op100_prevTime = TimeSpan.Zero;

            clouds04op025_nowTime  = TimeSpan.FromSeconds(5.0f);
            clouds04op025_prevTime = TimeSpan.Zero;
            clouds04op050_nowTime  = TimeSpan.FromSeconds(10.0f);
            clouds04op050_prevTime = TimeSpan.Zero;
            clouds04op075_nowTime  = TimeSpan.FromSeconds(15.0f);
            clouds04op075_prevTime = TimeSpan.Zero;
            clouds04op100_nowTime  = TimeSpan.FromSeconds(25.0f);
            clouds04op100_prevTime = TimeSpan.Zero;

            clouds01_rnd     = new Random();
            clouds01_050_rnd = new Random();
            clouds01_075_rnd = new Random();
            clouds01_100_rnd = new Random();
            clouds02_rnd     = new Random();
            clouds02_050_rnd = new Random();
            clouds02_075_rnd = new Random();
            clouds02_100_rnd = new Random();
            clouds03_rnd     = new Random();
            clouds03_050_rnd = new Random();
            clouds03_075_rnd = new Random();
            clouds03_100_rnd = new Random();
            clouds04_rnd     = new Random();
            clouds04_050_rnd = new Random();
            clouds04_075_rnd = new Random();
            clouds04_100_rnd = new Random();

            clouds01_rnd.Next();
            clouds01_050_rnd.Next();
            clouds01_075_rnd.Next();
            clouds01_100_rnd.Next();
            clouds02_rnd.Next();
            clouds02_050_rnd.Next();
            clouds02_075_rnd.Next();
            clouds02_100_rnd.Next();
            clouds03_rnd.Next();
            clouds03_050_rnd.Next();
            clouds03_075_rnd.Next();
            clouds03_100_rnd.Next();
            clouds04_rnd.Next();
            clouds04_050_rnd.Next();
            clouds04_075_rnd.Next();
            clouds04_100_rnd.Next();

            clouds02_rnd.Next();
            clouds02_050_rnd.Next();
            clouds02_075_rnd.Next();
            clouds02_100_rnd.Next();
            clouds03_rnd.Next();
            clouds03_050_rnd.Next();
            clouds03_075_rnd.Next();
            clouds03_100_rnd.Next();
            clouds04_rnd.Next();
            clouds04_050_rnd.Next();
            clouds04_075_rnd.Next();
            clouds04_100_rnd.Next();

            clouds03_rnd.Next();
            clouds03_050_rnd.Next();
            clouds03_075_rnd.Next();
            clouds03_100_rnd.Next();
            clouds04_rnd.Next();
            clouds04_050_rnd.Next();
            clouds04_075_rnd.Next();
            clouds04_100_rnd.Next();

            clouds04_rnd.Next();
            clouds04_050_rnd.Next();
            clouds04_075_rnd.Next();
            clouds04_100_rnd.Next();

            score = 0; // Set player's score to zero

            myGameState = FanwoolfGameState.fIntro;
            isGamePaused = false;

            maxLives = 3;
            currentLives = maxLives;

            basicMiGCount = 0; 

            northTile = new bkVertScrollTile();
            southTile = new bkVertScrollTile();

            clouds01op025_Start = new cloud();
            clouds01op050_Start = new cloud();
            clouds01op075_Start = new cloud();
            clouds01op100_Start = new cloud();

            clouds02op025_Start = new cloud();
            clouds02op050_Start = new cloud();
            clouds02op075_Start = new cloud();
            clouds02op100_Start = new cloud();

            clouds03op025_Start = new cloud();
            clouds03op050_Start = new cloud();
            clouds03op075_Start = new cloud();
            clouds03op100_Start = new cloud();

            clouds04op025_Start = new cloud();
            clouds04op050_Start = new cloud();
            clouds04op075_Start = new cloud();
            clouds04op100_Start = new cloud();

            alreadyRanOnce_CloudStarter = false; 

            base.Initialize();
        }

        public void ReInitialize()
        {
            // TODO: Add your initialization logic here

            //player = new Player(); // Initialize the player class
            playerMoveSpeed = 8.0f; // Set a constant player move speed

            //Enable the FreeDrag gesture.
            //TouchPanel.EnabledGestures = GestureType.FreeDrag;

            list_eMig_straight.Clear();
            list_eMig_straight = new List<eMiG_straight>();
            eMiG_straight_SpawnTime = TimeSpan.FromSeconds(1.0f);
            prev_eMiG_straight_SpawnTime = TimeSpan.Zero;
            //rnd_eMiG_straight = new Random();                // A random number generator

            list_eMig_diag = new List<eMiG_diag>();
            eMiG_diag_SpawnTime = TimeSpan.FromSeconds(1.0f);
            prev_eMiG_diag_SpawnTime = TimeSpan.Zero;
            //rnd_eMiG_diag = new Random();                // A random number generator

            list_heroBullet.Clear();
            list_heroBullet = new List<heroBullet>();
            fireTime = TimeSpan.FromSeconds(.15f);  // Set the laser to fire every quarter second
            explosions.Clear();
            explosions = new List<Animation>();
            list_bulletImpacts.Clear();
            list_bulletImpacts = new List<Animation>();

            list_eMig_straight_basicShooter.Clear();
            list_eMig_straight_basicShooter = new List<eMiG_straight_basicShooter>();
            eMiG_straight_basicShooter_SpawnTime = TimeSpan.FromSeconds(3.0f);
            prev_eMiG_straight_basicShooter_SpawnTime = TimeSpan.Zero;
            //rnd_eMiG_straight_basicShooter = new Random();

            list_eMig_wave.Clear();
            list_eMig_wave = new List<eMiG_wave>();
            eMiG_wave_SpawnTime = TimeSpan.FromSeconds(3.0f);
            prev_eMiG_wave_SpawnTime = TimeSpan.Zero;
            //rnd_eMiG_wave = new Random();

            list_enemyBulletBasic = new List<enemyBulletBasic>(); 

            score = 0; // Set player's score to zero

            myGameState = FanwoolfGameState.fIntro;
            isGamePaused = false;

            maxLives = 3;
            currentLives = maxLives;

            basicMiGCount = 0; 

            alreadyRanOnce_CloudStarter = false; 

            //base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load the player resources
            Animation playerAnimation = new Animation();
            //Texture2D playerTexture = Content.Load<Texture2D>("shipAnimation");
            Texture2D playerTexture = Content.Load<Texture2D>("paperRotorTest");

            playerAnimation.Initialize (
                playerTexture, 
                Vector2.Zero, 
                84, 
                84, 
                8, 
                6, 
                Color.White, 
                1f, 
                true
            );

            //Texture2D staticTexture = Content.Load<Texture2D>("player");
            Texture2D staticTexture = Content.Load<Texture2D>("af_Top_sm_new");

            Vector2 playerPosition = new Vector2(
                GraphicsDevice.Viewport.TitleSafeArea.X + (GraphicsDevice.Viewport.TitleSafeArea.Width / 3), 
                GraphicsDevice.Viewport.TitleSafeArea.Y + 2 * (GraphicsDevice.Viewport.TitleSafeArea.Height / 3)
            );

            player.Initialize (
                playerAnimation, 
                staticTexture, 
                playerPosition
            );

            // Load the parallaxing background
            /*
            bgLayer1.Initialize ( 
                Content, 
                "bgLayer1", 
                GraphicsDevice.Viewport.Width, 
                -1, 
                0
            );

            bgLayer2.Initialize (
                Content, 
                "bgLayer2", 
                GraphicsDevice.Viewport.Width, 
                -2, 
                0
            );
            */

            clouds01op025_Tex = Content.Load<Texture2D>("clouds01op025");
            clouds01op050_Tex = Content.Load<Texture2D>("clouds01op050");
            clouds01op075_Tex = Content.Load<Texture2D>("clouds01op075");
            clouds01op100_Tex = Content.Load<Texture2D>("clouds01op100");

            clouds02op025_Tex = Content.Load<Texture2D>("clouds02op025");
            clouds02op050_Tex = Content.Load<Texture2D>("clouds02op050");
            clouds02op075_Tex = Content.Load<Texture2D>("clouds02op075");
            clouds02op100_Tex = Content.Load<Texture2D>("clouds02op100");

            clouds03op025_Tex = Content.Load<Texture2D>("clouds03op025");
            clouds03op050_Tex = Content.Load<Texture2D>("clouds03op050");
            clouds03op075_Tex = Content.Load<Texture2D>("clouds03op075");
            clouds03op100_Tex = Content.Load<Texture2D>("clouds03op100");

            clouds04op025_Tex = Content.Load<Texture2D>("clouds04op025");
            clouds04op050_Tex = Content.Load<Texture2D>("clouds04op050");
            clouds04op075_Tex = Content.Load<Texture2D>("clouds04op075");
            clouds04op100_Tex = Content.Load<Texture2D>("clouds04op100");

            bMyTex = Content.Load<Texture2D>("testBG01");

            northTile.Initialize(bMyTex, new Vector2(0, -1439), 0.5f);
            southTile.Initialize(bMyTex, new Vector2(0,  -480), 0.5f); 

            eMiG_straightTex               = Content.Load<Texture2D>("small_myMiG15_2");
            eMiG_diagTex                   = Content.Load<Texture2D>("test02_MiG_19C_dark_sm");
            eMiG_straight_basicShooter_Tex = Content.Load<Texture2D>("test01_mig_21");
            eMiG_wave_Tex                  = Content.Load<Texture2D>("sm_IL-28");

            mainBackground    = Content.Load<Texture2D>("testAlvin_ground_01");

            hbTex             = Content.Load<Texture2D>("heroBullet");
            ebbTex            = Content.Load<Texture2D>("enemyBullet");
            
            explosionTexture = Content.Load<Texture2D>("explosion");
            biTex             = Content.Load<Texture2D>("testBulletImpact_02"); 
            fIntroTex         = Content.Load<Texture2D>("FW_Intro");
            fCreditsTex       = Content.Load<Texture2D>("FW_Credits02");
            fPauseTex         = Content.Load<Texture2D>("pause");
            fContinueTex      = Content.Load<Texture2D>("FW_Continue");
            fEndTex           = Content.Load<Texture2D>("FW_End");

            uiBackground        = Content.Load<Texture2D>("testUI_sidebar_02");
            uiTitle             = Content.Load<Texture2D>("aBaseTitle_01_sm");
            healthBarBackground = Content.Load<Texture2D>("indicatorBar");
            awLogo              = Content.Load<Texture2D>("aLogo");
            uiLife3             = Content.Load<Texture2D>("fwLifeCounter");
            uiLife2             = Content.Load<Texture2D>("fwLifeCounter");
            uiLife1             = Content.Load<Texture2D>("fwLifeCounter");

            iL_G_4_UI_Tex = Content.Load<Texture2D>("iL_Green");
            iL_G_3_UI_Tex = Content.Load<Texture2D>("iL_Green");
            iL_G_2_UI_Tex = Content.Load<Texture2D>("iL_Green");
            iL_G_1_UI_Tex = Content.Load<Texture2D>("iL_Green");
            iL_G_0_UI_Tex = Content.Load<Texture2D>("iL_Green");

            iL_Y_4_UI_Tex = Content.Load<Texture2D>("iL_Yellow");
            iL_Y_3_UI_Tex = Content.Load<Texture2D>("iL_Yellow");
            iL_Y_2_UI_Tex = Content.Load<Texture2D>("iL_Yellow");
            iL_Y_1_UI_Tex = Content.Load<Texture2D>("iL_Yellow");
            iL_Y_0_UI_Tex = Content.Load<Texture2D>("iL_Yellow");

            iL_O_4_UI_Tex = Content.Load<Texture2D>("iL_Orange");
            iL_O_3_UI_Tex = Content.Load<Texture2D>("iL_Orange");
            iL_O_2_UI_Tex = Content.Load<Texture2D>("iL_Orange");
            iL_O_1_UI_Tex = Content.Load<Texture2D>("iL_Orange");
            iL_O_0_UI_Tex = Content.Load<Texture2D>("iL_Orange");

            iL_R_4_UI_Tex = Content.Load<Texture2D>("iL_Red");
            iL_R_3_UI_Tex = Content.Load<Texture2D>("iL_Red");
            iL_R_2_UI_Tex = Content.Load<Texture2D>("iL_Red");
            iL_R_1_UI_Tex = Content.Load<Texture2D>("iL_Red");
            iL_R_0_UI_Tex = Content.Load<Texture2D>("iL_Red");

            gameplayMusic = Content.Load<Song>("Sounds/Combat_Airwolf"); // Load the music

            // Load the laser and explosion sound effect
            explosionSound    = Content.Load<SoundEffect>("Sounds/explosion");
            gunFireSound      = Content.Load<SoundEffect>("Sounds/singleGunShot_01");
            bulletImpactSound = Content.Load<SoundEffect>("Sounds/metal_impact_01");
            enemyCannonSound  = Content.Load<SoundEffect>("Sounds/testCannon_01"); 

            font = Content.Load<SpriteFont>("gameFont"); // Load the score font

            PlayMusic(gameplayMusic); // Start the music right away

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            
            // TODO: Add your update logic here

            // Save the previous state of the keyboard and game pad so we can determinesingle key/button presses
            previousGamePadState = currentGamePadState;
            previousKeyboardState = currentKeyboardState;

            // Read the current state of the keyboard and gamepad and store it
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            
            if      (myGameState == FanwoolfGameState.fIntro) 
            {
                if ((previousKeyboardState.IsKeyDown(Keys.P)) && (currentKeyboardState.IsKeyUp(Keys.P)))                                              { ReInitialize(); myGameState = FanwoolfGameState.fPlay; }
                else if ((previousGamePadState.Buttons.Start == ButtonState.Pressed) && (currentGamePadState.Buttons.Start == ButtonState.Released))  { ReInitialize(); myGameState = FanwoolfGameState.fPlay; } 
                else if ((previousKeyboardState.IsKeyDown(Keys.C))                   && (currentKeyboardState.IsKeyUp(Keys.C)))                       { myGameState = FanwoolfGameState.fCredits; }
                else if ((previousGamePadState.Buttons.X == ButtonState.Pressed)     && (currentGamePadState.Buttons.X == ButtonState.Released))      { myGameState = FanwoolfGameState.fCredits; } 
                else if ((previousKeyboardState.IsKeyDown(Keys.Escape))              && (currentKeyboardState.IsKeyUp(Keys.Escape)))                  { myGameState = FanwoolfGameState.fEnd; }
                else if ((previousGamePadState.Buttons.Back == ButtonState.Pressed)  && (currentGamePadState.Buttons.Back == ButtonState.Released))   { myGameState = FanwoolfGameState.fEnd; } 
            }
            else if (myGameState == FanwoolfGameState.fPlay)
            {
                if (alreadyRanOnce_CloudStarter == false)
                {
                    Add_StarterClouds();
                    alreadyRanOnce_CloudStarter = true;
                }

                if ((previousGamePadState.Buttons.Start == ButtonState.Pressed) && (currentGamePadState.Buttons.Start == ButtonState.Released))
                {
                    if      (isGamePaused == false) { isGamePaused = true; }
                    else if (isGamePaused == true)  { isGamePaused = false; }
                    else                            { isGamePaused = false; }
                }

                if ((previousKeyboardState.IsKeyDown(Keys.Pause)) && (currentKeyboardState.IsKeyUp(Keys.Pause)))
                {
                    if (isGamePaused == false) { isGamePaused = true; }
                    else if (isGamePaused == true) { isGamePaused = false; }
                    else { isGamePaused = false; }
                }

                // Allows the game to exit
                if ((previousKeyboardState.IsKeyDown(Keys.Escape)) && (currentKeyboardState.IsKeyUp(Keys.Escape)))                                 { myGameState = FanwoolfGameState.fContinue; }
                else if ((previousGamePadState.Buttons.Back == ButtonState.Pressed) && (currentGamePadState.Buttons.Back == ButtonState.Released)) { myGameState = FanwoolfGameState.fContinue; }

                // helper function to organize function calls 
                BigUpdater(gameTime);
            }
            else if (myGameState == FanwoolfGameState.fContinue)
            {
                if ((previousKeyboardState.IsKeyDown(Keys.P)) && (currentKeyboardState.IsKeyUp(Keys.P))) 
                {
                    ReInitialize(); 
                    myGameState = FanwoolfGameState.fPlay; 
                }
                else if ((previousGamePadState.Buttons.Start == ButtonState.Pressed) && (currentGamePadState.Buttons.Start == ButtonState.Released))
                {
                    ReInitialize();
                    myGameState = FanwoolfGameState.fPlay;
                }
                else if ((previousKeyboardState.IsKeyDown(Keys.C))                  && (currentKeyboardState.IsKeyUp(Keys.C)))                      { myGameState = FanwoolfGameState.fCredits; }
                else if ((previousGamePadState.Buttons.X == ButtonState.Pressed)    && (currentGamePadState.Buttons.X == ButtonState.Released))     { myGameState = FanwoolfGameState.fCredits; }
                else if ((previousKeyboardState.IsKeyDown(Keys.I))                  && (currentKeyboardState.IsKeyUp(Keys.I)))                      { myGameState = FanwoolfGameState.fIntro; }
                else if ((previousGamePadState.Buttons.Y == ButtonState.Pressed)    && (currentGamePadState.Buttons.Y == ButtonState.Released))     { myGameState = FanwoolfGameState.fIntro; }
                else if ((previousKeyboardState.IsKeyDown(Keys.Escape))             && (currentKeyboardState.IsKeyUp(Keys.Escape)))                 { myGameState = FanwoolfGameState.fEnd; }
                else if ((previousGamePadState.Buttons.Back == ButtonState.Pressed) && (currentGamePadState.Buttons.Back == ButtonState.Released))  { myGameState = FanwoolfGameState.fEnd; }
            }
            else if (myGameState == FanwoolfGameState.fCredits)
            {
                if      ((previousKeyboardState.IsKeyDown(Keys.P))      && (currentKeyboardState.IsKeyUp(Keys.P)))      
                {
                    ReInitialize(); 
                    myGameState = FanwoolfGameState.fPlay; 
                }
                else if ((previousGamePadState.Buttons.Start == ButtonState.Pressed) && (currentGamePadState.Buttons.Start == ButtonState.Released))
                {
                    ReInitialize();
                    myGameState = FanwoolfGameState.fPlay;
                }
                else if ((previousKeyboardState.IsKeyDown(Keys.I))                  && (currentKeyboardState.IsKeyUp(Keys.I)))                      { myGameState = FanwoolfGameState.fIntro; }
                else if ((previousGamePadState.Buttons.Y == ButtonState.Pressed)    && (currentGamePadState.Buttons.Y == ButtonState.Released))     { myGameState = FanwoolfGameState.fIntro; }
                else if ((previousKeyboardState.IsKeyDown(Keys.Escape))             && (currentKeyboardState.IsKeyUp(Keys.Escape)))                 { myGameState = FanwoolfGameState.fEnd; }
                else if ((previousGamePadState.Buttons.Back == ButtonState.Pressed) && (currentGamePadState.Buttons.Back == ButtonState.Released))  { myGameState = FanwoolfGameState.fEnd; }

            }

            else if (myGameState == FanwoolfGameState.fEnd) { QuitMyGame(); }
            else { myGameState = FanwoolfGameState.fIntro; }

            base.Update(gameTime);
        }

        void QuitMyGame()
        { this.Exit(); }

        private void PlayMusic(Song song)
        {
            // Due to the way the MediaPlayer plays music, we have to catch the exception. Music will play when the game is not tethered
            try
            {
                MediaPlayer.Play(song);          // Play the music
                MediaPlayer.IsRepeating = true;  // Loop the currently playing song
            }
            catch { }
        }

        public void BigUpdater(GameTime gTime)
        {
            // test run of a mega updating function 
            if (isGamePaused == false)
            {
                UpdatePlayer(gTime);      // Update the player

                northTile.Update(gTime);
                southTile.Update(gTime);

                Update_eMiG_Enemies(gTime);
                Update_eMig_straight_Collision();
                Update_eMig_diag_Collision();
                Update_eMig_straight_basicShooter_Collision();
                Update_eMig_wave_Collision();

                UpdateHeroMainProjectiles();
                UpdateEnemyBullets(); 
                UpdateExplosions(gTime);  // Update the explosions
                UpdateBulletExplosions(gTime);
                UpdateClouds(gTime);
            }

        }

        private void Add_eMiG_straight()
        {
            Texture2D enemyTex = eMiG_straightTex; 
            Vector2 pos        = new Vector2(random.Next(50, 640 - enemyTex.Height), -2 * enemyTex.Height); // Randomly generate the position of the enemy
            eMiG_straight eMiG = new eMiG_straight();                                   // Create an enemy

            eMiG.Initialize(enemyTex, pos); // Initialize the enemy
            list_eMig_straight.Add(eMiG);

            basicMiGCount++; 
        }

        private void Add_eMiG_diag()
        {
            Texture2D enemyTex = eMiG_diagTex;
            Vector2 pos = new Vector2(random.Next(50, 640 - enemyTex.Height), -2 * enemyTex.Height); // Randomly generate the position of the enemy
            eMiG_diag eMiG = new eMiG_diag();                                   // Create an enemy

            eMiG.Initialize(enemyTex, pos); // Initialize the enemy
            list_eMig_diag.Add(eMiG);
        }

        private void Add_eMiG_straight_basicShooter()
        {
            Texture2D enemyTex = eMiG_straight_basicShooter_Tex;
            Vector2 pos = new Vector2(random.Next(50, 640 - enemyTex.Height), -2 * enemyTex.Height); // Randomly generate the position of the enemy
            eMiG_straight_basicShooter eMiG = new eMiG_straight_basicShooter();                                   // Create an enemy

            eMiG.Initialize(enemyTex, pos); // Initialize the enemy
            list_eMig_straight_basicShooter.Add(eMiG);
        }

        private void Add_eMiG_wave()
        {
            Texture2D enemyTex = eMiG_wave_Tex;
            Vector2 pos = new Vector2(random.Next(50, 640 - enemyTex.Height), -2 * enemyTex.Height); // Randomly generate the position of the enemy
            eMiG_wave eMiG = new eMiG_wave();                                   // Create an enemy

            eMiG.Initialize(enemyTex, pos); // Initialize the enemy
            list_eMig_wave.Add(eMiG);

            basicMiGCount++;
        }

        private void AddHB(Vector2 position)
        {
            heroBullet hb = new heroBullet(); 
            hb.Initialize(hbTex, position);
            list_heroBullet.Add(hb);
        }

        private void Add_eBB(Vector2 position)
        {
            enemyBulletBasic newEBB = new enemyBulletBasic();
            newEBB.Initialize(ebbTex, position);
            list_enemyBulletBasic.Add(newEBB);
            enemyCannonSound.Play(0.6f, 0.0f, 0.0f);
        }

        private void AddExplosion(Vector2 position)
        {
            Animation explosion = new Animation();
            explosion.Initialize(explosionTexture, position, 134, 134, 12, 45, Color.White, 1f, false);
            explosions.Add(explosion);

            explosionSound.Play(0.75f, 0.0f, 0.0f); // Play the explosion sound
        }

        private void AddBulletImpactExplosion(Vector2 position)
        {
            Animation biexpl = new Animation();
            biexpl.Initialize(biTex, position, 67, 67, 8, 5, Color.White, 1f, false);
            list_bulletImpacts.Add(biexpl);

            bulletImpactSound.Play(0.20f, 0.0f, 0.0f);
        }

        private void Add_StarterClouds()
        {
            // Randomly generate the position of the cloud 
            float tmpCloudDrift;

            int tmpX, tmpY;
            Vector2 pos;

            tmpX = clouds01_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width));
            tmpY = clouds01_rnd.Next(0, GraphicsDevice.Viewport.Height); 
            pos = new Vector2 ( (float) tmpX, (float) tmpY );

            tmpCloudDrift = clouds01_rnd.Next(1, 2); 
            clouds01op025_Start.Initialize(clouds01op025_Tex, pos, tmpCloudDrift);
            clouds01op025_List.Add(clouds01op025_Start); // Add the cloud to the active clouds list 

            tmpX = clouds01_050_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width));
            tmpY = clouds01_050_rnd.Next(0, GraphicsDevice.Viewport.Height);
            pos = new Vector2((float)tmpX, (float)tmpY);

            tmpCloudDrift = clouds01_rnd.Next(1, 4);
            clouds01op050_Start.Initialize(clouds01op050_Tex, pos, tmpCloudDrift);
            clouds01op050_List.Add(clouds01op050_Start); // Add the cloud to the active clouds list 

            tmpX = clouds01_075_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width));
            tmpY = clouds01_075_rnd.Next(0, GraphicsDevice.Viewport.Height);
            pos = new Vector2((float)tmpX, (float)tmpY);

            tmpCloudDrift = clouds01_rnd.Next(1, 4);
            clouds01op075_Start.Initialize(clouds01op075_Tex, pos, tmpCloudDrift);
            clouds01op075_List.Add(clouds01op075_Start); // Add the cloud to the active clouds list 

            tmpX = clouds01_100_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width));
            tmpY = clouds01_100_rnd.Next(0, GraphicsDevice.Viewport.Height);
            pos = new Vector2((float)tmpX, (float)tmpY);

            tmpCloudDrift = clouds01_rnd.Next(1, 4);
            clouds01op100_Start.Initialize(clouds01op100_Tex, pos, tmpCloudDrift);
            clouds01op100_List.Add(clouds01op100_Start); // Add the cloud to the active clouds list 

            tmpX = clouds02_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width));
            tmpY = clouds02_rnd.Next(0, GraphicsDevice.Viewport.Height);
            pos = new Vector2((float)tmpX, (float)tmpY);

            tmpCloudDrift = clouds02_rnd.Next(1, 4);
            clouds02op025_Start.Initialize(clouds02op025_Tex, pos, tmpCloudDrift);
            clouds02op025_List.Add(clouds02op025_Start); // Add the cloud to the active clouds list 

            tmpX = clouds02_050_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width));
            tmpY = clouds02_050_rnd.Next(0, GraphicsDevice.Viewport.Height);
            pos = new Vector2((float)tmpX, (float)tmpY);

            tmpCloudDrift = clouds02_rnd.Next(1, 4);
            clouds02op050_Start.Initialize(clouds02op050_Tex, pos, tmpCloudDrift);
            clouds02op050_List.Add(clouds02op050_Start); // Add the cloud to the active clouds list 

            tmpX = clouds02_075_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width));
            tmpY = clouds02_075_rnd.Next(0, GraphicsDevice.Viewport.Height);
            pos = new Vector2((float)tmpX, (float)tmpY);

            tmpCloudDrift = clouds02_rnd.Next(1, 4);
            clouds02op075_Start.Initialize(clouds02op075_Tex, pos, tmpCloudDrift);
            clouds02op075_List.Add(clouds02op075_Start); // Add the cloud to the active clouds list 

            tmpX = clouds02_100_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width));
            tmpY = clouds02_100_rnd.Next(0, GraphicsDevice.Viewport.Height);
            pos = new Vector2((float)tmpX, (float)tmpY);

            tmpCloudDrift = clouds02_rnd.Next(1, 4);
            clouds02op100_Start.Initialize(clouds02op100_Tex, pos, tmpCloudDrift);
            clouds02op100_List.Add(clouds02op100_Start); // Add the cloud to the active clouds list 

            /*
            tmpX = clouds03_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width));
            tmpY = clouds03_rnd.Next(0, GraphicsDevice.Viewport.Height);
            pos = new Vector2((float)tmpX, (float)tmpY);

            tmpCloudDrift = clouds03_rnd.Next(1, 4);
            clouds03op025_Start.Initialize(clouds03op025_Tex, pos, tmpCloudDrift);
            clouds03op025_List.Add(clouds03op025_Start); // Add the cloud to the active clouds list 

            tmpX = clouds03_050_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width));
            tmpY = clouds03_050_rnd.Next(0, GraphicsDevice.Viewport.Height);
            pos = new Vector2((float)tmpX, (float)tmpY);

            tmpCloudDrift = clouds03_rnd.Next(1, 4);
            clouds03op050_Start.Initialize(clouds03op050_Tex, pos, tmpCloudDrift);
            clouds03op050_List.Add(clouds03op050_Start); // Add the cloud to the active clouds list 

            tmpX = clouds03_075_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width));
            tmpY = clouds03_075_rnd.Next(0, GraphicsDevice.Viewport.Height);
            pos = new Vector2((float)tmpX, (float)tmpY);

            tmpCloudDrift = clouds03_rnd.Next(1, 4);
            clouds03op075_Start.Initialize(clouds03op075_Tex, pos, tmpCloudDrift);
            clouds03op075_List.Add(clouds03op075_Start); // Add the cloud to the active clouds list 

            tmpX = clouds03_100_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width));
            tmpY = clouds03_100_rnd.Next(0, GraphicsDevice.Viewport.Height);
            pos = new Vector2((float)tmpX, (float)tmpY);

            tmpCloudDrift = clouds03_rnd.Next(1, 4);
            clouds03op100_Start.Initialize(clouds03op100_Tex, pos, tmpCloudDrift);
            clouds03op100_List.Add(clouds03op100_Start); // Add the cloud to the active clouds list 

            tmpX = clouds04_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width));
            tmpY = clouds04_rnd.Next(0, GraphicsDevice.Viewport.Height);
            pos = new Vector2((float)tmpX, (float)tmpY);

            tmpCloudDrift = clouds04_rnd.Next(1, 4);
            clouds04op025_Start.Initialize(clouds04op025_Tex, pos, tmpCloudDrift);
            clouds04op025_List.Add(clouds04op025_Start); // Add the cloud to the active clouds list 

            tmpX = clouds04_050_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width));
            tmpY = clouds04_050_rnd.Next(0, GraphicsDevice.Viewport.Height);
            pos = new Vector2((float)tmpX, (float)tmpY);

            tmpCloudDrift = clouds04_rnd.Next(1, 4);
            clouds04op050_Start.Initialize(clouds04op050_Tex, pos, tmpCloudDrift);
            clouds04op050_List.Add(clouds04op050_Start); // Add the cloud to the active clouds list 

            tmpX = clouds04_075_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width));
            tmpY = clouds04_075_rnd.Next(0, GraphicsDevice.Viewport.Height);
            pos = new Vector2((float)tmpX, (float)tmpY);

            tmpCloudDrift = clouds04_rnd.Next(1, 4);
            clouds04op075_Start.Initialize(clouds04op075_Tex, pos, tmpCloudDrift);
            clouds04op075_List.Add(clouds04op075_Start); // Add the cloud to the active clouds list 

            tmpX = clouds04_100_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width));
            tmpY = clouds04_100_rnd.Next(0, GraphicsDevice.Viewport.Height);
            pos = new Vector2((float)tmpX, (float)tmpY);

            tmpCloudDrift = clouds04_rnd.Next(1, 4);
            clouds04op100_Start.Initialize(clouds04op100_Tex, pos, tmpCloudDrift);
            clouds04op100_List.Add(clouds04op100_Start); // Add the cloud to the active clouds list 
            */

        }

        private void Add_clouds01op025()
        {
            // Randomly generate the position of the cloud 
            float tmpCloudDrift; 
            Vector2 pos = new Vector2 (clouds01_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width)), - clouds01op025_Tex.Height);
            tmpCloudDrift = clouds01_rnd.Next(1, 2); 
			cloud tmpCloud = new cloud(); 
            tmpCloud.Initialize(clouds01op025_Tex, pos, tmpCloudDrift);
            clouds01op025_List.Add(tmpCloud); // Add the cloud to the active clouds list 
        }

        private void Add_clouds01op050()
        {
            // Randomly generate the position of the cloud 
            float tmpCloudDrift;
            clouds01_050_rnd.Next();
            Vector2 pos = new Vector2(clouds01_050_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width)), -clouds01op050_Tex.Height);
            tmpCloudDrift = clouds01_050_rnd.Next(1, 2); 
			cloud tmpCloud = new cloud(); 
            tmpCloud.Initialize(clouds01op050_Tex, pos, tmpCloudDrift);
            clouds01op050_List.Add(tmpCloud); // Add the cloud to the active clouds list 
        }

        private void Add_clouds01op075()
        {
            // Randomly generate the position of the cloud 
            float tmpCloudDrift;
            clouds01_075_rnd.Next();
            clouds01_075_rnd.Next();
            Vector2 pos = new Vector2(clouds01_075_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width)), -clouds01op075_Tex.Height);
            tmpCloudDrift = clouds01_075_rnd.Next(1, 2); 
			cloud tmpCloud = new cloud(); 
            tmpCloud.Initialize(clouds01op075_Tex, pos, tmpCloudDrift);
            clouds01op075_List.Add(tmpCloud); // Add the cloud to the active clouds list 
        }

        private void Add_clouds01op100()
        {
            // Randomly generate the position of the cloud 
            float tmpCloudDrift;
            clouds01_100_rnd.Next();
            clouds01_100_rnd.Next();
            clouds01_100_rnd.Next();
            Vector2 pos = new Vector2(clouds01_100_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width)), -clouds01op100_Tex.Height);
            tmpCloudDrift = clouds01_100_rnd.Next(1, 2); 
			cloud tmpCloud = new cloud(); 
            tmpCloud.Initialize(clouds01op100_Tex, pos, tmpCloudDrift);
            clouds01op100_List.Add(tmpCloud); // Add the cloud to the active clouds list 
        }

        private void Add_clouds02op025()
        {
            // Randomly generate the position of the cloud 
            float tmpCloudDrift; 
            Vector2 pos = new Vector2 (clouds02_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width)), - clouds02op025_Tex.Height);
            tmpCloudDrift = clouds02_rnd.Next(1, 2); 
			cloud tmpCloud = new cloud(); 
            tmpCloud.Initialize(clouds02op025_Tex, pos, tmpCloudDrift);
            clouds02op025_List.Add(tmpCloud); // Add the cloud to the active clouds list 
        }

        private void Add_clouds02op050()
        {
            // Randomly generate the position of the cloud 
            float tmpCloudDrift;
            clouds02_050_rnd.Next();
            Vector2 pos = new Vector2(clouds02_050_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width)), -clouds02op050_Tex.Height);
            tmpCloudDrift = clouds02_050_rnd.Next(1, 2); 
			cloud tmpCloud = new cloud(); 
            tmpCloud.Initialize(clouds02op050_Tex, pos, tmpCloudDrift);
            clouds02op050_List.Add(tmpCloud); // Add the cloud to the active clouds list 
        }

        private void Add_clouds02op075()
        {
            // Randomly generate the position of the cloud 
            float tmpCloudDrift;
            clouds02_075_rnd.Next();
            clouds02_075_rnd.Next();
            Vector2 pos = new Vector2(clouds02_075_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width)), -clouds02op075_Tex.Height);
            tmpCloudDrift = clouds02_075_rnd.Next(1, 2); 
			cloud tmpCloud = new cloud(); 
            tmpCloud.Initialize(clouds02op075_Tex, pos, tmpCloudDrift);
            clouds02op075_List.Add(tmpCloud); // Add the cloud to the active clouds list 
        }

        private void Add_clouds02op100()
        {
            // Randomly generate the position of the cloud 
            float tmpCloudDrift;
            clouds02_100_rnd.Next();
            clouds02_100_rnd.Next();
            clouds02_100_rnd.Next();
            Vector2 pos = new Vector2(clouds02_100_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width)), -clouds02op100_Tex.Height);
            tmpCloudDrift = clouds02_100_rnd.Next(1, 2); 
			cloud tmpCloud = new cloud(); 
            tmpCloud.Initialize(clouds02op100_Tex, pos, tmpCloudDrift);
            clouds02op100_List.Add(tmpCloud); // Add the cloud to the active clouds list 
        }

        private void Add_clouds03op025()
        {
            // Randomly generate the position of the cloud 
            float tmpCloudDrift; 
            Vector2 pos = new Vector2 (clouds03_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width)), - clouds03op025_Tex.Height);
            tmpCloudDrift = clouds03_rnd.Next(1, 2); 
			cloud tmpCloud = new cloud(); 
            tmpCloud.Initialize(clouds03op025_Tex, pos, tmpCloudDrift);
            clouds03op025_List.Add(tmpCloud); // Add the cloud to the active clouds list 
        }

        private void Add_clouds03op050()
        {
            // Randomly generate the position of the cloud 
            float tmpCloudDrift;
            clouds03_050_rnd.Next();
            Vector2 pos = new Vector2(clouds03_050_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width)), -clouds03op050_Tex.Height);
            tmpCloudDrift = clouds03_050_rnd.Next(1, 2); 
			cloud tmpCloud = new cloud(); 
            tmpCloud.Initialize(clouds03op050_Tex, pos, tmpCloudDrift);
            clouds03op050_List.Add(tmpCloud); // Add the cloud to the active clouds list 
        }

        private void Add_clouds03op075()
        {
            // Randomly generate the position of the cloud 
            float tmpCloudDrift;
            clouds03_075_rnd.Next();
            clouds03_075_rnd.Next();
            Vector2 pos = new Vector2(clouds03_075_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width)), -clouds03op075_Tex.Height);
            tmpCloudDrift = clouds03_075_rnd.Next(1, 2); 
			cloud tmpCloud = new cloud(); 
            tmpCloud.Initialize(clouds03op075_Tex, pos, tmpCloudDrift);
            clouds03op075_List.Add(tmpCloud); // Add the cloud to the active clouds list 
        }

        private void Add_clouds03op100()
        {
            // Randomly generate the position of the cloud 
            float tmpCloudDrift;
            clouds03_100_rnd.Next();
            clouds03_100_rnd.Next();
            clouds03_100_rnd.Next();
            Vector2 pos = new Vector2(clouds03_100_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width)), -clouds03op100_Tex.Height);
            tmpCloudDrift = clouds03_100_rnd.Next(1, 2); 
			cloud tmpCloud = new cloud(); 
            tmpCloud.Initialize(clouds03op100_Tex, pos, tmpCloudDrift);
            clouds03op100_List.Add(tmpCloud); // Add the cloud to the active clouds list 
        }

        private void Add_clouds04op025()
        {
            // Randomly generate the position of the cloud 
            float tmpCloudDrift; 
            Vector2 pos = new Vector2 (clouds04_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width)), - clouds04op025_Tex.Height);
            tmpCloudDrift = clouds04_rnd.Next(1, 2); 
			cloud tmpCloud = new cloud(); 
            tmpCloud.Initialize(clouds04op025_Tex, pos, tmpCloudDrift);
            clouds04op025_List.Add(tmpCloud); // Add the cloud to the active clouds list 
        }

        private void Add_clouds04op050()
        {
            // Randomly generate the position of the cloud 
            float tmpCloudDrift;
            clouds04_050_rnd.Next();
            Vector2 pos = new Vector2(clouds04_050_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width)), -clouds04op050_Tex.Height);
            tmpCloudDrift = clouds04_050_rnd.Next(1, 2); 
			cloud tmpCloud = new cloud(); 
            tmpCloud.Initialize(clouds04op050_Tex, pos, tmpCloudDrift);
            clouds04op050_List.Add(tmpCloud); // Add the cloud to the active clouds list 
        }

        private void Add_clouds04op075()
        {
            // Randomly generate the position of the cloud 
            float tmpCloudDrift;
            clouds04_075_rnd.Next();
            clouds04_075_rnd.Next();
            Vector2 pos = new Vector2(clouds04_075_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width)), -clouds04op075_Tex.Height);
            tmpCloudDrift = clouds04_075_rnd.Next(1, 2); 
			cloud tmpCloud = new cloud(); 
            tmpCloud.Initialize(clouds04op075_Tex, pos, tmpCloudDrift);
            clouds04op075_List.Add(tmpCloud); // Add the cloud to the active clouds list 
        }

        private void Add_clouds04op100()
        {
            // Randomly generate the position of the cloud 
            float tmpCloudDrift;
            clouds04_100_rnd.Next();
            clouds04_100_rnd.Next();
            clouds04_100_rnd.Next();
            Vector2 pos = new Vector2(clouds04_100_rnd.Next(-(int)(0.5 * GraphicsDevice.Viewport.Width), (int)(0.5 * GraphicsDevice.Viewport.Width)), -clouds04op100_Tex.Height);
            tmpCloudDrift = clouds04_100_rnd.Next(1, 2); 
			cloud tmpCloud = new cloud(); 
            tmpCloud.Initialize(clouds04op100_Tex, pos, tmpCloudDrift);
            clouds04op100_List.Add(tmpCloud); // Add the cloud to the active clouds list 
        }

        private void UpdatePlayer(GameTime gameTime)
        {
            player.Update(gameTime);

            Vector2 sBulletPos = new Vector2();
            sBulletPos.X = player.Position.X - player.tWidth / 2;
            sBulletPos.Y = player.Position.Y + player.tHeight / 2;

            Vector2 pBulletPos = new Vector2();
            pBulletPos.X = player.Position.X + player.tWidth / 2;
            pBulletPos.Y = player.Position.Y + player.tHeight / 2; 


            // Fire only every interval we set as the fireTime
            if (gameTime.TotalGameTime - previousFireTime > fireTime)
            {
                // Reset our current time
                previousFireTime = gameTime.TotalGameTime;

                if (currentKeyboardState.IsKeyDown(Keys.Space) || currentGamePadState.Triggers.Right >= 0.25f)
                {
                    AddHB(player.Position + new Vector2(            0, player.tHeight / 2));
                    gunFireSound.Play(0.10f, 0.0f, 0.0f);
                    AddHB(player.Position + new Vector2(player.tWidth, player.tHeight / 2));
                    gunFireSound.Play(0.10f, 0.0f, 0.0f);
                    
                }
                 
            }

            // Get Thumbstick Controls
            player.Position.X += currentGamePadState.ThumbSticks.Left.X * playerMoveSpeed;
            player.Position.Y -= currentGamePadState.ThumbSticks.Left.Y * playerMoveSpeed;

            // Use the Keyboard / Dpad
            if (currentKeyboardState.IsKeyDown(Keys.Left)  || currentGamePadState.DPad.Left  == ButtonState.Pressed) { player.Position.X -= playerMoveSpeed; }
            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentGamePadState.DPad.Right == ButtonState.Pressed) { player.Position.X += playerMoveSpeed; }
            if (currentKeyboardState.IsKeyDown(Keys.Up)    || currentGamePadState.DPad.Up    == ButtonState.Pressed) { player.Position.Y -= playerMoveSpeed; }
            if (currentKeyboardState.IsKeyDown(Keys.Down)  || currentGamePadState.DPad.Down  == ButtonState.Pressed) { player.Position.Y += playerMoveSpeed; }

            // Make sure that the player does not go out of bounds
            player.Position.X = MathHelper.Clamp 
            (
                player.Position.X, 
                0, 
                640 - player.Width
            );

            player.Position.Y = MathHelper.Clamp 
            (
                player.Position.Y, 
                0, 
                GraphicsDevice.Viewport.Height - player.Height
            );

            // reset score if player health goes to zero
            if (player.Health <= 0)
            {
                player.Health = 100;
                score = 0;
            }
        }

        private void UpdateHeroMainProjectiles()
        {
            for (int i = list_heroBullet.Count - 1; i >= 0; i--)
            {
                list_heroBullet[i].Update();
                if (list_heroBullet[i].Active == false) { list_heroBullet.RemoveAt(i); }
            }
        }

        private void UpdateEnemyBullets()
        {
            for (int i = list_enemyBulletBasic.Count - 1; i >= 0; i--)
            {
                list_enemyBulletBasic[i].Update();
                if (list_enemyBulletBasic[i].Active == false) { list_enemyBulletBasic.RemoveAt(i); }
            }
        }

        private void UpdateExplosions(GameTime gameTime)
        {
            for (int i = explosions.Count - 1; i >= 0; i--)
            {
                explosions[i].Update(gameTime);
                if (explosions[i].Active == false) { explosions.RemoveAt(i); }
            }
        }

        private void UpdateBulletExplosions(GameTime gameTime)
        {
            for (int i = list_bulletImpacts.Count - 1; i >= 0; i--)
            {
                list_bulletImpacts[i].Update(gameTime);
                if (list_bulletImpacts[i].Active == false) { list_bulletImpacts.RemoveAt(i); }
            }
        }

        private void Update_eMig_straight_Collision()
        {
            // Use the Rectangle's built-in intersect function to
            // determine if two objects are overlapping
            Rectangle rect_eMig_straight; 
            Rectangle rect_heroBullet;
            Rectangle rect_player; 

            // Projectile vs eMig_straight Collision
            for (int i = 0; i < list_heroBullet.Count; i++)
            {

                rect_heroBullet = new Rectangle
                (
                    (int)list_heroBullet[i].Position.X - list_heroBullet[i].Width / 2,
                    (int)list_heroBullet[i].Position.Y - list_heroBullet[i].Height / 2,
                    list_heroBullet[i].Width,
                    list_heroBullet[i].Height
                );

                for (int j = 0; j < list_eMig_straight.Count; j++)
                {
                    // Create the rectangles we need to determine if we collided with each other
                    
                    rect_eMig_straight = new Rectangle 
                    (
                        (int)list_eMig_straight[j].Position.X,
                        (int)list_eMig_straight[j].Position.Y - list_eMig_straight[j].Height / 2,
                        list_eMig_straight[j].Width,
                        list_eMig_straight[j].Height
                    );
                    
                     // Determine if the two objects collided with each other
                    if (rect_heroBullet.Intersects(rect_eMig_straight))
                    {
                        AddBulletImpactExplosion(list_heroBullet[i].Position - new Vector2(list_eMig_straight[j].Width / 2, list_eMig_straight[j].Height / 4)); 
                        list_eMig_straight[j].Health -= list_heroBullet[i].Damage;
                        list_heroBullet[i].Active = false;
                    }
                }
            }

            // Only create the rectangle once for the player
            rect_player = new Rectangle 
            (
                (int)player.Position.X - (player.Width  / 2),
                (int)player.Position.Y - (player.Height / 2),
                player.Width,
                player.Height
            );

            // Do the collision between the player and the enemies
            for (int i = 0; i < list_eMig_straight.Count; i++)
            {
                rect_eMig_straight = new Rectangle 
                (
                    (int)list_eMig_straight[i].Position.X - (list_eMig_straight[i].Width / 2),
                    (int)list_eMig_straight[i].Position.Y - (list_eMig_straight[i].Height / 2),
                    list_eMig_straight[i].Width,
                    list_eMig_straight[i].Height
                );

                // Determine if the two objects collided with each
                // other
                if (rect_player.Intersects(rect_eMig_straight))
                {
                    // Subtract the health from the player based on
                    // the enemy damage
                    player.Health -= list_eMig_straight[i].Damage;

                    // Since the enemy collided with the player
                    // destroy it
                    list_eMig_straight[i].Health = 0;

                    // If the player health is less than zero we died
                    if (player.Health <= 0)
                    //player.Active = false;
                    {
                        currentLives--;
                        player.Health = 100;
                        if (currentLives <= 0)
                        {
                            myGameState = FanwoolfGameState.fContinue;
                        }
                    }
                }

            }

        }

        private void Update_eMig_diag_Collision()
        {
            // Use the Rectangle's built-in intersect function to
            // determine if two objects are overlapping
            Rectangle rect_eMig_diag;
            Rectangle rect_heroBullet;
            Rectangle rect_player;

            // Projectile vs eMig_straight Collision
            for (int i = 0; i < list_heroBullet.Count; i++)
            {

                rect_heroBullet = new Rectangle
                (
                    (int)list_heroBullet[i].Position.X - list_heroBullet[i].Width / 2,
                    (int)list_heroBullet[i].Position.Y - list_heroBullet[i].Height / 2,
                    list_heroBullet[i].Width,
                    list_heroBullet[i].Height
                );

                for (int j = 0; j < list_eMig_diag.Count; j++)
                {
                    // Create the rectangles we need to determine if we collided with each other

                    rect_eMig_diag = new Rectangle
                    (
                        (int)list_eMig_diag[j].Position.X,
                        (int)list_eMig_diag[j].Position.Y - list_eMig_diag[j].Height / 2,
                        list_eMig_diag[j].Width,
                        list_eMig_diag[j].Height
                    );

                    // Determine if the two objects collided with each other
                    if (rect_heroBullet.Intersects(rect_eMig_diag))
                    {
                        AddBulletImpactExplosion(list_heroBullet[i].Position - new Vector2(list_eMig_diag[j].Width / 2, list_eMig_diag[j].Height / 4));
                        list_eMig_diag[j].Health -= list_heroBullet[i].Damage;
                        list_heroBullet[i].Active = false;
                    }
                }
            }

            // Only create the rectangle once for the player
            rect_player = new Rectangle
            (
                (int)player.Position.X - (player.Width / 2),
                (int)player.Position.Y - (player.Height / 2),
                player.Width,
                player.Height
            );

            // Do the collision between the player and the enemies
            for (int i = 0; i < list_eMig_diag.Count; i++)
            {
                rect_eMig_diag = new Rectangle
                (
                    (int)list_eMig_diag[i].Position.X - (list_eMig_diag[i].Width / 2),
                    (int)list_eMig_diag[i].Position.Y - (list_eMig_diag[i].Height / 2),
                    list_eMig_diag[i].Width,
                    list_eMig_diag[i].Height
                );

                // Determine if the two objects collided with each other
                if (rect_player.Intersects(rect_eMig_diag))
                {
                    // Subtract the health from the player based on the enemy damage
                    player.Health -= list_eMig_diag[i].Damage;

                    // Since the enemy collided with the player
                    // destroy it
                    list_eMig_diag[i].Health = 0;

                    // If the player health is less than zero we died
                    if (player.Health <= 0)
                    //player.Active = false;
                    {
                        currentLives--;
                        player.Health = 100;
                        if (currentLives <= 0)
                        {
                            myGameState = FanwoolfGameState.fContinue;
                        }
                    }
                }

            }

        }

        private void Update_eMig_straight_basicShooter_Collision()
        {
            // Use the Rectangle's built-in intersect function to
            // determine if two objects are overlapping
            Rectangle rect_eMig_straight_basicShooter;
            Rectangle rect_heroBullet;
            Rectangle rect_player;
            Rectangle rect_enemyBullet;

            // Projectile vs eMig_straight Collision
            for (int i = 0; i < list_heroBullet.Count; i++)
            {

                rect_heroBullet = new Rectangle
                (
                    (int)list_heroBullet[i].Position.X - list_heroBullet[i].Width / 2,
                    (int)list_heroBullet[i].Position.Y - list_heroBullet[i].Height / 2,
                    list_heroBullet[i].Width,
                    list_heroBullet[i].Height
                );

                for (int j = 0; j < list_eMig_straight_basicShooter.Count; j++)
                {
                    // Create the rectangles we need to determine if we collided with each other

                    rect_eMig_straight_basicShooter = new Rectangle
                    (
                        (int)list_eMig_straight_basicShooter[j].Position.X,
                        (int)list_eMig_straight_basicShooter[j].Position.Y - list_eMig_straight_basicShooter[j].Height / 2,
                        list_eMig_straight_basicShooter[j].Width,
                        list_eMig_straight_basicShooter[j].Height
                    );

                    // Determine if the two objects collided with each other
                    if (rect_heroBullet.Intersects(rect_eMig_straight_basicShooter))
                    {
                        AddBulletImpactExplosion(list_heroBullet[i].Position - new Vector2(list_eMig_straight_basicShooter[j].Width / 2, list_eMig_straight_basicShooter[j].Height / 4));
                        list_eMig_straight_basicShooter[j].Health -= list_heroBullet[i].Damage;
                        list_heroBullet[i].Active = false;
                    }
                }
            }

            // Only create the rectangle once for the player
            rect_player = new Rectangle
            (
                (int)player.Position.X - (player.Width / 2),
                (int)player.Position.Y - (player.Height / 2),
                player.Width,
                player.Height
            );

            // Do the collision between the player and the enemies
            for (int i = 0; i < list_eMig_straight_basicShooter.Count; i++)
            {
                rect_eMig_straight_basicShooter = new Rectangle
                (
                    (int)list_eMig_straight_basicShooter[i].Position.X - (list_eMig_straight_basicShooter[i].Width / 2),
                    (int)list_eMig_straight_basicShooter[i].Position.Y - (list_eMig_straight_basicShooter[i].Height / 2),
                    list_eMig_straight_basicShooter[i].Width,
                    list_eMig_straight_basicShooter[i].Height
                );

                // Determine if the two objects collided with each
                // other
                if (rect_player.Intersects(rect_eMig_straight_basicShooter))
                {
                    // Subtract the health from the player based on
                    // the enemy damage
                    player.Health -= list_eMig_straight_basicShooter[i].Damage;

                    // Since the enemy collided with the player
                    // destroy it
                    list_eMig_straight_basicShooter[i].Health = 0;

                    // If the player health is less than zero we died
                    if (player.Health <= 0)
                    //player.Active = false;
                    {
                        currentLives--;
                        player.Health = 100;
                        if (currentLives <= 0)
                        {
                            myGameState = FanwoolfGameState.fContinue;
                        }
                    }
                }

            }

            // Do the collision between the player and the enemy bullets
            for (int i = 0; i < list_enemyBulletBasic.Count; i++)
            {
                rect_enemyBullet = new Rectangle
                (
                    (int)list_enemyBulletBasic[i].Position.X - (list_enemyBulletBasic[i].Width / 2),
                    (int)list_enemyBulletBasic[i].Position.Y - (list_enemyBulletBasic[i].Height / 2),
                    list_enemyBulletBasic[i].Width,
                    list_enemyBulletBasic[i].Height
                );

                // Determine if the two objects collided with each other
                if (rect_player.Intersects(rect_enemyBullet))
                {
                    AddBulletImpactExplosion(list_enemyBulletBasic[i].Position - new Vector2(list_enemyBulletBasic[i].Width / 2, 0)); 
                    // Subtract the health from the player based on the enemy damage
                    player.Health -= list_enemyBulletBasic[i].Damage;

                    // Since the enemy collided with the player destroy it
                    list_enemyBulletBasic[i].Active = false;

                    // If the player health is less than zero we died
                    if (player.Health <= 0)
                    //player.Active = false;
                    {
                        currentLives--;
                        player.Health = 100;
                        if (currentLives <= 0)
                        {
                            myGameState = FanwoolfGameState.fContinue;
                        }
                    }
                }

            }
        }

        private void Update_eMig_wave_Collision()
        {
            // Use the Rectangle's built-in intersect function to
            // determine if two objects are overlapping
            Rectangle rect_eMig_wave;
            Rectangle rect_heroBullet;
            Rectangle rect_player;

            // Projectile vs eMig_straight Collision
            for (int i = 0; i < list_heroBullet.Count; i++)
            {

                rect_heroBullet = new Rectangle
                (
                    (int)list_heroBullet[i].Position.X - list_heroBullet[i].Width / 2,
                    (int)list_heroBullet[i].Position.Y - list_heroBullet[i].Height / 2,
                    list_heroBullet[i].Width,
                    list_heroBullet[i].Height
                );

                for (int j = 0; j < list_eMig_wave.Count; j++)
                {
                    // Create the rectangles we need to determine if we collided with each other

                    rect_eMig_wave = new Rectangle
                    (
                        (int)list_eMig_wave[j].Position.X,
                        (int)list_eMig_wave[j].Position.Y - list_eMig_wave[j].Height / 2,
                        list_eMig_wave[j].Width,
                        list_eMig_wave[j].Height
                    );

                    // Determine if the two objects collided with each other
                    if (rect_heroBullet.Intersects(rect_eMig_wave))
                    {
                        AddBulletImpactExplosion(list_heroBullet[i].Position - new Vector2(list_eMig_wave[j].Width / 2, list_eMig_wave[j].Height / 4));
                        list_eMig_wave[j].Health -= list_heroBullet[i].Damage;
                        list_heroBullet[i].Active = false;
                    }
                }
            }

            // Only create the rectangle once for the player
            rect_player = new Rectangle
            (
                (int)player.Position.X - (player.Width / 2),
                (int)player.Position.Y - (player.Height / 2),
                player.Width,
                player.Height
            );

            // Do the collision between the player and the enemies
            for (int i = 0; i < list_eMig_wave.Count; i++)
            {
                rect_eMig_wave = new Rectangle
                (
                    (int)list_eMig_wave[i].Position.X - (list_eMig_wave[i].Width / 2),
                    (int)list_eMig_wave[i].Position.Y - (list_eMig_wave[i].Height / 2),
                    list_eMig_wave[i].Width,
                    list_eMig_wave[i].Height
                );

                // Determine if the two objects collided with each
                // other
                if (rect_player.Intersects(rect_eMig_wave))
                {
                    // Subtract the health from the player based on
                    // the enemy damage
                    player.Health -= list_eMig_wave[i].Damage;

                    // Since the enemy collided with the player
                    // destroy it
                    list_eMig_wave[i].Health = 0;

                    // If the player health is less than zero we died
                    if (player.Health <= 0)
                    //player.Active = false;
                    {
                        currentLives--;
                        player.Health = 100;
                        if (currentLives <= 0)
                        {
                            myGameState = FanwoolfGameState.fContinue;
                        }
                    }
                }

            }

        }

        private void Update_eMiG_Enemies(GameTime gameTime)
        {

            if (gameTime.TotalGameTime - prev_eMiG_straight_SpawnTime > eMiG_straight_SpawnTime) // Spawn a new enemy enemy every 1.5 seconds
            {
                prev_eMiG_straight_SpawnTime = gameTime.TotalGameTime;
                Add_eMiG_straight(); // Add an Enemy

                if (basicMiGCount >= 10)
                {
                    Add_eMiG_diag(); // Add an Enemy
                }

                if (basicMiGCount >= 20)
                {
                    Add_eMiG_wave(); // Add an Enemy
                }

                if (basicMiGCount >= 30)
                {
                    Add_eMiG_straight_basicShooter(); // Add an Enemy
                }


            }

            for (int i = list_eMig_straight.Count - 1; i >= 0; i--) // Update the Enemies
            {
                list_eMig_straight[i].Update(gameTime);

                if (list_eMig_straight[i].Active == false)
                {
                    // If not active and health <= 0
                    if (list_eMig_straight[i].Health <= 0)
                    {
                        // Add an explosion
                        list_eMig_straight[i].Position.X -= ((list_eMig_straight[i].Width / 2) - 7);
                        list_eMig_straight[i].Position.Y -= ((list_eMig_straight[i].Height / 2) - 7);
                        AddExplosion(list_eMig_straight[i].Position);

                        score += list_eMig_straight[i].Value; //Add to the player's score
                    }
                    list_eMig_straight.RemoveAt(i);

                }
            }

            for (int i = list_eMig_diag.Count - 1; i >= 0; i--) // Update the Enemies
            {
                list_eMig_diag[i].Update(gameTime);

                if (list_eMig_diag[i].Active == false)
                {
                    // If not active and health <= 0
                    if (list_eMig_diag[i].Health <= 0)
                    {
                        // Add an explosion
                        list_eMig_diag[i].Position.X -= ((list_eMig_diag[i].Width / 2) - 7);
                        list_eMig_diag[i].Position.Y -= ((list_eMig_diag[i].Height / 2) - 7);
                        AddExplosion(list_eMig_diag[i].Position);

                        score += list_eMig_diag[i].Value; //Add to the player's score
                    }
                    list_eMig_diag.RemoveAt(i);

                }
            }

            Vector2 ebPos = new Vector2(); 

            for (int i = list_eMig_straight_basicShooter.Count - 1; i >= 0; i--) // Update the Enemies
            {
                list_eMig_straight_basicShooter[i].Update(gameTime);

                if (gameTime.TotalGameTime - list_eMig_straight_basicShooter[i].prevFireTime_basicMiG > list_eMig_straight_basicShooter[i].fireTime_basicMiG)
                {
                    list_eMig_straight_basicShooter[i].prevFireTime_basicMiG = gameTime.TotalGameTime;

                    if (list_eMig_straight_basicShooter[i].maxShots > 0)
                    {
                        // shoot a bullet downward
                        ebPos = list_eMig_straight_basicShooter[i].Position;
                        ebPos.Y += list_eMig_straight_basicShooter[i].Height;
                        ebPos.X += list_eMig_straight_basicShooter[i].Width  / 2;
                        Add_eBB(ebPos);
                        list_eMig_straight_basicShooter[i].maxShots--;
                    }
                }

                if (list_eMig_straight_basicShooter[i].Active == false)
                {
                    // If not active and health <= 0
                    if (list_eMig_straight_basicShooter[i].Health <= 0)
                    {
                        // Add an explosion
                        list_eMig_straight_basicShooter[i].Position.X -= ((list_eMig_straight_basicShooter[i].Width / 2) - 7);
                        list_eMig_straight_basicShooter[i].Position.Y -= ((list_eMig_straight_basicShooter[i].Height / 2) - 7);
                        AddExplosion(list_eMig_straight_basicShooter[i].Position);

                        score += list_eMig_straight_basicShooter[i].Value; //Add to the player's score
                    }
                    list_eMig_straight_basicShooter.RemoveAt(i);

                }
            }

            for (int i = list_eMig_wave.Count - 1; i >= 0; i--) // Update the Enemies
            {
                list_eMig_wave[i].Update(gameTime);

                if (list_eMig_wave[i].Active == false)
                {
                    // If not active and health <= 0
                    if (list_eMig_wave[i].Health <= 0)
                    {
                        // Add an explosion
                        list_eMig_wave[i].Position.X -= ((list_eMig_wave[i].Width / 2) - 7);
                        list_eMig_wave[i].Position.Y -= ((list_eMig_wave[i].Height / 2) - 7);
                        AddExplosion(list_eMig_wave[i].Position);

                        score += list_eMig_wave[i].Value; //Add to the player's score
                    }
                    list_eMig_wave.RemoveAt(i);

                }
            }
        }

        private void UpdateClouds(GameTime gameTime)
        {
            int k;

            if (gameTime.TotalGameTime - clouds01op025_prevTime > clouds01op025_nowTime) { clouds01op025_prevTime = gameTime.TotalGameTime; Add_clouds01op025(); }
            if (gameTime.TotalGameTime - clouds01op050_prevTime > clouds01op050_nowTime) { clouds01op050_prevTime = gameTime.TotalGameTime; Add_clouds01op050(); }
            if (gameTime.TotalGameTime - clouds01op075_prevTime > clouds01op075_nowTime) { clouds01op075_prevTime = gameTime.TotalGameTime; Add_clouds01op075(); }
            if (gameTime.TotalGameTime - clouds01op100_prevTime > clouds01op100_nowTime) { clouds01op100_prevTime = gameTime.TotalGameTime; Add_clouds01op100(); }

            if (gameTime.TotalGameTime - clouds02op025_prevTime > clouds02op025_nowTime) { clouds02op025_prevTime = gameTime.TotalGameTime; Add_clouds02op025(); }
            if (gameTime.TotalGameTime - clouds02op050_prevTime > clouds02op050_nowTime) { clouds02op050_prevTime = gameTime.TotalGameTime; Add_clouds02op050(); }
            if (gameTime.TotalGameTime - clouds02op075_prevTime > clouds02op075_nowTime) { clouds02op075_prevTime = gameTime.TotalGameTime; Add_clouds02op075(); }
            if (gameTime.TotalGameTime - clouds02op100_prevTime > clouds02op100_nowTime) { clouds02op100_prevTime = gameTime.TotalGameTime; Add_clouds02op100(); }

            if (gameTime.TotalGameTime - clouds03op025_prevTime > clouds03op025_nowTime) { clouds03op025_prevTime = gameTime.TotalGameTime; Add_clouds03op025(); }
            if (gameTime.TotalGameTime - clouds03op050_prevTime > clouds03op050_nowTime) { clouds03op050_prevTime = gameTime.TotalGameTime; Add_clouds03op050(); }
            if (gameTime.TotalGameTime - clouds03op075_prevTime > clouds03op075_nowTime) { clouds03op075_prevTime = gameTime.TotalGameTime; Add_clouds03op075(); }
            if (gameTime.TotalGameTime - clouds03op100_prevTime > clouds03op100_nowTime) { clouds03op100_prevTime = gameTime.TotalGameTime; Add_clouds03op100(); }

            if (gameTime.TotalGameTime - clouds04op025_prevTime > clouds04op025_nowTime) { clouds04op025_prevTime = gameTime.TotalGameTime; Add_clouds04op025(); }
            if (gameTime.TotalGameTime - clouds04op050_prevTime > clouds04op050_nowTime) { clouds04op050_prevTime = gameTime.TotalGameTime; Add_clouds04op050(); }
            if (gameTime.TotalGameTime - clouds04op075_prevTime > clouds04op075_nowTime) { clouds04op075_prevTime = gameTime.TotalGameTime; Add_clouds04op075(); }
            if (gameTime.TotalGameTime - clouds04op100_prevTime > clouds04op100_nowTime) { clouds04op100_prevTime = gameTime.TotalGameTime; Add_clouds04op100(); }

            for (k = clouds01op025_List.Count - 1; k >= 0; k--) { clouds01op025_List[k].Update(gameTime); }
            for (k = clouds01op050_List.Count - 1; k >= 0; k--) { clouds01op050_List[k].Update(gameTime); }
            for (k = clouds01op075_List.Count - 1; k >= 0; k--) { clouds01op075_List[k].Update(gameTime); }
            for (k = clouds01op100_List.Count - 1; k >= 0; k--) { clouds01op100_List[k].Update(gameTime); }

            for (k = clouds02op025_List.Count - 1; k >= 0; k--) { clouds02op025_List[k].Update(gameTime); }
            for (k = clouds02op050_List.Count - 1; k >= 0; k--) { clouds02op050_List[k].Update(gameTime); }
            for (k = clouds02op075_List.Count - 1; k >= 0; k--) { clouds02op075_List[k].Update(gameTime); }
            for (k = clouds02op100_List.Count - 1; k >= 0; k--) { clouds02op100_List[k].Update(gameTime); }

            for (k = clouds03op025_List.Count - 1; k >= 0; k--) { clouds03op025_List[k].Update(gameTime); }
            for (k = clouds03op050_List.Count - 1; k >= 0; k--) { clouds03op050_List[k].Update(gameTime); }
            for (k = clouds03op075_List.Count - 1; k >= 0; k--) { clouds03op075_List[k].Update(gameTime); }
            for (k = clouds03op100_List.Count - 1; k >= 0; k--) { clouds03op100_List[k].Update(gameTime); }

            for (k = clouds04op025_List.Count - 1; k >= 0; k--) { clouds04op025_List[k].Update(gameTime); }
            for (k = clouds04op050_List.Count - 1; k >= 0; k--) { clouds04op050_List[k].Update(gameTime); }
            for (k = clouds04op075_List.Count - 1; k >= 0; k--) { clouds04op075_List[k].Update(gameTime); }
            for (k = clouds04op100_List.Count - 1; k >= 0; k--) { clouds04op100_List[k].Update(gameTime); }

        }

        public void FPlayDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mainBackground, Vector2.Zero, Color.White);

            int k; 

            // Draw the moving background
            //bgLayer1.Draw(spriteBatch);
            //bgLayer2.Draw(spriteBatch);

            northTile.Draw(spriteBatch);
            southTile.Draw(spriteBatch);

            // 100% opacity

            for (k = clouds01op100_List.Count - 1; k >= 0; k--) { clouds01op100_List[k].Draw(spriteBatch); }
            for (k = clouds02op100_List.Count - 1; k >= 0; k--) { clouds02op100_List[k].Draw(spriteBatch); }
            //for (k = clouds03op100_List.Count - 1; k >= 0; k--) { clouds03op100_List[k].Draw(spriteBatch); }
            //for (k = clouds04op100_List.Count - 1; k >= 0; k--) { clouds04op100_List[k].Draw(spriteBatch); }


            // 75% opacity
            for (k = clouds01op075_List.Count - 1; k >= 0; k--) { clouds01op075_List[k].Draw(spriteBatch); }
            for (k = clouds02op075_List.Count - 1; k >= 0; k--) { clouds02op075_List[k].Draw(spriteBatch); }
            //for (k = clouds03op075_List.Count - 1; k >= 0; k--) { clouds03op075_List[k].Draw(spriteBatch); }
            //for (k = clouds04op075_List.Count - 1; k >= 0; k--) { clouds04op075_List[k].Draw(spriteBatch); }

            // Draw the Enemies
            for (int i = 0; i < list_eMig_straight.Count;              i++) { list_eMig_straight[i].Draw(spriteBatch); }
            for (int i = 0; i < list_eMig_diag.Count;                  i++) { list_eMig_diag[i].Draw(spriteBatch); }
            for (int i = 0; i < list_eMig_straight_basicShooter.Count; i++) { list_eMig_straight_basicShooter[i].Draw(spriteBatch); }
            for (int i = 0; i < list_eMig_wave.Count;                  i++) { list_eMig_wave[i].Draw(spriteBatch); }

            // Draw the Projectiles
            for (int i = 0; i < list_heroBullet.Count;       i++) { list_heroBullet[i].Draw(spriteBatch); }
            for (int i = 0; i < list_enemyBulletBasic.Count; i++) { list_enemyBulletBasic[i].Draw(spriteBatch); }

            // 50% opacity

            for (k = clouds01op050_List.Count - 1; k >= 0; k--) { clouds01op050_List[k].Draw(spriteBatch); }
            for (k = clouds02op050_List.Count - 1; k >= 0; k--) { clouds02op050_List[k].Draw(spriteBatch); }
            //for (k = clouds03op050_List.Count - 1; k >= 0; k--) { clouds03op050_List[k].Draw(spriteBatch); }
            //for (k = clouds04op050_List.Count - 1; k >= 0; k--) { clouds04op050_List[k].Draw(spriteBatch); }


            // Draw the explosions
            for (int i = 0; i < explosions.Count; i++) { explosions[i].Draw(spriteBatch); }

            // Draw the explosions
            for (int i = 0; i < list_bulletImpacts.Count; i++) { list_bulletImpacts[i].Draw(spriteBatch); }

            // 25% opacity
            for (k = clouds01op025_List.Count - 1; k >= 0; k--) { clouds01op025_List[k].Draw(spriteBatch); }
            for (k = clouds02op025_List.Count - 1; k >= 0; k--) { clouds02op025_List[k].Draw(spriteBatch); }
            //for (k = clouds03op025_List.Count - 1; k >= 0; k--) { clouds03op025_List[k].Draw(spriteBatch); }
            //for (k = clouds04op025_List.Count - 1; k >= 0; k--) { clouds04op025_List[k].Draw(spriteBatch); }


            spriteBatch.Draw(uiBackground, new Vector2 (640, 0), Color.White);
            //spriteBatch.Draw(uiTitle, new Vector2(640, 0), Color.White);
            spriteBatch.Draw(awLogo, new Vector2(640, 40), Color.White);
            spriteBatch.Draw(healthBarBackground, new Vector2(665, 160), Color.White);

            if (player.Health > 95) spriteBatch.Draw(iL_G_4_UI_Tex, new Vector2(766, 164), Color.White);
            if (player.Health > 90) spriteBatch.Draw(iL_G_3_UI_Tex, new Vector2(761, 164), Color.White);
            if (player.Health > 85) spriteBatch.Draw(iL_G_2_UI_Tex, new Vector2(756, 164), Color.White);
            if (player.Health > 80) spriteBatch.Draw(iL_G_1_UI_Tex, new Vector2(751, 164), Color.White);
            if (player.Health > 75) spriteBatch.Draw(iL_G_0_UI_Tex, new Vector2(746, 164), Color.White);

            if (player.Health > 70) spriteBatch.Draw(iL_Y_4_UI_Tex, new Vector2(740, 164), Color.White);
            if (player.Health > 65) spriteBatch.Draw(iL_Y_3_UI_Tex, new Vector2(735, 164), Color.White);
            if (player.Health > 60) spriteBatch.Draw(iL_Y_2_UI_Tex, new Vector2(730, 164), Color.White);
            if (player.Health > 55) spriteBatch.Draw(iL_Y_1_UI_Tex, new Vector2(725, 164), Color.White);
            if (player.Health > 50) spriteBatch.Draw(iL_Y_0_UI_Tex, new Vector2(720, 164), Color.White);

            if (player.Health > 45) spriteBatch.Draw(iL_O_4_UI_Tex, new Vector2(714, 164), Color.White);
            if (player.Health > 40) spriteBatch.Draw(iL_O_3_UI_Tex, new Vector2(709, 164), Color.White);
            if (player.Health > 35) spriteBatch.Draw(iL_O_2_UI_Tex, new Vector2(704, 164), Color.White);
            if (player.Health > 30) spriteBatch.Draw(iL_O_1_UI_Tex, new Vector2(699, 164), Color.White);
            if (player.Health > 25) spriteBatch.Draw(iL_O_0_UI_Tex, new Vector2(694, 164), Color.White);

            if (player.Health > 20) spriteBatch.Draw(iL_R_4_UI_Tex, new Vector2(688, 164), Color.White);
            if (player.Health > 15) spriteBatch.Draw(iL_R_3_UI_Tex, new Vector2(683, 164), Color.White);
            if (player.Health > 10) spriteBatch.Draw(iL_R_2_UI_Tex, new Vector2(678, 164), Color.White);
            if (player.Health >  5) spriteBatch.Draw(iL_R_1_UI_Tex, new Vector2(673, 164), Color.White);
            if (player.Health >  0) spriteBatch.Draw(iL_R_0_UI_Tex, new Vector2(668, 164), Color.White);


            // Draw the score
            spriteBatch.DrawString(
                font,
                "Score: " + score,
                new Vector2(645, 190),
                Color.White
            );

            // Draw the player health
            spriteBatch.DrawString(
                font,
                "Health: " + player.Health,
                new Vector2(645, 210),
                Color.White
            );

            // Draw the player lives remaining
            spriteBatch.DrawString(
                font,
                "Lives left: " + currentLives,
                new Vector2(645, 230),
                Color.White
            );

            if (currentLives >= 3) { spriteBatch.Draw(uiLife3, new Vector2(749, 260), Color.White); }
            if (currentLives >= 2) { spriteBatch.Draw(uiLife2, new Vector2(715, 260), Color.White); }
            if (currentLives >= 1) { spriteBatch.Draw(uiLife1, new Vector2(681, 260), Color.White); }
            
            // Draw the Player
            player.Draw(spriteBatch);

            // draw pause screen if invoked 
            if (isGamePaused == true) { spriteBatch.Draw(fPauseTex, Vector2.Zero, Color.White); }
        } 

        public void FIntroDraw(SpriteBatch spriteBatch)
        { 
            spriteBatch.Draw(fIntroTex, Vector2.Zero, Color.White);
        }

        public void FCreditsDraw(SpriteBatch spriteBatch)
        { 
            spriteBatch.Draw(fCreditsTex, Vector2.Zero, Color.White);
        }

        public void FContinueDraw(SpriteBatch spriteBatch) 
        { 
            spriteBatch.Draw(fContinueTex, Vector2.Zero, Color.White);
            // Draw the score
            spriteBatch.DrawString(
                font,
                "final score: " + score,
                new Vector2(290, 303),
                Color.DeepSkyBlue
            );
        }

        public void FEndDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(fEndTex, Vector2.Zero, Color.White);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            // Start drawing
            spriteBatch.Begin();

            if (myGameState == FanwoolfGameState.fPlay)
            {
                FPlayDraw(spriteBatch);
            }
            else if (myGameState == FanwoolfGameState.fIntro)
            {
                FIntroDraw(spriteBatch); 
            }
            else if (myGameState == FanwoolfGameState.fCredits)
            {
                FCreditsDraw(spriteBatch); 
            }
            else if (myGameState == FanwoolfGameState.fContinue)
            {
                FContinueDraw(spriteBatch);
            }
            else if (myGameState == FanwoolfGameState.fEnd)
            {
                FEndDraw(spriteBatch); 
            }
            else
            {
                // default to fIntro stuff
                FIntroDraw(spriteBatch); 
            }

            // Stop drawing
            spriteBatch.End(); 

            base.Draw(gameTime);
        }
    }
}
