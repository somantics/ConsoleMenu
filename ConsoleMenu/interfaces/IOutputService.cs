namespace ConsoleMenu;

public interface IOutputService
{
    void PrintOptions(OptionsMenu menu);
    void PrintCommandPrompt(Menu menu);
    void PrintMessage(string message);
}