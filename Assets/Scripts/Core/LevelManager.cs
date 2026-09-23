using System.Collections.Generic;
using UnityEngine;
using Labirinto.Common;
using Labirinto.Enemies;
using Labirinto.Events;
using Labirinto.Level;
using Labirinto.Movement;

namespace Labirinto.Core
{
    // Unica responsabilidade: acompanhar quantas pastilhas faltam e decidir
    // quando a fase acabou, acionando o aumento de velocidade, a troca de cor
    // do mapa e a entrada de mais um inimigo.
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private float speedMultiplierPerPhase = 1.15f;
        [SerializeField] private float baseEnemySpeed = 9.5f;
        [SerializeField] private float minSpawnDistanceFromPlayer = 3f;

        private static readonly Color[] PhaseBackgroundColors =
        {
            new Color(0.08f, 0.08f, 0.12f),
            new Color(0.14f, 0.05f, 0.18f),
            new Color(0.04f, 0.14f, 0.18f),
            new Color(0.18f, 0.07f, 0.05f),
            new Color(0.05f, 0.16f, 0.07f),
            new Color(0.18f, 0.14f, 0.02f),
        };

        private PelletSpawner _spawner;
        private MazeBuilder _maze;
        private BackgroundSetup _background;
        private float _currentEnemySpeed;

        public int Phase { get; private set; } = 1;
        public int Remaining { get; private set; }

        private void Awake()
        {
            _spawner = FindAnyObjectByType<PelletSpawner>();
            _maze = FindAnyObjectByType<MazeBuilder>();
            _background = FindAnyObjectByType<BackgroundSetup>();
            _currentEnemySpeed = baseEnemySpeed;
        }

        private void OnEnable()
        {
            GameEvents.OnPelletCollected += HandlePelletCollected;
        }

        private void OnDisable()
        {
            GameEvents.OnPelletCollected -= HandlePelletCollected;
        }

        private void Start()
        {
            StartPhase();
        }

        private void StartPhase()
        {
            _spawner.SpawnAll();
            Remaining = _spawner.RemainingCount;
        }

        private void HandlePelletCollected()
        {
            Remaining--;
            if (Remaining <= 0)
            {
                AdvancePhase();
            }
        }

        private void AdvancePhase()
        {
            Phase++;
            _currentEnemySpeed *= speedMultiplierPerPhase;

            foreach (var mover in RigidbodyMover.Registry)
            {
                mover.MultiplySpeed(speedMultiplierPerPhase);
            }

            ApplyPhaseColors();
            SpawnExtraEnemy();
            StartPhase();
        }

        private void ApplyPhaseColors()
        {
            Color background = PhaseBackgroundColors[(Phase - 1) % PhaseBackgroundColors.Length];
            Color wall = Lighten(background, 0.14f);

            _background.SetColor(background);
            _maze.SetWallColor(wall);
        }

        private void SpawnExtraEnemy()
        {
            Vector3 spawnPosition = FindSpawnPositionAwayFromPlayer();
            EnemyFactory.Create(spawnPosition, _currentEnemySpeed, EnemyFactory.DefaultColor);
        }

        private Vector3 FindSpawnPositionAwayFromPlayer()
        {
            var player = GameObject.FindGameObjectWithTag(GameTags.Player);
            Vector3 playerPosition = player != null ? player.transform.position : Vector3.zero;

            List<(int row, int col)> openCells = GetOpenCells();
            var firstCell = openCells[Random.Range(0, openCells.Count)];
            Vector3 chosen = _maze.GetWorldPosition(firstCell.row, firstCell.col);

            int attempts = 0;
            while (Vector3.Distance(chosen, playerPosition) < minSpawnDistanceFromPlayer && attempts < 20)
            {
                var cell = openCells[Random.Range(0, openCells.Count)];
                chosen = _maze.GetWorldPosition(cell.row, cell.col);
                attempts++;
            }

            return chosen;
        }

        private static List<(int row, int col)> GetOpenCells()
        {
            int[,] grid = MazeLayout.Grid;
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            var cells = new List<(int, int)>();
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (grid[row, col] == 0) cells.Add((row, col));
                }
            }
            return cells;
        }

        private static Color Lighten(Color color, float amount)
        {
            return new Color(
                Mathf.Clamp01(color.r + amount),
                Mathf.Clamp01(color.g + amount),
                Mathf.Clamp01(color.b + amount));
        }
    }
}
