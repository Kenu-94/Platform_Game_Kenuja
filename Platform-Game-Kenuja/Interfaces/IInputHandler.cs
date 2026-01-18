using Microsoft.Xna.Framework.Input;

public interface IInputHandler
{
    bool IsJumpPressed { get; }
    bool IsLeftPressed { get; }
    bool IsRightPressed { get; }
    bool IsDownPressed { get; }
    bool IsAttackPressed { get; }
    bool IsEnterPressed { get; }
    void Update();
}
