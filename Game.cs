// Include code libraries you need below (use the namespace).
using System;
using System.Linq;
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
       
        // Circle constants (unsure if we are allowed to use const yet)
        const int maxCircles = 15; // Max amount of circles on screen
        const float maxCirlceRadius = 30f; // The biggest the circles will get
        const float secsToGrow = 5f; // Time to grow to max radius

        //Circle Properties
        int[] circlesX = new int[maxCircles]; // Array that holds the x position of all circles
        int[] circlesY = new int[maxCircles]; // Array that holds the y position of all circles
        float[] circlesRadius = new float[maxCircles]; // Array that holds the radius of all circles
        bool[] circlesActivity = new bool[maxCircles]; // Array that holds the active state of all circles


        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(800, 600);
            Window.TargetFPS = 60;

            InitCircles();

            AddCircle(400, 300);
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);

            EvaluateCircles(); 
        }


        void InitCircles()
        {
            for (int i = 0; i < maxCircles; i++)
            {
                RemoveCircle(i);
            }
        }



        void AddCircle(int x, int y)
        {
            for (int circle = 0; circle < maxCircles; circle++) { 
                if (!circlesActivity[circle])
                {
                    circlesX[circle] = x;
                    circlesY[circle] = y;
                    circlesRadius[circle] = 0;
                    circlesActivity[circle] = true;

                    break;
                }
            }
        }


        void RemoveCircle(int index)
        {
            circlesX[index] = 0;
            circlesY[index] = 0;
            circlesRadius[index] = 0;
            circlesActivity[index] = false;
        }


        void EvaluateCircles()
        {
            float growthIncrement = maxCirlceRadius / (secsToGrow * 60f); // Growth per frame

            for (int circle = 0; circle < maxCircles; circle++)
            {
                if (circlesActivity[circle])
                {
                    // Calculate the ratio of the current radius to the max radius
                    float ratio = circlesRadius[circle] / maxCirlceRadius;

                    // Ensure the ratio stays between 0 and 1
                    if (ratio > 1) ratio = 1;

                    // Calculate the RGB values
                    int red = 255; // Red stays constant
                    int green = (int)(255 * (1 - ratio)); // Decrease green as radius increases
                    int blue = (int)(255 * (1 - ratio)); // Decrease blue as radius increases

                    // Set the fill color based on the calculated RGB values
                    Draw.FillColor = new Color(red, green, blue);

                    // Draw the circle
                    Draw.Circle(circlesX[circle], circlesY[circle], circlesRadius[circle]);

                    if (circlesRadius[circle] < maxCirlceRadius) // Grow the circle if it's not at max size.
                    {
                        circlesRadius[circle] += growthIncrement;
                    }
                    else // If it is at max size, send it to remove circle function to be removed from all arrays
                    {
                        RemoveCircle(circle);
                    }
                }
            }
        }
    }
}
