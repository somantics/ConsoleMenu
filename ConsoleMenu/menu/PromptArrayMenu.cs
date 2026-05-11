namespace ConsoleMenu.Menu;

public class PromptArrayMenu(string? message, string? prompt, BusinessFunctionMultipleInput action) : Menu(message, prompt)
{

    public override void Run(IInputService input, IOutputService output, IMenuClient client)
    {
        output.PrintCommandPrompt(this);
        if (input.ParseStringMultiple(out string[] message))
        {
            bool success = action.Invoke(message, out string result);
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