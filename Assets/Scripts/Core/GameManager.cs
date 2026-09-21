using UnityEngine;
using UnityEngine.SceneManagement;
using Labirinto.Events;

namespace Labirinto.Core
{
    // Unica responsabilidade: reagir ao evento de "player pego" reiniciando a cena.
    // Nao sabe nada sobre colisao, fisica ou input.
    public class GameManager : MonoBehaviour
    {
        private void OnEnable()
        {
            GameEvents.OnPlayerCaught += HandlePlayerCaught;
        }

        private void OnDisable()
        {
            GameEvents.OnPlayerCaught -= HandlePlayerCaught;
        }

        private void HandlePlayerCaught()
        {
            Scene current = SceneManager.GetActiveScene();
            SceneManager.LoadScene(current.name);
        }
    }
}
