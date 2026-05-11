Ett bibliotek för CLI menyer som använder sig av delegates för att abstrahera bort enskilda programs business logic.

# Overview
Bliblioteket är indelat i tre delar:
* menyer
* I/O
* Pipelining

## I/O
Input och output hanteras av tre delar: en client, som håller kåll på vilken menu som ska visas, en printer, som sköter output, och en input eller parser, som läser user input.
Dessa definieras i varsit interface: IMenuClient, IInputService, IOutputService. En default implementation för respektive kommer med i CLIClient, CLIPrinter, CLIParser.

## Menyer
Alla menyer ärver av en abstrakt basklass Menu. Varje menu representerar en fråga från systemet till användaren, ibland en fråga x antal gånger. 

För huvudmenyer används typerna OptionsMenu eller Actions menu.
För input prompts används en av de tre prompt menyerna.

Varje meny som ska ha fler options som kan anges via ett kommande specificerar dessa som MenuOptions eller MenuCommands.

## Pipeline
Backendens externa funktioner behöver exponeras till blilioteket på ett sätt som matchar en av tre delegater:

bool BusinessFunction (string input, out string result)
bool BusinessFunctionMultipleInput (string[] input, out string result)
bool BusinessFunctionNoInput (out string result)

Dessa kan sedan injectas in i MenuCommands eller MenuOptions. Bool motsvarar huruvida operationen lyckades eller stötte på ett fel. 
