using System.Collections.Generic;
using UnityEngine;

namespace Labirinto.Movement
{
    // Unica responsabilidade: aplicar deslocamento fisico via Rigidbody2D,
    // usando Time.deltaTime para a movimentacao ser independente do framerate.
    // Tambem se registra como ISpeedAdjustable para que o LevelManager possa
    // acelerar todos os movers do jogo sem conhecer Rigidbody2D.
    [RequireComponent(typeof(Rigidbody2D))]
    public class RigidbodyMover : MonoBehaviour, IMover, ISpeedAdjustable
    {
        private static readonly List<ISpeedAdjustable> _registry = new List<ISpeedAdjustable>();
        public static IReadOnlyList<ISpeedAdjustable> Registry => _registry;

        [SerializeField] private float speed = 6f;

        private Rigidbody2D _rigidbody;
        private Vector2 _direction;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _registry.Add(this);
        }

        private void OnDisable()
        {
            _registry.Remove(this);
        }

        public void Move(Vector2 direction)
        {
            _direction = direction.sqrMagnitude > 1f ? direction.normalized : direction;
        }

        public void MultiplySpeed(float factor)
        {
            speed *= factor;
        }

        public void SetBaseSpeed(float value)
        {
            speed = value;
        }

        private void Update()
        {
            if (_direction == Vector2.zero) return;

            Vector2 delta = _direction * speed * Time.deltaTime;
            _rigidbody.MovePosition(_rigidbody.position + delta);
        }
    }
}
