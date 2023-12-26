using Mirror;
using Player.Game;
using Player.Interfaces;
using UnityEngine;

namespace Scripts.Game
{
    public class PlayerHealth : PlayerNetworkAction, IDamageable, IPlayerAction
    {
        [SyncVar(hook = nameof(OnHealthChange))] [SerializeField] private byte _hp;

        private byte HP
        {
            get => _hp;
            set => _hp = value;
        }

        PlayerHealth IDamageable.PlayerHealth => this;
        public PlayerActionType Type => PlayerActionType.Health;

        public void OnHealthChange(byte oldValue, byte newValue)
        {
            Debug.Log($"Old: {oldValue} | New: {newValue}");
        }

        public void Initialize(in IPlayerController playerController)
        {
            if (isServer)
                HP = 100;
        }

        public void Damage(byte damageValue)
        {
            RpcDamage(damageValue);
        }

        [ServerCallback]
        private void RpcDamage(byte damageValue)
        {
            HP -= damageValue;
        }
    }
}