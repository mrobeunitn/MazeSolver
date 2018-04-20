using System;

namespace RobertiMaze
{
    class Program
    {
        static void Main(string[] args)
        {
            //calling the solve Method
            try
            {
                MazeManager mazeManager = new MazeManager(args[0]);
                var solution = mazeManager.SolveMaze();
                Console.WriteLine(solution);
            }
            catch (IndexOutOfRangeException)
            {
                Console.Write("Index out of range");
            }
            catch (FieldAccessException)
            {
                Console.WriteLine("Impossible access file");
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("File not found.");
            }
            catch (Exception)
            {
                Console.WriteLine("A general Exception occurred");
            }

        }
    }
}
