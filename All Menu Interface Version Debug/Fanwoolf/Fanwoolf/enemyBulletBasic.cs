using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fanwoolf
{
    class enemyBulletBasic
    {
        public Texture2D Texture; // Image representing the Projectile
        public Vector2 Position; // Position of the Projectile relative to the upper left side of the screen
        public bool Active; // State of the Projectile
        public int Damage; // The amount of damage the projectile can inflict to an enemy
        Viewport viewport; // Represents the viewable boundary of the game
        public int Width { get { return Texture.Width; } } // Get the width of the projectile ship
        public int Height { get { return Texture.Height; } } // Get the height of the projectile ship
        float projectileMoveSpeed; // Determines how fast the projectile moves

        public void Initialize(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            Position.X--;
            Active = true;

            Damage = 2;

            projectileMoveSpeed = 5f;
        }

        public void Update()
        {
            Position.Y += projectileMoveSpeed; // these enemy projectiles will move down
            if (Position.Y >= 480) Active = false; // Deactivate the 20mm shell if it goes off screen
        }

        public void Draw(SpriteBatch spriteBatch)
        { spriteBatch.Draw(Texture, Position, null, Color.White, 0f, new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0f); } 

    }
}
