# Unity Skills Showcase

## Gameplay Demo

<p align="center">
  <img src="Repository/gameplay.gif" alt="Gameplay Demo" width="600">
</p>

## Overview

This is a personal project designed to showcase technical proficiency in Unity game development and software architecture patterns.

## Setup Instructions

1. Clone this repository
2. Open with Unity 2022.3 or later
3. Allow scripts to compile
4. Run from any scene from `Assets/Scenes/`

## Technical Stack

- **Unity Version**: 2022.3 LTS
- **C# Architecture**:
  - MVVM (Model-View-ViewModel) Pattern
  - Clean Architecture
- **Dependencies**:
  - [UniTask](https://github.com/Cysharp/UniTask) - Zero allocation async/await integration
  - [Zenject](https://github.com/modesttree/Zenject) - Dependency injection framework
  - [UniRx](https://github.com/neuecc/UniRx) - Reactive Extensions for Unity
- **Data Management**:
  - JSON configuration loading system

## Architecture Features

1. The project code is divided into states (e.g. Loading, Gamehub, Gameplay), each of which represents a certain stage of the gameplay.  
2. Active use of asynchronous programming to load levels or resources
3. Centralised configuration system using JSON format
4. Zenject is used as the main tool for DI and for splitting code into logic and presentation
5. MVVM pattern used for UI elements
6. Firebase Analytics is used for analytics
7. Unity Ads is used for advertisments


## Project Structure

```
Assets/
├── Scripts/
│   ├── Core/            # Core game systems
│   ├── Game/            # Business logic
├── ├── ├── Bootstrap/   # Game initialization logic
├── ├── ├── Common/      # Common code that used across modules
├── ├── ├── GameConfigs/ # Code representation for configs
├── ├── ├── GameHub/     # Code related to game hub (main menu) module
├── ├── ├── Gameplay/    # Code related to core gameplay
├── ├── ├── Loading/     # Code related to loading across scenes
├── Resources/           # Game Resources
├── ├── Configs/         # Game configs (level, player, enemies, etc)
└── Scenes/              # Game scenes
```

## License

[MIT License](LICENSE)

## Contact

Feel free to reach out with any questions or feedback!