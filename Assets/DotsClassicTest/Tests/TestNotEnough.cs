using System.Collections;
using System.Collections.Generic;
using DotsClassicTest.Board;
using DotsClassicTest.Utils;
using NUnit.Framework;
using UnityEngine.TestTools;

public class TestNotEnough
{
    [Test]
    public void TestNotEnoughSimplePasses()
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
        board.EndSelection();
        
        Assert.AreEqual(0,board.Points);
    }

    [UnityTest]
    public IEnumerator TestNotEnoughWithEnumeratorPasses()
    {
        yield return null;
    }
}
