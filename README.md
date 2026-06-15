<div align="center">

# 🌻 Plants vs. Zombies 2 — Custom Edition

> A faithful recreation of the classic tower-defense game built from scratch with C# and SplashKit SDK, featuring OOP design patterns, wave-based zombie AI, and a full game-state machine.

[![Language](https://img.shields.io/badge/Language-C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![Framework](https://img.shields.io/badge/Framework-.NET%209.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![SDK](https://img.shields.io/badge/Game%20SDK-SplashKit-FF6B35?style=for-the-badge)](https://splashkit.io/)
[![Platform](https://img.shields.io/badge/Platform-macOS%20%7C%20Windows%20%7C%20Linux-lightgrey?style=for-the-badge&logo=apple)](https://splashkit.io/)
[![Course](https://img.shields.io/badge/Course-COS%2020007-blue?style=for-the-badge)](https://www.swinburne.edu.au/)

</div>

---

## 📖 Table of Contents

- [About the Project](#-about-the-project)
- [Tech Stack](#-tech-stack)
- [Architecture & Design Patterns](#-architecture--design-patterns)
- [Game Structure](#-game-structure)
- [Plants Roster](#-plants-roster)
- [Zombie Roster](#-zombie-roster)
- [Wave System](#-wave-system)
- [Project Structure](#-project-structure)
- [Getting Started](#-getting-started)
- [Gameplay Guide](#-gameplay-guide)

---

## 🎮 About the Project

This is a custom, ground-up implementation of **Plants vs. Zombies** written as the final project for **COS 20007 — Object-Oriented Programming**. The game runs at 60 FPS in a 1240×700 window and features:

- A **3-screen game flow**: Main Menu → Seed Selection → In-Game
- **10 playable plants**, each with unique mechanics
- **5 zombie types** with escalating difficulty across timed waves
- A full **sun economy** (collect suns, spend to plant)
- **Sprite-layered animations** (e.g. Wallnut cracking, ScaredyShroom hiding/revealing)
- **Status effects**: slow (Ice Pea), stun (Lightning Pea), explosion (Potato Mine)
- Dynamic **background music** that changes with game events

---

## 🛠 Tech Stack

| Layer | Technology | Purpose |
|---|---|---|
| **Language** | C# 12 | All game logic and OOP architecture |
| **Runtime** | .NET 9.0 | Cross-platform execution host |
| **Game SDK** | SplashKit SDK | 2D rendering, sprites, audio, input, timers |
| **Build System** | MSBuild / `dotnet` CLI | Compilation and project management |
| **IDE** | Visual Studio Code | Development environment |
| **Asset Format** | PNG (sprites), MP3 (audio) | Game resources |

> **SplashKit** is a beginner-friendly game development framework that wraps OpenGL/SDL2 for 2D rendering, audio playback, sprite management, and hardware input — letting the codebase focus entirely on game logic.

---

## 🏛 Architecture & Design Patterns

This project deliberately applies three classical OOP design patterns from the Gang of Four:

### 1. Singleton — `GameContext`
```
┌─────────────────────────────────────┐
│           GameContext               │
│  (Only ONE instance ever exists)    │
│                                     │
│  + GetGameInstance(window) ──────── │──► returns the single shared instance
│  + CurrentState: GameState          │
│  + Update()                         │
└─────────────────────────────────────┘
```
`GameContext` is the single source of truth for the running game. All state transitions go through it.

---

### 2. State Pattern — Game Screens
```
          ┌──────────────┐
          │  «interface» │
          │  GameState   │
          │  + Update()  │
          │  + NextState │
          │  + PrevState │
          └──────┬───────┘
                 │ implements
    ┌────────────┼────────────┐
    ▼            ▼            ▼
MainMenuState  ChooseSeed  IngameState
  (menu BGM)   State        (gameplay loop,
               (card pick)   wave system)
```
Each screen is a self-contained state. Clicking "Play" swaps the state; the game loop calls `Update()` on whatever the current state is.

---

### 3. Factory Pattern — Zombie Spawning
```
«interface»
ZombieFactory
  └── FactoryNormalZombie    ──► NormalZombie
  └── FactoryConeheadZombie  ──► ConeheadZombie
  └── FactoryBucketheadZombie──► BucketheadZombie
  └── FactoryDoorZombie      ──► DoorZombie
  └── FactoryZombieFootball  ──► ZombieFootball

ZombieFactoryCreator (director)
  └── Selects the right factory based on elapsed timer
  └── Returns a new Zombie (or null if not yet time)
```
The creator reads the game timer and delegates to the appropriate factory, making it trivial to add new zombie types without touching existing logic.

---

### Inheritance Hierarchy (simplified)

```
GameObject
├── Map
├── Sun
├── Brain / Pizza        (win/lose overlays)
├── Bullet
│   ├── GreenPea
│   ├── IcePea
│   ├── LightningPea
│   └── ToxicScaredy
├── Plant (abstract)
│   ├── SunFlower
│   ├── TwinSunFlower
│   ├── Wallnut
│   ├── PotatoMine
│   └── ShooterPlant
│       ├── PeaShooter
│       ├── IcePeaShooter
│       ├── Repeater
│       ├── SoldierPea
│       ├── ElectricPeashooter
│       └── ScaredyShroom
└── Zombie (abstract)
    ├── NormalZombie
    ├── ConeheadZombie
    ├── BucketheadZombie
    ├── DoorZombie
    └── ZombieFootball
```

---

## 🗺 Game Structure

```
┌─────────────┐    Play     ┌─────────────────┐   Start   ┌──────────────┐
│  Main Menu  │ ──────────► │  Seed Selection │ ────────► │   In-Game    │
│  (BGM loop) │             │  (pick plants)  │           │  (60 FPS     │
└─────────────┘             └─────────────────┘           │   loop)      │
        ▲                           ▲                      └──────┬───────┘
        │                           │  Back                      │
        │                           └────────────────────────────┤ Lose
        │                                                        │ (zombie
        └────────────────────────────────────────────────────────┘ reaches
                                                            Win    left edge)
                                                        (50 kills)
```

**Map grid**: 5 rows × 9 columns = **45 placeable cells**  
Grid origin: `x = 520, y = 150` | Cell size: `100 × 105 px`

---

## 🌱 Plants Roster

| Plant | Sun Cost | Special Ability | Bullet Type |
|---|---|---|---|
| 🌻 **Sunflower** | 50 | Generates +25 sun periodically | — |
| 🌻🌻 **Twin Sunflower** | 125 | Generates sun faster | — |
| 🫛 **Peashooter** | 100 | Standard single pea | Green Pea |
| ❄️ **Snow Pea** | 175 | Slows zombies on hit | Ice Pea |
| 🔋 **Electric Peashooter** | 150 | Stuns zombies on hit | Lightning Pea |
| 🫛🫛 **Repeater** | 200 | Double-fires per reload | Green Pea ×2 |
| 🪖 **Soldier Pea** | 150 | Rapid-fire shooter | Green Pea |
| 🍄 **Scaredy Shroom** | 25 | Hides when safe, pops up to shoot toxic peas | Toxic Pea |
| 🥔 **Potato Mine** | 25 | Arms after delay, instant-kills on contact | Explosion |
| 🌰 **Wall-nut** | 50 | Defensive blocker, 3 visual health stages | — |

---

## 🧟 Zombie Roster

| Zombie | Health | Damage | Notes |
|---|---|---|---|
| 🧟 **Normal Zombie** | Low | Low | Standard walker |
| 🧢 **Conehead Zombie** | Medium | Low | Cone provides extra HP |
| 🪣 **Buckethead Zombie** | High | Low | Bucket armor absorbs hits |
| 🚪 **Door Zombie** | Very High | Medium | Carries door as shield |
| 🏈 **Football Zombie** | 500 | 99 | Fastest, deadliest — one-shots most plants |

---

## 🌊 Wave System

Zombie difficulty escalates based on elapsed game time:

```
Timeline (milliseconds)
──────────────────────────────────────────────────────────────►
   0      20k     40k     60k     80k    140k   160k
   │       │       │       │       │       │      │
   │  Prep │Normal │Normal │Normal │ All   │ All  │ Door
   │       │ only  │(slow) │+Cone  │ types │types │ only
   │       │       │       │       │ mixed │(fast)│ (boss)
```

- **Win condition**: Kill 50 zombies  
- **Lose condition**: Any zombie crosses `x < 280` (reaches the house)  
- **Total zombies**: 200 spawned per run

---

## 📁 Project Structure

```
Plants-vs-Zombies-2/
│
├── Program.cs                  # Entry point — creates 1240×700 window
├── GameContext.cs              # Singleton — owns the active game state
├── GameObject.cs               # Base class for everything renderable
├── Map.cs                      # 5×9 grid, sun HUD, zombie counter
├── Cell.cs                     # Single grid cell (position + isPlaced flag)
├── Sun.cs                      # Collectible sun drops
│
├── State/                      # State Pattern
│   ├── GameStage.cs            # «interface» GameState
│   ├── MainMenu.cs             # Screen 1: title + music
│   ├── ChooseSeedState.cs      # Screen 2: card picker
│   └── IngameStage.cs          # Screen 3: full gameplay loop
│
├── Plants/                     # Inheritance hierarchy
│   ├── Plant.cs                # Abstract base
│   ├── ShooterPlant.cs         # Abstract shooter (reload, bullet list)
│   ├── Peashooter.cs
│   ├── Snowpea.cs
│   ├── ElectricPeashooter.cs
│   ├── Repeater.cs
│   ├── SoldierPea.cs
│   ├── ScaredyShroom.cs
│   ├── SunFlower.cs
│   ├── TwinSunFlower.cs
│   ├── Wallnut.cs
│   └── PotatoMin.cs
│
├── Zombies/                    # Inheritance hierarchy
│   ├── Zombie.cs               # Abstract base (HP, speed, status effects)
│   ├── NormalZombie.cs
│   ├── ConeheadZombie.cs
│   ├── BucketheadZombie.cs
│   ├── DoorZombie.cs
│   └── ZombieFootball.cs
│
├── Bullets/                    # Projectile types
│   ├── Bullet.cs               # Base (velocity, damage)
│   ├── GreenPea.cs
│   ├── IcePea.cs               # Applies slow effect
│   ├── LightningPea.cs         # Applies stun effect
│   └── ToxicScaredy.cs
│
├── Factory/                    # Factory Pattern
│   ├── ZombieFactory.cs        # «interface»
│   ├── ZombieFactoryCreator.cs # Director — picks factory by timer
│   ├── FactoryNormalZombie.cs
│   ├── FactoryConeheadZombie.cs
│   ├── FactoryBucketheadZombie.cs
│   ├── FactoryDoorZombie.cs
│   └── FactoryZombieFootball.cs
│
├── Cards/                      # Plant selection cards (UI)
│   ├── Card.cs
│   ├── CardPeaShooter.cs
│   └── ...  (one per plant)
│
├── Buttons/                    # Interactive buttons (UI)
│   ├── Button.cs
│   ├── ButtonA.cs / ButtonB.cs
│   ├── ButtonX.cs / ButtonY.cs
│   ├── ButtonSelected.cs
│   └── ButtonRejected.cs
│
├── Resources/
│   ├── images/                 # PNG sprites & backgrounds
│   └── sounds/                 # MP3 music tracks
│
└── Custom_Program.csproj       # .NET 9 project file
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SplashKit SDK](https://splashkit.io/installation/) installed globally

### Run

```bash
# Clone the repo
git clone <your-repo-url>
cd Plants-vs-Zombies-2

# Build & run
dotnet run
```

The game window opens at **1240 × 700** pixels.

---

## 🕹 Gameplay Guide

```
1.  MAIN MENU  →  Click the button to start
                       │
                       ▼
2.  SEED SELECT  →  Click a plant card in the bottom panel to highlight it
                    Click the ✓ button to add it to your lineup
                    Click the ✗ button to remove a chosen card
                    Press X to start the game (need at least 1 card)
                    Press Y to return to the main menu
                       │
                       ▼
3.  IN-GAME
    ☀  Collect sun by clicking the glowing sun orbs on screen
    🌱  Click a card (top bar) → click a grid cell to plant
    🧟  Survive 200 zombies — kill 50 to win
    ❌  If any zombie reaches the left side — you lose
```

**Status effects cheat sheet**:

| Effect | Source | Duration | Result |
|---|---|---|---|
| ❄️ Slow | Ice Pea hit | 60 ticks | Zombie moves at half speed |
| ⚡ Stun | Lightning Pea hit | 60 ticks | Zombie freezes in place |
| 💥 Explosion | Potato Mine (armed) | Instant | Massive area damage |

---

<div align="center">

Made with ☕ and way too many SplashKit sprites for **COS 20007 — Object-Oriented Programming**

</div>
