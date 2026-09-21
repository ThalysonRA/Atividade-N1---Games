namespace Labirinto.Level
{
    // Apenas dados: o layout do labirinto. Trocar o labirinto e so trocar esta grade,
    // sem mexer em MazeBuilder, PlayerController ou EnemyController.
    public static class MazeLayout
    {
        // 1 = parede, 0 = caminho livre
        public static readonly int[,] Grid =
        {
            {1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,1,0,0,0,0,0,1,0,1},
            {1,0,1,0,1,0,1,1,1,0,1,0,1},
            {1,0,1,0,0,0,1,0,0,0,0,0,1},
            {1,0,1,1,1,1,1,0,1,1,1,0,1},
            {1,0,0,0,0,0,0,0,1,0,0,0,1},
            {1,1,1,1,1,0,1,1,1,0,1,1,1},
            {1,0,0,0,1,0,0,0,0,0,1,0,1},
            {1,0,1,0,1,1,1,1,1,0,1,0,1},
            {1,0,1,0,0,0,0,0,1,0,0,0,1},
            {1,0,1,1,1,1,1,0,1,1,1,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1},
        };
    }
}
