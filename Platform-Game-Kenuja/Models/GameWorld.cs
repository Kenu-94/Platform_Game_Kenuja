using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platform_Game_Kenuja.Models;
using Platform_Game_Kenuja.Interfaces;
using System.Collections.Generic;

public class GameWorld : ILevelManager
{
    public List<Coin> Coins { get; private set; } = new List<Coin>();
    public List<Enemy> Enemies { get; private set; } = new List<Enemy>();
    public Platform[] Platforms { get; private set; }
    public List<MovingPlatformData> MovingPlatforms { get; private set; } = new List<MovingPlatformData>();
    public EndFlag EndFlag { get; private set; }

    public Texture2D CoinTexture { get; set; }
    public Texture2D EnemyTexture { get; set; }
    public Texture2D PlatformTexture { get; set; }
    public Texture2D FlagTexture { get; set; }

    // ---------------- LEVEL 1 ----------------
    public void LoadLevel1()
    {
        Coins.Clear();
        Enemies.Clear();
        MovingPlatforms.Clear();

        Platforms = new Platform[]
        {
            new Platform { Texture = PlatformTexture, Position = new Vector2(0,400), Width=1200, Height=70 },
            new Platform { Texture = PlatformTexture, Position = new Vector2(200,300), Width=100, Height=40 },
            new Platform { Texture = PlatformTexture, Position = new Vector2(350,250), Width=100, Height=40 },
            new Platform { Texture = PlatformTexture, Position = new Vector2(500,300), Width=100, Height=40 }
        };

        AddCoinLine(120, 260, 25);
        AddCoinLine(50, 370, 15);

        Enemies.Add(new PatrolEnemy(EnemyTexture, new Vector2(300, 360), 180, 450));
        Enemies.Add(new FastEnemy(EnemyTexture, new Vector2(500, 360), 300, 650));
        Enemies.Add(new TrapEnemy(EnemyTexture, new Vector2(480, 360)));

        EndFlag = new EndFlag
        {
            Texture = FlagTexture,
            Position = new Vector2(710, 330),
            Scale = 0.2f,
            Activated = false
        };
    }

    // ---------------- LEVEL 2 ----------------
    public void LoadLevel2()
    {
        Coins.Clear();
        Enemies.Clear();
        MovingPlatforms.Clear();

        Platforms = new Platform[]
        {
            new Platform { Texture = PlatformTexture, Position = new Vector2(0, 450), Width = 1200, Height = 70 },  // statisch
            new Platform { Texture = PlatformTexture, Position = new Vector2(200, 350), Width = 120, Height = 40 },
            new Platform { Texture = PlatformTexture, Position = new Vector2(400, 280), Width = 120, Height = 40 },
            new Platform { Texture = PlatformTexture, Position = new Vector2(600, 200), Width = 120, Height = 40 }
        };

        // Coins
        AddCoinLine(100, 410, 12);
        AddCoinLine(220, 320, 6);
        AddCoinLine(420, 250, 6);
        AddCoinLine(620, 170, 8);

        // Moving platforms
        MovingPlatforms.Add(new MovingPlatformData { Plat = Platforms[1], Left = 150, Right = 350, Speed = 80 });
        MovingPlatforms.Add(new MovingPlatformData { Plat = Platforms[2], Left = 350, Right = 500, Speed = 70 });
        MovingPlatforms.Add(new MovingPlatformData { Plat = Platforms[3], Left = 550, Right = 700, Speed = 60 });

        // Enemies (max 1 TrapEnemy)
        Enemies.Add(new PatrolEnemy(EnemyTexture, new Vector2(300, 410), 200, 500));
        Enemies.Add(new FastEnemy(EnemyTexture, new Vector2(700, 160), 620, 820));
        Enemies.Add(new TrapEnemy(EnemyTexture, new Vector2(450, 200))); // 1 valstrik

        // EndFlag voor level einde
        EndFlag = new EndFlag
        {
            Texture = FlagTexture,
            Position = new Vector2(710, 380),
            Scale = 0.2f,
            Activated = false
        };

    }

    // ---------------- UPDATE MOVING PLATFORMS ----------------
    public void UpdateMovingPlatforms(float dt)
    {
        foreach (var mp in MovingPlatforms)
        {
            if (mp.MoveRight)
            {
                mp.Plat.Position.X += mp.Speed * dt; //dt: tijd sinds vorige frame
                if (mp.Plat.Position.X >= mp.Right)
                    mp.MoveRight = false;
            }
            else
            {
                mp.Plat.Position.X -= mp.Speed * dt;
                if (mp.Plat.Position.X <= mp.Left)
                    mp.MoveRight = true;
            }
        }
    }

    // ---------------- UPDATE ENEMIES ----------------
    public void UpdateEnemies(GameTime gameTime)
    {
        foreach (var e in Enemies)
            e.Update(gameTime);
    }

    // ---------------- HELPER ----------------
    private void AddCoinLine(int startX, int y, int count)
    {
        for (int i = 0; i < count; i++)
            Coins.Add(new Coin { Texture = CoinTexture, Position = new Vector2(startX + i * 30, y) });
    }

    public void Reset()
    {
        Coins.Clear();
        Enemies.Clear();
        MovingPlatforms.Clear();
        EndFlag = null;
        Platforms = null;
    }
}
