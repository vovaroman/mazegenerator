using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace maze
{
    public class Maze
    {

        public static dynamic N;

        public static dynamic M;

        public static dynamic[,] MazeField;


        public Point StartPoint;

        public Point CurrentPoint;


        public List<Point> PassedCells = new List<Point>();



        private void generateRandomTerrain()
        {
            var random = new Random();
            for(var i = 0; i < N; i++)
            {
                for(var j = 0; j < M; j++)
                {
                    MazeField[i,j] = random.Next(0, 101);
                }
            }
        }



        private Point GetRandomPointOnBorder()
        {
            var x = 0;
            var y = 0;

            var random = new Random();

            var limit = int.MaxValue;
            if (limit > N)
                limit = N;
            if (limit > M)
                limit = M;


            switch (random.Next(0, 4))
            {
                case 0:
                    //top border
                    x = 0;
                    y = random.Next(0, limit);
                    break;
                case 1:
                    //left border
                    x = random.Next(0, limit);
                    y = 0;
                    break;
                case 2:
                    //bottom border
                    x = limit - 1;
                    y = random.Next(0, limit);
                    break;
                case 3:
                    //right border
                    x = random.Next(0, limit);
                    y = limit - 1;
                   
                    break;
            }

            return new Point(x, y);
        }

        bool _firstPoint = true;

        public void DrawMaze()
        {
            //Console.Clear();
            Console.SetCursorPosition(0, 0);

            if(PassedCells.Count > 2)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(PassedCells[PassedCells.Count - 2].X, PassedCells[PassedCells.Count - 2].Y);
                Console.Write("█");
            }

            Console.SetCursorPosition(PassedCells.LastOrDefault().X, PassedCells.LastOrDefault().Y);
            if (_firstPoint)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                _firstPoint = false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;

            }
            Console.Write("█");

 

        }


        public Maze(int n, int m)
        {
            N = n;
            M = m;

            MazeField = new dynamic[N,M];

            generateRandomTerrain();

            StartPoint = GetRandomPointOnBorder();

            CurrentPoint = StartPoint;

            MazeField[StartPoint.X, StartPoint.Y] = Constants.WallSymbol;
    

        }


        private bool checkSubNeigbors(Point p)
        {

            Func<Point, bool> checkSubNeigborsValue = point => point == null ? true : (point.value.ToString() == Constants.WallSymbol ? true : false);

            p.FillNeigbors();

            var count = 0;

            p.Neighbors.ForEach(x =>
               {
                   if (checkSubNeigborsValue(x) )
                   {
                           count++;
                   }
               });

            if (count > 1)
                return false;
            return true;

        }
        public Point FindMinNeighbors()
        {
            Func<Point, bool> checkNeighbor = point => point == null ? false : true;

            var x = CurrentPoint.X;
            var y = CurrentPoint.Y;

            CurrentPoint.FillNeigbors();

            var minPoint = new Point() { value = int.MaxValue };


            CurrentPoint.Neighbors.ForEach(p =>
               {
                   if(checkNeighbor(p) && checkSubNeigbors(p))
                   {
                       if (minPoint.value > p.value)
                           minPoint = p;
                   }
               });

            
            if(minPoint.value == int.MaxValue)
            {
                return null;
            }
            return minPoint;
        }


        public void MarkCurrentPoint()
        {
            MazeField[CurrentPoint.X, CurrentPoint.Y] = Constants.WallSymbol;
        }


        

    }
}
