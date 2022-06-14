using System.Collections;
using System.Collections.Generic;
using DotsClassicTest.Board;
using DotsClassicTest.Utils;
using NUnit.Framework;
using UnityEngine.TestTools;

public class Test3InRow
{
    [Test]
    public void Test3InRowSimplePasses()
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
            ColorType.GREEN,ColorType.GREEN,ColorType.GREEN,
            ColorType.BLUE,ColorType.BLUE,ColorType.BLUE,
        };
        board.ReplenishCellWithColors(colors,0,0,3,3);
        board.SelectCell(0,0);
        board.SelectCell(0,1);
        board.SelectCell(0,2);
        board.EndSelection();
        
        Assert.AreEqual(3,board.Points);
    }

    [UnityTest]
    public IEnumerator Test3InRowWithEnumeratorPasses()
    {
        yield return null;
    }
}
