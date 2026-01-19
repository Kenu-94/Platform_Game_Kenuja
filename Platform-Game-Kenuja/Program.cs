using Platform_Game_Kenuja.Models;
using Platform_Game_Kenuja;

var world = new GameWorld();
var input = new InputHandler();
var game = new Game1(world, input);
game.Run();

//SOLID Principes
/*
 S (SRP): Coin, EndFlag, InputHandler, UIManager -> enkel over coin, enemy, input, UI.... -> Een duidelijke verantwoordelijkheid
 O (Open-Closed Principe): Enemy, gameManager -> Nieuwe vijanden toevoegen zonder code te wijzigen en GameManager -> Nieuwe level toevoegen zonder te wijzigen.
 L (Liskov substitution principle): PatrolEnemy, TrapEnemy,...-> Kan overal waar enemy (Subkalssen bruikbaar ipv. parentklasse)
 I (Interface seggration): IGameObject, IInputHandler, ILevelManager -> Alleen nodige methoden per klasse
 D (Dependency Injection): Game1 krijgt GameWorld en InputHandler
 */