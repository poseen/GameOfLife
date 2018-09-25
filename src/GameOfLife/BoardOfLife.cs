using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class BoardOfLife
    {
        private bool[,] life;

        private int generation = 0;
        private int maxLives = 1;

        public int MaxLives => this.maxLives;

        private int numberOfLivingCells;
        public  int NumberOfLivingCells
        {
            get
            {
                return numberOfLivingCells;
            }

            private set
            {
                numberOfLivingCells = value;
                maxLives = Math.Max(numberOfLivingCells, maxLives);
                this.history.Add(value);
            }
        }

        private List<int> history = new List<int>();

        public BoardOfLife(int width, int height)
        {
            this.life = new bool[width, height];
            this.Width = width;
            this.Height = height;
        }

        public int Width { get; }
        public int Height { get; }

        public int Generation => this.generation;

        public void FillLife(double percent)
        {
            var rnd = new Random();

            this.NumberOfLivingCells = 0;
            this.generation = 0;

            for (var x = 0; x < this.Width; x++)
            {
                for (var y = 0; y < this.Height; y++)
                {
                    var xx = rnd.NextDouble() < percent;
                    this.life[x, y] = xx;
                    this.NumberOfLivingCells += xx ? 1 : 0;
                }
            }

            //// Glider
            //this.InsertGlider(1, 1);

            //this.InsertGlider(12, 21);

            //this.InsertGlider(10, 31);
        }

        public void InsertGlider(int x, int y)
        {
            this.life[x, y] = true;
            this.life[x + 2, y] = true;

            this.life[x + 1, y + 1] = true;
            this.life[x + 2, y + 1] = true;

            this.life[x + 1, y + 2] = true;
        }

        public void InsertLightWeightSpaceship(int x, int y)
        {

        }

        public void Step()
        {
            bool[,] nextGeneration = new bool[this.Width, this.Height];
            this.generation++;
            this.NumberOfLivingCells = 0;

            for (var x = 0; x < this.Width; x++)
            {
                for (var y = 0; y < this.Height; y++)
                {
                    int numberOfNeighbors =
                        IsNeighborAlive(x, y, -1, 0)
                        + IsNeighborAlive(x, y, -1, 1)
                        + IsNeighborAlive(x, y, 0, 1)
                        + IsNeighborAlive(x, y, 1, 1)
                        + IsNeighborAlive(x, y, 1, 0)
                        + IsNeighborAlive(x, y, 1, -1)
                        + IsNeighborAlive(x, y, 0, -1)
                        + IsNeighborAlive(x, y, -1, -1);

                    bool shouldLive = false;
                    bool isAlive = this.life[x, y];

                    if (isAlive && (numberOfNeighbors == 2 || numberOfNeighbors == 3))
                    {
                        shouldLive = true;
                    }
                    else if (!isAlive && numberOfNeighbors == 3) // zombification
                    {
                        shouldLive = true;
                    }

                    nextGeneration[x, y] = shouldLive;
                    this.NumberOfLivingCells += shouldLive ? 1 : 0;
                }
            }

            this.life = nextGeneration;
        }

        public Image ToImage()
        {
            var bitmap = new Bitmap(this.Width * 4, this.Height * 4);
            var g = Graphics.FromImage(bitmap);
            for(int x = 0; x < this.Width; x++)
            {
                for(int y = 0; y < this.Height; y++)
                {
                    g.FillRectangle(this.life[x, y] ? Brushes.Black : Brushes.White, x*4, y*4, 4, 4);
                }
            }

            return bitmap as Image;
        }

        private int IsNeighborAlive(int x, int y, int offsetx, int offsety)
        {
            int result = 0;

            int proposedOffsetX = x + offsetx;
            int proposedOffsetY = y + offsety;
            bool outOfBounds = proposedOffsetX < 0 || proposedOffsetX >= this.Width | proposedOffsetY < 0 || proposedOffsetY >= this.Height;
            if (!outOfBounds)
            {
                result = this.life[x + offsetx, y + offsety] ? 1 : 0;
            }
            return result;
        }
    }
}
