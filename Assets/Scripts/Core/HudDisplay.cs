using UnityEngine;

namespace Labirinto.Core
{
    // Unica responsabilidade: exibir fase e pastilhas restantes na tela.
    public class HudDisplay : MonoBehaviour
    {
        private LevelManager _levelManager;
        private GUIStyle _style;

        private void Awake()
        {
            _levelManager = FindAnyObjectByType<LevelManager>();
        }

        private void OnGUI()
        {
            if (_levelManager == null) return;

            if (_style == null)
            {
                _style = new GUIStyle(GUI.skin.label)
                {
                    fontSize = 22
                };
                _style.normal.textColor = Color.white;
            }

            GUI.Label(new Rect(16, 16, 300, 30), $"Fase: {_levelManager.Phase}", _style);
            GUI.Label(new Rect(16, 44, 300, 30), $"Pastilhas: {_levelManager.Remaining}", _style);
        }
    }
}
