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
            categories = loadCategories(); // автоподгрузка при каждом возврате в меню
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
                        Tested tested = new() { name_ = currentUserName };
                        tested.Run(categories);
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
}