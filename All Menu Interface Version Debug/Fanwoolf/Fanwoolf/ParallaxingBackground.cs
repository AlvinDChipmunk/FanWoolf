// ParallaxingBackground.cs
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Fanwoolf
{
    class ParallaxingBackground
    {
        Texture2D texture; // The image representing the parallaxing background
        Vector2[] positions; // An array of positions of the parallaxing background
        int xSpeed; // The x component of speed which the background is moving
        int ySpeed; // The y component of speed which the background is moving

        public void Initialize (
            ContentManager content, 
            String texturePath, 
            int screenWidth, 
            int xDirSpeed, 
            int yDirSpeed 
        )

        {
            texture = content.Load<Texture2D>(texturePath); // Load the background texture we will be using
            this.xSpeed = xDirSpeed; // Set the speed of the background
            this.ySpeed = yDirSpeed; // Set the speed of the background

            // If we divide the screen with the texture width then we can determine the number of tiles need.
            positions = new Vector2[screenWidth / texture.Width + 1]; // We add 1 to it so that we won't have a gap in the tiling

            for (int i = 0; i < positions.Length; i++) // Set the initial positions of the parallaxing background
            {
                positions[i] = new Vector2(i * texture.Width, 0); // We need the tiles to be side by side to create a tiling effect
            }
        }

        public void Update()
        {

            for (int i = 0; i < positions.Length; i++) // Update the positions of the background
            {
                positions[i].X += xSpeed; // Update the position of the screen by adding the speed
                positions[i].Y += ySpeed; // Update the position of the screen by adding the speed

                if (xSpeed <= 0) // If the speed has the background moving to the left
                {
                    if (positions[i].X <= -texture.Width) // Check the texture is out of view then put that texture at the end of the screen
                    {
                        positions[i].X = texture.Width * (positions.Length - 1);
                    }
                }
                else // If the speed has the background moving to the right
                {
                    if (positions[i].X >= texture.Width * (positions.Length - 1)) // Check if the texture is out of view then position it to the start of the screen
                    {
                        positions[i].X = -texture.Width;
                    }
                }

                if (ySpeed <= 0) // If the speed has the background moving to the left
                {
                    if (positions[i].Y <= -texture.Height) // Check the texture is out of view then put that texture at the end of the screen
                    {
                        positions[i].Y = texture.Height * (positions.Length - 1);
                    }
                }
                else // If the speed has the background moving to the right
                {
                    if (positions[i].Y >= texture.Height * (positions.Length - 1)) // Check if the texture is out of view then position it to the start of the screen
                    {
                        positions[i].Y = -texture.Height;
                    }
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int g = 0; g < positions.Length; g++)
            {
                spriteBatch.Draw(texture, positions[g], Color.White);
            }
        }

    }
}
