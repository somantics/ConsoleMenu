using System;

namespace ConsoleMenu.Menu;


public class PromptMenu(string? Message, string? Prompt, BusinessFunction Action) : Menu(Message, Prompt)
{
    public override void Run(IInputService input, IOutputService output, IMenuClient client)
    {
        output.PrintCommandPrompt(this);
        if (input.ParseString(out string message))
        {
            bool success = Action.Invoke(message, out string result);
            output.PrintMessage(result);

            if (success)
            {
                client.CloseMenu();
                
            }
            else
            {
                CheckFailedAttempts(client);
            }
        }
        else
        {
            output.PrintMessage("Invalid input, please enter a string.");
            CheckFailedAttempts(client);
        }
    }
}