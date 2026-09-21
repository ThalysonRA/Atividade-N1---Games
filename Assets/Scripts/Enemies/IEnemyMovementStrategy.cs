using UnityEngine;

namespace Labirinto.Enemies
{
    // Open/Closed: novas IAs (perseguir, patrulhar rota fixa...) sao novas
    // implementacoes desta interface, sem alterar o EnemyController.
    public interface IEnemyMovementStrategy
    {
        Vector2 GetNextDirection();

        void NotifyBlocked();
    }
}
