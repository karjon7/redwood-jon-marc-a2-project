// Include code libraries you need below (use the namespace).
using System;
using System.Numerics;

// The namespace your code is in.
namespace Game10003
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        // Place your variables here:
        int bananas = 0;


        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(800, 600);
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);

            if (Input.IsMouseButtonPressed(MouseInput.Left) )
            {
                bananas++;

            }

            Text.Color = Color.Black;
            Text.Size = 50;

            if (bananas == 1)
            {
                Text.Draw($"You have {bananas} banana.", 0, 0);
            }
            else
            {
                Text.Draw($"You have {bananas} bananas.", 0, 0);

            }
        }
    }
}
