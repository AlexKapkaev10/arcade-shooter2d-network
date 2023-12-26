using Scripts.Views;
using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(PlayerCustomSettings), menuName = "SO/Player/PlayerCustomSettings")]
    public class PlayerCustomSettings : ScriptableObject
    {
        [SerializeField] private PlayerView _playerViewPrefab;
        [SerializeField] private Color _colorProxyBody;
        [SerializeField] private float _runSpeed = 10f;

        public PlayerView PlayerViewPrefab => _playerViewPrefab;
        public Color ColorProxyBody => _colorProxyBody;
        public float RunSpeed => _runSpeed;
    }
}