using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fanwoolf
{
    class enemy
    {
        public Animation EnemyAnimation;  // Animation representing the enemy
        public Texture2D EnemyTexture;    // Texture representing the enemy
        public Vector2 Position;          // The position of the enemy ship relative to the top left corner of thescreen
        public bool Active;               // The state of the Enemy Ship
        public int Health;                // The hit points of the enemy, if this goes to zero the enemy dies
        public int Damage;                // The amount of damage the enemy inflicts on the player ship
        public int Value;                 // The amount of score the enemy will give to the player

        public int Width // Get the width of the enemy ship
        {
            get { return EnemyAnimation.FrameWidth; }
        }

        public int Height // Get the height of the enemy ship
        {
            get { return EnemyAnimation.FrameHeight; }
        }

        public int tWidth // Get the width of the enemy ship
        {
            get { return EnemyTexture.Width; }
        }

        public int tHeight // Get the height of the enemy ship
        {
            get { return EnemyTexture.Height; }
        }

        float enemyMoveSpeed; // The speed at which the enemy moves

        public void Initialize(Animation animation, Texture2D texture, Vector2 position)
        {
            EnemyAnimation = animation; // Load the enemy ship texture
            EnemyTexture = texture;     // Load enemy NON-animation texture
            Position = position;        // Set the position of the enemy
            Active = true;              // We initialize the enemy to be active so it will be update in the game
            Health = 10;                // Set the health of the enemy
            Damage = 10;                // Set the amount of damage the enemy can do
            enemyMoveSpeed = 2f;        // Set how fast the enemy moves
            Value = 100;                // Set the score value of the enemy

        }

        public void Update(GameTime gameTime)
        {
            Position.X -= enemyMoveSpeed; // The enemy always moves to the left so decrement it's xposition
            EnemyAnimation.Position = Position; // Update the position of the Animation
            EnemyAnimation.Update(gameTime); // Update Animation
            if (Position.X < -Width || Health <= 0) // If the enemy is past the screen or its health reaches 0 then deactivate it
            {
                Active = false; // By setting the Active flag to false, the game will remove this objet from the active game list
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                EnemyTexture, 
                Position, 
                null, 
                Color.White, 
                0f, 
                Vector2.Zero, 
                1f, 
                SpriteEffects.None, 
                0f
            );

            // Draw the animation
            //EnemyAnimation.Draw(spriteBatch);
        }

    }
}
