using UnityEngine;

namespace Scripts.Views
{
    public interface IPlayerView
    {
        public void SetName(string value);
        public void SetPosition(Vector3 value);
        public GameObject GameObject { get; }
    }
}