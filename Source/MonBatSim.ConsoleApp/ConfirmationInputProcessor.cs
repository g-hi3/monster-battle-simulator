namespace MonBatSim.ConsoleApp;

public class ConfirmationInputProcessor : IConsoleInputProcessor
{
    private string? _input;
    
    public bool IsConfirmed => _input is "Y";

    public bool IsInputValid()
    {
        return _input is "Y" or "N";
    }

    public void Process()
    {
        _input = Console.ReadLine()?.Trim().ToUpper();
    }
}