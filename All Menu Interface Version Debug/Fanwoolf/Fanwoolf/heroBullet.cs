//Using declarations
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fanwoolf
{
    class heroBullet
    {
        public Texture2D pTex; // Image representing the Projectile
        public Vector2 Position; // Position of the Projectile relative to the upper left side of the screen
        public bool Active; // State of the Projectile
        public int Damage; // The amount of damage the projectile can inflict to an enemy

        public int Width // Get the width of the projectile 
        {
            get { return pTex.Width; }
        }

        public int Height// Get the height of the projectile 
        {
            get { return pTex.Height; }
        }

        float projectileMoveSpeed; // Determines how fast the projectile moves

        public void Initialize(Texture2D texture, Vector2 position)
        {
            pTex = texture;
            Position = position;
            Active = true;
            Damage = 2;
            projectileMoveSpeed = 10f;
        }

        public void Update()
        {
            Position.Y -= projectileMoveSpeed; // Projectiles always move to the right

            if (Position.Y + pTex.Height < 0) // Deactivate the bullet if it goes out of screen
            {
                Active = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                pTex,
                Position,
                null, Color.White,
                0f,
                new Vector2(Width / 2, Height / 2),
                1f,
                SpriteEffects.None,
                0f
            );
        }
    }
}
