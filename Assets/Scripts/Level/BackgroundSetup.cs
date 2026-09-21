using UnityEngine;
using Labirinto.Common;

namespace Labirinto.Level
{
    // Unica responsabilidade: criar o plano de fundo do cenario,
    // dimensionado de acordo com o tamanho do labirinto atual.
    [RequireComponent(typeof(MazeBuilder))]
    public class BackgroundSetup : MonoBehaviour
    {
        [SerializeField] private Color backgroundColor = new Color(0.08f, 0.08f, 0.12f);
        [SerializeField] private float padding = 2f;

        private void Start()
        {
            var maze = GetComponent<MazeBuilder>();

            var background = new GameObject("Background");
            var renderer = background.AddComponent<SpriteRenderer>();
            renderer.sprite = ProceduralSprite.CreateSolid(backgroundColor);
            renderer.sortingOrder = -10;

            background.transform.position = maze.GetCenterPosition();
            background.transform.localScale = new Vector3(
                maze.ColumnCount * maze.CellSize + padding,
                maze.RowCount * maze.CellSize + padding,
                1f);
        }
    }
}
