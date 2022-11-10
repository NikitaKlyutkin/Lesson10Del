public enum Operation
{
    Sum,
    Subtract,
    Multiply
}
public class OperationManager
{
    private int _first;
    private int _second;
    private ExecutionManager _manager;
    public OperationManager(int first, int second)
    {
        _first = first;
        _second = second;
        _manager = new ExecutionManager();
        _manager.PopulateFunctions(Sum, Subtract, Multiply);
        _manager.PrepareExecution();
    }
    private int Sum()
    {
        return _first + _second;
    }
    private int Subtract()
    {
        return _first - _second;
    }
    private int Multiply()
    {
        return _first * _second;
    }
    public int Execute(Operation operation)
    {
       return _manager.FuncExecute.TryGetValue(operation, out Func<int> del) ? del.Invoke() : -1;
    }
}

//Implement functionality
public class ExecutionManager
{
    public Dictionary<Operation, Func<int>> FuncExecute { get; set; }
    private Func<int> _sum;
    private Func<int> _subtract;
    private Func<int> _multiply;
    public ExecutionManager()
    {
        FuncExecute = new Dictionary<Operation, Func<int>>(3);
    }
    public void PopulateFunctions(Func<int> Sum, Func<int> Subtract, Func<int> Multiply)
    {
        _sum = Sum;
        _subtract = Subtract;
        _multiply = Multiply;
    }
    public void PrepareExecution()
    {
        FuncExecute.Add(Operation.Sum, _sum);
        FuncExecute.Add(Operation.Subtract, _subtract);
        FuncExecute.Add(Operation.Multiply, _multiply);
    }

}

class Program
{
    static void Main(string[] args)
    {
        var opManager = new OperationManager(20, 10);
        var result = opManager.Execute(Operation.Sum);
        Console.WriteLine($"The result of the operation is {result}");
        Console.ReadKey();
    }
}