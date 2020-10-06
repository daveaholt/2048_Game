using System;
using System.Diagnostics;
using System.Drawing;

namespace _2048
{
    public class Cell : IEquatable<Cell>, ICloneable
    {
        public int Value { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        private int w;
        private int h;

        private Pen borderPen = new Pen(Brushes.DarkGray);

        private Color blue;
        private Color orange;
        private Color green;
        private Color red;
        private Color gray;
        private Color darkGray;
        private Color yellow;
        private Color purple;
        private Color darkOrange;
        private Color darkBlue;
        private Color white;


        public Cell(int value, int x, int y, int width, int height)
        {
            Value = value;
            X = x;
            Y = y;
            w = width;
            h = height;
            

            borderPen.Width = 8.0F;

            blue = ColorTranslator.FromHtml("#5793C8");
            orange = ColorTranslator.FromHtml("#DA9626");
            green = ColorTranslator.FromHtml("#48A224");
            red = ColorTranslator.FromHtml("#DD3A27");
            gray = ColorTranslator.FromHtml("#CCCCCC");
            darkGray = ColorTranslator.FromHtml("#333333");
            yellow = ColorTranslator.FromHtml("#DBDF44");
            purple = ColorTranslator.FromHtml("#C649C6");
            darkOrange = ColorTranslator.FromHtml("#DC4C05");
            darkBlue = ColorTranslator.FromHtml("#0704F7");
            white = ColorTranslator.FromHtml("#FFFFFF");
        }

        public void Double() => Value *= 2;

        public void MoveLeft()
        {
            X -= w;
        }

        public void MoveRight()
        {
            X += w;
        }

        public void MoveUp()
        {
            Y -= h;
        }

        public void MoveDown()
        {
            Y += h;
        }

        public void Display(Process p)
        {
            var rect = new Rectangle(X, Y, w, h);
            using (var g = Graphics.FromHwnd(p.MainWindowHandle))
            {
                switch(Value)
                {
                    case 2:
                        g.FillRectangle(new SolidBrush(gray), rect);
                        g.DrawRectangle(Pens.DarkGray, rect);
                        g.DrawString("2", new Font("Arial", (w / 3)), new SolidBrush(darkGray), X + (w / 3), Y + (h/ 4));
                        return;
                    case 4:
                        g.FillRectangle(new SolidBrush(gray), rect);
                        g.DrawRectangle(Pens.DarkGray, rect);
                        g.DrawString("4", new Font("Arial", (w / 3)), new SolidBrush(darkGray), X + (w / 3), Y + (h / 4));
                        return;
                    case 8:
                        g.FillRectangle(new SolidBrush(orange), rect);
                        g.DrawRectangle(Pens.DarkGray, rect);
                        g.DrawString("8", new Font("Arial", (w / 3)), new SolidBrush(darkGray), X + (w / 3), Y + (h / 4));
                        return;
                    case 16:
                        g.FillRectangle(new SolidBrush(blue), rect);
                        g.DrawRectangle(Pens.DarkGray, rect);
                        g.DrawString("16", new Font("Arial", ((float)(w / 3.5))), new SolidBrush(darkGray), (X + (w / 4)), (float)(Y + (h / 3.5)));
                        return;
                    case 32:
                        g.FillRectangle(new SolidBrush(green), rect);
                        g.DrawRectangle(Pens.DarkGray, rect);
                        g.DrawString("32", new Font("Arial", ((float)(w / 3.5))), new SolidBrush(darkGray), (X + (w / 4)), (float)(Y + (h / 3.5)));
                        return;
                    case 64:
                        g.FillRectangle(new SolidBrush(red), rect);
                        g.DrawRectangle(Pens.DarkGray, rect);
                        g.DrawString("64", new Font("Arial", ((float)(w / 3.5))), new SolidBrush(darkGray), (X + (w / 4)), (float)(Y + (h / 3.5)));
                        return;
                    case 128:
                        g.FillRectangle(new SolidBrush(yellow), rect);
                        g.DrawRectangle(Pens.DarkGray, rect);
                        g.DrawString("128", new Font("Arial", ((float)(w / 4))), new SolidBrush(darkGray), ((float)(X + (w / 4.8))), (float)(Y + (h / 3.2 )));
                        return;
                    case 256:
                        g.FillRectangle(new SolidBrush(purple), rect);
                        g.DrawRectangle(Pens.DarkGray, rect);
                        g.DrawString("256", new Font("Arial", ((float)(w / 4))), new SolidBrush(darkGray), ((float)(X + (w / 4.8))), (float)(Y + (h / 3.2 )));
                        return;
                    case 512:
                        g.FillRectangle(new SolidBrush(darkOrange), rect);
                        g.DrawRectangle(Pens.DarkGray, rect);
                        g.DrawString("512", new Font("Arial", ((float)(w / 4))), new SolidBrush(darkGray), ((float)(X + (w / 4.8))), (float)(Y + (h / 3.2 )));
                        return;
                    case 1024:
                        g.FillRectangle(new SolidBrush(darkBlue), rect);
                        g.DrawRectangle(Pens.DarkGray, rect);
                        g.DrawString("1024", new Font("Arial", ((float)(w / 4.5))), new SolidBrush(white), ((float)(X + (w / 5.5))), (float)(Y + (h / 3 )));
                        return;
                    case 2048:
                        var finalRect = new Rectangle(0, 0, w * 5, h * 5);
                        g.FillRectangle(new SolidBrush(gray), finalRect);
                        g.DrawRectangle(Pens.DarkGray, finalRect);
                        g.DrawString("You Win!", new Font("Arial", ((float)(w / 3))), new SolidBrush(darkGray), ((float)(w * 1.5)), (h * 2));
                        return;
                    default:
                        g.DrawRectangle(Pens.DarkGray, rect);
                        return;
                }
                
            }
        }

        internal void PreDelete()
        {
            X = -99;
            Y = -99;
            Value = 0;
        }

        public object Clone() => MemberwiseClone();

        public override int GetHashCode()
        {
            return Value.GetHashCode() ^ X.GetHashCode() ^ Y.GetHashCode();
        }

        public bool Equals(Cell cell)
        {
            if (cell is null)
            {
                return false;
            }               
            return Value == cell.Value && X == cell.X && Y == cell.Y;
        }

        public override bool Equals(object obj)
        {
            var c = obj as Cell;
            return Equals(c);
        }
    }
}
