
namespace TestSystemCSharp;

public class MainMenu
{
    public string user_mode { get; private set; } = "";

    public void Run()
    {
        while (true)
        {
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
                        Console.WriteLine($"[вход выполнен] Режим: {user_mode}");
                        logout();
                    }
                    break;
                case "2":
                    if (login("tested"))
                    {
                        user_mode = "Tested";
                        Console.WriteLine($"[вход выполнен] Режим: {user_mode}");
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
            Console.WriteLine($"Добро пожаловать, {name}.");
        }
        return true;
    }

    public void logout()
    {
        user_mode = "";
        Console.WriteLine("Вы вышли из режима.");
    }
}