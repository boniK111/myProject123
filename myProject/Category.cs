namespace TestSystemCSharp;

public class Category
{
    public string name_ { get; set; } = "";
    public int points_ { get; set; }
    public List<Question> questions_ { get; set; } = new();
    public bool is_finished_ { get; set; }
    public bool is_active_ { get; set; } = true;

    public const int MinNameLength = 1;
    public const int MaxNameLength = 100;

    public int maxPoints() => questions_.Sum(q => q.points_);

    public bool isValid(out string error)
    {
        if (string.IsNullOrWhiteSpace(name_) || name_.Length < MinNameLength || name_.Length > MaxNameLength)
        { error = $"Название должно быть {MinNameLength}..{MaxNameLength} символов."; return false; }

        for (int i = 0; i < questions_.Count; i++)
        {
            if (!questions_[i].isValid(out string qerr))
            { error = $"Вопрос #{i + 1}: {qerr}"; return false; }
        }

        error = "";
        return true;
    }

    public void showInfo()
    {
        Console.WriteLine($"Категория: {name_}");
        Console.WriteLine($"Вопросов: {questions_.Count}");
        Console.WriteLine($"Максимум баллов: {maxPoints()}");
        Console.WriteLine($"Статус: {(is_active_ ? "доступна" : "неактивна")}");
    }

    public void showQuestions()
    {
        if (questions_.Count == 0) { Console.WriteLine("Вопросов нет."); return; }
        for (int i = 0; i < questions_.Count; i++)
        {
            Console.WriteLine($"\n{i + 1}. {questions_[i].content_}");
            for (int j = 0; j < questions_[i].answers_.Count; j++)
            {
                string mark = j == questions_[i].correctAnswerIndex_ ? " ✔" : "";
                Console.WriteLine($"   {j + 1}) {questions_[i].answers_[j]}{mark}");
            }
            Console.WriteLine($"   Баллы: {questions_[i].points_}");
        }
    }

    public void addQuestion()
    {
        Console.WriteLine("\n--- Новый вопрос ---");
        Question question = new();
        question.edit();

        if (!question.isValid(out string error))
        {
            Console.WriteLine($"Вопрос не добавлен: {error}");
            return;
        }

        questions_.Add(question);
        Console.WriteLine("Вопрос добавлен.");
    }

    public void removeQuestion(int index)
    {
        if (index >= 0 && index < questions_.Count)
        {
            questions_.RemoveAt(index);
            Console.WriteLine("Вопрос удалён.");
        }
        else Console.WriteLine("Некорректный номер вопроса.");
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
            Console.WriteLine($"=== Категория: {name_} ===");
            Question question = questions_[index];
            question.show(index + 1, questions_.Count);
            Console.WriteLine("\nКоманды: N — следующий, P — предыдущий, E — завершить.");
            Console.Write("Ваш выбор: ");
            string choice = (Console.ReadLine() ?? "").Trim();

            if (choice.Equals("E", StringComparison.OrdinalIgnoreCase)) break;

            if (choice.Equals("P", StringComparison.OrdinalIgnoreCase))
            { index = Math.Max(0, index - 1); continue; }

            if (choice.Equals("N", StringComparison.OrdinalIgnoreCase))
            { index++; continue; }

            if (int.TryParse(choice, out int answer) && answer >= 1 && answer <= question.answers_.Count)
            {
                question.correct_ = (answer - 1) == question.correctAnswerIndex_;
                question.is_resolved_ = true;
                index++;
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Нажмите Enter.");
                Console.ReadLine();
            }
        }

        points_ = questions_.Sum(q => q.countPoints());
        is_finished_ = true;
        
        showStats();
        Console.WriteLine($"\nТест завершён. Баллы: {points_} из {maxPoints()}");
    }

    public int end()
    {
        is_finished_ = true;
        points_ = questions_.Sum(q => q.countPoints());
        return points_;
    }
    public int answeredCount() => questions_.Count(q => q.is_resolved_);
    public int correctCount() => questions_.Count(q => q.correct_);
    public int wrongCount() => questions_.Count(q => q.is_resolved_ && !q.correct_);
    public int skippedCount() => questions_.Count(q => !q.is_resolved_);

    public void showStats()
    {
        Console.WriteLine($"=== Статистика: {name_} ===");
        Console.WriteLine($"Всего вопросов: {questions_.Count}");
        Console.WriteLine($"Отвечено:       {answeredCount()}");
        Console.WriteLine($"  ✔ верно:      {correctCount()}");
        Console.WriteLine($"  ✘ неверно:    {wrongCount()}");
        Console.WriteLine($"Пропущено:      {skippedCount()}");
        Console.WriteLine($"Баллы:          {points_} из {maxPoints()}");
    }
}