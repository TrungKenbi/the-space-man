using UnityEngine;

namespace UI
{
    public class FollowCamera : Singleton<FollowCamera>
    {
        public float dampTime = 0.3f;
        public float maxDistance = 7.5f;

        public Transform cursorTransform;
        public Transform playerTransform;

        private Vector3 _moveVelocity;
        private Vector3 _desiredPosition;
        
        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            FindAveragePosition();
            transform.position = Vector3.SmoothDamp(transform.position, _desiredPosition, ref _moveVelocity, dampTime);
        }

        private void FindAveragePosition()
        {
            Vector3 averagePos = new Vector3();

            averagePos += playerTransform.position + cursorTransform.position;
            averagePos /= 2;

            float distance = Vector3.Distance(playerTransform.position, cursorTransform.position);

            if (distance > maxDistance)
            {
                var position = playerTransform.position;
                Vector3 direction = (cursorTransform.position - position).normalized;

                _desiredPosition = position + (maxDistance / 2 * direction);
                _desiredPosition.z = transform.position.z;
                return;
            }

            _desiredPosition = averagePos;
        }
    }
}
