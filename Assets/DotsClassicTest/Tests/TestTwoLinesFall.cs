using System.Collections;
using System.Collections.Generic;
using DotsClassicTest.Board;
using DotsClassicTest.Utils;
using NUnit.Framework;
using UnityEngine.TestTools;

public class TestTwoLinesFall
{
    [Test]
    public void TestTwoLinesFallSimplePasses()
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
            ColorType.BLUE,ColorType.BLUE,ColorType.RED,
            ColorType.RED,ColorType.RED,ColorType.BLUE,
        };
        board.ReplenishCellWithColors(colors,0,0,3,3);
        board.SelectCell(2,0);
        board.SelectCell(2,1);
        board.SelectCell(2,2);
        board.EndSelection();
        board.SelectCell(2,0);
        board.SelectCell(2,1);
        board.SelectCell(2,2);
        board.EndSelection();
        
        Assert.AreEqual(5,board.Points);
    }
}
