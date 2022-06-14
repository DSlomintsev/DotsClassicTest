using System;
using System.Collections.Generic;
using DG.Tweening;
using DotsClassicTest.Cell;
using DotsClassicTest.Constants;
using DotsClassicTest.Interactables;
using UnityEngine;
using UnityEngine.Pool;


namespace DotsClassicTest.Board
{
    public interface IBoardView
    {
        public void CreateCell(int col, int row, Action action);
        public void SetCellColor(int id, Color color);
        public void AddHighlight(Color color);
        public void RemoveHighlight();
        public void DestroyCellAnim(int id, int row, int col);
        public void FallCellAnim(int id, int startRow, int endRow, int col);
    }
}