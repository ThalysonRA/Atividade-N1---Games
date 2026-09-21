using UnityEngine;
using Labirinto.Movement;

namespace Labirinto.Player
{
    // Unica responsabilidade: ler a direcao desejada e repassar ao IMover.
    // Nao sabe como o movimento e aplicado nem de onde vem o input.
    public class PlayerController : MonoBehaviour
    {
        private IMovementInput _input;
        private IMover _mover;

        private void Awake()
        {
            _mover = GetComponent<IMover>();
            _input = new KeyboardMovementInput();
        }

        private void Update()
        {
            _mover.Move(_input.GetDirection());
        }
    }
}
