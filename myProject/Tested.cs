namespace TestSystemCSharp;

public class Tested
{
    public string name_ { get; set; } = "";
    public int total_points_ { get; set; }

    public void Run()
    {
        Console.WriteLine($"\n=== РЕЖИМ ТЕСТИРУЕМОГО ({name_}) ===");
        Console.WriteLine("Здесь будет выбор категории и прохождение теста.");
        Console.Write("Нажмите Enter, чтобы выйти: ");
        Console.ReadLine();
    }

    public void getResults()
    {
        Console.WriteLine($"Тестируемый: {name_}");
        Console.WriteLine($"Всего набрано баллов: {total_points_}");
    }
}