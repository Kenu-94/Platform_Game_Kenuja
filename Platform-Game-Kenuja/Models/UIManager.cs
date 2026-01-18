using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Net.Mime.MediaTypeNames;

public class UIManager
{
    private SpriteFont _font;
    private Texture2D _menuBackground;
    private Texture2D _gameEndBackground;
    private float _pressAlpha = 1f;
    private float _alphaDirection = -1f;

    public void Initialize(SpriteFont font, Texture2D menuBg, Texture2D gameEndBg)
    {
        _font = font; _menuBackground = menuBg; _gameEndBackground = gameEndBg;
    }

    public void DrawMenu(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
    {
        spriteBatch.Draw(_menuBackground, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
        spriteBatch.DrawString(_font, "PLATFORM ADVENTURE", new Vector2(180, 80), Color.Black, 0f, Vector2.Zero, 2.1f, SpriteEffects.None, 0f);
        spriteBatch.DrawString(_font, "PLATFORM ADVENTURE",
                new Vector2(180, 80), Color.Black, 0f, Vector2.Zero, 2.1f, SpriteEffects.None, 0f);

        spriteBatch.DrawString(_font, "CONTROLS:", new Vector2(200, 150), Color.Yellow);
        spriteBatch.DrawString(_font, "A = Attack", new Vector2(200, 180), Color.White);
        spriteBatch.DrawString(_font, "UP = Jump", new Vector2(200, 210), Color.White);
        spriteBatch .DrawString(_font, "RIGHT = Move Right", new Vector2(200, 270), Color.White);
        spriteBatch.DrawString(_font, "LEFT = Move Left", new Vector2(200, 240), Color.White);
        spriteBatch.DrawString(_font, "Press ENTER to Start",
            new Vector2(260, 330), Color.Yellow * _pressAlpha);
        _pressAlpha += _alphaDirection * 0.04f;
        if (_pressAlpha <= 0.3f || _pressAlpha >= 1f) _alphaDirection *= -1;
    }

    public void DrawGameOver(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, int score)
    {
        spriteBatch.Draw(_gameEndBackground, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
        spriteBatch.DrawString(_font, "GAME OVER!", new Vector2(300, 200), Color.Red, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);
        spriteBatch.DrawString(_font, $"Final Score: {score}", new Vector2(320, 280), Color.Yellow);
        spriteBatch.DrawString(_font, "ENTER = Menu", new Vector2(320, 350), Color.White);
    }
}
