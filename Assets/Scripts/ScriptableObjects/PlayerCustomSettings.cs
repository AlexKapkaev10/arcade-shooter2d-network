using System;
using Scripts.Game;
using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(PlayerCustomSettings), menuName = "SO/Player/PlayerCustomSettings")]
    public class PlayerCustomSettings : ScriptableObject
    {
        [SerializeField] private ProjectileData[] _projectilesData;
        [SerializeField] private float _runSpeed = 10f;

        public float RunSpeed => _runSpeed;

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