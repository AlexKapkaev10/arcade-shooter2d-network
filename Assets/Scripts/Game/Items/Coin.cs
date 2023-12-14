using Scripts.Interfaces;
using UnityEngine;

namespace Scripts.Game
{
    public class Coin : MonoBehaviour, ICollectable
    {
        public void Collect()
        {
            Destroy(gameObject);
        }
    }
}