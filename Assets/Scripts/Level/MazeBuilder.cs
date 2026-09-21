using UnityEngine;
using Labirinto.Common;

namespace Labirinto.Level
{
    // Unica responsabilidade: instanciar as paredes fisicas a partir de MazeLayout
    // e expor conversao de celula -> posicao no mundo para quem precisar (player,
    // inimigos, camera, background).
    public class MazeBuilder : MonoBehaviour
    {
        [SerializeField] private float cellSize = 1f;
        [SerializeField] private Color wallColor = new Color(0.2f, 0.2f, 0.28f);

        private Sprite _wallSprite;

        private void Awake()
        {
            Build();
        }

        private void Build()
        {
            _wallSprite = ProceduralSprite.CreateSolid(wallColor);

            int[,] grid = MazeLayout.Grid;
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (grid[row, col] == 1)
                    {
                        CreateWall(GetWorldPosition(row, col));
                    }
                }
            }
        }

        private void CreateWall(Vector3 position)
        {
            var wall = new GameObject("Wall");
            wall.transform.SetParent(transform);
            wall.transform.position = position;
            wall.tag = GameTags.Wall;

            var renderer = wall.AddComponent<SpriteRenderer>();
            renderer.sprite = _wallSprite;

            var collider = wall.AddComponent<BoxCollider2D>();
            collider.size = Vector2.one * cellSize;
        }

        public Vector3 GetWorldPosition(int row, int col)
        {
            return new Vector3(col * cellSize, -row * cellSize, 0f);
        }

        public Vector3 GetCenterPosition()
        {
            int rows = MazeLayout.Grid.GetLength(0);
            int cols = MazeLayout.Grid.GetLength(1);
            return new Vector3((cols - 1) * cellSize / 2f, -(rows - 1) * cellSize / 2f, 0f);
        }

        public int RowCount => MazeLayout.Grid.GetLength(0);
        public int ColumnCount => MazeLayout.Grid.GetLength(1);
        public float CellSize => cellSize;
    }
}
