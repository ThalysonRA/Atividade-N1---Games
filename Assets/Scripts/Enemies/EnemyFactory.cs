using UnityEngine;
using Labirinto.Common;
using Labirinto.Movement;

namespace Labirinto.Enemies
{
    // Unica responsabilidade: montar um inimigo completo (visual, fisica,
    // movimento e IA). Usada tanto na montagem inicial da cena (SceneBuilder)
    // quanto em tempo de execucao pelo LevelManager, evitando duplicar a
    // configuracao dos componentes nos dois lugares.
    public static class EnemyFactory
    {
        public static readonly Color DefaultColor = new Color(1f, 0.3f, 0.3f);

        public static GameObject Create(Vector3 position, float speed, Color color)
        {
            var go = new GameObject("Enemy");
            go.tag = GameTags.Enemy;
            go.transform.position = position;

            var renderer = go.AddComponent<SpriteRenderer>();
            renderer.sprite = ProceduralSprite.CreateSolid(color);
            renderer.sortingOrder = 5;

            var rb = go.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            var collider = go.AddComponent<CircleCollider2D>();
            collider.radius = 0.4f;

            var mover = go.AddComponent<RigidbodyMover>();
            mover.SetBaseSpeed(speed);

            go.AddComponent<EnemyController>();

            return go;
        }
    }
}
