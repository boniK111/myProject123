namespace TestSystemCSharp;

public class Tested
{
    public string name_ { get; set; } = "";
    public List<Category> available_categories_ { get; set; } = new();
    public int total_points_ { get; set; }

    public void Run(List<Category> categories)
    {
        Console.WriteLine($"\n=== РЕЖИМ ТЕСТИРУЕМОГО ({name_}) ===");
        List<Category> available = categories.Where(c => c.is_active_ && c.questions_.Count > 0).ToList();
        available_categories_ = available;

        if (available.Count == 0)
        {
            Console.WriteLine("Нет доступных категорий с вопросами.");
            Console.Write("Нажмите Enter, чтобы выйти: ");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Доступные категории:");
        for (int i = 0; i < available.Count; i++)
            Console.WriteLine($"{i + 1}. {available[i].name_} — {available[i].questions_.Count} вопросов");
        Console.Write("Номер категории: ");

        if (!int.TryParse(Console.ReadLine(), out int n) || n < 1 || n > available.Count)
        {
            Console.WriteLine("Некорректный номер.");
            return;
        }

        Category selected = available[n - 1];
        int points = startCategory(selected);
        int maxPoints = selected.questions_.Sum(q => q.points_);
        getResults();
    }

    public int startCategory(Category current_category)
    {
        current_category.start();
        total_points_ += current_category.end();
        return current_category.points_;
    }

    public void getResults()
    {
        Console.WriteLine($"Тестируемый: {name_}");
        Console.WriteLine($"Всего набрано баллов: {total_points_}");
    }
}