using Philotechnia.enums;
using Philotechnia.interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace _2048
{
    public class Game_2048 : IGame
    {
        public int CurrentScore { get; set; }
        public static int HighScore { get; set; }
        public Cells Cells { get; set; }

        private int gameSquareWidth = 75;
        private int gameSquareHeight = 75;
        private int gameBoardWidth = 4;
        private int gameBoardHeight = 4;
        private bool isAborted;

        private Random rand = new Random(Guid.NewGuid().GetHashCode());

        public Game_2048()
        {
            CurrentScore = 0;
            Cells = new Cells();         
        }

        public void Start()
        {
            isAborted = false;
            CurrentScore = 0;
            Cells = new Cells();
            AddCell();
            AddCell();
        }

        public void End(Process p)
        {
            if(!IsWon())
            {
                using (var g = Graphics.FromHwnd(p.MainWindowHandle))
                {
                    g.DrawString("You Loose!", new Font("Arial", 25), new SolidBrush(Color.Red), 75, 100);
                }
            } 
        }

        public void Draw(Process p)
        {
            DrawBoard(p);
            foreach (var cell in Cells.Collection)
            {
                cell.Display(p);
            }
        }

        public void ProcessInput(ConsoleKeyInfo cki)
        {
            switch (cki.Key)
            {
                case ConsoleKey.LeftArrow:
                    MoveCells(Direction.Left);
                    break;
                case ConsoleKey.RightArrow:
                    MoveCells(Direction.Right);
                    break;
                case ConsoleKey.UpArrow:
                    MoveCells(Direction.Up);
                    break;
                case ConsoleKey.DownArrow:
                    MoveCells(Direction.Down);
                    break;
                case ConsoleKey.Q:
                    isAborted = true;
                    break;
            }
        }

        public bool IsOver()
        {
            if (isAborted)
            {
                return true;
            }

            if (IsFieldFull())
            {
                foreach (var c in Cells.Collection)
                {
                    if (Cells.CanMergeLeft(c, gameSquareWidth) != null)
                    {
                        return false;
                    }
                    if(Cells.CanMergeRight(c, gameSquareWidth)!= null)
                    {
                        return false;
                    }
                    if(Cells.CanMergeUp(c, gameSquareHeight)!= null)
                    {
                        return false;
                    }
                    if(Cells.CanMergeDown(c, gameSquareHeight)!= null)
                    {
                        return false;
                    }
                }
                return true;
            }            
            return false;
        }

        public bool IsWon()
        {
            return Cells.Collection.Any(c => c.Value == 2048);
        }

        private void MoveCells(Direction dir)
        {
            var toRemove = new List<Cell>();
            switch (dir)
            {
                case Direction.Left:
                    Cells.SortForLeft();
                    if (!Cells.CanAdjustLeft(gameSquareWidth, gameBoardWidth, gameBoardHeight))
                    {
                        return;
                    }
                    foreach (var c in Cells.Collection)
                    {
                        while (Cells.CanMoveLeft(c, gameSquareWidth))
                        {
                            c.MoveLeft();
                        }

                        var mergeTo = Cells.CanMergeLeft(c, gameSquareWidth);
                        if (mergeTo != null)
                        {
                            mergeTo.Double();
                            CalculateScore(mergeTo.Value);
                            c.PreDelete();
                            toRemove.Add(c);
                        }
                    }
                    AddCell();
                    break;
                case Direction.Right:
                    Cells.SortForRight();
                    foreach (var c in Cells.Collection)
                    {
                        while (Cells.CanMoveRight(c, gameSquareWidth, gameBoardWidth))
                        {
                            c.MoveRight();
                        }

                        var mergeTo = Cells.CanMergeRight(c, gameSquareWidth);
                        if (mergeTo != null)
                        {
                            mergeTo.Double();
                            CalculateScore(mergeTo.Value);
                            c.PreDelete();
                            toRemove.Add(c);
                        }
                    }
                    AddCell();
                    break;
                case Direction.Up:
                    Cells.SortForUp();
                    foreach (var c in Cells.Collection)
                    {
                        while (Cells.CanMoveUp(c, gameSquareWidth))
                        {
                            c.MoveUp();
                        }

                        var mergeTo = Cells.CanMergeUp(c, gameSquareHeight);
                        if (mergeTo != null)
                        {
                            mergeTo.Double();
                            CalculateScore(mergeTo.Value);
                            c.PreDelete();
                            toRemove.Add(c);
                        }
                    }
                    AddCell();
                    break;
                case Direction.Down:
                    Cells.SortForDown();
                    foreach (var c in Cells.Collection)
                    {
                        while (Cells.CanMoveDown(c, gameSquareWidth, gameBoardWidth))
                        {
                            c.MoveDown();
                        }

                        var mergeTo = Cells.CanMergeDown(c, gameSquareHeight);
                        if (mergeTo != null)
                        {
                            mergeTo.Double();
                            CalculateScore(mergeTo.Value);
                            c.PreDelete();
                            toRemove.Add(c);
                        }
                    }
                    AddCell();
                    break;
                default:
                    return;
            }
            foreach (var c in toRemove)
            {
                Cells.Collection.Remove(c);
            }
        }

        private bool IsFieldFull()
        {
            return Cells.Collection.Count() == gameBoardHeight * gameBoardWidth;
        }

        private void CalculateScore(int value)
        {
            CurrentScore += value;
            if (CurrentScore > HighScore)
                HighScore = CurrentScore;
        }

        private void DrawBoard(Process p)
        {
            Rectangle rect;
            using (var g = Graphics.FromHwnd(p.MainWindowHandle))
            {
                for (int x = 0; x < gameBoardWidth; x++)
                {
                    for (int y = 0; y < gameBoardHeight; y++)
                    {
                        rect = new Rectangle((x * gameSquareWidth), (y * gameSquareHeight), gameSquareWidth, gameSquareHeight);
                        g.DrawRectangle(Pens.DarkGray, rect);
                    }
                }
                g.DrawString("HighScore: " + HighScore.ToString("D6"), new Font("Arial", 25), new SolidBrush(Color.Gray),35, (gameBoardHeight * gameSquareHeight + 10));
                g.DrawString("CurrentScore: " + CurrentScore.ToString("D6"), new Font("Arial", 25), new SolidBrush(Color.Gray),2, (gameBoardHeight * gameSquareHeight + 50));
            }
        }

        private void AddCell()
        {
            if (IsFieldFull())
            {
                return;
            }
                
            Cell cell;
            do
            {
                var x = rand.Next(0, gameBoardWidth);
                var y = rand.Next(0, gameBoardHeight);
                cell = new Cell(rand.NextDouble() < 0.9 ? 2 : 4, (x * gameSquareWidth), (y * gameSquareHeight), gameSquareWidth, gameSquareHeight);
            } while (Cells.Contains(cell));
            
            Cells.Add(cell);
        }
    }
}
