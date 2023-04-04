namespace MonBatSim.ConsoleApp;

public class ConfirmationInputProcessor : IConsoleInputProcessor
{
    private string? _input;

    public ConfirmationInputProcessor(string label)
    {
        Label = label;
    }
    
    public bool IsConfirmed => _input is "Y";

    public string Label { get; }

    public bool IsInputValid()
    {
        return _input is not "Y" and not "N";
    }

    public void Process()
    {
        _input = Console.ReadLine()?.Trim().ToUpper();
    }
}