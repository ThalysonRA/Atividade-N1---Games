# Atividade N1 - Games

Projeto 01 - Labirinto Fantasma: jogo top-down em Unity 2D, estilo Pac-Man,
onde o player anda por um labirinto comendo pastilhas enquanto foge de
inimigos com movimentação automática.

## Como abrir o projeto

1. Instale o [Unity Hub](https://unity.com/download)
2. No Unity Hub, instale a versão **6000.4.0f1** (ou outra versão do Unity 6 -
   o Hub oferece pra abrir com a versão que você tiver instalada)
3. No Unity Hub, clique em **Add** e selecione a pasta deste projeto (a que
   tem `Assets`, `Packages` e `ProjectSettings`)
4. Abra o projeto. Na primeira vez, o Unity vai reimportar tudo e recompilar
   os scripts — isso pode levar alguns minutos
5. Na aba **Project**, abra `Assets/Scenes/Labirinto.unity` (se ainda não
   abrir sozinha)
6. Clique em **Play** para jogar

## Como jogar

- Mova o player com **WASD** ou as **setas do teclado**
- Coma as pastilhas amarelas espalhadas pelo labirinto
- Fuja dos inimigos vermelhos — eles vagam aleatoriamente, mas perseguem o
  player quando ele chega perto
- Encostar em um inimigo reinicia a cena
- Ao comer todas as pastilhas, a fase avança: o jogo fica mais rápido, entra
  mais um inimigo e o mapa muda de cor

## Estrutura do projeto

Os scripts ficam em `Assets/Scripts/`, organizados por responsabilidade:

| Pasta | Responsabilidade |
|---|---|
| `Movement/` | Aplica o deslocamento físico (`Rigidbody2D` + `Time.deltaTime`) |
| `Player/` | Leitura de input e controle do player |
| `Enemies/` | IA dos inimigos (perambulação aleatória e perseguição) |
| `Collectibles/` | Pastilhas coletáveis |
| `Level/` | Geração procedural do labirinto, fundo e spawner de pastilhas |
| `Events/` | Eventos que desacoplam colisão/coleta de suas consequências |
| `Core/` | Gerenciamento de fase (`LevelManager`), reinício de cena (`GameManager`) e HUD |
| `Common/` | Constantes e utilitários compartilhados |

O código segue princípios SOLID: cada classe tem uma única responsabilidade,
e comportamentos como a IA dos inimigos ou o input do player são trocáveis
via interface sem alterar quem os consome.

## Recriando a cena do zero

Se a cena `Labirinto.unity` for perdida ou corrompida, ela pode ser
reconstruída automaticamente pelo menu **Labirinto > Build Scene** no Editor
do Unity (implementado em `Assets/Editor/SceneBuilder.cs`).

## Observações

- A pasta `Library/` (e outras geradas pelo Unity) não fica no repositório —
  o `.gitignore` já cuida disso. O Unity recria essas pastas sozinho na
  primeira vez que o projeto é aberto.
