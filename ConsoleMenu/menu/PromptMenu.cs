using System;

namespace ConsoleMenu.Menu;

/// <summary>
/// Menu class for prompting the user for a single input.
/// The input will be piped to the business function as
/// a whole string.  
/// </summary>
/// <param name="Message">Message describing the required input. </param>
/// <param name="Prompt">Prompt for user input. </param>
/// <param name="Action">Business logic to apply to the input. </param>
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