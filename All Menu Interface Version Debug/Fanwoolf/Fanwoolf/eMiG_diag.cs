using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fanwoolf
{
    class eMiG_diag
    {
        public Texture2D ETexture;    // Texture representing the enemy
        public Vector2 Position;          // The position of the enemy ship relative to the top left corner of thescreen
        public bool Active;               // The state of the Enemy Ship
        public int Health;                // The hit points of the enemy, if this goes to zero the enemy dies
        public int Damage;                // The amount of damage the enemy inflicts on the player ship
        public int Value;                 // The amount of score the enemy will give to the player

        public int Width // Get the width of the enemy ship
        {
            get { return ETexture.Width; }
        }

        public int Height // Get the height of the enemy ship
        {
            get { return ETexture.Height;  }
        }

        // new movement variables that show off the random diagonal movement 
        // with these two variables, we will first determine 2D vector direction 
        // values of these should be determined by a random range 
        int xRandomVec;
        int yRandomVec;
        // with this one, we will get a magnitude from the resulting random 2D vector 
        double hypDir;

        // with the above three variables, we can now calculate actual vertical and 
        // horizontal speeds, with the help of the two below variables 
        double xMiG_drift;
        double yMiG_drift; 

        float enemyMoveSpeed; // The speed at which the enemy moves

        public void Initialize(Texture2D texture, Vector2 position)
        {
            ETexture = texture;     // Load enemy NON-animation texture
            Position = position;        // Set the position of the enemy
            Active = true;              // We initialize the enemy to be active so it will be update in the game
            Health = 8;                 // Set the health of the enemy
            Damage = 10;                // Set the amount of damage the enemy can do
            enemyMoveSpeed = 3f;        // Set how fast the enemy moves
            Value = 150;                // Set the score value of the enemy

            // set up first the random vector 
            System.Random RandNum = new System.Random();

            xRandomVec = RandNum.Next(-5, 5);
            yRandomVec = RandNum.Next(4, 8);

            // now get a magnitude
            hypDir = Math.Sqrt((double)(xRandomVec * xRandomVec) + (double)(yRandomVec * yRandomVec));

            // now get move values from the now existing vector and magnitude 
            xMiG_drift = (enemyMoveSpeed * xRandomVec) / hypDir;
            yMiG_drift = (enemyMoveSpeed * yRandomVec) / hypDir; 

        }

        public void Update(GameTime gameTime)
        {
            // old code: Position.Y += enemyMoveSpeed; // The enemy always moves to the left so decrement its x position

            // new code: MiG now goes diagonally 
            Position.X += (float)xMiG_drift;
            Position.Y += (float)yMiG_drift; 

            if (Position.Y > 480 || Health <= 0) // If the enemy is past the screen or its health reaches 0 then deactivate it
            {
                Active = false; // By setting the Active flag to false, the game will remove this objet from the active game list
            }

            //both horizontal edges check 
            if (Position.X < -Width || Position.X > (800 + Width))
            {
                // By setting the Active flag to false, the game will remove this objet from the 
                // active game list 
                Active = false;
            } 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                ETexture,
                Position,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                0f
            );
        }
    }
}
