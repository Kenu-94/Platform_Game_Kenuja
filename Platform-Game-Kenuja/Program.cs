using Platform_Game_Kenuja.Models;
using Platform_Game_Kenuja;

var world = new GameWorld();
var input = new InputHandler();
var game = new Game1(world, input);
game.Run();
