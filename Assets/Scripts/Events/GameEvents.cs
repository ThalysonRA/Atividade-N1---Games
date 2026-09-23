using System;

namespace Labirinto.Events
{
    // Barramento de eventos simples: quem detecta o problema (colisao)
    // nao precisa conhecer quem resolve o problema (GameManager).
    public static class GameEvents
    {
        public static event Action OnPlayerCaught;
        public static event Action OnPelletCollected;

        public static void RaisePlayerCaught()
        {
            OnPlayerCaught?.Invoke();
        }

        public static void RaisePelletCollected()
        {
            OnPelletCollected?.Invoke();
        }
    }
}
