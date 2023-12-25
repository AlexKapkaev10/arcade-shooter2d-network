using UnityEngine;

namespace Scripts.Game
{
    public class Body : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetVisible(bool value)
        {
            var color = _spriteRenderer.color;
            color.a = value ? 1f : 0f;
            _spriteRenderer.color = color;
        }
        
        public void Initialize(Color color)
        {
            _spriteRenderer.color = color;
        }
    }
}