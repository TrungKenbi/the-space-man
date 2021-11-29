using ECS;
using Manager;
using UnityEngine;

namespace Hero
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HeroMovement : BaseComponent
    {
        public float moveSpeed = 5f;

        private Rigidbody2D _rb;
        private Vector2 _movement;
        private Vector2 _mousePos;

        private bool _facingRight = true;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public override void DoUpdate()
        {
            _movement.x = InputManager.MoveHorizontal;
            _movement.y = InputManager.MoveVertical;
            _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Move();
        }

        private void Move()
        {
            if (GameManager.Instance.isPaused)
                return;
            
            _rb.MovePosition(_rb.position + _movement * moveSpeed * Time.fixedDeltaTime);

            if (_mousePos.x > transform.position.x && !_facingRight)
            {
                Flip();
            } else if (_mousePos.x < transform.position.x && _facingRight)
            {
                Flip();
            }
        }

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            _facingRight = !_facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
