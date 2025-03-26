using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroHitBoxView : MonoBehaviour
    {
        [SerializeField] private HeroEntityView _heroEntityView;

        public HeroEntityView HeroEntityView => _heroEntityView;
    }
}