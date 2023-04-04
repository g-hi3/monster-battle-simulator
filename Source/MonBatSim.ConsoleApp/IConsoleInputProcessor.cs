namespace MonBatSim.ConsoleApp;

public interface IConsoleInputProcessor
{
    string Label { get; }
    bool IsInputValid();
    void Process();
}