namespace ConsoleMenu;

public interface IMenuClient
{
    void QueueMenu(Menu menu);
    void CloseMenu();

}