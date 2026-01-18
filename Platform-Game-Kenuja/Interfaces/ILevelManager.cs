using Microsoft.Xna.Framework.Graphics;
using Platform_Game_Kenuja.Models;
using System.Collections.Generic;

public interface ILevelManager
{
    List<Coin> Coins { get; }
    List<Enemy> Enemies { get; }
    Platform[] Platforms { get; }
    List<MovingPlatformData> MovingPlatforms { get; }
    EndFlag EndFlag { get; }
    void LoadLevel1();
    void LoadLevel2();
    void Reset();
}
