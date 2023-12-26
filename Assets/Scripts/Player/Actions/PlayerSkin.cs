using Player.Game;
using Player.Interfaces;
using Scripts.ScriptableObjects;
using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Animator))]
    public class PlayerSkin : MonoBehaviour, IPlayerAction
    {
        [SerializeField] private PlayerSkinSettings _settings;
        
        private SpriteRenderer _spriteRenderer;

        public PlayerActionType Type => PlayerActionType.Skin;
        public void Initialize(in IPlayerController playerController)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = playerController.IsLocalPlayer ? Color.white : _settings.ColorProxy;
            if (playerController.IsLocalPlayer)
                SetVisible(false);
        }

        public void SetVisible(bool value)
        {
            var color = _spriteRenderer.color;
            color.a = value ? 1f : 0f;
            _spriteRenderer.color = color;
        }
    }
}