using Dissonance;
using Game.Interfaces;
using Mirror;
using Player.Game;
using Player.Interfaces;
using Scripts.Dates;
using Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace Scripts.Game
{
    [RequireComponent(typeof(VoiceBroadcastTrigger))]
    public class GamePhone : PlayerNetworkAction, IPlayerAction, IGamePhone
    {
        [SyncVar] [SerializeField] private int _phoneNumber;

        private VoiceBroadcastTrigger _broadcastTrigger;
        private IGameView _gameView;

        public PlayerActionType Type => PlayerActionType.Phone;

        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            _gameView = resolver.Resolve<IGameView>();
            _gameView.OnSetPlayer += SetPlayerData;
        }

        public void Initialize(in IPlayerController playerController)
        {
            
        }

        private void Start()
        {
            _broadcastTrigger = GetComponent<VoiceBroadcastTrigger>();
            _broadcastTrigger.IsMuted = true;
        }

        private void SetPlayerData(PlayerData data)
        {
            _gameView.OnSetPlayer -= SetPlayerData;
            CmdSetPhoneNumber(data.PhoneNumber);
        }

        [Command]
        private void CmdSetPhoneNumber(int value)
        {
            _phoneNumber = value;
            RpcSetPhoneNumber(value);
        }

        [ClientRpc]
        private void RpcSetPhoneNumber(int phoneNumber)
        {
            _phoneNumber = phoneNumber;
            Debug.Log(phoneNumber);
        }
    }
}