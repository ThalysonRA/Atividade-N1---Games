using UnityEngine;

namespace Labirinto.Enemies
{
    // IA basica: escolhe uma direcao aleatoria e mantem por um intervalo de tempo.
    // Ao ser bloqueada por uma parede, troca de direcao imediatamente.
    public class RandomWanderStrategy : IEnemyMovementStrategy
    {
        private static readonly Vector2[] Directions =
        {
            Vector2.up, Vector2.down, Vector2.left, Vector2.right
        };

        private readonly float _changeIntervalSeconds;
        private float _timer;
        private Vector2 _currentDirection;

        public RandomWanderStrategy(float changeIntervalSeconds = 1.5f)
        {
            _changeIntervalSeconds = changeIntervalSeconds;
            _currentDirection = PickRandomDirection();
        }

        public Vector2 GetNextDirection()
        {
            _timer += Time.deltaTime;
            if (_timer >= _changeIntervalSeconds)
            {
                _timer = 0f;
                _currentDirection = PickRandomDirection();
            }
            return _currentDirection;
        }

        public void NotifyBlocked()
        {
            _timer = 0f;
            _currentDirection = PickRandomDirection();
        }

        private static Vector2 PickRandomDirection()
        {
            return Directions[Random.Range(0, Directions.Length)];
        }
    }
}
