using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DotsClassicTest.Cell;
using DotsClassicTest.Constants;
using DotsClassicTest.Interactables;
using DotsClassicTest.Line;
using DotsClassicTest.Utils;
using UnityEngine;


namespace DotsClassicTest.Board
{
    public class BoardView : MonoBehaviour
    {
        private List<CellView> _cells = new ();

        public void CreateCell(CellData data, Action action)
        {
            var pos = new Vector3(data.Col, -data.Row);
            var cell = App.Spawner.Spawn<CellView>(path: PrefabConstants.Cell, position: pos, parent: transform);
            cell.Init(data.Color.ToColor());
            _cells.Add(cell);

            if (action != null)
            {
                var interactable = cell.gameObject.AddComponent<Interactable>();
                interactable.Init(action);
            }
        }

        public void SetCellColor(int id, Color color)
        {
            _cells[id].Color = color;
        }

        public void FallCellAnim(int id, int startRow, int endRow, int col)
        {
            var startPos = new Vector3(col, -startRow);
            var endPos = new Vector3(col, -endRow);
            var cellView = _cells[id];
            cellView.transform.position = startPos;
            var dot = cellView.transform.DOMove(endPos,1f).SetEase(Ease.OutBounce);
            dot.Play();
        }

        
    }
}