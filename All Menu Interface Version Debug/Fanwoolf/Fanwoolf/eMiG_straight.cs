﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fanwoolf
{
    class eMiG_straight
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


        float enemyMoveSpeed; // The speed at which the enemy moves

        public void Initialize(Texture2D texture, Vector2 position)
        {
            ETexture = texture;     // Load enemy NON-animation texture
            Position = position;        // Set the position of the enemy
            Active = true;              // We initialize the enemy to be active so it will be update in the game
            Health = 8;                 // Set the health of the enemy
            Damage = 10;                // Set the amount of damage the enemy can do
            enemyMoveSpeed = 3f;        // Set how fast the enemy moves
            Value = 225;                // Set the score value of the enemy

        }

        public void Update(GameTime gameTime)
        {
            Position.Y += enemyMoveSpeed; // The enemy always moves to the left so decrement it's xposition
            if (Position.Y > 480 || Health <= 0) // If the enemy is past the screen or its health reaches 0 then deactivate it
            {
                Active = false; // By setting the Active flag to false, the game will remove this objet from the active game list
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