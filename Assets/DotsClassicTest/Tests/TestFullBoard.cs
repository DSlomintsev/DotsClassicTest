using System.Collections.Generic;
using DotsClassicTest.Board;
using DotsClassicTest.Utils;
using NUnit.Framework;


public class TestFullBoard
{
    [Test]
    public void TestFullBoardSimplePasses()
    {
        var board = new BoardPresenter
        {
            Config = new BoardConfig(),
            Model = new BoardModel(),
            View = new BoardViewDummy(),
        };
        board.InitBoard(3,3);
        var colors = new List<ColorType>
        {
            ColorType.RED,ColorType.RED,ColorType.RED,
            ColorType.RED,ColorType.RED,ColorType.RED,
            ColorType.RED,ColorType.RED,ColorType.RED,
        };
        board.ReplenishCellWithColors(colors,0,0,3,3);
        board.SelectCell(0,0);
        board.SelectCell(1,0);
        board.SelectCell(2,0);
        board.SelectCell(2,1);
        board.SelectCell(1,1);
        board.SelectCell(0,1);
        board.SelectCell(0,2);
        board.SelectCell(1,2);
        board.SelectCell(2,2);
        board.EndSelection();
        
        Assert.AreEqual(9,board.Points);
    }
}
