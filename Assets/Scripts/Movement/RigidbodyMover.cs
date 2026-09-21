using UnityEngine;

namespace Labirinto.Movement
{
    // Unica responsabilidade: aplicar deslocamento fisico via Rigidbody2D,
    // usando Time.deltaTime para a movimentacao ser independente do framerate.
    [RequireComponent(typeof(Rigidbody2D))]
    public class RigidbodyMover : MonoBehaviour, IMover
    {
        [SerializeField] private float speed = 4f;

        private Rigidbody2D _rigidbody;
        private Vector2 _direction;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 direction)
        {
            _direction = direction.sqrMagnitude > 1f ? direction.normalized : direction;
        }

        private void Update()
        {
            if (_direction == Vector2.zero) return;

            Vector2 delta = _direction * speed * Time.deltaTime;
            _rigidbody.MovePosition(_rigidbody.position + delta);
        }
    }
}
