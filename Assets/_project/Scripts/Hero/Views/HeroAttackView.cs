using CodeBase.Enemy;
using CodeBase.Servises.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroAttackView : MonoBehaviour
    {

        [SerializeField] private ParticleSystem _attackFx;

        private Collider[] _hits;       
        private HeroModel _heroModel;
        private Transform _attackPoint;
        private float _attackCooldown;
        private int _layerMask;
        private bool _canAttack;

        [Inject] private IInputService InputService { get; set; }

        public void Init(HeroModel heroModel, Transform attackPoint)
        {
            _attackPoint = attackPoint;
            _heroModel = heroModel;
            _layerMask = 1 << LayerMask.NameToLayer("Enemy");
        }

        private void Update()
        {
            UpdateCooldown();

            TryAttackWithCooldown();
        }

        private void TryAttackWithCooldown()
        {
            var cooldownIsUp = _attackCooldown <= 0;
            if (cooldownIsUp && !_canAttack)
            {
                _canAttack = true;
            }

            if (_canAttack && InputService.IsAttackButtonUp())
            {
                Attack();
            }
        }

        private void Attack()
        {
            _canAttack = false;
            _attackFx.Play();
            _attackCooldown = _heroModel.DamageModel.AttackCooldown.Value;
            if (Hit(out var enemyViews))
            {
                foreach (var enemyView in enemyViews)
                {
                    HitEnemy(enemyView);
                }
            }
        }

        private void HitEnemy(Collider enemyView)
        {
            var enemyModel = enemyView.GetComponent<EnemyHitBoxView>()?.EnemyEntityView.EnemyModel;
            if (enemyModel?.HealthModel != null &&
                enemyModel.HealthModel.TakeDamage(_heroModel.DamageModel.Damage.Value))
            {
                _heroModel.SkillPointsModel.AddSkillPoints(enemyModel.Reward);
            }
        }

        private bool Hit(out Collider[] hits)
        {
            hits = Physics.OverlapSphere(_attackPoint.position,
                _heroModel.DamageModel.AttackRadius.Value, _layerMask);
            return hits.Length > 0;
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