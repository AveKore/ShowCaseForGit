using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyType _enemyType;

        public EnemyType EnemyType => _enemyType;
    }
}