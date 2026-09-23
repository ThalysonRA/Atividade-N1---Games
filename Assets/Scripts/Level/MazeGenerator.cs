using System.Collections.Generic;

namespace Labirinto.Level
{
    // Gera um labirinto maior por backtracking recursivo (arvore geradora) e
    // depois derruba algumas paredes extras para criar rotas alternativas,
    // deixando corredores mais abertos, no estilo Pac-Man.
    public static class MazeGenerator
    {
        public static int[,] Generate(int rows, int cols, int seed, float extraOpenChance)
        {
            if (rows % 2 == 0) rows++;
            if (cols % 2 == 0) cols++;

            var grid = new int[rows, cols];
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    grid[r, c] = 1;
                }
            }

            Carve(grid, rows, cols, seed);
            OpenExtraPaths(grid, rows, cols, seed, extraOpenChance);

            return grid;
        }

        private static void Carve(int[,] grid, int rows, int cols, int seed)
        {
            var random = new System.Random(seed);
            var stack = new Stack<(int row, int col)>();
            (int dr, int dc)[] directions = { (-2, 0), (2, 0), (0, -2), (0, 2) };

            var start = (row: 1, col: 1);
            grid[start.row, start.col] = 0;
            stack.Push(start);

            while (stack.Count > 0)
            {
                var current = stack.Peek();
                var candidates = new List<(int row, int col, int wallRow, int wallCol)>();

                foreach (var (dr, dc) in directions)
                {
                    int nr = current.row + dr;
                    int nc = current.col + dc;
                    if (nr > 0 && nr < rows - 1 && nc > 0 && nc < cols - 1 && grid[nr, nc] == 1)
                    {
                        candidates.Add((nr, nc, current.row + dr / 2, current.col + dc / 2));
                    }
                }

                if (candidates.Count == 0)
                {
                    stack.Pop();
                    continue;
                }

                var chosen = candidates[random.Next(candidates.Count)];
                grid[chosen.wallRow, chosen.wallCol] = 0;
                grid[chosen.row, chosen.col] = 0;
                stack.Push((chosen.row, chosen.col));
            }
        }

        private static void OpenExtraPaths(int[,] grid, int rows, int cols, int seed, float chance)
        {
            var random = new System.Random(seed + 1);

            for (int r = 2; r < rows - 2; r++)
            {
                for (int c = 2; c < cols - 2; c++)
                {
                    if (grid[r, c] != 1) continue;

                    bool horizontalGap = r % 2 == 1 && c % 2 == 0 && grid[r, c - 1] == 0 && grid[r, c + 1] == 0;
                    bool verticalGap = r % 2 == 0 && c % 2 == 1 && grid[r - 1, c] == 0 && grid[r + 1, c] == 0;

                    if ((horizontalGap || verticalGap) && random.NextDouble() < chance)
                    {
                        grid[r, c] = 0;
                    }
                }
            }
        }
    }
}
