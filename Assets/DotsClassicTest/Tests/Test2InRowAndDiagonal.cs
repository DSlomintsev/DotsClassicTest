using System.Collections;
using System.Collections.Generic;
using DotsClassicTest.Board;
using DotsClassicTest.Utils;
using NUnit.Framework;
using UnityEngine.TestTools;

public class Test2InRowAndDiagonal
{
    [Test]
    public void Test2InRowAndDiagonalSimplePasses()
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
            ColorType.GREEN,ColorType.GREEN,ColorType.RED,
            ColorType.BLUE,ColorType.BLUE,ColorType.BLUE,
        };
        board.ReplenishCellWithColors(colors,0,0,3,3);
        board.SelectCell(0,0);
        board.SelectCell(0,1);
        board.SelectCell(1,2);
        board.EndSelection();
        
        Assert.AreEqual(2,board.Model.Points);
    }

    [UnityTest]
    public IEnumerator Test2InRowAndDiagonalWithEnumeratorPasses()
    {
        yield return null;
    }
}
