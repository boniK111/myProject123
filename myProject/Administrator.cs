namespace TestSystemCSharp;

public class Administrator
{
    public string name_ { get; set; } = "Администратор";
    private readonly DataStorage storage;

    public Administrator(DataStorage storage)
    {
        this.storage = storage;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\n=== РЕЖИМ АДМИНИСТРАТОРА ===");
            Console.WriteLine("1. Добавить категорию");
            Console.WriteLine("2. Изменить категорию / добавить вопросы");
            Console.WriteLine("3. Удалить категорию");
            Console.WriteLine("4. Просмотреть категории");
            Console.WriteLine("5. Просмотреть результаты");
            Console.WriteLine("0. Назад");
            Console.Write("Выбор: ");

            switch (Console.ReadLine())
            {
                case "1": Console.WriteLine("[следующий шаг] addCategory"); break;
                case "2": Console.WriteLine("[следующий шаг] editCategory"); break;
                case "3": Console.WriteLine("[следующий шаг] removeCategory"); break;
                case "4": ShowCategories(); break;
                case "5": ShowResults(); break;
                case "0": return;
                default: Console.WriteLine("Неверный пункт меню."); break;
            }
        }
    }

    private void ShowCategories()
    {
        List<Category> categories = storage.loadCategories();
        if (categories.Count == 0) { Console.WriteLine("Категорий пока нет."); return; }
        foreach (Category category in categories) { category.showInfo(); Console.WriteLine(); }
    }

    private void ShowResults()
    {
        List<TestResult> results = storage.LoadResults();
        if (results.Count == 0) { Console.WriteLine("Результатов пока нет."); return; }
        foreach (TestResult result in results.OrderByDescending(r => r.Date))
            Console.WriteLine($"{result.Date:dd.MM.yyyy HH:mm} | {result.StudentName} | {result.CategoryName} | {result.Points}/{result.MaxPoints}");
    }
}