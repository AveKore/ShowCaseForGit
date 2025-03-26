using CodeBase.Configs;
using CodeBase.Enemy;
using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Core.Factory
{
    public class PrefabFactory : BaseFactory, IPrefabFactory
    {
        private const string HERO_PREFAB_PATH = "Prefabs/Hero";
        private const string INITIAL_POINT_TAG = "InitialPoint";

        public HeroEntityView LoadPlayer()
        {
            var initialPoint = GameObject.FindGameObjectWithTag(INITIAL_POINT_TAG);
            var player = InstantiateWithInjection<HeroEntityView>(HERO_PREFAB_PATH, initialPoint.transform.position,
                initialPoint.transform.root);
            return player;
        }

        public EnemyEntityView CreateEnemy(EnemyConfig enemyConfig, Transform enemySpawner)
        {
            var enemy = InstantiateWithInjection<EnemyEntityView>(enemyConfig.Prefab, enemySpawner.transform.position,
                enemySpawner.transform.root);
            return enemy;
        }
    }
}