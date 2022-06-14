using DotsClassicTest.Cell;
using DotsClassicTest.Utils;

namespace DotsClassicTest.Board
{
    public class BoardUtils
    {
        public static int GetCellId(int row, int col, BoardConfig config)
        {
            return row * config.Rows + col;
        }

        public static void MarkSquaredCellsToDestroy(bool isSquare, CellData[,] cells, ColorType color)
        {
            if (!isSquare) return;

            foreach (var cell in cells)
            {
                if (cell.Color != color) continue;

                cell.State = CellState.DESTROY;
            }
        }

        public static bool FreeUpperCell(CellData[,] cells, int startRow, int col, out CellData freeCell)
        {
            freeCell = null;

            for (var row = startRow - 1; row >= 0; row--)
            {
                var cell = cells[row, col];
                if (cell != null)
                {
                    freeCell = cell;
                    break;
                }
            }

            return freeCell != null;
        }
        
        public static void SwitchCells(CellData[,] cells, int rowA,int colA,int rowB,int colB)
        {
            (cells[rowA,colA], cells[rowB, colB]) = (cells[rowB, colB], cells[rowA,colA]);
        }
    }
}