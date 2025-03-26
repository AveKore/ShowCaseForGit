using System;
using System.Collections.Generic;
using CodeBase.Hero;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Enemy
{
    public class EnemyEntityView : MonoBehaviour, IDisposable
    {
#if UNITY_EDITOR
        public Transform AttackPoint => _attackPoint;
        public float AttackRadius => 2f;
#endif

        [SerializeField] private EnemyNavigationView enemyNavigationView;
        [SerializeField] private EnemyAttackView _enemyAttackView;
        [SerializeField] private EntityHealthView _enemyHealthView;
        [SerializeField] private EnemyDeathView _enemyDeathView;
        [SerializeField] private Transform _attackPoint;

        public EnemyModel EnemyModel { get; private set; }

        private readonly List<IDisposable> _disposables = new();

        public void Init(EnemyModel enemyModel, Transform followTarget, HeroHealthModel playerModelHealthModel)
        {
            playerModelHealthModel.IsDead.Subscribe(OnPlayerKilled).AddTo(_disposables);
            EnemyModel = enemyModel;
            enemyNavigationView.SetFollowTarget(followTarget);
            _enemyAttackView.Init(enemyModel.DamageModel, _attackPoint);
            _enemyDeathView.Init(enemyModel.HealthModel);
            _enemyHealthView.Init(enemyModel.HealthModel);
            enemyNavigationView.Init(enemyModel.SpeedModel.Speed.Value,
                Vector3.Distance(transform.position, _attackPoint.position), _enemyAttackView, enemyModel.HealthModel);
        }

        private void OnPlayerKilled(bool isDead)
        {
            if (isDead)
            {
                enemyNavigationView.SetFollowTarget(null);
            }
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }

            _disposables.Clear();
        }
    }
}