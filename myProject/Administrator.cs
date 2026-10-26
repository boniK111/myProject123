namespace TestSystemCSharp;

public class Administrator
{
    public string name_ { get; set; } = "Администратор";

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
                case "1": Console.WriteLine("addCategory"); break;
                case "2": Console.WriteLine("editCategory"); break;
                case "3": Console.WriteLine("removeCategory"); break;
                case "4": Console.WriteLine("howCategories"); break;
                case "5": Console.WriteLine("ShowResults"); break;
                case "0": return;
                default: Console.WriteLine("Неверный пункт меню."); break;
            }
        }
    }
}