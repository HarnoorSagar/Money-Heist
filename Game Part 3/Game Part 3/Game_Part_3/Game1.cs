/* Harnoor Sagar
 * Game: Part 3
 * December 13, 2023
 * ICS4U
 */

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

namespace Game_Part_3
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //declares the players 
        Players player1, player2;

        //declares background variable
        GameItem background;

        //declares win/lose variables
        GameItem win;
        GameItem lose;

        //declares vault variables
        GameItem vaultClosed;
        GameItem vaultOpen;

        //declares the keysCollected variable
        int keysCollected = 0;

        //declares the list variable for platforms and the keys
        List<GameItem> keyList;

        //declares platform array
        GameItem[] platforms = new GameItem[11];
        
        //declares the timer variables
        Text timerText;
        int gameTimer = 45;
        int timeCount = 3600;
        int newTimeCount;

        //declares the states
        GamePadState pad, oldPad, pad2, oldPad2;
        KeyboardState kb, oldKb;

        //declares the song variable
        Song backgroundSong;

        //declares a bool for the lose/win sound variables
        bool loseSoundPlaying, winSoundPlaying;

        bool winGame;

        //declares font
        SpriteFont font;

        //declares screen size variables
        int windowWidth;
        int windowHeight;

        //sets player width and height
        const int playerWidth = 60;
        const int playerHeight = 80;

        //sets the platform height
        const int platformHeight = 30;

        //sets the key size
        const int keySize = 40;

        //declares platform hittest bools
        bool player1PlatL = false, player1PlatR = false, player2PlatL = false, player2PlatR = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            //sets the size of the graphics device to the size of the window
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 30;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

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
            //sets window width and height to window width and height
            windowWidth = GraphicsDevice.Viewport.Width;
            windowHeight = GraphicsDevice.Viewport.Height;

            //initializes players and background
            player1 = new Players(15, MovingGameItem.Direction.left, 5, (new Rectangle(980, GraphicsDevice.Viewport.Height - platformHeight - 100, playerWidth, playerHeight)), Content.Load<Texture2D>("Professor left"), Color.White, Content.Load<SoundEffect>("JumpSound"));
            player2 = new Players(15, MovingGameItem.Direction.right, 5, (new Rectangle(830, GraphicsDevice.Viewport.Height - platformHeight - 100, playerWidth, playerHeight)), Content.Load<Texture2D>("Tokyo right"), Color.White, Content.Load<SoundEffect>("JumpSound"));
            background = new GameItem(new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Content.Load<Texture2D>("background"), Color.White, null);
            vaultClosed = new GameItem((new Rectangle(windowWidth - 180, windowHeight - 220, 180, 180)), Content.Load<Texture2D>("vaultClosed"), Color.White, null);

            //initializes platforms
            platforms[0] = new GameItem(new Rectangle(platformX(0), platformY(144), 575, platformHeight), Content.Load<Texture2D>("platform_image"), Color.White, null);
            platforms[1] = new GameItem(new Rectangle(platformX(777), platformY(144), 381, platformHeight), Content.Load<Texture2D>("platform_image"), Color.White, null);
            platforms[2] = new GameItem(new Rectangle(platformX(1328), platformY(144), windowWidth - 1328, platformHeight), Content.Load<Texture2D>("platform_image"), Color.White, null);
            platforms[3] = new GameItem(new Rectangle(platformX(1458), platformY(309), 334, platformHeight), Content.Load<Texture2D>("platform_image"), Color.White, null);
            platforms[4] = new GameItem(new Rectangle(platformX(578), platformY(309), 382, platformHeight), Content.Load<Texture2D>("platform_image"), Color.White, null);
            platforms[5] = new GameItem(new Rectangle(platformX(0), platformY(452), 523, platformHeight), Content.Load<Texture2D>("platform_image"), Color.White, null);
            platforms[6] = new GameItem(new Rectangle(platformX(1155), platformY(422), windowWidth - 1155, platformHeight), Content.Load<Texture2D>("platform_image"), Color.White, null);
            platforms[7] = new GameItem(new Rectangle(platformX(0), platformY(579), 1709, platformHeight), Content.Load<Texture2D>("platform_image"), Color.White, null);
            platforms[8] = new GameItem(new Rectangle(platformX(996), platformY(712), windowWidth - 996, platformHeight), Content.Load<Texture2D>("platform_image"), Color.White, null);
            platforms[9] = new GameItem(new Rectangle(platformX(143), platformY(847), 1389, platformHeight), Content.Load<Texture2D>("platform_image"), Color.White, null);
            platforms[10] = new GameItem(new Rectangle(platformX(0), windowHeight - (platformHeight + 20), windowWidth, platformHeight), Content.Load<Texture2D>("platform_image"), Color.White, null);

            //initializes keys
            keyList = new List<GameItem>();

            //initializes the keys
            keyList.Add(new GameItem(new Rectangle(objectX(50), objectY(75), keySize, keySize), Content.Load<Texture2D>("key"), Color.LightGray, Content.Load<SoundEffect>("keySound")));
            keyList.Add(new GameItem(new Rectangle(objectX(1780), objectY(75), keySize, keySize), Content.Load<Texture2D>("key"), Color.LightGray, Content.Load<SoundEffect>("keySound")));
            keyList.Add(new GameItem(new Rectangle(objectX(840), objectY(245), keySize, keySize), Content.Load<Texture2D>("key"), Color.LightGray, Content.Load<SoundEffect>("keySound")));
            keyList.Add(new GameItem(new Rectangle(objectX(1540), objectY(75), keySize, keySize), Content.Load<Texture2D>("key"), Color.LightGray, Content.Load<SoundEffect>("keySound")));
            keyList.Add(new GameItem(new Rectangle(objectX(180), objectY(388), keySize, keySize), Content.Load<Texture2D>("key"), Color.LightGray, Content.Load<SoundEffect>("keySound")));
            keyList.Add(new GameItem(new Rectangle(objectX(1740), objectY(358), keySize, keySize), Content.Load<Texture2D>("key"), Color.LightGray, Content.Load<SoundEffect>("keySound")));
            keyList.Add(new GameItem(new Rectangle(objectX(340), objectY(515), keySize, keySize), Content.Load<Texture2D>("key"), Color.LightGray, Content.Load<SoundEffect>("keySound")));
            keyList.Add(new GameItem(new Rectangle(objectX(1490), objectY(647), keySize, keySize), Content.Load<Texture2D>("key"), Color.LightGray, Content.Load<SoundEffect>("keySound")));
            keyList.Add(new GameItem(new Rectangle(objectX(243), objectY(782), keySize, keySize), Content.Load<Texture2D>("key"), Color.LightGray, Content.Load<SoundEffect>("keySound")));
            keyList.Add(new GameItem(new Rectangle(objectX(1259), objectY(782), keySize, keySize), Content.Load<Texture2D>("key"), Color.LightGray, Content.Load<SoundEffect>("keySound")));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //load timer text
            font = Content.Load<SpriteFont>("SpriteFont1");

            //loads the background son
            backgroundSong = Content.Load<Song>("bellaCiao");

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

            //gets states
            kb = Keyboard.GetState();
            pad = GamePad.GetState(PlayerIndex.One);
            pad2 = GamePad.GetState(PlayerIndex.Two);

            // Allows the game to exit
            if (pad.Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            // Allows the game to exit
            if (pad2.Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            //if the mediaplayer is paused
            if (MediaPlayer.State == MediaState.Paused)
            {
                //resume the mediaplayer
                MediaPlayer.Resume();
            }
            else if (MediaPlayer.State != MediaState.Playing)
            {
                //play the mediaplayer
                MediaPlayer.Play(backgroundSong);
            }

            //---------------------------------------------MOVING-------------------------------------------------


            //---------------PLAYER 1------------

            //if the thubstick is pointing left
            if ((pad.ThumbSticks.Left.X < 0 || kb.IsKeyDown(Keys.Left)) && player1.getRect().X >= 0)
            {
                //changes player 1 image to left
                player1.setSprite(Content.Load<Texture2D>("Professor left"));

                //moves the player1 using the move method
                player1.Move(MovingGameItem.Direction.left);
                
            }
            //if the thubstick is pointing right
            if ((pad.ThumbSticks.Left.X > 0 || kb.IsKeyDown(Keys.Right)) && player1.getRect().X <= GraphicsDevice.Viewport.Width - 60)
            {
                //changes player 1 image to left
                player1.setSprite(Content.Load<Texture2D>("Professor right"));

                //moves the player1 using the move method
                player1.Move(MovingGameItem.Direction.right);
            }

            //---------------PLAYER 2------------

            //if a is pressed
            if ((pad2.ThumbSticks.Left.X < 0 || kb.IsKeyDown(Keys.A)) && player2.getRect().X >= 0)
            {
                //changes player 2 image to left
                player2.setSprite(Content.Load<Texture2D>("Tokyo left"));

                //moves the player2 using the move method
                player2.Move(MovingGameItem.Direction.left);
            }
            //if d is pressed
            if ((pad2.ThumbSticks.Left.X > 0 || kb.IsKeyDown(Keys.D)) && player2.getRect().X <= GraphicsDevice.Viewport.Width - 60)
            {
                //changes player 2 image to left
                player2.setSprite(Content.Load<Texture2D>("Tokyo right"));

                //moves the player2 using the move method
                player2.Move(MovingGameItem.Direction.right);
            }

            //---------------------------------------------JUMPING AND PLATFORM TESTS-------------------------------------------------

            //---------------PLAYER 1------------
            //If the up button or the A button on the controller is pressed,
            if ((kb.IsKeyDown(Keys.Up) && oldKb.IsKeyUp(Keys.Up))|| (pad.Buttons.A == ButtonState.Pressed && oldPad.Buttons.A == ButtonState.Released))
            {
                //make player1 jump
                player1.Jump();
            }

            //checks through all platforms
            for (int i = 0; i < platforms.Length; i++)
            {
                //if player 1 hits the platform
                if (player1.hitTest(platforms[i].getRect()))
                {
                    ///---------top and bottom-----------

                    //if the bottom of the player is touching the top of the platform
                    if (player1.getRect().Bottom >= platforms[i].getRect().Top && player1.getRect().Bottom <= platforms[i].getRect().Top + player1.getGravity() + 1)
                    {
                        //sets player 1 falling to false
                        player1.setFalling(false);

                        //set gravity to 0
                        player1.setGravity(0);

                        //sets the position of rectangle (teleports it)
                        player1.setRect(new Rectangle(player1.getRect().X, platforms[i].getRect().Top - playerHeight + 1, playerWidth, playerHeight));

                        //break to test other platform hit tests
                        break;
                    }
                    //else if the top of the player1 is touching the bottom of any platform
                    else if (player1.getRect().Top <= platforms[i].getRect().Bottom)
                    {
                        //sets player 1 jumping to false
                        player1.setJumping(false);

                        //sets player1 falling to true
                        player1.setFalling(true);

                        //break to test other platform hit tests
                        break;
                    }
                }
                //otherwise if the player1 is not jumping
                else if (!player1.getJumping())
                {
                    //set player1's falling to true
                    player1.setFalling(true);
                    
                }

            }

            //checks through the platforms
            for (int i = 0; i < platforms.Length; i++)
            {
                //if player 1 hits the platform
                if (player1.hitTest(platforms[i].getRect()))
                {
                    ///---------left and right-----------

                    //else if the right of the player2 is touching the left of any platform and the player is not on top of the platform
                    if (player1.getRect().Right >= platforms[i].getRect().Left && player1.getRect().Right <= platforms[i].getRect().Left + player1.getSpeed() && player1.getRect().Bottom > platforms[i].getRect().Top + 1)
                    {
                        //sets player speed to 0
                        player1.setSpeed(0);

                        //break to test other platform hit tests
                        break;

                    }

                    //else if the left of the player1 is touching the right of any platform
                    else if (player1.getRect().Left <= platforms[i].getRect().Right && player1.getRect().Left >= platforms[i].getRect().Right - player1.getSpeed() && player1.getRect().Bottom > platforms[i].getRect().Top + 1)
                    {
                        //sets player speed to 0
                        player1.setSpeed(0);

                        //break to test other platform hit tests
                        break;
                    }
                }
                //otherwise...
                else
                {
                    //sets player speed to 5
                    player1.setSpeed(5);

                }

            }

            //run player 1 update
            player1.Update();

            //-------------------------PLAYER 2---------------------------

            //If the W button or the A button on the controller is pressed,
            if (kb.IsKeyDown(Keys.W) || pad2.Buttons.A == ButtonState.Pressed)
            {
                //make player2 jump
                player2.Jump();

            }

            //checks through all platforms
            for (int i = 0; i < platforms.Length; i++)
            {
                //if player 1 hits the platform
                if (player2.hitTest(platforms[i].getRect()))
                {
                    ///---------top and bottom-----------

                    //if the bottom of the player is touching the top of the platform
                    if (player2.getRect().Bottom >= platforms[i].getRect().Top && player2.getRect().Bottom <= platforms[i].getRect().Top + player2.getGravity() + 1)
                    {
                        //sets player2 falling to false
                        player2.setFalling(false);

                        //set gravity to 0
                        player2.setGravity(0);

                        //sets the position of rectangle (teleports it)
                        player2.setRect(new Rectangle(player2.getRect().X, platforms[i].getRect().Top - playerHeight + 1, playerWidth, playerHeight));

                        //break to test other platform hit tests
                        break;
                    }
                    //else if the top of the player2 is touching the bottom of any platform
                    else if (player2.getRect().Top <= platforms[i].getRect().Bottom)
                    {
                        //sets player 1 jumping to false
                        player2.setJumping(false);

                        //sets player2 falling to true
                        player2.setFalling(true);

                        //break to test other platform hit tests
                        break;
                    }

                }
                //otherwise if the player2 is not jumping
                else if (!player2.getJumping())
                {
                    //set player2's falling to true
                    player2.setFalling(true);

                }

            }

            //checks through the platforms
            for (int i = 0; i < platforms.Length; i++)
            {
                //if player 1 hits the platform
                if (player2.hitTest(platforms[i].getRect()))
                {
                    ///---------left and right-----------

                    //else if the right of the player2 is touching the left of any platform and the player is not on top of the platform
                    if (player2.getRect().Right >= platforms[i].getRect().Left && player2.getRect().Right <= platforms[i].getRect().Left + player2.getSpeed() && player2.getRect().Bottom > platforms[i].getRect().Top + 1)
                    {
                        //sets player speed to 0
                        player2.setSpeed(0);

                        //break to test other platform hit tests
                        break;

                        
                    }

                    //else if the left of the player2 is touching the right of any platform
                    else if (player2.getRect().Left <= platforms[i].getRect().Right && player2.getRect().Left >= platforms[i].getRect().Right - player2.getSpeed() && player2.getRect().Bottom > platforms[i].getRect().Top + 1)
                    {
                        //sets player speed to 0
                        player2.setSpeed(0);

                        //break to test other platform hit tests
                        break;
                    }
                }
                //otherwise...
                else 
                {
                    //sets player speed to 5
                    player2.setSpeed(5);
                 
                }

            }

            //run player 2 update
            player2.Update();

            //---------------------------------------------KEY HIT TESTS-------------------------------------------------

            //-------------------------PLAYER 1---------------------------

            //checks through all the keys in the list
            for (int i = 0; i < keyList.Count; i++)
            {
                //if player 1 intersects with the key
                if (player1.hitTest(keyList[i].getRect()))
                {
                    //plays the key sound
                    keyList[i].PlaySound(1, 0, 0);

                    //remove the key it interacts with
                    keyList.RemoveAt(i);

                    //increments keys collected by 1
                    keysCollected++;
                    
                }
            }

            //-------------------------PLAYER 2---------------------------

            //checks through all the keys in the list
            for (int i = 0; i < keyList.Count; i++)
            {
                //if player 2 intersects with the key
                if (player2.hitTest(keyList[i].getRect()))
                {
                    //plays the key sound
                    keyList[i].PlaySound(1, 0, 0);

                    //remove the key it interacts with
                    keyList.RemoveAt(i);

                    //increments keys collected by 1
                    keysCollected++;
                }
            }

            //---------------------------------------------TIMER-------------------------------------------------
            
            //loads the text
            timerText = new Text(("Timer: " + gameTimer.ToString()), new Rectangle(900, 0, 150, 40), Content.Load<Texture2D>("onebyonepixel"), Color.Maroon, null);
            
            //increments timecount by 1
            timeCount--;

            //if the time count divide by 60 doesn't give a remainder
            if (timeCount % 60 == 0)
            {
                //reduce game timer by 1
                gameTimer--;
            }

            //if game timer is 0 or the player has won the game, 
            if (gameTimer == 0 || winGame)
            {
                //stop the timer
                timeCount = 0;
            }

            //---------------------------------------------OLD STATES-------------------------------------------------

            //gets the old states of the pad and keyboard
            oldPad = GamePad.GetState(PlayerIndex.One);
            oldPad2 = GamePad.GetState(PlayerIndex.Two);
            oldKb = Keyboard.GetState();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //a little blanched almond
            GraphicsDevice.Clear(Color.BlanchedAlmond);

            //begins the spritebatch
            spriteBatch.Begin();

            //draws the background and vault
            background.drawSprite(spriteBatch);
            vaultClosed.drawSprite(spriteBatch);

            //if 10 keys are collected and the game hasnt ended
            if (keysCollected == 10 && gameTimer != 0)
            {
                //draw the vault open image
                vaultOpen = new GameItem((new Rectangle(windowWidth - 180, windowHeight - 230, 180, 180)), Content.Load<Texture2D>("vaultOpen"), Color.White, null);
                vaultOpen.drawSprite(spriteBatch);

            }

            //draws the players
            player1.drawSprite(spriteBatch);
            player2.drawSprite(spriteBatch);

            //draws the platform in the array
            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i].drawSprite(spriteBatch);
            }

            //draws the keys in the list
            for (int i = 0; i < keyList.Count; i++)
            {
                keyList[i].drawSprite(spriteBatch);
            }

            //if 10 keys are collected and the game hasnt ended
            if (keysCollected == 10 && gameTimer != 0)
            {
                //if the player 1 and player 2 are touching the vault
                if (player1.getRect().Right >= vaultOpen.getRect().Left && player2.getRect().Right >= vaultOpen.getRect().Left)
                {
                    //if the mediaplayer is playing
                    if (MediaPlayer.State == MediaState.Playing)
                    {
                        //pause the mediaplayer
                        MediaPlayer.Stop();
                    }

                    //make winGame true 
                    winGame = true;

                    //display win screen
                    win = new GameItem(new Rectangle(800, 300, 340, 410), Content.Load<Texture2D>("youwin"), Color.White, Content.Load<SoundEffect>("WinSound"));

                    //if lose sound is not playing
                    if (!winSoundPlaying)
                    {
                        //plays lose sound
                        win.PlaySound(1, 0, 0);

                        //set losesoundplaying to true
                        winSoundPlaying = true;
                    }

                    //draws the win screen
                    win.drawSprite(spriteBatch);
                }
            }
            //if 10 keys are not collected and game has ended
            else if ((keysCollected != 10 && gameTimer == 0) || gameTimer == 0)
            {
                //if the mediaplayer is playing
                if (MediaPlayer.State == MediaState.Playing)
                {
                    //pause the mediaplayer
                    MediaPlayer.Stop();
                }

                //display lose screen
                lose = new GameItem(new Rectangle(800, 300, 340, 410), Content.Load<Texture2D>("youlose"), Color.White, Content.Load<SoundEffect>("LoseSound"));

                //if lose sound is not playing
                if (!loseSoundPlaying)
                {
                    //plays lose sound
                    lose.PlaySound(1, 0, 0);

                    //set losesoundplaying to true
                    loseSoundPlaying = true;
                }
                
                //draws the lose screen
                lose.drawSprite(spriteBatch);
            }
            

            //draws the timer text
            timerText.drawSprite(spriteBatch);
            timerText.setColor(Color.White);

            //draws the timer
            timerText.DrawText(spriteBatch, font, new Vector2(timerText.getRect().X + 19, timerText.getRect().Y + 9));

            //ends spritebatch
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //creates method platformX to change the original width based on the screen width
        public int platformX(int xPos)
        {
            return windowWidth * xPos / 1920;
        }

        //creates method platformY to change the original width based on the screen height
        public int platformY(int yPos)
        {
            return windowHeight * yPos / 1050;
        }

        //creates method objectX to change the original width based on the screen width
        public int objectX(int xPos)
        {
            return windowWidth * xPos / 1920;
        }

        //creates method objectY to change the original width based on the screen height
        public int objectY(int yPos)
        {
            return windowHeight * yPos / 1050;
        }
    }
}
