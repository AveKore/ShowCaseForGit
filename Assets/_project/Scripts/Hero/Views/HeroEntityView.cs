using CodeBase.Enemy;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Hero
{
    public class HeroEntityView : MonoBehaviour
    {
        [FormerlySerializedAs("_heroMove")] [SerializeField]
        private HeroMoveView heroMoveView;

        [SerializeField] private HeroAttackView _heroAttackView;
        [SerializeField] private HeroDeathView _heroDeathView;
        [SerializeField] private EntityHealthView _heroHealthView;

        [SerializeField] private Transform _attackPoint;

        public HeroModel HeroModel { get; private set; }

        public void Init(HeroModel heroModel)
        {
            HeroModel = heroModel;
            heroMoveView.Init(HeroModel.SpeedModel, HeroModel.HealthModel);
            _heroDeathView.Init(HeroModel.HealthModel);
            _heroHealthView.Init(HeroModel.HealthModel);
            _heroAttackView.Init(HeroModel, _attackPoint);
        }

        public void StopHero()
        {
            heroMoveView.enabled = false;
            _heroAttackView.enabled = false;
        }
    }
}