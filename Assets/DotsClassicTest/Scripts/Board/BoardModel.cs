using DotsClassicTest.Cell;
using DotsClassicTest.Utils.Data;

namespace DotsClassicTest.Board
{
    public class BoardModel
    {
        public CellData[,] Cells { get; set; }
        public int Points { get; set; }
        public bool IsSquare { get; set; }
        public ActiveStackData<CellData> SelectedCells { get; set; } = new();
    }
}