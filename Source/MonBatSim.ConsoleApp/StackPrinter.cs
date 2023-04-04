namespace MonBatSim.ConsoleApp;

public class StackPrinter
{
    private readonly Stack<string> _stack = new();

    public void Print()
    {
        Console.Clear();
        foreach (var item in _stack.Reverse())
            Console.Write(item);
    }

    public void Push(string item)
    {
        _stack.Push(item);
    }

    public void Pop()
    {
        _ = _stack.Pop();
    }
}