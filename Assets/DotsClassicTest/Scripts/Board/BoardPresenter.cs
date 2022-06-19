using System;
using System.Collections;
using System.Collections.Generic;
using DotsClassicTest.Cell;
using DotsClassicTest.Interactables;
using DotsClassicTest.Utils;
using UnityEngine;


namespace DotsClassicTest.Board
{
    public class BoardPresenter
    {
        public BoardConfig Config { get; set; }
        public Camera Camera { get; set; }
        public BoardModel Model { get; set; }
        public IBoardView View { get; set; }
        private BoardInput _input;

        public BoardInput Input
        {
            set
            {
                if (_input != null)
                {
                    _input.StartSelection -= OnStartSelection;
                    _input.EndSelection -= OnEndSelection;
                    _input.Replenish -= OnReplenish;
                }

                _input = value;

                if (_input != null)
                {
                    _input.StartSelection += OnStartSelection;
                    _input.EndSelection += OnEndSelection;
                    _input.Replenish += OnReplenish;
                }
            }
        }

        public void InitBoard(int rows, int cols)
        {
            FillView(rows,cols);
                
            Model.Cells = new CellData[rows, cols];
            
            ReFillCells();
        }
        
        private void FillView(int rows, int cols)
        {
            View.Clear();
            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    var rowId = row;
                    var colId = col;

                    View.CreateCell(rowId, colId, () => { SelectCell(rowId, colId); });
                }
            }
        }
        

        public void SelectCell(int row, int col)
        {
            var cellData = Model.Cells[row, col];

            if (IsPossibleToPeekCell(cellData))
            {
                if (cellData != Model.SelectedCells.PreLast)
                {
                    Model.SelectedCells.Push(cellData);
                }
                else
                {
                    Model.SelectedCells.Pop();
                }

                if (Model.SelectedCells.Count > 1)
                {
                    var tempCell = Model.SelectedCells.Pop();
                    Model.SelectedCells.PreLast = Model.SelectedCells.Peek();
                    Model.SelectedCells.Push(tempCell);

                    CheckSquare();
                }
            }
        }
        
        private bool IsPossibleToPeekCell(CellData cellData)
        {
            var result = true;

            if (Model.SelectedCells.Count > 0)
            {
                var prevCell = Model.SelectedCells.Peek();

                var isAnotherCell = prevCell != cellData;
                var isSameColor = prevCell.Color == cellData.Color;
                var isCellNear = Math.Abs(prevCell.Col - cellData.Col) + Math.Abs(prevCell.Row - cellData.Row) < 2;

                result = isAnotherCell && isSameColor && isCellNear;
            }

            return result;
        }

        private void CheckSquare()
        {
            var isSquare = IsSquare;

            if (Model.IsSquare == isSquare) return;

            Model.IsSquare = isSquare;

            if (isSquare)
            {
                View.AddHighlight(Model.SelectedCells.Peek().Color.ToColor());
            }
            else
            {
                View.RemoveHighlight();
            }
        }

        private bool IsSquare => Model.SelectedCells.IsHasDuplicates;

        private void OnReplenish()
        {
            OnEndSelection();
            foreach (var cellData in Model.Cells)
            {
                cellData.State = CellState.DESTROY;
            }

            DestroyCells();
            ReFillCells();
        }

        #region Selection begavior

        private Coroutine _selectionCor;

        private void OnStartSelection()
        {
            _selectionCor = App.CoroutineRunner.StartCoroutine(Selection());
        }

        private IEnumerator Selection()
        {
            Collider2D prevCollider = null;
            var delay = new WaitForEndOfFrame();
            while (true)
            {
                var ray = Camera.ScreenPointToRay(_input.MousePosition);
                var hit = Physics2D.Raycast(ray.origin, ray.direction, float.MaxValue, LayerUtils.InteractLayer);

                if (hit.collider != null && prevCollider != hit.collider)
                {
                    prevCollider = hit.collider;

                    var interactable = hit.collider.GetComponent(typeof(IInteractable)) as IInteractable;
                    interactable?.DoAction();
                }

                yield return delay;
            }
        }

        private void OnEndSelection()
        {
            App.CoroutineRunner.StopCoroutine(_selectionCor);
            EndSelection();
        }
        
        public void EndSelection()
        {
            if (IsEnoughSelectedCells)
            {
                BoardUtils.MarkSquaredCellsToDestroy(Model.IsSquare, Model.Cells,Model.SelectedCells, Model.SelectedCells.Peek().Color);

                foreach (var cell in Model.SelectedCells)
                {
                    cell.State = CellState.DESTROY;
                    Model.Points++;
                }
            }

            Model.SelectedCells.Clear();
            CheckSquare();

            DestroyCells();
            ReFillCells();
        }

        #endregion

        private void DestroyCells()
        {
            var cells = Model.Cells;

            foreach (var cell in cells)
            {
                if (cell.State == CellState.DESTROY)
                {
                    var cellId = BoardUtils.GetCellId(cell.Row, cell.Col, cells.GetRows());
                    View.DestroyCellAnim(cellId, cell.Row, cell.Col);
                    Model.Cells[cell.Row, cell.Col] = null;
                }
            }
        }

        private void ReFillCells()
        {
            var cells = Model.Cells;
            var rows = cells.GetRows();
            var cols = cells.GetCols();
            for (var col = 0; col < cols; col++)
            {
                for (var row = rows - 1; row >= 0; row--)
                {
                    var cell = cells[row, col];
                    if (cell == null)
                    {
                        var startRow = 0;
                        if (BoardUtils.FreeUpperCell(cells, row, col, out var freeUpperCell))
                        {
                            startRow = freeUpperCell.Row;
                            BoardUtils.SwitchCells(cells, row, col, freeUpperCell.Row, freeUpperCell.Col);
                        }
                        else
                        {
                            startRow -= Config.FallHeight;
                            freeUpperCell = new CellData { Color = Config.Colors.GetRandom() };
                            cells[row, col] = freeUpperCell;
                        }

                        freeUpperCell.Row = row;
                        freeUpperCell.Col = col;

                        var cellId = BoardUtils.GetCellId(row, col, cells.GetRows());
                        View.SetCellColor(cellId, freeUpperCell.Color.ToColor());
                        View.FallCellAnim(cellId, startRow, row, col);
                    }
                }
            }
        }

        public void ReplenishCellWithColors(List<ColorType> colors,int rowShift,int colShift, int rows,int cols)
        {
            var cells = Model.Cells;
            var index = 0;
            for (var i = rowShift; i < rows; i++)
            {
                for (var j = colShift; j < cols; j++)
                {
                    var cell = cells[i,j];
                    cell.Color = colors[index];
                    var cellId = BoardUtils.GetCellId(cell.Row, cell.Col, cells.GetRows());
                    View.SetCellColor(cellId, cell.Color.ToColor());
                
                    index++;    
                }
            }
        }

        private bool IsEnoughSelectedCells => Model.SelectedCells.Count >= Config.MinCellRequiredToSelect;
    }
}