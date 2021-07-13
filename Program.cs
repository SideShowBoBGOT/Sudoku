using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,][] sudoku = new int[9, 9][];
            InicializeSudoku(sudoku);
            ShowSudoku(sudoku);
            Console.WriteLine("-------------------------------------------------");
            List<int[]> sudokuList = SudokuToList(sudoku);
            int[,][] result = SolveSudoku(sudokuList);

            FullFillSudoku(result);
            ShowSudoku(result);
        }
        static void ShowSudoku(int[,][] sudoku)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write($" {sudoku[i, j][2]} ");
                }
                Console.WriteLine();
                for (int j = 0; j < 9; j++)
                {
                    Console.Write($"{sudoku[i, j][0]}{sudoku[i, j][1]} ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }
        static void InicializeSudoku(int[,][] sudoku)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sudoku[i, j] = new int[3];
                }
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (j == 0)
                        sudoku[i, j][0] = 1;
                    else
                        sudoku[i, j][0] = sudoku[i, j - 1][0] + 1;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sudoku[i, j][1] = i + 1;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (j == 0)
                    {
                        sudoku[i, j][2] = sudoku[i, j][0] + sudoku[i, j][1];
                        if (sudoku[i, j][2] > 9)
                            sudoku[i, j][2] = 1;
                    }
                    else
                    {
                        if (sudoku[i, j - 1][2] == 9)
                            sudoku[i, j][2] = 1;
                        else
                            sudoku[i, j][2] = sudoku[i, j - 1][2] + 1;
                    }
                }
            }
        }
        static List<int[]> SudokuToList(int[,][] sudoku)
        {
            List<int[]> sudokuList = new List<int[]>();
            foreach (var i in sudoku)
                sudokuList.Add(i);
            return sudokuList;
        }
        static void FullFillSudoku(int[,][] sudoku)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudoku[i, j] == null)
                        sudoku[i, j] = new int[3] { 1, 1, 1 };
                }
            }
        }
        static int[,][] SolveSudoku(List<int[]> suppSudokuList)
        {
            int[,][] result = new int[9, 9][];

            Random rnd = new Random();
            List<int[]> suppSudokuList_1 = new List<int[]>();
            for (int b = 0; b < suppSudokuList.Count; b++)
            {
                suppSudokuList_1.Add(suppSudokuList[b]);
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    //first element in a row
                    List<int[]> suppSudokuList_2 = new List<int[]>();
                    for (int b = 0; b < suppSudokuList_1.Count; b++)
                    {
                        suppSudokuList_2.Add(suppSudokuList_1[b]);
                    }
                    if (j == 0)
                    {
                        if (i != 0)
                        {
                            bool success = false;
                            while (success == false)
                            {
                                result[i, j] = suppSudokuList_2[rnd.Next(0, suppSudokuList_2.Count)];
                                for (int k = 0; k < i; k++)
                                {

                                    if (result[k, j][0] == result[i, j][0] || result[k, j][1] == result[i, j][1] || result[k, j][2] == result[i, j][2])
                                    {
                                        success = false;
                                        break;
                                    }
                                    else
                                    {
                                        success = true;
                                    }
                                }
                                if (success == true)
                                {
                                    suppSudokuList_1.Remove(result[i, j]);
                                    suppSudokuList_2.Remove(result[i, j]);
                                }
                            }
                        }
                        else
                        {
                            result[i, j] = suppSudokuList_1[rnd.Next(0, suppSudokuList_1.Count)];
                            suppSudokuList_1.Remove(result[i, j]);
                            suppSudokuList_2.Remove(result[i, j]);
                        }
                    }
                    //other elements in the row
                    else
                    {
                        bool success = false;

                        while (success == false)
                        {
                            //if no right element found then repeat everything
                            if (suppSudokuList_2.Count == 0)
                            {
                                Console.WriteLine();
                                result = new int[9, 9][];
                                suppSudokuList_1.Clear();
                                suppSudokuList_2.Clear();
                                for (int b = 0; b < suppSudokuList.Count; b++)
                                {
                                    suppSudokuList_1.Add(suppSudokuList[b]);
                                }
                                j = -1;
                                i = 0;
                                break;
                            }
                            //////////////////////////////////////////
                            result[i, j] = suppSudokuList_2[rnd.Next(0, suppSudokuList_2.Count - 1)];
                            /////////////////////////////////////////////
                            //Checking elements for the first row
                            for (int k = 0; k < j; k++)
                            {

                                if (result[i, k][0] == result[i, j][0] || result[i, k][1] == result[i, j][1] || result[i, k][2] == result[i, j][2])
                                {
                                    success = false;
                                    suppSudokuList_2.Remove(result[i, j]);
                                    break;
                                }
                                else
                                {
                                    success = true;
                                }
                            }
                            if (i == 0 && success == true)
                            {
                                suppSudokuList_1.Remove(result[i, j]);
                                suppSudokuList_2.Remove(result[i, j]);
                            }

                            //for rows below the first
                            if (i != 0)
                            {
                                if (success == true)
                                {
                                    for (int k = 0; k < i; k++)
                                    {

                                        if (result[k, j][0] == result[i, j][0] || result[k, j][1] == result[i, j][1] || result[k, j][2] == result[i, j][2])
                                        {
                                            success = false;
                                            suppSudokuList_2.Remove(result[i, j]);
                                            break;
                                        }
                                    }

                                }
                                if (success == true)
                                {
                                    suppSudokuList_1.Remove(result[i, j]);
                                    suppSudokuList_2.Remove(result[i, j]);
                                }
                            }


                        }
                    }

                }

            }

            return result;
        }
    }
}
