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
        const int maxCircles = 15;
        const int maxCirlceRadius = 30;

        //Circle Properties
        int[] circlesX = new int[maxCircles];
        int[] circlesY = new int[maxCircles];
        int[] circlesRadius = new int[maxCircles];
        bool[] circlesActivity = new bool[maxCircles];


        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(800, 600);
            AddCircle(400, 300);
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);

            Draw.FillColor = Color.Red;
            EvaluateCircles(); 
        }


        void InitCircles(int x, int y)
        {
            for (int i = 0; i < maxCircles; i++)
            {
                circlesX[i] = 0;
                circlesY[i] = 0;
                circlesRadius[i] = 0;
                circlesActivity[i] = false;
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


        void EvaluateCircles()
        {
            for (int circle = 0; circle < maxCircles; circle++) {

                if (circlesActivity[circle])
                {
                    Draw.Circle(circlesX[circle], circlesY[circle], circlesRadius[circle]);

                    if (circlesRadius[circle] < maxCirlceRadius)
                    {

                        circlesRadius[circle]++;

                    }
                }
            }
        }
    }
}
