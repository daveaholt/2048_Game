using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048
{
    public class Cells
    {
        public Cells()
        {
            Collection = new List<Cell>();
        }

        public List<Cell> Collection { get; private set; }

        public void Add(Cell c)
        {
            Collection.Add(c);
        }

        public void SortForLeft()
        {
            Collection = Collection.OrderBy(y => y.Y).ThenBy(x => x.X).ToList();
        }

        public void SortForRight()
        {
            Collection = Collection.OrderBy(y => y.Y).ThenByDescending(x => x.X).ToList();
        }

        public void SortForUp()
        {
            Collection = Collection.OrderBy(y => y.X).ThenBy(x => x.Y).ToList();
        }

        public void SortForDown()
        {
            Collection = Collection.OrderBy(y => y.X).ThenByDescending(x => x.Y).ToList();
        }

        public bool Contains(Cell c)
        {
            return Collection.Any(x => x.X == c.X && x.Y == c.Y);
        }

        public bool CanAdjustLeft(int cellWidth, int gameBoardWidth, int gameBoardHeight)
        {
            if(!Collection.Any(x=> x.X == 0))
            {
                return true;
            }

            if (Collection.Where(x => x.X == 0).Count() == gameBoardHeight)
            {
                return false;
            }

            return true;
        }

        public bool CanMoveLeft(Cell c, int cellWidth)
        {
            return !Collection.Any(x => x.X == c.X - cellWidth && x.Y == c.Y) && c.X > 0;
        }

        public Cell CanMergeLeft(Cell c, int cellWidth)
        {
            return Collection.FirstOrDefault(x => x.X == (c.X - cellWidth) && x.Y == c.Y && x.Value == c.Value);
        }

        public bool CanMoveRight(Cell c, int cellWidth, int gameBoardWidth)
        {
            var maxX = cellWidth * (gameBoardWidth - 1);
            return !Collection.Any(x => x.X == c.X + cellWidth && x.Y == c.Y) && c.X < maxX;
        }
        public Cell CanMergeRight(Cell c, int cellWidth)
        {
            return Collection.FirstOrDefault(x => x.X == (c.X + cellWidth) && x.Y == c.Y && x.Value == c.Value);
        }

        public bool CanMoveUp(Cell c, int cellHeight)
        {
            return !Collection.Any(x => x.Y == (c.Y - cellHeight) && x.X == c.X) && c.Y > 0;
        }

        public Cell CanMergeUp(Cell c, int cellHeight)
        {
            return Collection.FirstOrDefault(x => x.Y == (c.Y - cellHeight) && x.X == c.X && x.Value == c.Value);
        }

        public bool CanMoveDown(Cell c, int cellHeight, int gameBoardHeight)
        {
            var maxY = cellHeight * (gameBoardHeight - 1);
            return !Collection.Any(x => x.Y == (c.Y + cellHeight) && x.X == c.X) && c.Y < maxY;
        }

        public Cell CanMergeDown(Cell c, int cellHeight)
        {
            return Collection.FirstOrDefault(x => x.Y == (c.Y + cellHeight) && x.X == c.X && x.Value == c.Value);
        }

        internal void Delete(Cell c)
        {
            Collection.Remove(c);
        }
    }
}
