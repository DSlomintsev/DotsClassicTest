using System;
using System.Collections;
using DotsClassicTest.Cell;
using DotsClassicTest.Interactables;
using DotsClassicTest.Utils;
using DotsClassicTest.Utils.Data;
using UnityEngine;


namespace DotsClassicTest.Board
{
    public class BoardPresenter
    {
        public BoardConfig Config { get; set; }
        public Camera Camera { get; set; }
        public BoardModel Model { get; set; }
        public BoardView View { get; set; }
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

        private bool _isSquare;
        private CellData _preLast;
        private ActiveStackData<CellData> _selectedCells = new ();
        public ActiveStackData<CellData> SelectedCells => _selectedCells;

        public void FillBoard()
        {
            var rows = Config.Rows;
            var cols = Config.Cols;

            Model.Cells = new CellData[rows, cols];

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    var rowId = row;
                    var colId = col;
                    
                    View.CreateCell(rowId,colId, ()=>
                    {
                        SelectCell(rowId,colId);
                    });
                }
            }
            
            ReFillCells();
        }

        private CellData CreateCellData(int row, int col)
        {
            return new CellData
            {
                Row = row,
                Col = col,
                Color = Config.Colors.GetRandom()
            };
        }

        private bool IsPossibleToPeekCell(CellData cellData)
        {
            var result = true;

            if (_selectedCells.Count > 0)
            {
                var prevCell = _selectedCells.Peek();
                
                var isAnotherCell = prevCell != cellData;
                var isSameColor = prevCell.Color == cellData.Color;
                var isCellNear = Math.Abs(prevCell.Col - cellData.Col) + Math.Abs(prevCell.Row - cellData.Row) < 2;
                
                result = isAnotherCell && isSameColor && isCellNear;
            }

            return result;
        }

        private void SelectCell(int row, int col)
        {
            var cellData = Model.Cells[row, col];

            if (IsPossibleToPeekCell(cellData))
            {
                if (cellData != _preLast)
                {
                    _selectedCells.Push(cellData);
                }
                else
                {
                    _selectedCells.Pop();
                }

                if (_selectedCells.Count > 1)
                {
                    var tempCell = _selectedCells.Pop();
                    _preLast = _selectedCells.Peek();
                    _selectedCells.Push(tempCell);

                    CheckSquare();
                }
            }
        }
        
        private void CheckSquare()
        {
            var isSquare = IsSquare();
                    
            if (_isSquare != isSquare)
            {
                _isSquare = isSquare;
                if (isSquare)
                {
                    View.Highlight(_selectedCells.Peek().Color.ToColor());
                }
                else
                {
                    View.RemoveHighlight();
                }
                    
            }
        }

        private bool IsSquare()
        {
            return _selectedCells.IsHasDuplicates;
        }

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
                    interactable.DoAction();
                }

                yield return delay;
            }
        }

        private void OnEndSelection()
        {
            App.CoroutineRunner.StopCoroutine(_selectionCor);
            if (IsEnoughtSelectedCells)
            {
                if (_isSquare)
                {
                    BoardUtils.MarkSquaredCellsToDestroy(Model.Cells, _selectedCells.Peek().Color);
                }
                
                foreach (var cell in _selectedCells)
                {
                    cell.State = CellState.DESTROY;
                }
            }
            
            _selectedCells.Clear();
            _preLast = null;
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
                    var cellId = BoardUtils.GetCellId(cell.Row, cell.Col, Config);
                    View.DestroyCellAnim(cellId, cell.Row, cell.Col);
                    Model.Cells[cell.Row, cell.Col] = null;
                }
            }
        }
        
        private void ReFillCells()
        {
            var cells = Model.Cells;
            for (var col = 0; col < Config.Cols; col++)
            {
                for (var row = Config.Rows - 1; row >= 0; row--)
                {
                    var cell = cells[row, col];
                    if (cell == null)
                    {
                        var upperFreeCell = BoardUtils.FindFreeUpperCell(Model.Cells, row - 1, col);

                        var startRow = 0;
                        if (upperFreeCell == null)
                        {
                            startRow -= Config.FallHeight;
                            upperFreeCell = CreateCellData(row, col);
                        }
                        else
                        {
                            startRow = upperFreeCell.Row;
                        }

                        cells[row, col] = upperFreeCell;
                        cells[upperFreeCell.Row, upperFreeCell.Col] = null;

                        upperFreeCell.Row = row;
                        upperFreeCell.Col = col;
                        Model.Cells[row, col] = upperFreeCell;
                        var cellId = BoardUtils.GetCellId(row, col, Config);
                        View.SetCellColor(cellId, upperFreeCell.Color.ToColor());
                        View.FallCellAnim(cellId, startRow, row, col);
                    }
                }
            }
        }
        
        private bool IsEnoughtSelectedCells => _selectedCells.Count >= Config.MinCellRequiredToSelect;
    }
}