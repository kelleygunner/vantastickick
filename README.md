# Vantastic kick
Penalty Kick game prototype
by Anatoly Ivanov
# Goal
Create a full-loop game
# Requirments
- Core mechanic
- UI
- Animations
- Configuration
# Dev Info
Engine Version: Unity 2021.2.18
Target Platform: Android
# Project Structure
Open scene Scenes/Main.
Project is being setup in System/SceneContent.
There are 3 Installers:
- Config Installer
- Game Installer
- UI Installer
# Game Loop
- UI Installer opens Main Menu screen
- By Pressing Play button user starts the game.
- When user runs out of attempts, user goes to Finish screen
- By pressing Finish button, player opens Main Menu
# Architecture features
- Dependency Inversion pattern
- MVC pattern
- SOLID principles
# Technologies
- Extenject (ex. Zenject)
- DOTween
- Json.Net
# Implementation
# Architecture
- DI.
The architecture design is based on the 5th SOLID principle - Dependency Inversion.
Classes don't have direct dependencies to other classes but use an interface of communication, so
DI container provides with implementations. Extenject (ex. Zenject) is used as DI container plugin.
- MVC. 
MVC variation is used to detach business logic from visual elements. There is a loop: 
Controller -> Model -> View -> Controler.
For example user makes a shot, so
1. Controller checks if it reaches a target and update Model.
2. Model calculates score, combo etc and updates View
3. View shows animations, updates score counters etc, than updates controller, that
visual part is done, so controller can handle user input again.
- UI.
For making each UI screen to be easier there is UiFramework, that provides abstract classes:
UiController -> UiModel -> UiView.
In order to add a new screen, developer should create 3 child classes (Model could be null),
and override OnOpen and OnClose methods in View class to visually show the screen.
To Implement MVC (read above) View subscribes to Model, Controller subscribes to View.
Controller can directly change Model;
- MonoBehaviour vs NonMonoBehavour classes.
Model and Controllers aren't MonoBehaviour classes, so they can be easily tested.
For example, instead of having Runtime Manual testing, we can create a Test that
calls Controller method HandleShot with test values. This method calls Model method
to calculate points. Than Test compare results in Model with expecting values.
So we can avoid using View classes (which are MonoBehaviours) to test Business logic
(Without input and Visual part).
- SOLID.
The project follows SOLID principles where it's really needed (There are no complications).
Most of the classes have only one responsibility. They don't have direct connections between each other. They arent overloaded by
multifunctional logic.
# Configuration
Game is adjustable. There is a config file which is presented as a JSON.
1. Gameplay:
- Ball Velocity, velocity multiplier
- Scatter Factor, represents a random scatter
2. Game round:
- Attempts" : 10, number of attempt per game round
- BasicPoints : 50, point given to user for regular scoring 
- TargetPoints : 100, ponts given to user for hitting a target
- Combo Bonus Points : [0,20,50,150,400], extra points given to player for hitting a target in row
