using UnityEngine;

namespace Labirinto.Player
{
    public class KeyboardMovementInput : IMovementInput
    {
        public Vector2 GetDirection()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            return new Vector2(horizontal, vertical);
        }
    }
}
