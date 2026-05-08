using System;

namespace ConsoleMenu;


public class PromptMenu(string? message, string? prompt, BusinessFunction action) : Menu(message, prompt)
{
    protected BusinessFunction action = action;

    public override void Run(IInputService input, IOutputService output, IMenuClient client)
    {
        output.PrintCommandPrompt(this);
        if (input.ParseString(out string message))
        {
            bool success = action.Invoke(message, out string result);
            output.PrintMessage(result);

            if (success) client.CloseMenu();
        }
        else
        {
            output.PrintMessage("Invalid input, please enter a string.");
        }
    }
}