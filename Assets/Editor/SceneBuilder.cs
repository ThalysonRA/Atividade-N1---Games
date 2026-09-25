using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
using UnityEngine;
using Labirinto.Common;
using Labirinto.Core;
using Labirinto.Enemies;
using Labirinto.Level;
using Labirinto.Movement;
using Labirinto.Player;

namespace Labirinto.EditorTools
{
    // Ferramenta de Editor: monta a cena de jogo inteira do zero de forma
    // reprodutivel (util para recriar a cena caso ela seja perdida ou
    // para rodar via linha de comando com -executeMethod).
    public static class SceneBuilder
    {
        private const string ScenePath = "Assets/Scenes/Labirinto.unity";

        [MenuItem("Labirinto/Build Scene")]
        public static void BuildScene()
        {
            EnsureTagsExist();

            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            CreateGameManager();
            var maze = CreateMaze();
            var reservedCells = new List<(int row, int col)> { PlayerSpawn };
            reservedCells.AddRange(EnemySpawns(maze));

            CreatePlayer(maze);
            CreateEnemies(maze);
            CreateLevelSystems(maze, reservedCells);
            SetupCamera(maze);

            EnsureFolder("Assets/Scenes");
            EditorSceneManager.SaveScene(scene, ScenePath);
            AddSceneToBuildSettings(ScenePath);

            Debug.Log("Cena do Labirinto Fantasma gerada em " + ScenePath);
        }

        private static void EnsureTagsExist()
        {
            EnsureTag(GameTags.Enemy);
            EnsureTag(GameTags.Wall);
        }

        private static void EnsureTag(string tag)
        {
            var existing = new List<string>(InternalEditorUtility.tags);
            if (!existing.Contains(tag))
            {
                InternalEditorUtility.AddTag(tag);
            }
        }

        private static void CreateGameManager()
        {
            var go = new GameObject("GameManager");
            go.AddComponent<GameManager>();
        }

        private static readonly (int row, int col) PlayerSpawn = (1, 1);

        private static (int row, int col)[] EnemySpawns(MazeBuilder maze)
        {
            return new[]
            {
                (1, maze.ColumnCount - 2),
                (maze.RowCount - 2, 1),
                (maze.RowCount - 2, maze.ColumnCount - 2)
            };
        }

        private static MazeBuilder CreateMaze()
        {
            var go = new GameObject("Maze");
            var maze = go.AddComponent<MazeBuilder>();
            go.AddComponent<BackgroundSetup>();
            return maze;
        }

        private static void CreateLevelSystems(MazeBuilder maze, List<(int row, int col)> reservedCells)
        {
            var spawner = maze.gameObject.AddComponent<PelletSpawner>();
            spawner.SetReservedCells(reservedCells);

            var levelManagerGo = new GameObject("LevelManager");
            levelManagerGo.AddComponent<LevelManager>();

            var hudGo = new GameObject("Hud");
            hudGo.AddComponent<HudDisplay>();
        }

        private static void CreatePlayer(MazeBuilder maze)
        {
            var go = new GameObject("Player");
            go.tag = GameTags.Player;
            go.transform.position = maze.GetWorldPosition(PlayerSpawn.row, PlayerSpawn.col);

            var renderer = go.AddComponent<SpriteRenderer>();
            renderer.sprite = ProceduralSprite.CreateSolid(new Color(0.3f, 0.7f, 1f));
            renderer.sortingOrder = 5;

            var rb = go.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            var collider = go.AddComponent<CircleCollider2D>();
            collider.radius = 0.4f;

            var mover = go.AddComponent<RigidbodyMover>();
            mover.SetBaseSpeed(13f);
            go.AddComponent<PlayerController>();
            go.AddComponent<PlayerCollisionDetector>();
        }

        private static void CreateEnemies(MazeBuilder maze)
        {
            foreach (var point in EnemySpawns(maze))
            {
                EnemyFactory.Create(maze.GetWorldPosition(point.row, point.col), 9.5f, EnemyFactory.DefaultColor);
            }
        }

        private static void SetupCamera(MazeBuilder maze)
        {
            var go = new GameObject("Main Camera");
            go.tag = "MainCamera";

            var cam = go.AddComponent<Camera>();
            cam.orthographic = true;
            cam.orthographicSize = Mathf.Max(maze.RowCount, maze.ColumnCount) * maze.CellSize * 0.55f;
            cam.backgroundColor = new Color(0.08f, 0.08f, 0.12f);

            go.transform.position = maze.GetCenterPosition() + new Vector3(0f, 0f, -10f);
            go.AddComponent<AudioListener>();
        }

        private static void EnsureFolder(string path)
        {
            if (!AssetDatabase.IsValidFolder(path))
            {
                AssetDatabase.CreateFolder("Assets", "Scenes");
            }
        }

        private static void AddSceneToBuildSettings(string path)
        {
            var scenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);

            foreach (var s in scenes)
            {
                if (s.path == path) return;
            }

            scenes.Add(new EditorBuildSettingsScene(path, true));
            EditorBuildSettings.scenes = scenes.ToArray();
        }
    }
}
