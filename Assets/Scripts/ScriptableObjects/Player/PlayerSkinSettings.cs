using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(PlayerSkinSettings), menuName = "SO/Player/PlayerSkinSettings")]
    public class PlayerSkinSettings : ScriptableObject
    {
        [SerializeField] private Color _colorProxy;

        public Color ColorProxy => _colorProxy;
    }
}