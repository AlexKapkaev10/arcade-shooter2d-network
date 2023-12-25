using Dissonance;
using Game.Interfaces;
using Mirror;
using Scripts.Dates;
using Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace Scripts.Game
{
    [RequireComponent(typeof(VoiceBroadcastTrigger))]
    public class GamePhone : NetworkBehaviour, IGamePhone
    {
        [SyncVar] [SerializeField] private int _phoneNumber;

        private VoiceBroadcastTrigger _broadcastTrigger;
        private IGameView _gameView;
        
        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            _gameView = resolver.Resolve<IGameView>();
            _gameView.OnSetPlayer += SetPlayerData;
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