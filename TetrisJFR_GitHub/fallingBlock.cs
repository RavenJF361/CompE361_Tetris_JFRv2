using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TetrisJFR_GitHub
{
    public class fallingBlock : DrawableGameComponent
    {

        Game1 game;
        SpriteBatch spriteBatch;
        Texture2D droppingBlock;
        static float globalVariable = 380;


        //Location of the block
        // Spawns on top middle -ish
        public float x = 80;
        public float y = 0;

        int blockType = 0;
        int blockColor = 0;

        // Default Constructor
        public fallingBlock(Game1 game) : base(game)
        {
            this.game = game;

        }

        // This constructor used to create different shapes
        public fallingBlock(Game1 game, int _blockType) : base(game)
        {
            this.game = game;
            this.blockType = _blockType;
        }

        // This constructor used to create 1x1 of the desired color from a specific block
        // For example. A blockColor of 3 will result in a yellow 1x1 block

        // This constructor is used in tandem with the delete row.
        // 11/26/18 NOTE TO SELF, REDEFINE _X AND _Y TO BE ROW AND COLUMN
        // TO PREVENT ANY CONFUSION
        public fallingBlock(Game1 game, int _blockColor, int COLUMN, int ROW) : base(game)
        {
            this.game = game;
            this.blockColor = _blockColor;
            this.x = COLUMN;
            this.y = ROW;

            this.blockType = 9000; // blockType not used here, so give it a random value.
        }

        public override void Initialize()
        {
            /* // REMOVING THIS FIXES THE WEIRD PLACEMENT OF BLOCK L ON BOARD
            if (this.blockType == 3)
            {
                this.y = 40; // Set the bottom of the block to be this y coordinate
                // ERROR/ BUG HERE 
            }
            */
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load the texture
            droppingBlock = game.Content.Load<Texture2D>("greyBlock");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();

            // Creates a 1x1 block
            if (blockType == 0)
            {
                spriteBatch.Draw(droppingBlock, new Vector2(x, y), Color.MediumPurple);
            }

            // Creates a shape with four blocks, like a "L"
            else if (blockType == 3)
            {


                spriteBatch.Draw(droppingBlock, new Vector2(x, y), Color.Yellow);
                spriteBatch.Draw(droppingBlock, new Vector2(x, y + 20), Color.Yellow);
                spriteBatch.Draw(droppingBlock, new Vector2(x, y + 40), Color.Yellow);
                spriteBatch.Draw(droppingBlock, new Vector2(x + 20, y + 40), Color.Yellow);

            }

            // Creates the block from the grid itself. (greyBlock) This block is used in tandem
            // with the clearing of the rows
            else if (blockColor == -21)
            {
                spriteBatch.Draw(droppingBlock, new Vector2(x, y), Color.White);
            }

            // Creates a MediumPurple 1x1 block
            else if (blockColor == 0)
            {
                spriteBatch.Draw(droppingBlock, new Vector2(x, y), Color.MediumPurple);
            }

            // Creates a Yellow 1x1 block
            else if (blockColor == 3)
            {
                spriteBatch.Draw(droppingBlock, new Vector2(x, y), Color.Yellow);
            }

            // Testing if you can draw outside the draw function. DOESNT WORK
            //spriteBatch.Begin();

            // DELETE ME THIS IS CLUTTER
            // spriteBatch.Draw(droppingBlock, new Vector2(0, 360), Color.Red);

            //spriteBatch.End();


            spriteBatch.End();




            base.Draw(gameTime);


        }

        public void changeY()
        {
            this.y = globalVariable;
        }


    }
}
