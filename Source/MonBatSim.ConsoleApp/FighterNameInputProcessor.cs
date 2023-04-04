namespace MonBatSim.ConsoleApp;

public class FighterNameInputProcessor : IConsoleInputProcessor
{
    public string FighterName { get; private set; } = string.Empty;

    public string Label => "Enter fighter name: ";
    
    public bool IsInputValid()
    {
        return string.IsNullOrWhiteSpace(FighterName);
    }

    public void Process()
    {
        FighterName = Console.ReadLine() ?? string.Empty;
    }
}