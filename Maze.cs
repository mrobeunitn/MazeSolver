using System;
using System.Text;

namespace RobertiMaze
{
    ///<summary> Maze class: This class represent the model of the object Maze. This class contain all the attribute of a Maze and the method ToString() for print the solved maze </summary>
    public class Maze
    {

        public int[] MazeDim;
        public int[] Start;
        public int[] End;
        public int[,] MazeMatrix;
        public bool[,] TrackMatrix;
        public string[,] ResultMatrix;



        ///<summary> Constructor that initialize the object Maze </summary>
        ///<param name="start"> An array that contains start X position and start Y position</param>
        ///<param name="end"> An array that contains end X end and start Y position</param>
        ///<param name="matrixToSolve"> A matrix containing the structure of the maze to solve</param>
        ///<param name="trackMatrix"> Matrix used to track the spot where you have already been durign the maze visit </param>
        ///<param name="resultMatrix"> Matrix that contains the solution, if it exists </param>
        ///<param name="MazeDim">Array that cointins X dimension and Y dimension of the Maze</param>

        public Maze(int[] start, int[] end, int[,] matrixToSolve, bool[,] trackMatrix, string[,] resultMatrix, int[] mazeDim)
        {
            this.Start = start;
            this.End = end;
            this.TrackMatrix = trackMatrix;
            this.ResultMatrix = resultMatrix;
            this.MazeMatrix = matrixToSolve;
            this.MazeDim = mazeDim;
        }

        ///<summary> Overrided method ToString, and used to return the formatted result string </summary>
        ///<param>No param </param>
        ///<return>Resutl Formatted String</return> 

        public override string ToString()
        {
            StringBuilder returnString = new StringBuilder();

            for (int i = 0; i < MazeDim[1]; i++)
            {
                for (int j = 0; j < MazeDim[0]; j++)
                {
                    //appending "S" in start position
                    if (this.Start[1] == i && this.Start[0] == j)
                    {
                        returnString.Append("S");
                    }
                    else if (this.End[1] == i && this.End[0] == j)
                    {
                        returnString.Append("E");
                    }
                    else
                    {
                        returnString.Append(ResultMatrix[i, j]);
                    }
                }
                returnString.Append("\n");
            }
            return returnString.ToString();

        }


    }
}
