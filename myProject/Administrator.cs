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
                case "1": addCategory(); break;
                case "2": editCategory(); break;
                case "3": removeCategory(); break;
                case "4": ShowCategories(); break;
                case "5": ShowResults(); break;
                case "0": return;
                default: Console.WriteLine("Неверный пункт меню."); break;
            }
        }
    }

    public void addCategory()
    {
        Console.Write("Название новой категории: ");
        string name = (Console.ReadLine() ?? "").Trim();
        if (name.Length == 0) { Console.WriteLine("Название не может быть пустым."); return; }

        List<Category> categories = storage.loadCategories();
        if (categories.Any(c => c.name_.Equals(name, StringComparison.OrdinalIgnoreCase)))
        { Console.WriteLine("Такая категория уже существует."); return; }

        categories.Add(new Category { name_ = name });
        storage.saveCategories(categories);
        Console.WriteLine("Категория добавлена.");
    }

    public void editCategory(string category_name = "")
    {
        List<Category> categories = storage.loadCategories();
        Category? category = FindCategory(categories, category_name);
        if (category == null) return;

        while (true)
        {
            Console.WriteLine($"\nКатегория: {category.name_}");
            Console.WriteLine("1. Добавить вопрос");
            Console.WriteLine("2. Удалить вопрос");
            Console.WriteLine("3. Изменить название");
            Console.WriteLine("4. Включить/выключить категорию");
            Console.WriteLine("0. Сохранить и выйти");
            Console.Write("Выбор: ");
            switch (Console.ReadLine())
            {
                case "1": category.addQuestion(); break;
                case "2":
                    for (int i = 0; i < category.questions_.Count; i++)
                        Console.WriteLine($"{i + 1}. {category.questions_[i].content_}");
                    Console.Write("Номер вопроса: ");
                    if (int.TryParse(Console.ReadLine(), out int n)) category.removeQuestion(n - 1);
                    break;
                case "3":
                    Console.Write("Новое название: ");
                    string newName = (Console.ReadLine() ?? "").Trim();
                    if (newName.Length > 0) category.name_ = newName;
                    break;
                case "4": category.is_active_ = !category.is_active_; break;
                case "0": storage.saveCategories(categories); return;
                default: Console.WriteLine("Неверный пункт меню."); break;
            }
        }
    }

    public void removeCategory(string category_name = "")
    {
        List<Category> categories = storage.loadCategories();
        Category? category = FindCategory(categories, category_name);
        if (category == null) return;
        Console.Write($"Удалить категорию «{category.name_}»? (да/нет): ");
        if ((Console.ReadLine() ?? "").Trim().Equals("да", StringComparison.OrdinalIgnoreCase))
        {
            categories.Remove(category);
            storage.saveCategories(categories);
            Console.WriteLine("Категория удалена.");
        }
    }

    private Category? FindCategory(List<Category> categories, string categoryName)
    {
        if (categories.Count == 0) { Console.WriteLine("Категорий пока нет."); return null; }
        if (string.IsNullOrWhiteSpace(categoryName))
        {
            for (int i = 0; i < categories.Count; i++)
                Console.WriteLine($"{i + 1}. {categories[i].name_} ({categories[i].questions_.Count} вопросов)");
            Console.Write("Выберите номер категории: ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n < 1 || n > categories.Count)
            { Console.WriteLine("Некорректный номер."); return null; }
            return categories[n - 1];
        }
        return categories.FirstOrDefault(c => c.name_.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
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