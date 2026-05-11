

namespace ConsoleMenu.Menu;

public class PromptMultipleMenu(string? message, string? prompt, string? repeatPrompt, BusinessFunctionMultipleInput action) : Menu(message, prompt)
{
    protected int? _amount;
    protected List<string> _inputs = [];

    public override void Run(IInputService input, IOutputService output, IMenuClient client)
    {
        if (_amount is null)
        {
            PromptAmount(input, output);
        }
        
        PromptDataPoint(input, output);

        if (_inputs.Count >= _amount)
        {
            bool success = RunBusinessLogic(output);
            ResetInput();

            if (success)
            {
                client.CloseMenu();
            }
            
        }
    }

    protected void PromptAmount(IInputService input, IOutputService output)
    {
        output.PrintCommandPrompt(this);
        if (!input.ParseAmount(out int result))
        {
            output.PrintMessage("Not a valid amount.");
            return;
        }

        _amount = result;
    }

    protected void PromptDataPoint(IInputService input, IOutputService output)
    {
        output.PrintCommandPrompt(repeatPrompt ?? "Enter data: ");
        if (input.ParseString(out string message))
        {
            _inputs.Add(message);
        }
        else
        {
            output.PrintMessage("Invalid input, please enter a string.");
        }
    }

    protected bool RunBusinessLogic(IOutputService output)
    {
        bool success = action.Invoke(_inputs.ToArray(), out string result);
        output.PrintMessage(result);
        return success;
    }

    protected void ResetInput()
    {
        _amount = null;
        _inputs = [];
    }
}