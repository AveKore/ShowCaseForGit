using System;
using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyAttackView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _attackFx;

        private EnemyDamageModel _damageModel;
        private Transform _attackPoint;

        private float _attackCooldown;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];

        public event Action AttackAction;

        public void Init(EnemyDamageModel damageModel, Transform attackPoint)
        {
            _attackPoint = attackPoint;
            _damageModel = damageModel;
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            UpdateCooldown();

            var cooldownIsUp = _attackCooldown <= 0;
            if (cooldownIsUp)
            {
                Attack();
            }
        }

        private void Attack()
        {
            if (!Hit(out var heroView))
            {
                return;
            }

            heroView.GetComponent<HeroHitBoxView>().HeroEntityView.HeroModel.HealthModel
                .TakeDamage(_damageModel.Damage.Value);
            _attackFx.Play();
            _attackCooldown = _damageModel.AttackCooldown.Value;
            AttackAction?.Invoke();
        }

        private bool Hit(out Collider hit)
        {
            var hitCount = Physics.OverlapSphereNonAlloc(_attackPoint.position, _damageModel.AttackRadius.Value, _hits,
                _layerMask);

            hit = hitCount > 0 ? _hits[0] : null;

            return hitCount > 0;
        }

        private void UpdateCooldown()
        {
            if (_attackCooldown > 0)
            {
                _attackCooldown -= Time.deltaTime;
            }
        }
    }
}