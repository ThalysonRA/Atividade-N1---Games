using UnityEngine;

namespace Labirinto.Movement
{
    // Abstracao de movimento (Dependency Inversion): quem consome nao sabe
    // se o movimento e feito por Rigidbody2D, NavMesh, etc.
    public interface IMover
    {
        void Move(Vector2 direction);
    }
}
