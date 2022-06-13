using UnityEngine;


namespace DotsClassicTest.Line
{
    public class LinePresenter
    {
        public Camera Camera;
        private LineModel _model;

        public LineModel Model
        {
            get => _model;
            set
            {
                if (_model != null && _model.PointData != null)
                {
                    _model.PointData.ColorEvent -= OnColorEvent;
                    _model.PointData.AddEvent -= OnPointAdd;
                    _model.PointData.RemoveEvent -= OnPointRemove;
                }

                _model = value;

                if (_model != null && _model.PointData != null)
                {
                    _model.PointData.ColorEvent += OnColorEvent;
                    _model.PointData.AddEvent += OnPointAdd;
                    _model.PointData.RemoveEvent += OnPointRemove;
                }
            }
        }

        public LineView View;

        private LineInput _input;

        public LineInput Input
        {
            set
            {
                if (_input != null)
                {
                    _input.PointerPosition.UpdateEvent -= OnPointerPositionUpdate;
                }

                _input = value;

                if (_input != null)
                {
                    _input.PointerPosition.UpdateEvent += OnPointerPositionUpdate;
                }
            }
        }

        private void OnColorEvent(Color color)
        {
            View.SetColor(color);
        }

        private void OnPointerPositionUpdate(Vector2 position)
        {
            position = Camera.ScreenToWorldPoint(new Vector3(position.x, position.y, Camera.nearClipPlane));
            View.SetPointerPos(position);
        }

        private void OnPointAdd(Vector3 point)
        {
            View.AddPoint(point);
            OnPointerPositionUpdate(_input.PointerPosition.Value);
        }

        private void OnPointRemove(Vector3 point)
        {
            View.RemoveLastPoint();
            OnPointerPositionUpdate(_input.PointerPosition.Value);
        }
    }
}