using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyHitBoxView : MonoBehaviour
    {
        [SerializeField] private EnemyEntityView _enemyEntityView;

        public EnemyEntityView EnemyEntityView => _enemyEntityView;
    }
}