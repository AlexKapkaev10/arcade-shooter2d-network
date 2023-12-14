using UnityEngine;

namespace Scripts.Game
{
    public class HealthSystem : MonoBehaviour, IDamageable
    {
        public void Damage()
        {
            Debug.Log("Damage");
        }
    }
}