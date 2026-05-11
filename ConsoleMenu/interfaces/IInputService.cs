namespace ConsoleMenu;
public interface IInputService
{
    bool ParseAmount(out int amount);
    bool ParseString(out string input);

    bool ParseStringMultiple(out string[] input);

}