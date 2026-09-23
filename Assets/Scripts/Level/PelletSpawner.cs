using System.Collections.Generic;
using UnityEngine;
using Labirinto.Collectibles;
using Labirinto.Common;

namespace Labirinto.Level
{
    // Unica responsabilidade: criar (e recriar, a cada fase) as pastilhas em
    // todas as celulas livres do labirinto, exceto os pontos de spawn reservados.
    [RequireComponent(typeof(MazeBuilder))]
    public class PelletSpawner : MonoBehaviour
    {
        [SerializeField] private Color pelletColor = new Color(1f, 0.85f, 0.3f);
        [SerializeField] private float pelletScale = 0.28f;

        private readonly List<GameObject> _pellets = new List<GameObject>();
        private HashSet<(int row, int col)> _reservedCells = new HashSet<(int, int)>();

        private MazeBuilder _maze;
        private Sprite _pelletSprite;

        public int RemainingCount => _pellets.Count;

        private void Awake()
        {
            _maze = GetComponent<MazeBuilder>();
            _pelletSprite = ProceduralSprite.CreateSolid(pelletColor);
        }

        public void SetReservedCells(IEnumerable<(int row, int col)> cells)
        {
            _reservedCells = new HashSet<(int, int)>(cells);
        }

        public void SpawnAll()
        {
            ClearExisting();

            int[,] grid = MazeLayout.Grid;
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (grid[row, col] != 0) continue;
                    if (_reservedCells.Contains((row, col))) continue;

                    SpawnPellet(_maze.GetWorldPosition(row, col));
                }
            }
        }

        private void SpawnPellet(Vector3 position)
        {
            var go = new GameObject("Pellet");
            go.transform.SetParent(transform);
            go.transform.position = position;
            go.transform.localScale = Vector3.one * pelletScale;

            var renderer = go.AddComponent<SpriteRenderer>();
            renderer.sprite = _pelletSprite;
            renderer.sortingOrder = 2;

            var collider = go.AddComponent<CircleCollider2D>();
            collider.isTrigger = true;

            go.AddComponent<Pellet>();

            _pellets.Add(go);
        }

        private void ClearExisting()
        {
            foreach (var pellet in _pellets)
            {
                if (pellet != null) Destroy(pellet);
            }
            _pellets.Clear();
        }
    }
}
