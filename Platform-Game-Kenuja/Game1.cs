using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Platform_Game_Kenuja.Models;
using Platform_Game_Kenuja.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Platform_Game_Kenuja
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private GameWorld _world;
        private InputHandler _input;
        private UIManager _ui;

        private Persoon _player;

        private Texture2D _background;
        private Texture2D _knifeTexture;
        private Texture2D _particleTexture;

        private SoundEffect _coinSound;
        private SoundEffect _enemyDeathSound;
        private SoundEffect _playerDeathSound;

        private SoundEffect _music;
        private SoundEffectInstance _musicInstance;

        private List<ExplosionParticle> _particles = new List<ExplosionParticle>();

        private GameState _state = GameState.Menu;
        private int _score = 0;

        public Game1(GameWorld world = null, InputHandler input = null)
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Dependency injection met fallback
            _world = world ?? new GameWorld();
            _input = input ?? new InputHandler();
            _ui = new UIManager();
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // World textures
            _world.CoinTexture = Content.Load<Texture2D>("coin");
            _world.EnemyTexture = Content.Load<Texture2D>("enemy");
            _world.PlatformTexture = Texture2D.FromStream(GraphicsDevice, File.OpenRead("Content/platform.png"));
            _world.FlagTexture = Content.Load<Texture2D>("checkpoint");

            // Other textures
            _background = Content.Load<Texture2D>("background");
            _knifeTexture = Content.Load<Texture2D>("knife");
            _particleTexture = Content.Load<Texture2D>("particle");

            // Sounds
            _coinSound = Content.Load<SoundEffect>("coins_sound");
            _enemyDeathSound = Content.Load<SoundEffect>("death_sound");
            _playerDeathSound = Content.Load<SoundEffect>("player_dead");

            _music = Content.Load<SoundEffect>("music"); // muziekbestand in Content (bijv. WAV)
            _musicInstance = _music.CreateInstance();
            _musicInstance.IsLooped = true;
            _musicInstance.Volume = 0.4f;
            _musicInstance.Play();

            // Player
            _player = new Persoon
            {
                Texture = Texture2D.FromStream(GraphicsDevice, File.OpenRead("Content/person.png")),
                Position = new Vector2(100, 100),
                Width = 90,
                Height = 175
            };

            // UI
            SpriteFont font = Content.Load<SpriteFont>("font");
            Texture2D menuBg = Content.Load<Texture2D>("menu_foto");
            Texture2D gameEndBg = Content.Load<Texture2D>("game_end");
            _ui.Initialize(font, menuBg, gameEndBg);

            _world.LoadLevel1();
        }

        protected override void Update(GameTime gameTime)
        {
            _input.Update();

            switch (_state)
            {
                case GameState.Menu:
                    if (_input.IsEnterPressed)
                    {
                        ResetGame();
                        _state = GameState.Level1;
                    }
                    break;

                case GameState.Level1:
                    UpdateGameplay(gameTime);
                    break;

                case GameState.ScoreScreen:
                    if (_input.IsEnterPressed)
                    {
                        _world.LoadLevel2();
                        _player.Position = new Vector2(100, 100);
                        _state = GameState.Level2;
                    }
                    break;

                case GameState.Level2:
                    UpdateGameplay(gameTime);
                    break;

                case GameState.GameEnd:
                    if (_input.IsEnterPressed)
                    {
                        _state = GameState.Menu;
                    }
                    break;

                case GameState.GameOver:
                    if (_input.IsEnterPressed)
                    {
                        _state = GameState.Menu;
                    }
                    break;
            }

            base.Update(gameTime);
        }


        private void UpdateGameplay(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Platforms (static + moving)
            List<Platform> platforms = new List<Platform>(_world.Platforms);
            foreach (var mp in _world.MovingPlatforms)
                platforms.Add(mp.Plat);

            _player.Update(gameTime, platforms.ToArray());

            _world.UpdateMovingPlatforms(dt);
            _world.UpdateEnemies(gameTime);

            CheckCoins();
            CheckEnemies();
            CheckEndFlag();

            UpdateParticles(gameTime);
        }

        private void CheckCoins()
        {
            for (int i = _world.Coins.Count - 1; i >= 0; i--)
            {
                if (_world.Coins[i].BoundingBox.Intersects(_player.BoundingBox))
                {
                    _coinSound.Play();
                    _score++;
                    _world.Coins.RemoveAt(i);
                }
            }
        }

        private void CheckEnemies()
        {
            for (int i = _world.Enemies.Count - 1; i >= 0; i--)
            {
                Enemy e = _world.Enemies[i];

                if (!(_player.IsAttacking) && e.BoundingBox.Intersects(_player.BoundingBox))
                {
                    _playerDeathSound.Play();
                    _state = GameState.GameOver;
                }
                else if (_player.IsAttacking && _player.AttackBox.Intersects(e.BoundingBox))
                {
                    _enemyDeathSound.Play();
                    SpawnExplosion(e.Position);
                    _score += 2;
                    _world.Enemies.RemoveAt(i);
                }
            }
        }

        private void CheckEndFlag()
        {
            if (_world.EndFlag == null || _world.EndFlag.Activated)
                return;

            if (_world.EndFlag.BoundingBox.Intersects(_player.BoundingBox))
            {
                _world.EndFlag.Activated = true;

                if (_state == GameState.Level1)
                    _state = GameState.ScoreScreen; // Level 1 voorbij
                else if (_state == GameState.Level2)
                    _state = GameState.GameEnd; // Level 2 voorbij → WIN
            }
        }



        private void UpdateParticles(GameTime gameTime)
        {
            for (int i = _particles.Count - 1; i >= 0; i--)
            {
                _particles[i].Update(gameTime);
                if (_particles[i].Lifetime <= 0)
                    _particles.RemoveAt(i);
            }
        }

        private void SpawnExplosion(Vector2 pos)
        {
            for (int i = 0; i < 20; i++)
            {
                _particles.Add(new ExplosionParticle
                {
                    Position = pos,
                    Velocity = new Vector2(RandomHelper(-200, 200), RandomHelper(-200, 200)),
                    Lifetime = 1f,
                    Scale = 0.4f,
                    Color = Color.OrangeRed
                });
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            switch (_state)
            {
                case GameState.Menu:
                    _ui.DrawMenu(_spriteBatch, _graphics);
                    break;

                case GameState.Level1:
                case GameState.Level2:
                    DrawGameplay();
                    break;

                case GameState.ScoreScreen:
                    DrawScoreScreen();
                    break;

                case GameState.GameEnd:
                    _ui.DrawGameOver(_spriteBatch, _graphics, _score);
                    break;

                case GameState.GameOver:
                    _ui.DrawGameOver(_spriteBatch, _graphics, _score);
                    break;
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawScoreScreen()
        {
            // Achtergrond tekenen
            _spriteBatch.Draw(
                _background,
                new Rectangle(
                    0,
                    0,
                    _graphics.PreferredBackBufferWidth,
                    _graphics.PreferredBackBufferHeight),
                Color.White
            );

            // Tekst
            _spriteBatch.DrawString(
                Content.Load<SpriteFont>("font"),
                "LEVEL 1 COMPLETED!",
                new Vector2(260, 180),
                Color.Gold,
                0f,
                Vector2.Zero,
                2f,
                SpriteEffects.None,
                0f
            );

            _spriteBatch.DrawString(
                Content.Load<SpriteFont>("font"),
                "Score: " + _score,
                new Vector2(320, 260),
                Color.White
            );

            _spriteBatch.DrawString(
                Content.Load<SpriteFont>("font"),
                "Press ENTER to start Level 2",
                new Vector2(240, 320),
                Color.Yellow
            );
        }




        private void ResetGame()
        {
            _score = 0;
            _particles.Clear();
            _player.Position = new Vector2(100, 100);
            _world.LoadLevel1();
            _state = GameState.Menu;
        }

        private void DrawGameplay()
        {
            // Background
            _spriteBatch.Draw(
                _background,
                new Rectangle(
                    0,
                    0,
                    _graphics.PreferredBackBufferWidth,
                    _graphics.PreferredBackBufferHeight),
                Color.White
            );

            // Platforms
            foreach (var platform in _world.Platforms)
                platform.Draw(_spriteBatch);

            // Moving platforms
            foreach (var mp in _world.MovingPlatforms)
                mp.Plat.Draw(_spriteBatch);

            // Coins
            foreach (var coin in _world.Coins)
                coin.Draw(_spriteBatch);

            // Enemies
            foreach (var enemy in _world.Enemies)
                enemy.Draw(_spriteBatch);

            // Particles
            foreach (var particle in _particles)
                particle.Draw(_spriteBatch, _particleTexture);

            // End flag
            _world.EndFlag?.Draw(_spriteBatch);

            // Player
            _player.Draw(_spriteBatch);
            _player.DrawKnife(_spriteBatch, _knifeTexture);

            // Score
            _spriteBatch.DrawString(
                Content.Load<SpriteFont>("font"),
                "Score: " + _score,
                new Vector2(20, 20),
                Color.Yellow
            );
        }


        private float RandomHelper(float min, float max)
        {
            return (float)new System.Random().NextDouble() * (max - min) + min;
        }
    }
}
