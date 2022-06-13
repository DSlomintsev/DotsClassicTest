using UnityEngine;


namespace DotsClassicTest.Cell
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer icon;
        [SerializeField] private CircleCollider2D collider;

        public SpriteRenderer Icon => icon;
        public CircleCollider2D Collider => collider;
        
        public Color Color
        {
            get => icon.color;
            set => icon.color = value;
        }

        
    }
}