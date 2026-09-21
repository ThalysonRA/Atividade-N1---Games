using UnityEngine;

namespace Labirinto.Common
{
    // Gera sprites solidas em runtime para nao depender de arquivos de arte externos.
    public static class ProceduralSprite
    {
        public static Sprite CreateSolid(Color color, int size = 8)
        {
            var texture = new Texture2D(size, size)
            {
                filterMode = FilterMode.Point
            };

            var pixels = new Color[size * size];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = color;
            }

            texture.SetPixels(pixels);
            texture.Apply();

            return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
        }
    }
}
