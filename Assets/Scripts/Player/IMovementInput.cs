using UnityEngine;

namespace Labirinto.Player
{
    // Isola a origem do input (teclado, mobile, IA de teste...) de quem move o player.
    public interface IMovementInput
    {
        Vector2 GetDirection();
    }
}
