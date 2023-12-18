using System;
using Scripts.Game;
using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(PlayerCustomSettings), menuName = "SO/Player/PlayerCustomSettings")]
    public class PlayerCustomSettings : ScriptableObject
    {
        [SerializeField] private ProjectileData[] _projectilesData;
        [SerializeField] private Color _colorProxyBody;
        
        [SerializeField] private float _runSpeed = 10f;
        [SerializeField] private float _shootForce = 10;
        
        public Color ColorProxyBody => _colorProxyBody;
        public float RunSpeed => _runSpeed;
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