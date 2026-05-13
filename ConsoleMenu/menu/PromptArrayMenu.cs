namespace ConsoleMenu.Menu;

/// <summary>
/// Menu class for prompting the user for multiple pieces of data in one line.
/// The input will be split into an array of strings, with empty entries
/// removed and whitespace characters trimmed.
/// </summary>
/// <param name="Message">Not used currently. </param>
/// <param name="Prompt">Prompt for user input. </param>
/// <param name="Action">Business logic to apply to the input. </param>
public class PromptArrayMenu(string? Message, string? Prompt, BusinessFunctionMultipleInput Action) : Menu(Message, Prompt)
{

    public override void Run(IInputService input, IOutputService output, IMenuClient client)
    {
        output.PrintCommandPrompt(this);
        if (input.ParseStringMultiple(out string[] message))
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