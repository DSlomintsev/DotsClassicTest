using System;
using DotsClassicTest.Cell;
using DotsClassicTest.Line.Data;
using DotsClassicTest.Utils;
using DotsClassicTest.Utils.Data;
using UnityEngine;

namespace DotsClassicTest.Line
{
    public class CellLinePointWrapper : ILinePointWrapper, IDisposable
    {
        public event Action<Color> ColorEvent;
        public event Action<Vector3> AddEvent;
        public event Action<Vector3> RemoveEvent;
        
        private ActiveStackData<CellData> _cells;

        public CellLinePointWrapper(ActiveStackData<CellData> cells)
        {
            _cells = cells;

            _cells.AddEvent += OnAddCell;
            _cells.RemoveEvent += OnRemoveCell;
        }

        public void Dispose()
        {
            _cells.AddEvent -= OnAddCell;
            _cells.RemoveEvent -= OnRemoveCell;
            _cells = null;
        }

        private void OnAddCell(CellData data)
        {
            if (_cells.Count == 1)
            {
                ColorEvent.Call(data.Color.ToColor());
            }
            AddEvent.Call(new Vector3(data.Col, -data.Row));
        }

        private void OnRemoveCell(CellData data)
        {
            RemoveEvent.Call(new Vector3(data.Col, -data.Row));
        }
    }
}