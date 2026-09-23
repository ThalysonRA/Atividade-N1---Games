namespace Labirinto.Level
{
    // Apenas dados: dimensoes e semente do labirinto. Trocar o tamanho ou o
    // layout e so mexer aqui, sem afetar MazeBuilder, PlayerController ou
    // EnemyController (Open/Closed).
    public static class MazeLayout
    {
        public const int Rows = 19;
        public const int Columns = 23;

        private const int Seed = 12345;
        private const float ExtraOpenChance = 0.12f;

        private static int[,] _grid;

        // 1 = parede, 0 = caminho livre
        public static int[,] Grid => _grid ??= MazeGenerator.Generate(Rows, Columns, Seed, ExtraOpenChance);
    }
}
