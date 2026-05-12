

namespace ConsoleMenu.Menu;

public class PromptMultipleMenu(string? Message, string? Prompt, string? RepeatPrompt, BusinessFunctionMultipleInput Action) : Menu(Message, Prompt)
{
    protected int? _amount;
    protected List<string> _inputs = [];

    public override void Run(IInputService input, IOutputService output, IMenuClient client)
    {
        if (_amount is null)
        {
            PromptAmount(input, output, client);
        }
        
        PromptDataPoint(input, output, client);

        if (_inputs.Count >= _amount)
        {
            bool success = RunBusinessLogic(output);
            ResetInput();

            if (success)
            {
                client.CloseMenu();
            }
            else
            {
                CheckFailedAttempts(client);
            }
            
        }
    }

    protected void PromptAmount(IInputService input, IOutputService output, IMenuClient client)
    {
        output.PrintCommandPrompt(this);
        if (!input.ParseAmount(out int result))
        {
            output.PrintMessage("Not a valid amount.");
            CheckFailedAttempts(client);
            return;
        }

        _amount = result;
    }

    protected void PromptDataPoint(IInputService input, IOutputService output, IMenuClient client)
    {
        output.PrintCommandPrompt(RepeatPrompt ?? "Enter data: ");
        if (input.ParseString(out string message))
        {
            _inputs.Add(message);
        }
        else
        {
            output.PrintMessage("Invalid input, please enter a string.");
            CheckFailedAttempts(client);
        }
    }

    protected bool RunBusinessLogic(IOutputService output)
    {
        bool success = Action.Invoke(_inputs.ToArray(), out string result);
        output.PrintMessage(result);
        return success;
    }

    protected void ResetInput()
    {
        _amount = null;
        _inputs = [];
        _failedAttempts = 0;
    }
}