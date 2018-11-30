using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using System;

namespace TetrisJFR_GitHub
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>

    /*
        -This class is controls the creation of the board, and the movement of
        the falling Block.

        -It also contains the code necessary to handle the keyboard inputs

         
       - When deleting blocks or clearing rows, have an array that has all the characteritics
        -of the block, so you can bring it down later using the draw function
         
    */

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Texture for the playing grid
        Texture2D blockForGrid;

        // Object for the falling block that spawns
        fallingBlock currentBlock;

        // Object used to clear rows, by creating blocks that come from the playing grid
        // By doing so, it gives the illusion that the row has been cleared
        fallingBlock deleteBlock;

        // Object used to bring a block down, if the row has been cleared
        fallingBlock bringDownBlock;

        // Texture for the Tetris Logo
        Texture2D tetrisLogo;

        // Creating text for the score
        SpriteFont scoreText;

        // Used when handling Keyboard Inputs
        private KeyboardState oldState;

        // Initializing our timer
        // Source used to create timer:
        // https://stackoverflow.com/questions/13394892/how-to-create-a-timer-counter-in-c-sharp-xna

        int counter = 0;
        float countDuration = 1; // one second
        float currentTime = 0;

        int score = 100;

        /*
            When deleting blocks or clearing rows, have an array that has all the characteritics
            of the block, so you can bring it down later using the draw function
         
        */

        int xBoard;
        int yBoard;

        // Multiple variable instances used to track the coordinates of different objects
        // on the board, since they all have different sizes.  
        int xBoard2;
        int xBoard3;
        int xBoard4;

        int yBoard2;
        int yBoard3;
        int yBoard4;


        int blockType = 3; // initialize the first object to spawn for testing purposes






        // 20 rows, 10 columns
        int[,] digitalBoard = new int[20, 10]
        {                  //x <-- start here
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        };

        int[,] blockColorArray = new int[20, 10]
        {
            // -21 represends a grey "grid" block
                                     //x <-- start here
                {-21, -21, -21, -21, -21, -21, -21, -21, -21, -21 }, //1
                {-21, -21, -21, -21, -21, -21, -21, -21, -21, -21 }, //2
                {-21, -21, -21, -21, -21, -21, -21, -21, -21, -21 }, //3
                {-21, -21, -21, -21, -21, -21, -21, -21, -21, -21 }, //4
                {-21, -21, -21, -21, -21, -21, -21, -21, -21, -21 }, //5
                {-21, -21, -21, -21, -21, -21, -21, -21, -21, -21 }, //6
                {-21, -21, -21, -21, -21, -21, -21, -21, -21, -21 }, //7
                {-21, -21, -21, -21, -21, -21, -21, -21, -21, -21 }, //8
                {-21, -21, -21, -21, -21, -21, -21, -21, -21, -21 }, //9
                {-21, -21, -21, -21, -21, -21, -21, -21, -21, -21 },//10
                {-21, -21, -21, -21, -21, -21, -21, -21, -21, -21 }, //11
                {-21, -21, -21, -21, -21, -21, -21, -21, -21, -21 }, //12
                {-21, -21, -21, -21, -21, -21, -21, -21, -21, -21 },//13
                {-21, -21, -21, -21, -21, -21, -21, -21, -21, -21 }, //14
                {-21, -21, -21, -21, -21, -21, -21, -21, -21, -21 }, //15
                {-21, -21, -21, -21, -21, -21, -21, -21, -21, -21 }, //16
                {-21, -21, -21, -21, -21, -21, -21, -21, -21, -21 }, //17
                {-21, -21, -21, -21, -21, -21, -21, -21, -21, -21 }, //18
                {-21, -21, -21, -21, -21, -21, -21, -21, -21, -21 }, //19
                {-21, -21, -21, -21, -21, -21, -21, -21, -21, -21 },  //20th row
        };



        int[,] locationYArray = new int[20, 10]
        {                  //x <-- start here
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                {20, 20, 20, 20, 20, 20, 20, 20, 20, 20 },
                {40, 40, 40, 40, 40, 40, 40, 40, 40, 40 },
                {60, 60, 60, 60, 60, 60, 60, 60, 60, 60 },
                {80, 80, 80, 80, 80, 80, 80, 80, 80, 80 },
                {100, 100, 100, 100, 100, 100, 100, 100, 100, 100 },
                {120, 120, 120, 120, 120, 120, 120, 120, 120, 120 },
                {140, 140, 140, 140, 140, 140, 140, 140, 140, 140 },
                {160, 160, 160, 160, 160, 160, 160, 160, 160, 160 },
                {180, 180, 180, 180, 180, 180, 180, 180, 180, 180 },
                {200, 200, 200, 200, 200, 200, 200, 200, 200, 200 },
                {220, 220, 220, 220, 220, 220, 220, 220, 220, 220 },
                {240, 240, 240, 240, 240, 240, 240, 240, 240, 240 },
                {260, 260, 260, 260, 260, 260, 260, 260, 260, 260 },
                {280, 280, 280, 280, 280, 280, 280, 280, 280, 280 },
                {300, 300, 300, 300, 300, 300, 300, 300, 300, 300 },
                {320, 320, 320, 320, 320, 320, 320, 320, 320, 320 },
                {340, 340, 340, 340, 340, 340, 340, 340, 340, 340 },
                {360, 360, 360, 360, 360, 360, 360, 360, 360, 360 },
                {380, 380, 380, 380, 380, 380, 380, 380, 380, 380 }
        };

        int[,] locationXArray = new int[20, 10]
        {                  //x <-- start here
                {0, 20, 40, 60, 80, 100, 120, 140, 160, 180 },
                {0, 20, 40, 60, 80, 100, 120, 140, 160, 180 },
                {0, 20, 40, 60, 80, 100, 120, 140, 160, 180 },
                {0, 20, 40, 60, 80, 100, 120, 140, 160, 180 },
                {0, 20, 40, 60, 80, 100, 120, 140, 160, 180 },
                {0, 20, 40, 60, 80, 100, 120, 140, 160, 180 },
                {0, 20, 40, 60, 80, 100, 120, 140, 160, 180 },
                {0, 20, 40, 60, 80, 100, 120, 140, 160, 180 },
                {0, 20, 40, 60, 80, 100, 120, 140, 160, 180 },
                {0, 20, 40, 60, 80, 100, 120, 140, 160, 180 },
                {0, 20, 40, 60, 80, 100, 120, 140, 160, 180 },
                {0, 20, 40, 60, 80, 100, 120, 140, 160, 180 },
                {0, 20, 40, 60, 80, 100, 120, 140, 160, 180 },
                {0, 20, 40, 60, 80, 100, 120, 140, 160, 180 },
                {0, 20, 40, 60, 80, 100, 120, 140, 160, 180 },
                {0, 20, 40, 60, 80, 100, 120, 140, 160, 180 },
                {0, 20, 40, 60, 80, 100, 120, 140, 160, 180 },
                {0, 20, 40, 60, 80, 100, 120, 140, 160, 180 },
                {0, 20, 40, 60, 80, 100, 120, 140, 160, 180 },
                {0, 20, 40, 60, 80, 100, 120, 140, 160, 180 }
        };

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

            // Creates the size for our "game window" //
            graphics.PreferredBackBufferHeight = 500;
            graphics.PreferredBackBufferWidth = 500;
            graphics.ApplyChanges();

            // Makes the mouse visible to the user.
            this.IsMouseVisible = true;

            // Generate the blockType using a randomNumberGenerator
            blockType = randomNumberGenerator();

            // Create the block that will be falling
            currentBlock = new fallingBlock(this, blockType);
            Components.Add(currentBlock); // add it to the Game1 object.

            //deleteBlock = new fallingBlock(this, 0, 180, 380);
            //Components.Add(deleteBlock);

            // Initializing the coordinates of the first object
            // depending on its block type.
            // 11/25/18 I REMOVED THE IF AND ELSE IF STATEMENTS, TO SEE
            // IF IT FIXES THE WEIRD ANOMLAY ERROR
            //if (blockType == 0)
            //{
            yBoard = 0;
            xBoard = 4;
            //}

            // else if (blockType == 3)
            // {
            yBoard = 0;
            yBoard2 = 1;
            yBoard3 = 2;
            yBoard4 = 2;

            xBoard = 4;
            xBoard2 = 4;
            xBoard3 = 4;
            xBoard4 = 5;

            // }

            /*
                blockType == 0 below

               *     ----->   xBoard/yBoard
             


                blockType == 3 below

                *              yBoard/xBoard
                *     ---->    yBoard2/xBoard2
                * *            yBoard3/xBoard3  yBoard4/xBoard4
             
            */




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

            // Load the texture for the playing field
            blockForGrid = Content.Load<Texture2D>("greyBlock");

            // Load the texture for the Tetris Logo
            tetrisLogo = Content.Load<Texture2D>("tetrisLogoRed");

            // Load the spriteFont for the score
            scoreText = Content.Load<SpriteFont>("score");

            // Load the texture for the block that is falling.  
            // Note to self - this is done in the fallingBlock.cs file


            base.LoadContent();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // A handler that handles the keyboard inputs, that you
            // need to program.
            KeyboardHandler();

            // Code to check the boundary of the game window
            /*
            if (currentBlock.y >= 500)
            {
                currentBlock.y = 0;
            }
            */



            // Increment the timer by 1 second....
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // If one second has elapsed, bring the block down
            if (currentTime >= countDuration)
            {

                counter++; // This may be removed.
                currentTime = 0; //Restart the timer...

                // Move the 1x1 block.
                if (blockType == 0)
                {


                    currentBlock.y += 20;
                    yBoard += 1;

                    // Used to prevent any out of bounds error with the board array
                    if (yBoard == 20)
                    {
                        yBoard--;
                    }


                    if (currentBlock.y >= 380)
                    {
                        //currentBlock.y -= 20;



                        // Row -> Column notation
                        digitalBoard[yBoard, xBoard] = 1;
                        blockColorArray[yBoard, xBoard] = 0;

                        // Reset the board coordinates
                        xBoard = 4; // change this for different blocks
                        yBoard = 0;


                        // Create a new block
                        blockType = randomNumberGenerator();


                        currentBlock = new fallingBlock(this, blockType);
                        Components.Add(currentBlock); // add it to the Game1 object.

                    }

                    else if (isNextSpotFilled())
                    {
                        //currentBlock.y -= 20;

                        // Row -> Column notation
                        digitalBoard[yBoard, xBoard] = 1;
                        blockColorArray[yBoard, xBoard] = 0;

                        // Reset the board coordinates
                        xBoard = 4; // change this for different blocks
                        yBoard = 0;


                        // Create a new block
                        blockType = randomNumberGenerator();


                        currentBlock = new fallingBlock(this, blockType);
                        Components.Add(currentBlock); // add it to the Game1 object.
                    }

                } // end of the code used to move the 1x1 block



                //Move the "L" block with 4 blocks.
                else if (blockType == 3)
                {
                    currentBlock.y += 20;

                    yBoard += 1;
                    yBoard2 += 1;
                    yBoard3 += 1;
                    yBoard4 += 1;

                    // This if statement used to prevent any out-of-bounds errors
                    if (yBoard == 20)
                    {
                        yBoard--;
                    }



                    // if you reach the bottom, stop here and change the board array
                    if (currentBlock.y >= 380)
                    {
                        //currentBlock.y -= 20;



                        // Row -> Column notation
                        digitalBoard[yBoard, xBoard] = 1;
                        digitalBoard[yBoard2, xBoard2] = 1;
                        digitalBoard[yBoard3, xBoard3] = 1;
                        digitalBoard[yBoard4, xBoard4] = 1;

                        blockColorArray[yBoard, xBoard] = 3;
                        blockColorArray[yBoard2, xBoard2] = 3;
                        blockColorArray[yBoard3, xBoard3] = 3;
                        blockColorArray[yBoard4, xBoard4] = 3;

                        // Reset the board coordinates
                        yBoard = 0;
                        yBoard2 = 1;
                        yBoard3 = 2;
                        yBoard4 = 2;

                        xBoard = 4;
                        xBoard2 = 4;
                        xBoard3 = 4;
                        xBoard4 = 5;


                        // Create a new block
                        blockType = randomNumberGenerator();


                        currentBlock = new fallingBlock(this, blockType); // Set to three to test the L block
                        Components.Add(currentBlock); // add it to the Game1 object.

                    }

                    // WEIRD ANOMALY< OUT OF RANGE EXCEPTION WHEN GOING TO THE LEFT
                    // OCCURS RANDOMLY AT TIMES, when simultaneously pressing A and P
                    else if (isNextSpotFilled())
                    {
                        //currentBlock.y -= 20;

                        // Row -> Column notation
                        digitalBoard[yBoard, xBoard] = 1;
                        digitalBoard[yBoard2, xBoard2] = 1;
                        digitalBoard[yBoard3, xBoard3] = 1;
                        digitalBoard[yBoard4, xBoard4] = 1;

                        blockColorArray[yBoard, xBoard] = 3;
                        blockColorArray[yBoard2, xBoard2] = 3;
                        blockColorArray[yBoard3, xBoard3] = 3;
                        blockColorArray[yBoard4, xBoard4] = 3;

                        // Reset the board coordinates
                        yBoard = 0;
                        yBoard2 = 1;
                        yBoard3 = 2;
                        yBoard4 = 2;

                        xBoard = 4;
                        xBoard2 = 4;
                        xBoard3 = 4;
                        xBoard4 = 5;


                        // Create a new block
                        blockType = randomNumberGenerator();
                        /*
                        while (blockType != 0 && blockType != 3)
                        {
                            blockType = randomNumberGenerator.Next(0, 3);
                        }
                        */
                        currentBlock = new fallingBlock(this, blockType);
                        Components.Add(currentBlock); // add it to the Game1 object.
                    }

                }


                ////////
                /// Now doing row detection to delete a row. 
                ////////
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();


            // Using two nested for loops allows us to create our 20 x 10 block grid.
            // 20 x 10 = 20 rows and 10 columns
            // Rows
            for (int j = 0; j <= 19; j++)
            {
                // Columns
                for (int i = 0; i <= 9; i++)
                {
                    spriteBatch.Draw(blockForGrid, new Vector2((i * 20), (j * 20)), Color.White);
                }
            }

            // Draw the logo
            spriteBatch.Draw(tetrisLogo, new Vector2(250, 10), Color.White);

            // Draw the Score
            spriteBatch.DrawString(scoreText, score.ToString(), new Vector2(300, 100), Color.Black);

            ///////////////////////////////////////////////////////////////////////
            // Checking if a row has been completed, if it has, increment the score
            // and bring every block down. 
            ///////////////////////////////////////////////////////////////////////
            ///

            // SECTION 9 OF FATE TUT

            if (digitalBoard[19, 0] == 1 && digitalBoard[19, 1] == 1 &&
                digitalBoard[19, 2] == 1 && digitalBoard[19, 3] == 1 &&
                digitalBoard[19, 4] == 1 && digitalBoard[19, 5] == 1 &&
                digitalBoard[19, 6] == 1 && digitalBoard[19, 7] == 1 &&
                digitalBoard[19, 8] == 1 && digitalBoard[19, 9] == 1)
            {
                score += 100;
                digitalBoard[19, 0] = 0;
            }

            /*
                // Below works, but doesnt turn the blocks white, or bring down other blocks
                digitalBoard[19, 0] = 0;
                digitalBoard[19, 1] = 0;
                digitalBoard[19, 2] = 0;
                digitalBoard[19, 3] = 0;
                digitalBoard[19, 4] = 0;
                digitalBoard[19, 5] = 0;
                digitalBoard[19, 6] = 0;
                digitalBoard[19, 7] = 0;
                digitalBoard[19, 8] = 0;
                digitalBoard[19, 9] = 0;

                // 11/25/18 NOTE TO SELF, START WORKING HERE

                for (int i = 0; i < 10; i++)
                {
                    score += 100;
                    // bring down the block and set it to 1 on the digitalBoard
                    if (digitalBoard[18, i] == 1)
                    {
                        // Bring down the block, and set it to the grid color
                        digitalBoard[18, i] = 0;
                        spriteBatch.Draw(blockForGrid, new Vector2(locationYArray[18, i], locationXArray[18, i]), Color.White);

                        digitalBoard[19, 0] = 1;
                        if (blockTypeArray[18, i] == 0)
                        {
                            spriteBatch.Draw(blockForGrid, new Vector2(19, i), Color.MediumPurple);
                        }

                        else if (blockTypeArray[18, i] == 3)
                        {
                            spriteBatch.Draw(blockForGrid, new Vector2(19, i), Color.Yellow);
                        }

                    }
                }

                spriteBatch.Draw(blockForGrid, new Vector2(380, 380), Color.MediumPurple);

            }
            */
            spriteBatch.End();





            spriteBatch.Begin();







            if (score == 200)
            {

                // For loop used to delete the row.
                for (int i = 0; i < 10; i++)
                {
                    // Arrays follow the _array[row,column] notation
                    deleteBlock = new fallingBlock(this, -21, locationXArray[19, i], 380);
                    Components.Add(deleteBlock);
                    digitalBoard[19, i] = 0; // Reset that spot back to 0, indicating no 
                    // block has been placed
                }


                // Now drop the 19th row down and repopulate the digitalBoard
                // after dropping the row, make row 19 clear for now



                // marker that below code is good
                for (int i = 0; i < 10; i++) // handles the columns
                {
                    // Dropping row
                    bringDownBlock = new fallingBlock(this, blockColorArray[18, i],
                        locationXArray[18, i], 380);
                    Components.Add(bringDownBlock);



                    // Repopulating the digitalBoard
                    if (blockColorArray[18 + 1, i] == 0 || blockColorArray[18 + 1, i] == 3)
                    {
                        digitalBoard[18 + 1, i] = 1;
                    }


                    // makes the 19th row clear. REMEMBER: there is a 0th index
                    // so row 20 is actually row 19 and row 19 is actually row 18
                    //bringDownBlock = new fallingBlock(this, -21, locationXArray[18, i], 380);
                    //Components.Add(bringDownBlock);
                }
                // marker that good code ends 

                // NOTE TO SELF, TO GET RID OF OUTER FOR LOOP, change "j"
                // back to bring the 19th row down to the 20th row for the inner for loop

                score += 1; // REMOVE THIS. ADDED ONE
                // BECAUSE IF YOU DONT DO IT, THE CODE WILL KEEP RUNNING THIS
                // IF STATEMENT, THEREFORE MAKING THE PROGRAM SLOWER.

            }

            spriteBatch.End();


            base.Draw(gameTime);
        }

        void KeyboardHandler()
        {
            KeyboardState newState = Keyboard.GetState();
            //bool leftArrowKeyDown = state.IsKeyDown(Keys.Left);


            // Move Left
            if (oldState.IsKeyUp(Keys.A) && newState.IsKeyDown(Keys.A))
            {
                if (blockType == 0)
                {
                    if (currentBlock.x - 20 <= -1)
                    {
                        // Dont move the object left, since its out of the grid
                    }

                    else
                    {
                        currentBlock.x -= 20;
                        xBoard -= 1;
                    }
                }

                else if (blockType == 3)
                {
                    if (currentBlock.x - 20 <= -1)
                    {
                        // Dont move the object to the left, since its out of the grid
                    }
                    else
                    {
                        currentBlock.x -= 20;
                        xBoard -= 1;
                        xBoard2 -= 1;
                        xBoard3 -= 1;
                        xBoard4 -= 1;
                    }
                }

            }


            // Move Right
            else if (oldState.IsKeyUp(Keys.D) && newState.IsKeyDown(Keys.D))
            {
                if (blockType == 0)
                {
                    if (xBoard + 1 >= 10)
                    {
                        // Dont move the object to the right, since its out of the grid
                    }

                    else
                    {
                        currentBlock.x += 20;
                        xBoard += 1;
                    }
                }

                else if (blockType == 3)
                {
                    if (xBoard + 2 >= 10)
                    {
                        // Dont move the object to the right, since its out of the grid
                    }
                    else
                    {
                        currentBlock.x += 20;
                        xBoard += 1;
                        xBoard2 += 1;
                        xBoard3 += 1;
                        xBoard4 += 1;
                    }
                }
            }

            else if (oldState.IsKeyUp(Keys.P) && newState.IsKeyDown(Keys.P))
            {
                if (blockType == 0)
                {
                    // currentBlock.changeY();
                    currentBlock.y += 20;
                    yBoard += 1;
                }

                else if (blockType == 3)
                {
                    currentBlock.y += 20;
                    yBoard += 1;
                    yBoard2 += 1;
                    yBoard3 += 1;
                    yBoard4 += 1;
                }

            }

            oldState = newState;


        } // end of keyboardhandler

        bool isNextSpotFilled()
        {
            try
            {
                if (blockType == 0)
                {
                    if (currentBlock.y >= 380)
                    {
                        return false;
                    }


                    // Row -> column notation
                    else if (digitalBoard[yBoard + 1, xBoard] == 0)
                    {
                        return false;
                    }

                    else if (digitalBoard[yBoard + 1, xBoard] == 1)
                    {
                        return true;
                    }
                }

                else if (blockType == 3)
                {
                    if (currentBlock.y >= 380)
                    {
                        return false;
                    }


                    // Row -> column notation
                    else if (digitalBoard[yBoard + 1, xBoard] == 0
                        && digitalBoard[yBoard2 + 1, xBoard2] == 0
                        && digitalBoard[yBoard3 + 1, xBoard3] == 0
                        && digitalBoard[yBoard4 + 1, xBoard4] == 0)
                    {
                        return false;
                    }

                    else if (digitalBoard[yBoard + 1, xBoard] == 1
                        || digitalBoard[yBoard2 + 1, xBoard2] == 1
                        || digitalBoard[yBoard3 + 1, xBoard3] == 1
                        || digitalBoard[yBoard4 + 1, xBoard4] == 1)
                    {
                        return true;
                    }
                }
            }

            catch
            {
                // throw away the exception and continue playing. 
            }


            return true;
        }


        int randomNumberGenerator()
        {
            /*
                
            From the Microsoft Documentation:
            The following example calls the Next(Int32, Int32) method to
            generate 10 random numbers between -10 and 10. Note that the second
            argument to the method specifies the exclusive upper bound of the 
            range of random values returned by the method. In other words, the largest integer 
            that the method can return is one less than this value.

            Therefore, to randomly generate a number between 0 and 3, it must be
            called like this:
            randomNumber = RNG.Next(0, 4);

             
            */
            Random RNG = new Random();
            int randomNumber = RNG.Next(0, 3);

            while (randomNumber != 0 && randomNumber != 3)
            {
                randomNumber = RNG.Next(0, 4);
            }

            return randomNumber;
        }


    }
}
