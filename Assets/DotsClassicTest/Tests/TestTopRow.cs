using System.Collections;
using System.Collections.Generic;
using DotsClassicTest.Board;
using DotsClassicTest.Utils;
using NUnit.Framework;
using UnityEngine.TestTools;

public class TestTopRow
{
    [Test]
    public void TestTopRowSimplePasses()
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
            ColorType.RED,ColorType.RED,ColorType.GREEN,
            ColorType.RED,ColorType.RED,ColorType.GREEN,
            ColorType.BLUE,ColorType.BLUE,ColorType.RED,
        };
        board.ReplenishCellWithColors(colors,0,0,3,3);
        board.SelectCell(0,0);
        board.SelectCell(0,1);
        board.EndSelection();
        
        Assert.AreEqual(2,board.Points);
    }
}
