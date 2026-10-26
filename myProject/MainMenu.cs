namespace TestSystemCSharp;

public class MainMenu
{
    public string user_mode { get; private set; } = "";
    public List<Category> categories { get; private set; } = new();
    private readonly DataStorage storage = new();
    //...
    public void Run()
    {
        while (true)
        {
            categories = loadCategories();
            Console.WriteLine("\n=== СИСТЕМА ТЕСТИРОВАНИЯ ===");
            Console.WriteLine("1. Администратор");
            Console.WriteLine("2. Тестируемый");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите режим: ");
            string choice = Console.ReadLine() ?? "";

            if (choice == "1")
            { 
                if (login("admin"))
                {
                    user_mode = "Administrator";
                    new Administrator(storage).Run();
                    logout();
                }
            }
            else if (choice == "2")
            {
                if (login("tested"))
                {
                    user_mode = "Tested";
                    RunTestedMode();
                    logout();
                }
            }
            else if (choice == "0") return;
            else Console.WriteLine("Неверный пункт меню.");
        }
    }

    public bool login(string mode)
    {
        if (mode == "admin")
        {
            Console.Write("Пароль администратора (по умолчанию admin): ");
            if ((Console.ReadLine() ?? "") != "admin")
            {
                Console.WriteLine("Неверный пароль.");
                return false;
            }
        }
        return true;
    }

    public void logout()
    {
        user_mode = "";
        Console.WriteLine("Вы вышли из режима.");
    }

    public List<Category> loadCategories() => storage.loadCategories();

    public void saveCategories() => storage.saveCategories(categories);

    private void RunTestedMode()
    {
        Console.Write("Имя тестируемого: ");
        Tested tested = new() { name_ = (Console.ReadLine() ?? "").Trim() };
        if (tested.name_.Length == 0) tested.name_ = "Без имени";

        List<Category> available = loadCategories().Where(c => c.is_active_ && c.questions_.Count > 0).ToList();
        tested.available_categories_ = available;
        if (available.Count == 0) { Console.WriteLine("Нет доступных категорий с вопросами."); return; }

        Console.WriteLine("\nДоступные категории:");
        for (int i = 0; i < available.Count; i++)
            Console.WriteLine($"{i + 1}. {available[i].name_} — {available[i].questions_.Count} вопросов");
        Console.Write("Номер категории: ");
        if (!int.TryParse(Console.ReadLine(), out int n) || n < 1 || n > available.Count)
        { Console.WriteLine("Некорректный номер."); return; }

        Category selected = available[n - 1];
        int points = tested.startCategory(selected);
        int maxPoints = selected.questions_.Sum(q => q.points_);
        tested.getResults();
        storage.SaveResult(new TestResult
        {
            StudentName = tested.name_,
            CategoryName = selected.name_,
            Points = points,
            MaxPoints = maxPoints,
            Date = DateTime.Now
        });
    }
}
