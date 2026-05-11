using System.Collections.Generic;

namespace ConsoleMenu.CLI;

public class CLIClient(Menu.Menu StartMenu) : IMenuClient
{
    readonly private CLIParser parser = new();
    readonly private CLIPrinter output = new();
    private Stack<Menu.Menu> menus = new([StartMenu]);

    public void Run()
    {
        while(menus.Count > 0)
        {
            var currentMenu = menus.Peek();
            currentMenu.Run(parser, output, this);
        }
    }

    public void QueueMenu(Menu.Menu menu)
    {
        menus.Push(menu);
    }

    public void CloseMenu()
    {
        menus.Pop();
    }
}