using System;
using Scripts.Interfaces;
using UnityEngine;

namespace Scripts.Game
{
    public class Coin : MonoBehaviour, ICollectable
    {
        public event Action OnCollect;
        
        public void Collect()
        {
            OnCollect?.Invoke();
            OnCollect = null;
            Destroy(gameObject);
        }
    }
}