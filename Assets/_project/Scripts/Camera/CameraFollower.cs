using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraFollower : MonoBehaviour
    {
        private Transform _target;
        [SerializeField] private float _rotatinoAngleX;
        [SerializeField] private int _distance;
        [SerializeField] private float _offsetY;

        private void LateUpdate()
        {
            if (!_target)
                return;

            Quaternion rotation = Quaternion.Euler(_rotatinoAngleX, 0, 0);

            Vector3 position = rotation * new Vector3(0, 0, -_distance) + FollowingPoitPosition();

            transform.rotation = rotation;
            transform.position = position;
        }
        public void Follow(GameObject following)
        {
            _target = following.transform;
        }
        private Vector3 FollowingPoitPosition()
        {
            Vector3 followPosition = _target.position;
            followPosition.y += _offsetY;
            return followPosition;
        }
    }
}

