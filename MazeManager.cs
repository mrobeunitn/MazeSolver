using System;
using System.Text;

namespace RobertiMaze
{
    ///<summary> MazeManager class: this class manage the Maze and all the logic of the program is here. The view part can interface with the Maze only through this class. </summary>
    public class MazeManager
    {

        private Maze mazeToSolve;
        private const int arrayLenghtDim = 2;
        //used for array index into solveMaze
        private const int X = 0;
        private const int Y = 1;

        ///<summary>Class constructor</summary>
        ///<param name="filePath">Location of the file that contains the Maze</param>
        ///<return>No return values</return>
        public MazeManager(string filePath)
        {
            this.initMaze(filePath);
        }

        ///<summary>Method used to initialize the object Maze </summary>
        ///<param name="filePath">Location of the file that contains the Maze</param>
        ///<return>No return values</return>
        private void initMaze(string filePath)
        {
            int[] tempDim = new int[arrayLenghtDim];
            int[] tempStartCoordinates = new int[arrayLenghtDim];
            int[] tempEndCoordinates = new int[arrayLenghtDim];
            int[,] tempMazeMatrix;
            bool[,] tempTrackMatrix = null;
            string[,] tempOutputMatrix = null;

            //calling the method createMaze, and the matrix are passed through reference
            tempMazeMatrix = this.CreateMaze(filePath, ref tempTrackMatrix, ref tempOutputMatrix, ref tempDim, ref tempStartCoordinates, ref tempEndCoordinates);
            //instantiating the objeect
            this.mazeToSolve = new Maze(tempStartCoordinates, tempEndCoordinates, tempMazeMatrix, tempTrackMatrix, tempOutputMatrix, tempDim);
        }

        ///<summary>Method that read from file and initialize the Maze to solve</summary>
        ///<param name="filePath">Location of the file that contains the Maze</param>
        ///<param name="trackMatrix">Pass the reference of the track matrix. After the execution of this method, the matrix passe, will be inizialized at false.</param>
        ///<param name="trackMatrix">Pass the reference of the result matrix. After the execution of this method, the matrix passed will be inizialized as the output rules.</param>
        ///<param name="start">Pass the reference of the start position array. After the execution of this method, the array passed will be inizialized at the startX([0]) and startY([1]) position.</param>
        ///<param name="end">Pass the reference of the end position array. After the execution of this method, the array passed will be inizialized at the endX([0]) and endY([1]) position.</param>
        ///<return name="returnMaze">Matrix that contains the numerical maze</return>
        private int[,] CreateMaze(string filePath, ref bool[,] trackMatrix, ref string[,] resultMatrix, ref int[] dim, ref int[] start, ref int[] end)
        {
            int[,] returnMaze = null;
            try
            {
                string[] fileLines = System.IO.File.ReadAllLines(filePath);
                //creating the maze
                var stringDim = fileLines[0].Split(" ");
                dim[0] = Int32.Parse(stringDim[0]);
                dim[1] = Int32.Parse(stringDim[1]);
                //start point
                var startString = fileLines[1].Split(" ");
                start[0] = Int32.Parse(startString[0]);
                start[1] = Int32.Parse(startString[1]);
                //end point
                var endString = fileLines[2].Split(" ");
                end[0] = Int32.Parse(endString[0]);
                end[1] = Int32.Parse(endString[1]);
                //initializin the three matrix
                returnMaze = new int[dim[1], dim[0]];
                trackMatrix = new bool[dim[1], dim[0]];
                resultMatrix = new string[dim[1], dim[0]];
                for (int i = 3, j = 0; i < fileLines.Length; i++, j++)
                {
                    var splittedRow = fileLines[i].Split(" ");
                    for (int z = 0; z < splittedRow.Length; z++)
                    {
                        returnMaze[j, z] = Int32.Parse(splittedRow[z]);

                        //setting-up util matrix
                        trackMatrix[j, z] = false;

                        //setting-up result matrix
                        if (returnMaze[j, z] == 1)
                        {
                            resultMatrix[j, z] = "#";
                        }
                        else if (returnMaze[j, z] == 0)
                        {
                            resultMatrix[j, z] = " ";
                        }
                    }
                }

                return returnMaze;

            }
            catch (FieldAccessException e)
            {
                throw e;
            }
            catch (IndexOutOfRangeException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        ///<summary>Wrapper method of solveMaze, call this method in order to solve the Maze.</summary>
        ///<param>No params needed</param>
        ///<return name="result">Formatted result string(if solution exists) otherwise a string that let you know that there aren' solutions.</return>
        public string SolveMaze()
        {
            var result = _solveMaze(this.mazeToSolve.Start[0], this.mazeToSolve.Start[1]);

            if (result)
            {

                return this.mazeToSolve.ToString();
            }
            else
            {
                return "No solution founded";
            }
        }


        ///<summary>Method that contains the logic used to solve the Maze.</summary>
        ///<param name="positionX">X start position coordinate</param>
        ///<param name="positionY">Y start position coordinate</param>
        ///<return>True if a solution is founded, false if not</return>
        private bool _solveMaze(int positionX, int positionY)
        {
            //this is the case when I achieved the exit (if exists)
            if (this.mazeToSolve.End[X] == positionX && this.mazeToSolve.End[Y] == positionY)
            {
                return true;
            }
            //when i find an invalid path, i mark it as false in order to know that is already visited and not valid (and so not to be lost in loops)
            if (this.mazeToSolve.MazeMatrix[positionY, positionX] == 1 || this.mazeToSolve.TrackMatrix[positionY, positionX])
            {
                return false;
            }

            //tracking the position visited
            this.mazeToSolve.TrackMatrix[positionY, positionX] = true;

            //west move
            if (positionX != 0)
            {
                if (_solveMaze(positionX - 1, positionY))
                {
                    this.mazeToSolve.ResultMatrix[positionY, positionX] = "X";
                    return true;
                }
            }

            //est move
            if (positionX != this.mazeToSolve.MazeDim[X] - 1)
            {
                if (_solveMaze(positionX + 1, positionY))
                {
                    this.mazeToSolve.ResultMatrix[positionY, positionX] = "X";
                    return true;
                }
            }

            //north move
            if (positionY != 0)
            {
                if (_solveMaze(positionX, positionY - 1))
                {
                    this.mazeToSolve.ResultMatrix[positionY, positionX] = "X";
                    return true;
                }
            }

            //south move
            if (positionY != this.mazeToSolve.MazeDim[Y] - 1)
            {
                if (_solveMaze(positionX, positionY + 1))
                {
                    this.mazeToSolve.ResultMatrix[positionY, positionX] = "X";
                    return true;
                }
            }

            return false;
        }
    }
}
