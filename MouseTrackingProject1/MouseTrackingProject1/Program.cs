using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RecordPlayMouseMove
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        public struct POINT
        {
            public int X;
            public int Y;
        }

        static void Main()
        {
            //upis u text file 
            string[] lines = { "First line", "Second line", lines };
            System.IO.File.WriteAllLines(@"C:\Users\Public\TestFolder\WriteLines.txt", lines);

            //citanje koordinata miša
            POINT current_pos, prev_pos;
            List<POINT> coords = new List<POINT>();
            
            prev_pos.X = 0;
            prev_pos.Y = 0;


            Console.WriteLine("Press any key to start/stop recording mouse movements.");
            Console.ReadKey();
            do
            {
                if (GetCursorPos(out current_pos))
                {

                    if ((current_pos.X != prev_pos.X) || (current_pos.Y != prev_pos.Y))
                    {

                        Console.WriteLine("({0},{1})", current_pos.X, current_pos.Y);
                        coords.Add(current_pos);
                    }

                    prev_pos.X = current_pos.X;
                    prev_pos.Y = current_pos.Y;
                }

            } while (!Console.KeyAvailable);
        }

        private static int List<T>()
        {
            throw new NotImplementedException();
        }
    }
}