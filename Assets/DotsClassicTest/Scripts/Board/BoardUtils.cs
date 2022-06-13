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
        
        public static void MarkSquaredCellsToDestroy(CellData[,] cells, ColorType color)
        {
            for (var i = 0; i < cells.GetLength(0); i++)
            {
                for (var j = 0; j < cells.GetLength(1); j++)
                {
                    var cell = cells[i, j];
                    if (cell.Color == color)
                    {
                        cell.State = CellState.DESTROY;
                    }
                }
            }
        }
        
        public static CellData FindFreeUpperCell(CellData[,] cells, int startRow,int col)
        {
            CellData result = null;

            for (var row = startRow; row >= 0; row--)
            {
                var cell = cells[row, col];
                if (cell!=null)
                {
                    result = cell;
                    break;
                }
            }

            return result;
        }
    }
}