using System.Collections.Generic;

namespace ConsoleMenu.CLI;

public class CLIClient(Menu.Menu StartMenu) : IMenuClient
{
    readonly private CLIParser _parser = new();
    readonly private CLIPrinter _output = new();
    readonly private Stack<Menu.Menu> _menus = new([StartMenu]);

    public void Run()
    {
        while(_menus.Count > 0)
        {
            var currentMenu = _menus.Peek();
            currentMenu.Run(_parser, _output, this);
        }
    }

    public void QueueMenu(Menu.Menu menu)
    {
        _menus.Push(menu);
    }

    public void CloseMenu()
    {
        _menus.Pop();
    }
}