using Microsoft.Xna.Framework.Input;
using Platform_Game_Kenuja.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_Game_Kenuja.Models
{
    public class InputHandler : IInputHandler
    {
        private KeyboardState _currentKeyboard;
        private KeyboardState _previousKeyboard;

        public bool IsJumpPressed => _currentKeyboard.IsKeyDown(Keys.Up);
        public bool IsLeftPressed => _currentKeyboard.IsKeyDown(Keys.Left);
        public bool IsRightPressed => _currentKeyboard.IsKeyDown(Keys.Right);
        public bool IsDownPressed => _currentKeyboard.IsKeyDown(Keys.Down); //DOWN!
        public bool IsAttackPressed => _currentKeyboard.IsKeyDown(Keys.A);
        public bool IsEnterPressed => _currentKeyboard.IsKeyDown(Keys.Enter);

        public void Update()
        {
            _previousKeyboard = _currentKeyboard;
            _currentKeyboard = Keyboard.GetState();
        }
    }

}
