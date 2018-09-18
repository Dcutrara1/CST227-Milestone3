using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Milestone_1___Grid_Based_Game
{
    class MinesweeperGame : Grid
    {
        // Variables
        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public bool GameOver = false;

        public MinesweeperGame(int Rows, int Cols)
        {
        }

        public void PlayGame()
        {
            Reveal(Rows, Cols);
            GetInput();
        }

        public void GetInput()
        {
            // Get input from user
            Console.WriteLine(" ");
            Console.WriteLine("Enter the Row position you chose between 0 and "
                + (Board.GetLength(0) - 1) + ": ");

            // Verify User Input
            if (int.TryParse(Console.ReadLine(), out int inputCol))
            {
                if (inputCol >= 0 && inputCol < Board.GetLength(0))
                {
                    Console.WriteLine(" Enter the Column position you chose between 0 and "
                        + (Board.GetLength(1) - 1) + ": ");

                    if (int.TryParse(Console.ReadLine(), out int inputRow))
                    {
                        while (Board[inputRow, inputCol].Bomb != true)
                        {
                            if (inputRow >= 0 && inputRow < Board.GetLength(1))
                            {
                                if (Board[inputRow, inputCol].Visited == true)
                                {
                                    Console.WriteLine("Cell already visited.");
                                    Console.ReadLine();
                                    Console.Clear();
                                    PlayGame();
                                }

                                if (Board[inputRow, inputCol].Bomb == false)
                                {
                                    Board[inputRow, inputCol].Visited = true;
                                    OpenCells(inputRow, inputCol);
                                    Console.Clear();
                                    PlayGame();
                                }
                            }
                        }

                        if (Board[inputRow, inputCol].Bomb == true)
                        {
                            GameOver = true;
                            Console.Clear();
                            Console.WriteLine(" ");
                            Console.Write("You Selected: " + inputRow + ", " + inputCol + ". ");
                            Console.WriteLine("You hit a BOMB!");
                            base.Reveal(inputRow, inputCol);
                            Console.WriteLine(" ");
                            Console.WriteLine("Game Over"); Console.ReadLine();
                            GameOverSteps();
                        }
                    }
                    else { Console.WriteLine("Enter a valid Column."); }
                }
                else { Console.WriteLine("Enter a valid Row."); }                
            }
        }

        public void GameOverSteps()
        {
            if (GameOver == true)
            {
                //Close the current process
                Environment.Exit(0);
            }
        }

        public override void Reveal(int Rows, int Cols)
        {            
            Console.Write("\nThe board is : \n");
            Console.WriteLine(" ");
            for (int y = 0; y < Board.GetLength(1); ++y)
            {
                for (int x = 0; x < Board.GetLength(0); ++x)
                {
                    Game_Cell.Count = 0;

                    if (Board[x, y].BombLeft == true) { Game_Cell.Count++; }
                    if (Board[x, y].BombRight == true) { Game_Cell.Count++; }
                    if (Board[x, y].BombUp == true) { Game_Cell.Count++; }
                    if (Board[x, y].BombDown == true) { Game_Cell.Count++; }
                    if (Board[x, y].BombUpLeft == true) { Game_Cell.Count++; }
                    if (Board[x, y].BombUpRight == true) { Game_Cell.Count++; }
                    if (Board[x, y].BombDownLeft == true) { Game_Cell.Count++; }
                    if (Board[x, y].BombDownRight == true) { Game_Cell.Count++; }
                    if (Board[x, y].Bomb == true) { Game_Cell.Count = 9; }

                    // If Cell has been visited, Display number of bombs surrounding or ~ if 0.
                    if (Board[x, y].Visited == true)
                    {
                        if (Game_Cell.Count == 0)
                        { Console.Write(" " + " "); }

                        if (Game_Cell.Count != 0 && Game_Cell.Count != 9)
                        { Console.Write(Game_Cell.Count + " "); }
                    }

                    // If Cell has not been visited, place a ?
                    if (Board[x, y].Visited == false)
                    { Console.Write("?" + " "); }
                }
                Console.WriteLine();
            }
        }

        public void OpenCells(int r, int c)
        {
            Game_Cell.Count = 0;

            if (Board[r, c].BombLeft == true) { Game_Cell.Count++; }
            if (Board[r, c].BombRight == true) { Game_Cell.Count++; }
            if (Board[r, c].BombUp == true) { Game_Cell.Count++; }
            if (Board[r, c].BombDown == true) { Game_Cell.Count++; }
            if (Board[r, c].BombUpLeft == true) { Game_Cell.Count++; }
            if (Board[r, c].BombUpRight == true) { Game_Cell.Count++; }
            if (Board[r, c].BombDownLeft == true) { Game_Cell.Count++; }
            if (Board[r, c].BombDownRight == true) { Game_Cell.Count++; }
            if (Board[r, c].Bomb == true) { Game_Cell.Count = 9; }

            if (Game_Cell.Count > 0)
            { return; }

            for (var Row = -1; Row <= 1; Row++)
            {
                var NextRow = Row + r;
                var NextCol = c;
                if (NextRow > -1 && NextRow < Board.GetLength(0)
                    && NextCol > -1 && NextCol < Board.GetLength(1))
                {
                    if (Board[NextRow, NextCol].Bomb != true &&
                             Board[NextRow, NextCol].Explored != true &&
                             Board[NextRow, NextCol].Visited != true && Game_Cell.Count > 0)
                    {
                        Board[NextRow, NextCol].Visited = true;
                        OpenCells(NextRow, NextCol);
                    }


                    if (Board[NextRow, NextCol].Bomb != true &&
                       Board[NextRow, NextCol].Explored != true &&
                       Board[NextRow, NextCol].Visited != true && Game_Cell.Count == 0)
                    {

                        Board[NextRow, NextCol].Visited = true;
                        Board[NextRow, NextCol].Explored = true;

                        OpenCells(NextRow, NextCol);
                    }

                }
            }

            for (var Col = -1; Col <= 1; Col++)
            {
                var NextRow2 = r;
                var NextCol2 = Col + c;
                if (NextRow2 > -1 && NextRow2 < Board.GetLength(0)
                    && NextCol2 > -1 && NextCol2 < Board.GetLength(1))
                {
                    if (Board[NextRow2, NextCol2].Bomb != true &&
                             Board[NextRow2, NextCol2].Explored != true &&
                             Board[NextRow2, NextCol2].Visited != true && Game_Cell.Count > 0)
                    {
                        Board[NextRow2, NextCol2].Visited = true;
                        OpenCells(NextRow2, NextCol2);
                    }

                    if (Board[NextRow2, NextCol2].Bomb != true &&
                       Board[NextRow2, NextCol2].Explored != true &&
                       Board[NextRow2, NextCol2].Visited != true && Game_Cell.Count == 0)
                    {
                        Board[NextRow2, NextCol2].Visited = true;
                        Board[NextRow2, NextCol2].Explored = true;
                        OpenCells(NextRow2, NextCol2);
                    }
                }
            }

            for (var Row = -1; Row <= 1; Row++)
            {
                var NextRow = Row + r;
                var NextCol = c+1;
                if (NextRow > -1 && NextRow < Board.GetLength(0)
                    && NextCol > -1 && NextCol < Board.GetLength(1))
                {
                    if (Board[NextRow, NextCol].Bomb != true &&
                             Board[NextRow, NextCol].Explored != true &&
                             Board[NextRow, NextCol].Visited != true && Game_Cell.Count > 0)
                    {
                        Board[NextRow, NextCol].Visited = true;
                        OpenCells(NextRow, NextCol);
                    }

                    if (Board[NextRow, NextCol].Bomb != true &&
                       Board[NextRow, NextCol].Explored != true &&
                       Board[NextRow, NextCol].Visited != true && Game_Cell.Count == 0)
                    {
                        Board[NextRow, NextCol].Visited = true;
                        Board[NextRow, NextCol].Explored = true;
                        OpenCells(NextRow, NextCol);
                    }
                }
            }

            for (var Col = -1; Col <= 1; Col++)
            {
                var NextRow2 = r+1;
                var NextCol2 = Col + c;
                if (NextRow2 > -1 && NextRow2 < Board.GetLength(0)
                    && NextCol2 > -1 && NextCol2 < Board.GetLength(1))
                {
                    if (Board[NextRow2, NextCol2].Bomb != true &&
                             Board[NextRow2, NextCol2].Explored != true &&
                             Board[NextRow2, NextCol2].Visited != true && Game_Cell.Count > 0)
                    {
                        Board[NextRow2, NextCol2].Visited = true;
                        OpenCells(NextRow2, NextCol2);
                    }
                    
                    if (Board[NextRow2, NextCol2].Bomb != true &&
                       Board[NextRow2, NextCol2].Explored != true &&
                       Board[NextRow2, NextCol2].Visited != true && Game_Cell.Count == 0)
                    {
                        Board[NextRow2, NextCol2].Visited = true;
                        Board[NextRow2, NextCol2].Explored = true;
                        OpenCells(NextRow2, NextCol2);
                    }
                }
            }

            for (var Row = -1; Row <= 1; Row++)
            {
                var NextRow = Row + r;
                var NextCol = c - 1;
                if (NextRow > -1 && NextRow < Board.GetLength(0)
                    && NextCol > -1 && NextCol < Board.GetLength(1))
                {
                    if (Board[NextRow, NextCol].Bomb != true &&
                             Board[NextRow, NextCol].Explored != true &&
                             Board[NextRow, NextCol].Visited != true && Game_Cell.Count > 0)
                    {
                        Board[NextRow, NextCol].Visited = true;
                        OpenCells(NextRow, NextCol);
                    }

                    if (Board[NextRow, NextCol].Bomb != true &&
                       Board[NextRow, NextCol].Explored != true &&
                       Board[NextRow, NextCol].Visited != true && Game_Cell.Count == 0)
                    {
                        Board[NextRow, NextCol].Visited = true;
                        Board[NextRow, NextCol].Explored = true;
                        OpenCells(NextRow, NextCol);
                    }
                }
            }

            for (var Col = -1; Col <= 1; Col++)
            {
                var NextRow2 = r - 1;
                var NextCol2 = Col + c;
                if (NextRow2 > -1 && NextRow2 < Board.GetLength(0)
                    && NextCol2 > -1 && NextCol2 < Board.GetLength(1))
                {
                    if (Board[NextRow2, NextCol2].Bomb != true &&
                             Board[NextRow2, NextCol2].Explored != true &&
                             Board[NextRow2, NextCol2].Visited != true && Game_Cell.Count > 0)
                    {
                        Board[NextRow2, NextCol2].Visited = true;
                        OpenCells(NextRow2, NextCol2);
                    }

                    if (Board[NextRow2, NextCol2].Bomb != true &&
                       Board[NextRow2, NextCol2].Explored != true &&
                       Board[NextRow2, NextCol2].Visited != true && Game_Cell.Count == 0)
                    {
                        Board[NextRow2, NextCol2].Visited = true;
                        Board[NextRow2, NextCol2].Explored = true;
                        OpenCells(NextRow2, NextCol2);
                    }
                }
            }
            return;        
        }
    }
}   