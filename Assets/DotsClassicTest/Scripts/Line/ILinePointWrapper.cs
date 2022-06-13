using System;
using DotsClassicTest.Cell;
using DotsClassicTest.Utils.Data;
using UnityEngine;

namespace DotsClassicTest.Line
{
    public interface ILinePointWrapper
    {
        public event Action<Vector3> AddEvent;
        public event Action<Vector3> RemoveEvent;
    }
}