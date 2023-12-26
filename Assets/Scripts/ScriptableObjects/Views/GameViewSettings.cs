using Scripts.Views;
using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(GameViewSettings), menuName = "SO/Views/GameViewSettings")]
    public class GameViewSettings : ScriptableObject
    {
        [SerializeField] private PlayerView _playerViewPrefab;

        public PlayerView PlayerViewPrefab => _playerViewPrefab;
    }
}