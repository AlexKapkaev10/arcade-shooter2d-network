using UnityEngine;

namespace Scripts.Game
{
    public class Body : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        
        public void Initialize(Color color)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = color;
        }
    }
}