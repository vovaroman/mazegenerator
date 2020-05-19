using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace maze
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.CursorVisible = false;

            var m = new Maze(200, 5);

            ProcessCell(m);



            Console.ReadKey();

        }


        public static void ProcessCell(Maze m)
        {
            var index = 0;

            while (true)
            {
                var min = m.FindMinNeighbors();

                if (min == null)
                {
                    try
                    {
                        min = m.PassedCells[index];
                        index++;
                        //continue;
                    }
                    catch(Exception ex)
                    {
                        m.DrawMaze();
                        break;
                    }


                }
                else
                {
                    index = 0;
                    m.PassedCells.Add(min);
                }


                m.CurrentPoint = min;
                m.MarkCurrentPoint();
                //Thread.Sleep(100);

                m.DrawMaze();
            }
        }

    }
}
