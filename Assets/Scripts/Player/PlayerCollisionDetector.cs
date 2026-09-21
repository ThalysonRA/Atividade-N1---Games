using UnityEngine;
using Labirinto.Common;
using Labirinto.Events;

namespace Labirinto.Player
{
    // Unica responsabilidade: perceber o toque no inimigo e avisar o jogo.
    // Nao decide o que acontece depois (isso e do GameManager).
    public class PlayerCollisionDetector : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(GameTags.Enemy))
            {
                GameEvents.RaisePlayerCaught();
            }
        }
    }
}
