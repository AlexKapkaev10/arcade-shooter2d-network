using TMPro;
using UnityEngine;

namespace Scripts.Views
{
    public class PlayerView : View, IPlayerView
    {
        [SerializeField] private TMP_Text _textName;
        [SerializeField] private CanvasGroup _canvasGroupVoice;
        [SerializeField] private Vector3 _offSetPosition;

        private Transform _transform;

        public GameObject GameObject => gameObject;

        public void SetVisibleVoice(bool value)
        {
            _canvasGroupVoice.alpha = value ? 1f : 0f;
        }
        
        public void SetName(string value)
        {
            _textName.SetText(value);
        }

        public void SetPosition(Vector3 value)
        {
            _transform.position = value + _offSetPosition;
        }

        private void Awake()
        {
            _transform = transform;
        }
    }
}