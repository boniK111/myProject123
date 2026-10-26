namespace TestSystemCSharp;

public class Category
{
    public string name_ { get; set; } = "";
    public int points_ { get; set; }
    public List<Question> questions_ { get; set; } = new();
    public bool is_finished_ { get; set; }
    public bool is_active_ { get; set; } = true;

    public void showInfo()
    {
        Console.WriteLine($"Категория: {name_}");
        Console.WriteLine($"Вопросов: {questions_.Count}");
        Console.WriteLine($"Максимум баллов: {questions_.Sum(q => q.points_)}");
        Console.WriteLine($"Статус: {(is_active_ ? "доступна" : "неактивна")}");
    }

    public void start()
    {
        if (questions_.Count == 0)
        {
            Console.WriteLine("В этой категории нет вопросов.");
            return;
        }

        is_finished_ = false;
        points_ = 0;
        int index = 0;

        while (index < questions_.Count)
        {
            Console.Clear();
            Console.WriteLine($"Категория: {name_} | Вопрос {index + 1} из {questions_.Count}");
            Question question = questions_[index];
            Console.WriteLine(question.content_);
            for (int i = 0; i < question.answers_.Count; i++)
                Console.WriteLine($"{i + 1}. {question.answers_[i]}");

            Console.WriteLine("\nВведите номер ответа. Команды: N — следующий, P — предыдущий, E — завершить.");
            Console.Write("Ваш выбор: ");
            string choice = (Console.ReadLine() ?? "").Trim();

            if (choice.Equals("E", StringComparison.OrdinalIgnoreCase))
                break;
            if (choice.Equals("P", StringComparison.OrdinalIgnoreCase))
            {
                index = Math.Max(0, index - 1);
                continue;
            }
            if (choice.Equals("N", StringComparison.OrdinalIgnoreCase))
            {
                index++;
                continue;
            }

            if (int.TryParse(choice, out int answer) && answer >= 1 && answer <= question.answers_.Count)
            {
                question.correct_ = answer - 1 == question.correctAnswerIndex_;
                question.is_resolved_ = true;
                index++;
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Нажмите Enter и попробуйте снова.");
                Console.ReadLine();
            }
        }

        points_ = questions_.Sum(q => q.countPoints());
        is_finished_ = true;
        Console.WriteLine($"Тест завершён. Баллы: {points_} из {questions_.Sum(q => q.points_)}");
    }

    public int end()
    {
        is_finished_ = true;
        points_ = questions_.Sum(q => q.countPoints());
        return points_;
    }

    public void addQuestion()
    {
        Question question = new();
        question.edit();
        questions_.Add(question);
    }

    public void removeQuestion(int index)
    {
        if (index >= 0 && index < questions_.Count)
            questions_.RemoveAt(index);
    }
}