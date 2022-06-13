using DotsClassicTest.Utils;

namespace DotsClassicTest.Board
{
    public class BoardConfig
    {
        public int MinCellRequiredToSelect = 2;
        public int Rows = 10;
        public int Cols = 10;
        public int FallHeight = 10;
        
        public ColorType[] Colors = { ColorType.RED, ColorType.GREEN, ColorType.BLUE,ColorType.PURPLE,ColorType.YELLOW };
    }
}