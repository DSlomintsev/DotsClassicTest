using System;
using UnityEngine;


namespace DotsClassicTest.Board
{
    public class BoardViewDummy: IBoardView
    {
        public void CreateCell(int col, int row, Action action)
        {
        }

        public void SetCellColor(int id, Color color)
        {
        }

        public void AddHighlight(Color color)
        {
        }

        public void RemoveHighlight()
        {
        }

        public void DestroyCellAnim(int id, int row, int col)
        {
        }

        public void FallCellAnim(int id, int startRow, int endRow, int col)
        {
        }
        
        public void Clear()
        {
        }
    }
}