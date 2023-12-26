using System;
using Scripts.Game;
using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(PlayerGunSettings), menuName = "SO/Player/PlayerGunSettings")]
    public class PlayerGunSettings : ScriptableObject
    {
        [SerializeField] private ProjectileData[] _projectilesData;
        [SerializeField] private float _shootForce = 10;
        
        public float ShootForce => _shootForce;

        public Projectile GetProjectileByType(ProjectileType type)
        {
            foreach (var data in _projectilesData)
            {
                if (data.Type == type)
                    return data.Prefab;
            }

            return null;
        }
    }
    
    [Serializable]
    public struct ProjectileData
    {
        public ProjectileType Type;
        public Projectile Prefab;
    }

    public enum ProjectileType
    {
        Boomerang,
        Bullet
    }
}