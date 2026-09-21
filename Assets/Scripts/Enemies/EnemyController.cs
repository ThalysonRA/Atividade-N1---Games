using UnityEngine;
using Labirinto.Common;
using Labirinto.Movement;

namespace Labirinto.Enemies
{
    // Unica responsabilidade: perguntar a estrategia qual a proxima direcao
    // e repassar ao IMover. A logica de "como" mover e "como decidir" fica
    // em outras classes (RigidbodyMover e IEnemyMovementStrategy).
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float directionChangeInterval = 1.5f;

        private IMover _mover;
        private IEnemyMovementStrategy _strategy;

        private void Awake()
        {
            _mover = GetComponent<IMover>();
            _strategy = new RandomWanderStrategy(directionChangeInterval);
        }

        private void Update()
        {
            _mover.Move(_strategy.GetNextDirection());
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(GameTags.Wall))
            {
                _strategy.NotifyBlocked();
            }
        }
    }
}
