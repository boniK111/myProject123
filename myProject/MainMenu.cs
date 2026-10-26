namespace TestSystemCSharp;

public class MainMenu
{
    public string user_mode { get; private set; } = "";
    public List<Category> categories { get; private set; } = new();
    private readonly DataStorage storage = new();
    private string currentUserName = "";

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

            switch (choice)
            {
                case "1":
                    if (login("admin"))
                    {
                        user_mode = "Administrator";
                        new Administrator(storage).Run();
                        logout();
                    }
                    break;
                case "2":
                    if (login("tested"))
                    {
                        user_mode = "Tested";
                        RunTestedMode();
                        logout();
                    }
                    break;
                case "0":
                    Console.WriteLine("До свидания!");
                    return;
                default:
                    Console.WriteLine("Неверный пункт меню.");
                    break;
            }
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
            currentUserName = "Администратор";
            Console.WriteLine("Добро пожаловать, Администратор.");
        }
        else if (mode == "tested")
        {
            Console.Write("Введите ваше имя: ");
            string name = (Console.ReadLine() ?? "").Trim();
            if (name.Length == 0)
            {
                Console.WriteLine("Имя не может быть пустым.");
                return false;
            }
            currentUserName = name;
            Console.WriteLine($"Добро пожаловать, {name}.");
        }
        return true;
    }

    public void logout()
    {
        user_mode = "";
        currentUserName = "";
        Console.WriteLine("Вы вышли из режима.");
    }

    public List<Category> loadCategories() => storage.loadCategories();

    private void RunTestedMode()
    {
        Tested tested = new() { name_ = currentUserName };
        List<Category> available = loadCategories().Where(c => c.is_active_ && c.questions_.Count > 0).ToList();
        tested.available_categories_ = available;

        if (available.Count == 0) { Console.WriteLine("Нет доступных категорий с вопросами."); return; }

        Console.WriteLine("Доступные категории:");
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
        Console.WriteLine("Результат сохранён в results.json.");
    }
}