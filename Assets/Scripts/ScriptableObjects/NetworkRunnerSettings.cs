using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(NetworkRunnerSettings), menuName = "SO/CustomNetwork/NetworkRunnerSettings")]
    public class NetworkRunnerSettings : ScriptableObject
    {
        [Header("Network Address Settings")]
        [SerializeField] private bool _isUseIp;
        [SerializeField] private string _ip;
        private const string _localHost = "localhost";

        public string GetNetworkAddress()
        {
            return _isUseIp ? _ip : _localHost;
        }
    }
}