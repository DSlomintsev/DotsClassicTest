using UnityEngine;


namespace DotsClassicTest.Cell
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer icon;

        public Color Color
        {
            set => icon.color = value;
        }

        public void Init(Color color)
        {
            Color = color;
        }
    }
}