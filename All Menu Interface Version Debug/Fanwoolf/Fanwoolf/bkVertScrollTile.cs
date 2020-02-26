using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Fanwoolf
{
    class bkVertScrollTile
    {
        public Texture2D bgTex;        // background texture
        public Vector2 Position;          // The position of the Cloud decoration object relative to the top left corner of thescreen 
        float bgSpeed;             // The speed at which the Cloud moves 

        public int Width { get { return bgTex.Width; } }    // Get the width of the background tile decoration object 
        public int Height { get { return bgTex.Height; } }  // Get the height of the background tile decoration object 

        public void Initialize(Texture2D tex, Vector2 position, float spd)
        {
            bgTex = tex;         // Load the background tile decoration object texture 
            Position = position;    // Set the position of the background tile 
            bgSpeed = spd;  // Set how fast the background tile moves 
        }

        public void Update(GameTime gameTime)
        {
            Position.Y += bgSpeed;        // The background tile always moves down so decrement it's y position 

            // mechanism to reset tile
            if (Position.Y > 480) { Position.Y = -1438; } 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                bgTex,
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
