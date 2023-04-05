namespace MonBatSim.ConsoleApp;

public interface IConsoleInputProcessor
{
    bool IsInputValid();
    void Process();
}