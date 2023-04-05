using System.Text;
using MonBatSim.Core;

namespace MonBatSim.ConsoleApp;

public class StatsInputProcessor : IConsoleInputProcessor
{
    private readonly StringBuilder _currentInput = new();
    private readonly StatDistribution _statDistribution;
    private StatMenuItem _statMenuItem = StatMenuItem.HealthPoints;
    private bool _inputValid;

    public StatsInputProcessor(StatDistribution statDistributionDistribution)
    {
        _statDistribution = statDistributionDistribution;
    }
    
    public bool IsInputValid()
    {
        return _inputValid;
    }

    public void Process()
    {
        PrintStatDistribution(_statDistribution, _statMenuItem, $"{_currentInput}");
        Console.CursorVisible = _statMenuItem is not StatMenuItem.None and not StatMenuItem.Confirm;
        var inputKey = Console.ReadKey();
        if (inputKey.Key is ConsoleKey.UpArrow or ConsoleKey.W)
        {
            _currentInput.Clear();
            _statMenuItem = MoveUp(_statMenuItem);
        }
        else if (inputKey.Key is ConsoleKey.DownArrow or ConsoleKey.S)
        {
            _currentInput.Clear();
            _statMenuItem = MoveDown(_statMenuItem);
        }
        else if (inputKey.Key is ConsoleKey.Backspace)
        {
            _currentInput.Remove(_currentInput.Length - 1, 1);
        }
        else if (inputKey.Key is ConsoleKey.Enter)
        {
            if (_statMenuItem == StatMenuItem.Confirm)
            {
                _inputValid = true;
                Console.CursorVisible = true;
                return;
            }

            float? inputValue = float.TryParse($"{_currentInput}", out var floatValue) ? floatValue : null;
            _currentInput.Clear();
            UpdateStatInDistribution(inputValue, _statMenuItem, _statDistribution);

            _statMenuItem = MoveDown(_statMenuItem);
        }
        else
        {
            _currentInput.Append(inputKey.KeyChar);
        }
    }

    private static void PrintStatDistribution(
        StatDistribution statDistribution,
        StatMenuItem statMenuItem,
        string currentInput)
    {
        var remaining = statDistribution.Max - statDistribution.Total;
        Console.Write($"Stat distribution ({remaining}): ");
        if (statMenuItem == StatMenuItem.Confirm)
        {
            (Console.ForegroundColor, Console.BackgroundColor) = (Console.BackgroundColor, Console.ForegroundColor);
            Console.Write("  OK  ");
            (Console.BackgroundColor, Console.ForegroundColor) = (Console.ForegroundColor, Console.BackgroundColor);
        }
        
        Console.WriteLine();

        var healthPointsValue = statMenuItem != StatMenuItem.HealthPoints
            ? $"{statDistribution.HealthPoints}"
            : currentInput;
        Console.WriteLine($"Health points (HP): {healthPointsValue}");
        var attackPointsValue = statMenuItem != StatMenuItem.AttackPoints
            ? $"{statDistribution.AttackPoints}"
            : currentInput;
        Console.WriteLine($"Attack points (AP): {attackPointsValue}");
        var defensePointsValue = statMenuItem != StatMenuItem.DefensePoints
            ? $"{statDistribution.DefensePoints}"
            : currentInput;
        Console.WriteLine($"Defense points (DP): {defensePointsValue}");
        var speedPointsValue = statMenuItem != StatMenuItem.Speed
            ? $"{statDistribution.Speed}"
            : currentInput;
        Console.WriteLine($"Speed: {speedPointsValue}");
        var horizontalCursorPosition = statMenuItem switch
        {
            StatMenuItem.DefensePoints => 21,
            StatMenuItem.Speed => 7,
            _ => 20
        } + currentInput.Length;
        var verticalCursorOffset = statMenuItem switch
        {
            StatMenuItem.HealthPoints => 4,
            StatMenuItem.AttackPoints => 3,
            StatMenuItem.DefensePoints => 2,
            StatMenuItem.Speed => 1,
            _ => 0
        };
        (Console.CursorLeft, Console.CursorTop) = (horizontalCursorPosition, Console.CursorTop - verticalCursorOffset);
    }

    private static void UpdateStatInDistribution(
        float? inputValue,
        StatMenuItem statMenuItem,
        StatDistribution statDistribution)
    {
        if (!inputValue.HasValue)
            return;

        switch (statMenuItem)
        {
            case StatMenuItem.HealthPoints:
                statDistribution.HealthPoints = inputValue.Value;
                break;
            case StatMenuItem.AttackPoints:
                statDistribution.AttackPoints = inputValue.Value;
                break;
            case StatMenuItem.DefensePoints:
                statDistribution.DefensePoints = inputValue.Value;
                break;
            case StatMenuItem.Speed:
                statDistribution.Speed = inputValue.Value;
                break;
            case StatMenuItem.None:
            case StatMenuItem.Confirm:
            default:
                break;
        }
    }

    private static StatMenuItem MoveUp(StatMenuItem item)
    {
        return item switch
        {
            StatMenuItem.None => StatMenuItem.Confirm,
            StatMenuItem.Confirm => StatMenuItem.Speed,
            StatMenuItem.HealthPoints => StatMenuItem.Confirm,
            StatMenuItem.AttackPoints => StatMenuItem.HealthPoints,
            StatMenuItem.DefensePoints => StatMenuItem.AttackPoints,
            StatMenuItem.Speed => StatMenuItem.DefensePoints,
            _ => default
        };
    }

    private static StatMenuItem MoveDown(StatMenuItem item)
    {
        return item switch
        {
            StatMenuItem.None => StatMenuItem.HealthPoints,
            StatMenuItem.Confirm => StatMenuItem.HealthPoints,
            StatMenuItem.HealthPoints => StatMenuItem.AttackPoints,
            StatMenuItem.AttackPoints => StatMenuItem.DefensePoints,
            StatMenuItem.DefensePoints => StatMenuItem.Speed,
            StatMenuItem.Speed => StatMenuItem.Confirm,
            _ => default
        };
    }
}