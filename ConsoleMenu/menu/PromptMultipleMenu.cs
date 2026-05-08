using System.Collections.Generic;

namespace ConsoleMenu;

public class PromptMultipleMenu(string? message, string? prompt, BusinessFunctionMultiple action) : Menu(message, prompt)
{
    protected int? amount;
    protected List<string> inputs = [];

    public override void Run(IInputService input, IOutputService output, IMenuClient client)
    {
        if (amount is null)
        {
            PromptAmount(input, output);
        }
        
        output.PrintMessage($"Ange ålder (besökare {inputs.Count + 1}): ");
        if (input.ParseString(out string message))
        {
            inputs.Add(message);
        }
        else
        {
            output.PrintMessage("Invalid input, please enter a string.");
        }

        if (inputs.Count >= amount)
        {
            
            bool success = action.Invoke(inputs.ToArray(), out string result);
            output.PrintMessage(result);
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

        amount = result;
    }

    protected void ResetInput()
    {
        amount = null;
        inputs = [];
    }
}