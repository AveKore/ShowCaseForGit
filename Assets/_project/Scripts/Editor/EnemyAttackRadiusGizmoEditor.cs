using CodeBase.Enemy;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(EnemyEntityView))]
    public class EnemyAttackRadiusGizmoEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(EnemyEntityView enemyAttack, GizmoType gizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(enemyAttack.AttackPoint.transform.position, enemyAttack.AttackRadius);
        }
    }
}