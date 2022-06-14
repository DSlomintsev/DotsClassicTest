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
    public class BoardView : MonoBehaviour
    {
        private readonly Vector3 _defaultCellScale = Vector3.one * .7f;
        private const float DestroyAnimCellScale = 1f;
        private const float DestroyAnimCellFade = 0f;
        private const float DestroyAnimDuration = .3f;
        private const float FallAnimDuration = 1f;
        
        private readonly ObjectPool<CellView> _pool = new(createFunc: CreateCellView, actionOnRelease: ActionOnRelease);

        private static void ActionOnRelease(CellView cell)
        {
            cell.gameObject.SetActive(false);
        }

        private static CellView CreateCellView()
        {
            return App.Spawner.Spawn<CellView>(path: PrefabConstants.Cell);
        }

        private List<CellView> _cells = new();

        private CellView GetCell(Vector3 position, Transform parent, Color color, bool isCollider)
        {
            var cell = _pool.Get();
            var cellTransform = cell.transform;
            cellTransform.position = position;
            cellTransform.localScale = _defaultCellScale;
            cellTransform.SetParent(parent);
            cell.Collider.enabled = isCollider;
            cell.Color = color;
            
            cell.gameObject.SetActive(true);
            return cell;
        }

        public void CreateCell(int col, int row, Action action)
        {
            var pos = new Vector3(col, -row);
            var cell = GetCell(pos, transform, Color.white, true);
            _cells.Add(cell);

            if (action != null)
            {
                var interactable = cell.gameObject.AddComponent<Interactable>();
                interactable.Init(action);
            }
        }
        
        private List<Tweener> _squareHighlight = new ();

        public void AddHighlight(Color color)
        {
            var likeColorCells = _cells.FindAll(cell => cell.Color == color);
            likeColorCells.ForEach(cell =>
            {
                var scaleAnim = cell.transform.DOScale(DestroyAnimCellScale, .5f);
                scaleAnim.SetLoops(-1, LoopType.Yoyo);
                scaleAnim.Play();
                _squareHighlight.Add(scaleAnim);
            });
        }

        public void RemoveHighlight()
        {
            _squareHighlight.ForEach(tweener => { tweener.Kill(); });
            _squareHighlight.Clear();
            _cells.ForEach(cell => { cell.transform.localScale = _defaultCellScale; });
        }

        public void DestroyCellAnim(int id, int row, int col)
        {
            var pos = new Vector3(col, -row);
            var cell = GetCell(pos, transform, _cells[id].Color, false);

            var scaleAnim = cell.transform.DOScale(DestroyAnimCellScale, DestroyAnimDuration);
            scaleAnim.Play();
            var fadeAnim = cell.Icon.DOFade(DestroyAnimCellFade, DestroyAnimDuration);
            fadeAnim.onComplete = delegate { _pool.Release(cell); };
            fadeAnim.Play();
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
            var dot = cellView.transform.DOMove(endPos, FallAnimDuration).SetEase(Ease.OutBounce);
            dot.Play();
        }
    }
}