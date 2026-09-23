namespace Labirinto.Movement
{
    // Interface pequena e dedicada (Interface Segregation): so quem pode ter
    // a velocidade alterada implementa isto, sem inchar IMover.
    public interface ISpeedAdjustable
    {
        void MultiplySpeed(float factor);
    }
}
