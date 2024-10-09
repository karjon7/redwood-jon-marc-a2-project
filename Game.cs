// Include code libraries you need below (use the namespace).
using Raylib_cs;
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
        const int maxCircles = 10; // Max amount of circles on screen
        const float maxCirlceRadius = 35f; // The biggest the circles will get
        const float secsToGrow = 5f; // Time to grow to max radius

        //Circle Properties
        int[] circlesX = new int[maxCircles]; // Array that holds the x position of all circles
        int[] circlesY = new int[maxCircles]; // Array that holds the y position of all circles
        float[] circlesRadius = new float[maxCircles]; // Array that holds the radius of all circles
        bool[] circlesActivity = new bool[maxCircles]; // Array that holds the active state of all circles

        // Spawning logic variables
        const float minSpawnInterval = 0.1f; // Minimum time between checks
        const float maxSpawnProbability = 0.9f; // Max probaility chance
        const float spawnIntervalDecrease = 0.005f; // How much the interval decreases each time
        const float spawnProbabilityIncrease = 0.01f; // How much the probability increases each time

        float timeSinceLastCheck = 0f; // Time since the last spawn check
        float spawnInterval = 1f;      // Start checking every 1 seconds
        float spawnProbability = 0.5f; // Start with a 50% chance of spawning

        // Player Score
        int playerScore = 0;


        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(800, 600);
            Window.TargetFPS = 60;

            InitCircles();
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);

            UpdateSpawnTimer(); // Check if it's time to spawn a circle

            EvaluateCircles(); // Update and draw the circles

            CheckCircleClick(); // Detect and handle player clicks

            DrawText();
        }


        void DrawText()
        {
            Text.Color = Color.Black;
            Text.Size = 25;

            Text.Draw($"You've scored {playerScore} points. \nTime: {Time.SecondsElapsed}", 0, 0);
        }


        void CheckCircleClick()
        {
            // Check if the mouse button is pressed
            if (Input.IsMouseButtonPressed(MouseInput.Left))
            {
                // Loop through active circles
                for (int i = 0; i < maxCircles; i++)
                {
                    if (circlesActivity[i])
                    {
                        // Calculate the distance from the mouse click to the center of the circle
                        Vector2 circle_pos = new Vector2(circlesX[i], circlesY[i]); // I know we arent supposed to use vectors but i dont want to do the math with floats
                        float distance = Vector2.Distance(Input.GetMousePosition(), circle_pos);

                        // If the distance is less than or equal to the circle's radius, it's a hit
                        if (distance <= circlesRadius[i])
                        {
                            // Handle the click (e.g., remove the circle)
                            RemoveCircle(i);
                            playerScore++;
                            break; // Exit after detecting the first clicked circle
                        }
                    }
                }
            }
        }


        // Initializes the circles to default (inactive) state
        void InitCircles()
        {
            for (int i = 0; i < maxCircles; i++)
            {
                RemoveCircle(i);
            }
        }


        // Adds a new circle
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


        // Removes a circle, resetting its properties
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
                    else
                    {
                        RemoveCircle(circle);
                        playerScore--;
                    }
                }
            }
        }


        // Function to update the spawn timer and determine if a circle should spawn
        void UpdateSpawnTimer()
        {
            timeSinceLastCheck += Time.DeltaTime; // Increment time since last check

            if (timeSinceLastCheck >= spawnInterval)
            {
                TryToSpawnCircle(); // Attempt to spawn a circle
                timeSinceLastCheck = 0f; // Reset timer after checking

                // Adjust spawn interval and probability
                AdjustSpawnIntervalAndProbability();
            }
        }


        // Attempts to spawn a circle based on spawn probability
        void TryToSpawnCircle()
        {
            // Generate a random float between 0 and 1, and compare with spawn probability
            if (Random.Float() < spawnProbability)
            {
                AddCircle(Random.Integer(0 + (int)maxCirlceRadius, Window.Width - (int)maxCirlceRadius), Random.Integer(0 + (int)maxCirlceRadius, Window.Height - (int)maxCirlceRadius)); // Spawn circle at random position
            }
        }


        // Adjust the spawn interval and probability over time
        void AdjustSpawnIntervalAndProbability()
        {
            // Decrease spawn interval (but never below minSpawnInterval)
            if (spawnInterval > minSpawnInterval)
            {
                spawnInterval -= spawnIntervalDecrease;
                if (spawnInterval < minSpawnInterval)
                {
                    spawnInterval = minSpawnInterval;
                }
            }

            // Increase spawn probability over time
            if (spawnProbability < maxSpawnProbability)
            {
                spawnProbability += spawnProbabilityIncrease; 
                if (spawnProbability > maxSpawnProbability)
                {
                    spawnProbability = maxSpawnProbability; // Cap the probability at max probability
                }
            }
        }
    }
}
