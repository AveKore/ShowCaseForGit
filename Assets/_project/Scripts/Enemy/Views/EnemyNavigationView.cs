using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class EnemyNavigationView : MonoBehaviour, IDisposable
    {
        private const float ATTACK_STOP_DURATION = 1f;

        [SerializeField] private NavMeshAgent _navMeshAgent;
        private Transform _followTarget;
        private EnemyAttackView _enemyAttackView;
        private readonly List<IDisposable> _disposables = new();

        public void Init(float speed, float attackDistance, EnemyAttackView enemyAttackView,
            EnemyHealthModel enemyHealthModel)
        {
            enemyHealthModel.IsDead.Subscribe(StopNavigation).AddTo(_disposables);
            _navMeshAgent.stoppingDistance = attackDistance;
            _navMeshAgent.speed = speed;
            _enemyAttackView = enemyAttackView;
            _enemyAttackView.AttackAction += StopNavWhileAttack;
        }

        private void StopNavigation(bool isDead)
        {
            if (!isDead)
            {
                return;
            }

            _navMeshAgent.enabled = false;
        }

        private  void StopNavWhileAttack()
        {
            _navMeshAgent.enabled = false;
            try
            {
                UniTask.WaitForSeconds(ATTACK_STOP_DURATION).AttachExternalCancellation(destroyCancellationToken)
                    .ContinueWith(StopAgent).Forget();

            }
            catch (Exception _)
            {
                // ignored
            }
        }

        private bool StopAgent()
        {
            return _navMeshAgent.enabled = true;
        }


        public void SetFollowTarget(Transform followTarget)
        {
            _navMeshAgent.enabled = followTarget != null;
            _followTarget = followTarget;
        }

        private void LateUpdate()
        {
            if (_followTarget == null|| !_navMeshAgent.enabled || _navMeshAgent.isStopped )
            {
                return;
            }

            _navMeshAgent.destination = _followTarget.position;
        }

        private void OnDestroy()
        {
            _enemyAttackView.AttackAction -= StopNavWhileAttack;
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