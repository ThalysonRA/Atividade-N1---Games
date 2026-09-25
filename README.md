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

## Prompt para configurar com outra IA

Se quiser pedir para outra IA (ChatGPT, outra sessão do Claude, etc.) configurar
e rodar este projeto do zero, copie e cole o bloco abaixo:

```
Preciso que você configure e rode um projeto de jogo Unity 2D chamado "Labirinto Fantasma" 
(estilo Pac-Man), reproduzindo exatamente o mesmo resultado que já funciona em outra máquina.

## Contexto do projeto

- Repositório: https://github.com/ThalysonRA/Atividade-N1---Games.git
- Branch com o jogo completo: main
- Engine: Unity 6 (versão exata usada: 6000.4.0f1, definida em ProjectSettings/ProjectVersion.txt)
- Plataforma de desenvolvimento original: Windows

## O que é o jogo

Jogo top-down de labirinto (estilo Pac-Man) feito em Unity 2D:
- Labirinto grande gerado proceduralmente (recursive backtracking + rotas extras
  abertas aleatoriamente para criar loops, como em Pac-Man)
- Player controlado por teclado (WASD/setas), usa Rigidbody2D + Time.deltaTime
- Pastilhas amarelas coletáveis espalhadas por todo o labirinto
- Inimigos (vermelhos) com IA: vagam aleatoriamente, mas perseguem o player quando
  ele entra num raio de detecção
- Ao comer todas as pastilhas, avança de fase: velocidade aumenta, entra mais um
  inimigo, e o mapa muda de cor (paleta cíclica de cores de fundo/parede)
- Encostar em um inimigo reinicia a cena
- HUD simples (OnGUI) no canto superior esquerdo mostrando "Fase" e "Pastilhas"

## Passo a passo para configurar

1. Verifique se o Unity Hub está instalado. Se não estiver, baixe em unity.com/download
2. No Unity Hub, garanta que a versão 6000.4.0f1 (ou outra versão Unity 6 recente)
   está instalada. Se não tiver essa versão exata, o Hub geralmente oferece para
   abrir com outra versão do Unity 6 instalada — normalmente funciona sem problema,
   pois o projeto usa apenas API padrão do Unity.
3. Clone o repositório:
   git clone https://github.com/ThalysonRA/Atividade-N1---Games.git
   cd Atividade-N1---Games
   (confirme que está na branch "main" com `git status` / `git branch`)
4. Abra o projeto pelo Unity Hub (Add > selecione a pasta clonada, que contém
   Assets/, Packages/ e ProjectSettings/)
5. Na primeira abertura, o Unity vai reimportar todos os assets e recompilar os
   scripts — isso pode levar alguns minutos. Aguarde terminar (ícone de loading
   no canto inferior direito do Editor).
6. Verifique o Console: não deve haver nenhum erro vermelho (avisos amarelos tipo
   "obsolete" não são problema).
7. Na aba Project, abra Assets/Scenes/Labirinto.unity (dê duplo clique).
8. Clique em Play. O jogo deve carregar mostrando o labirinto completo, o player
   azul, os inimigos vermelhos e as pastilhas amarelas.

## Como testar se funcionou corretamente

- Mover o player com WASD/setas deve funcionar (clique dentro da janela Game
  primeiro, senão o teclado não tem foco)
- Passar por cima de uma pastilha deve fazê-la desaparecer e o contador "Pastilhas"
  no HUD deve diminuir
- Encostar em um inimigo deve reiniciar a cena
- Ao zerar as pastilhas, a fase deve avançar (aparece "Fase: 2" no HUD), o jogo
  fica mais rápido e surge mais um inimigo

## Estrutura do código (caso precise editar)

Scripts em Assets/Scripts/, organizados por responsabilidade única (SOLID):
- Movement/ — IMover (abstração) + RigidbodyMover (aplica Rigidbody2D.MovePosition
  usando Time.deltaTime) + ISpeedAdjustable (permite acelerar em runtime)
- Player/ — IMovementInput/KeyboardMovementInput (input desacoplado) +
  PlayerController + PlayerCollisionDetector
- Enemies/ — IEnemyMovementStrategy + RandomWanderStrategy (vagar aleatório) +
  ChaseWhenNearStrategy (decorator: persegue quando perto, senão delega pro
  wander) + EnemyController + EnemyFactory (constrói o GameObject completo do
  inimigo, usada tanto no editor quanto em runtime)
- Collectibles/ — Pellet (detecta trigger com o player, dispara evento)
- Level/ — MazeGenerator (algoritmo procedural) + MazeLayout (dados: tamanho,
  seed) + MazeBuilder (instancia paredes físicas) + BackgroundSetup +
  PelletSpawner
- Events/ — GameEvents (barramento estático de eventos, desacopla "detectar" de
  "reagir")
- Core/ — GameManager (reinicia cena ao pegar OnPlayerCaught) + LevelManager
  (controla fases, cor do mapa, velocidade, spawn de novo inimigo) + HudDisplay
- Common/ — GameTags (constantes) + ProceduralSprite (gera sprites sólidas em
  runtime via Texture2D, sem depender de arte externa)

Assets/Editor/SceneBuilder.cs é uma ferramenta de editor (menu "Labirinto >
Build Scene") que monta a cena inteira do zero programaticamente — útil se a
cena for corrompida ou você quiser recriar do zero. Não precisa rodar isso na
configuração normal, só se algo quebrar.

## Armadilhas conhecidas (encontradas ao configurar isso originalmente)

- Paredes, pastilhas e background só aparecem quando você aperta Play — eles são
  gerados em Awake()/Start() (runtime), não ficam salvos como objetos estáticos
  na cena. Isso é esperado, não é bug.
- Se o teclado não mover o player, geralmente é falta de foco na janela Game —
  clique nela antes de testar.
- Se dois processos do Unity tentarem abrir o mesmo projeto ao mesmo tempo
  (ex: um batch-mode + um Editor gráfico já aberto), um deles falha por conflito
  de lock do projeto. Feche um antes de abrir o outro.
- Em versões recentes do Unity 6, `FindObjectOfType<T>()` e até
  `FindFirstObjectByType<T>()` estão marcados como obsoletos — use
  `FindAnyObjectByType<T>()` se aparecerem warnings CS0618.
- O .gitignore já exclui Library/, Temp/, Logs/, UserSettings/ — não se preocupe
  com esses, o Unity recria sozinho.

Depois de configurar, me confirme se o Play funcionou e se conseguiu comer
pastilhas e avançar de fase, ou me diga qual erro apareceu no Console.
```
