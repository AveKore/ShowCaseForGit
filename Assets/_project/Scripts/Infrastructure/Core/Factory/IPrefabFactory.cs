using CodeBase.Configs;
using CodeBase.Enemy;
using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Core.Factory
{
    public interface IPrefabFactory
    {
        public HeroEntityView LoadPlayer();
        public EnemyEntityView CreateEnemy(EnemyConfig enemyConfig, Transform enemySpawner);
    }
}