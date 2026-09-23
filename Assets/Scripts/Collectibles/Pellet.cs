using UnityEngine;
using Labirinto.Common;
using Labirinto.Events;

namespace Labirinto.Collectibles
{
    // Unica responsabilidade: perceber que o player passou por cima e avisar
    // o jogo (via evento), sem saber quem conta pontos ou avanca de fase.
    public class Pellet : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(GameTags.Player)) return;

            GameEvents.RaisePelletCollected();
            Destroy(gameObject);
        }
    }
}
