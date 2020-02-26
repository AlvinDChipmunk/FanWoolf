using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fanwoolf
{
    class Player
    {

        // Animation representing the player
        public Texture2D PlayerTexture;

        // Animation representing the player
        public Animation PlayerAnimation;

        // Position of the Player relative to the upper left side of the screen
        public Vector2 Position;

        // State of the player
        public bool Active;

        // Amount of hit points that player has
        public int Health;

        // Get the texture width of the player ship
        public int tWidth
        {
            get { return PlayerTexture.Width; }
        }

        // Get the texture height of the player ship
        public int tHeight
        {
            get { return PlayerTexture.Height; }
        }

        // Get the width of the player ship
        public int Width
        {
            get { return PlayerAnimation.FrameWidth; }
        }

        // Get the height of the player ship
        public int Height
        {
            get { return PlayerAnimation.FrameHeight; }
        }

        // Initialize the player
        public void Initialize(
            Animation animation,
            Texture2D texture, 
            Vector2 position
        ) 
        {
            PlayerAnimation = animation;

            PlayerTexture = texture;

            // Set the starting position of the player around the middle of the screen and to the back
            Position = position;

            // Set the player to be active
            Active = true;

            // Set the player health
            Health = 100;
        }

        // Update the player animation
        public void Update(GameTime gameTime)
        {
            PlayerAnimation.Position = Position - new Vector2(25, 10);
            PlayerAnimation.Update(gameTime);
        }

        // Draw the player
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlayerTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            PlayerAnimation.Draw(spriteBatch);
        }
        
    }
}

