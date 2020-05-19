using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maze
{
    public class Point
    {

        public int X;

        public int Y;

        public List<Point> Neighbors = new List<Point>();


        public dynamic value;

        public Point() { }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void FillNeigbors()
        {
            //if (Neighbors.Count > 0) return;

            //Neighbors.Add(tryGetValueFromMatrix(X, Y - 1));


            Neighbors.Clear();


            if (Y != 0)
            {
                Neighbors.Add(tryGetValueFromMatrix(X, Y - 1));
                //North = tryGetValueFromMatrix(X, Y - 1);
            }
            // Check for the neighbor to the East
            if (X < Maze.N - 1)
            {
                Neighbors.Add(tryGetValueFromMatrix(X + 1, Y));
                //East = tryGetValueFromMatrix(X + 1, Y);
            }
            // Check for the neighbor to the South
            if (Y < Maze.M - 1)
            {
                Neighbors.Add(tryGetValueFromMatrix(X, Y + 1));
                //South = tryGetValueFromMatrix(X, Y + 1);
            }
            // Check for the neighbor to the West
            if (X != 0)
            {
                Neighbors.Add(tryGetValueFromMatrix(X - 1, Y));
                //West = tryGetValueFromMatrix(X - 1, Y);
            }
        }


        private Point tryGetValueFromMatrix(int x, int y)
        {
            try
            {
                var temp = new Point(x, y)
                {
                    value = Maze.MazeField[x, y]
                };

                if (temp.value == null || temp.value.ToString() == Constants.WallSymbol)
                {
                    temp = null;
                }
                return temp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
