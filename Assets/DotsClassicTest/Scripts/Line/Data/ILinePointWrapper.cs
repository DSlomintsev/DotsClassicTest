using System;
using UnityEngine;

namespace DotsClassicTest.Line.Data
{
    public interface ILinePointWrapper
    {
        public event Action<Color> ColorEvent;
        public event Action<Vector3> AddEvent;
        public event Action<Vector3> RemoveEvent;
    }
}