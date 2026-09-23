using UnityEngine;
using Labirinto.Common;

namespace Labirinto.Level
{
    // Unica responsabilidade: criar o plano de fundo do cenario, dimensionado
    // de acordo com o tamanho do labirinto atual, e permitir retintar (usado
    // pelo LevelManager para mudar a cor do mapa a cada fase).
    [RequireComponent(typeof(MazeBuilder))]
    public class BackgroundSetup : MonoBehaviour
    {
        [SerializeField] private Color backgroundColor = new Color(0.08f, 0.08f, 0.12f);
        [SerializeField] private float padding = 2f;

        private SpriteRenderer _renderer;

        private void Start()
        {
            var maze = GetComponent<MazeBuilder>();

            var background = new GameObject("Background");
            _renderer = background.AddComponent<SpriteRenderer>();
            _renderer.sprite = ProceduralSprite.CreateSolid(Color.white);
            _renderer.color = backgroundColor;
            _renderer.sortingOrder = -10;

            background.transform.position = maze.GetCenterPosition();
            background.transform.localScale = new Vector3(
                maze.ColumnCount * maze.CellSize + padding,
                maze.RowCount * maze.CellSize + padding,
                1f);
        }

        public void SetColor(Color color)
        {
            backgroundColor = color;
            if (_renderer != null) _renderer.color = color;
        }
    }
}
