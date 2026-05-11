
using ConsoleMenu.Menu;
namespace ConsoleMenu;

public interface IOutputService
{
    void PrintOptions(OptionsMenu menu);
    void PrintOptions(ActionsMenu menu);
    void PrintCommandPrompt(Menu.Menu menu);
    void PrintCommandPrompt(string message);
    void PrintMessage(string message);
}