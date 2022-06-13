using UnityEngine;


namespace DotsClassicTest.Line
{
    public class LineView:MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;

        public void SetColor(Color color)
        {
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
        }

        public void AddPoint(Vector2 pos)
        {
            var isFirstPoint = lineRenderer.positionCount == 0;
            lineRenderer.positionCount = isFirstPoint ? 2 : lineRenderer.positionCount + 1;
            lineRenderer.SetPosition(lineRenderer.positionCount - 2, new Vector3(pos.x, pos.y));
        }

        public void RemoveLastPoint()
        {
            lineRenderer.positionCount = lineRenderer.positionCount == 1 ? 0 : lineRenderer.positionCount - 1;
        }

        public void SetPointerPos(Vector2 pointerPos)
        {
            lineRenderer.SetPosition(lineRenderer.positionCount-1,pointerPos);
        }
    }
}