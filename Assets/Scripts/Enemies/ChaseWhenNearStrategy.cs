using UnityEngine;

namespace Labirinto.Enemies
{
    // Decora outra estrategia: persegue o alvo (o player) quando ele esta
    // dentro do raio de deteccao; fora disso, delega para a estrategia base
    // (ex: RandomWanderStrategy). Nenhuma outra classe precisa mudar (Open/Closed).
    public class ChaseWhenNearStrategy : IEnemyMovementStrategy
    {
        private readonly Transform _self;
        private readonly Transform _target;
        private readonly IEnemyMovementStrategy _fallback;
        private readonly float _detectionRadius;

        public ChaseWhenNearStrategy(Transform self, Transform target, IEnemyMovementStrategy fallback, float detectionRadius)
        {
            _self = self;
            _target = target;
            _fallback = fallback;
            _detectionRadius = detectionRadius;
        }

        public Vector2 GetNextDirection()
        {
            if (_target == null) return _fallback.GetNextDirection();

            Vector2 toTarget = (Vector2)_target.position - (Vector2)_self.position;
            if (toTarget.magnitude <= _detectionRadius)
            {
                return toTarget.normalized;
            }

            return _fallback.GetNextDirection();
        }

        public void NotifyBlocked()
        {
            _fallback.NotifyBlocked();
        }
    }
}
