using System;

namespace ConsoleMenu.CLI;

public class CLIParser: IInputService
{
    
    public bool ParseAmount(out int amount)
    {
        string? rawInput = Console.ReadLine();

        if (int.TryParse(rawInput, out int result))
        {
            amount = result;
            return true;
        }

        amount = 0;
        return false;
    }

    public bool ParseString(out string input)
    {
        string? rawInput = Console.ReadLine();

        if (rawInput is not null)
        {
            input = rawInput;
            return true;
        }

        input = "";
        return false;
    }

    public bool ParseStringMultiple(out string[] input)
    {
        string? rawInput = Console.ReadLine();

        if (rawInput is not null)
        {
            input = rawInput.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            return true;
        }

        input = [];
        return false;
    }
}