using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game2048
{
    class Field : Panel
    {

        private readonly Dictionary<int, CellColor> cellBackColors;

        private Cell[,] cells;

        private int[,] field;
        public Field()
        {
            BackColor = Color.FromArgb(188, 174, 159);

            field = new int[4, 4];
            field.Initialize();

            cells = new Cell[4,4];
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    int x = j * Cell.SizeValue + (j + 1) * Cell.MarginValue;
                    int y = i * Cell.SizeValue + (i + 1) * Cell.MarginValue;
                    cells[i, j] = new Cell(x, y);
                    Controls.Add(cells[i, j]);
                }
            }

            cellBackColors = new Dictionary<int, CellColor>()
            {
                {0, new CellColor(Color.FromArgb(121, 112, 99), Color.FromArgb(216, 206, 196))},
                {2, new CellColor(Color.FromArgb(121, 112, 99), Color.FromArgb(240, 228, 217))},
                {4, new CellColor(Color.FromArgb(121, 112, 99), Color.FromArgb(238, 225, 199))},
                {8, new CellColor(Color.FromArgb(255, 246, 230), Color.FromArgb(253, 175, 112))},
                {16, new CellColor(Color.FromArgb(255, 246, 230), Color.FromArgb(255, 143, 86))},
                {32, new CellColor(Color.FromArgb(255, 246, 230), Color.FromArgb(255, 112, 80))},
                {64, new CellColor(Color.FromArgb(255, 246, 230), Color.FromArgb(255, 70, 18))},
                {128, new CellColor(Color.FromArgb(255, 246, 230), Color.FromArgb(241, 210, 104))},
                {256, new CellColor(Color.FromArgb(255, 246, 230), Color.FromArgb(241, 208, 86))},
                {512, new CellColor(Color.FromArgb(255, 246, 230), Color.FromArgb(240, 203, 65))},
                {1024, new CellColor(Color.FromArgb(255, 246, 230), Color.FromArgb(242, 201, 39))},
                {2048, new CellColor(Color.FromArgb(255, 246, 230), Color.FromArgb(243, 197, 0))},

            };
        }

       
        public void UpdateUI()
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    cells[i, j].Text = (field[i, j] == 0) ? "" : $"{field[i, j]}";
                    cells[i, j].Style = cellBackColors[field[i, j]];
                }
            }
        }

        
        public void Reset()
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j] = 0;
                }
            }
            UpdateUI();
        }

      
        public void AddRandomItem()
        {
            Random rnd = new Random();
            List<Point> emptyCells = new List<Point>();
            int value = (rnd.Next(1, 10) == 10) ? 4 : 2;

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == 0)
                    {
                        emptyCells.Add(new Point(j, i));

                    }
                }
            }

            Point randomCoord = emptyCells[rnd.Next(emptyCells.Count)];
            field[randomCoord.Y, randomCoord.X] = value;
        }

        public bool ChangeByDirection(EDirection direction, out int score)
        {
            return ChangeStateByDirection(direction, ref field, out  score);
        }

        public bool IsGameOver()
        {
            int[,] fieldClone = (int[,])field.Clone();
            int score;
           
            return !(
                ChangeStateByDirection(EDirection.UP, ref fieldClone, out  score) ||
                ChangeStateByDirection(EDirection.RIGHT, ref fieldClone, out  score) ||
                ChangeStateByDirection(EDirection.DOWN, ref fieldClone, out  score) ||
                ChangeStateByDirection(EDirection.LEFT, ref fieldClone, out  score)
            );
        }
        public bool IsWin()
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == 2048)
                    {
                        
                        return true;
                    }
                }
            }
            return false;
        }

            private bool ChangeStateByDirection(EDirection direction, ref int[,] field, out int score)
        {
            bool isMove = false;
            int last1 = field.GetUpperBound(0);
            int last2 = field.GetUpperBound(1);

            switch (direction)
            {
                case EDirection.UP:
                    isMove = MoveValues(0, last2, 0, last1, true, ref field, out score);
                    break;
                case EDirection.RIGHT:
                    isMove = MoveValues(0, last1, last2, 0, false, ref field, out score);
                    break;
                case EDirection.DOWN:
                    isMove = MoveValues(0, last2, last1, 0, true, ref field, out score);
                    break;
                case EDirection.LEFT:
                    isMove = MoveValues(0, last1, 0, last2, false, ref field, out score);
                    break;
                default:
                    score = 0;
                    break;
            }

            return isMove;
        }

     
        private bool MoveValues(
            int from1,
            int to1,
            int from2,
            int to2,
            bool isVertical,
            ref int[,] field,
            out int score)
        {
            bool isMove = false;
            Stack<int> stack = new Stack<int>();
            score = 0;

            for (int j = from1; (from1 < to1) ? j <= to1 : j >= to1;j = from1 < to1 ? j + 1 : j - 1)
            {
                for (int i = from2, lastValue = -1; (from2 < to2) ? i <= to2 : i >= to2; i = from2 < to2 ? i + 1 : i - 1)
                {
                    int irow = isVertical ? i : j;
                    int icolumn = isVertical ? j : i;

                    int value = field[irow, icolumn];

                    if (value != 0)
                    {
                        bool isSameValues = stack.Count != 0 && stack.Peek() == value && lastValue == value;

                        if (isSameValues)
                        {
                            int next = GetNextValue(stack.Pop());
                            score += next;
                            stack.Push(next);
                        }
                        else
                        {
                            stack.Push(value);
                            lastValue = value;
                        }

                    }
                }

               
                stack = new Stack<int>(stack);

                for (
                    int i = from2; (from2 < to2) ? i <= to2 : i >= to2; i = (from2 < to2) ? i + 1 : i - 1)
                {
                    int irow = isVertical ? i : j;
                    int icolumn = isVertical ? j : i;

                    if (stack.Count != 0 && stack.Peek() != field[irow, icolumn])
                    {
                        isMove = true;
                    }

                    field[irow, icolumn] = (stack.Count != 0) ? stack.Pop() : 0;
                }
            }

            return isMove;
        }

        
        private int GetNextValue(int value)
        {
            int i;
            for (i = -1; value != 0; i++)
            {
                value >>= 1;
            }
            int log2 = (i == -1) ? 0 : i;

            return (int)Math.Pow(2, log2 + 1);
        }
    }
}

