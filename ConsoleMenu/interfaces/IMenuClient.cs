namespace ConsoleMenu;

public interface IMenuClient
{
    void QueueMenu(Menu.Menu menu);
    void CloseMenu();

}