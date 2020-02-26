using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace Fanwoolf
{
    class cloud
    {
        public Texture2D CloudTex;        // cloud texture
        public Vector2 Position;          // The position of the Cloud decoration object relative to the top left corner of thescreen 
        public bool Active;               // The state of the Cloud decoration object 
        float CloudMoveSpeed;             // The speed at which the Cloud moves 

        public int Width { get { return CloudTex.Width; } }    // Get the width of the Cloud decoration object 
        public int Height { get { return CloudTex.Height; } }  // Get the height of the Cloud decoration object 

        public void Initialize(Texture2D tex, Vector2 position)
        {
            CloudTex = tex;         // Load the Cloud decoration object texture 
            Position = position;    // Set the position of the Cloud 
            Active = true;          // We initialize the Cloud to be active so it will be update in the game 
            CloudMoveSpeed = 0.5f;  // Set how fast the Cloud moves 
        }

        public void Initialize(Texture2D tex, Vector2 position, float spd)
        {
            CloudTex = tex;         // Load the Cloud decoration object texture 
            Position = position;    // Set the position of the Cloud 
            Active = true;          // We initialize the Cloud to be active so it will be update in the game 
            CloudMoveSpeed = spd;  // Set how fast the Cloud moves 
        }

        public void Update(GameTime gameTime)
        {
            Position.Y += CloudMoveSpeed;        // The Cloud always moves to the left so decrement it's xposition 

            // If the Cloud is past the screen or its health reaches 0 then deactivate it 
            if (Position.Y > Height) { Active = false; } // By setting the Active flag to false, the game will remove this objet from the active game list 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                CloudTex,
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
