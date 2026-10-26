namespace TestSystemCSharp;

public class Question
{
    public string content_ { get; set; } = "";
    public List<string> answers_ { get; set; } = new();
    public int correctAnswerIndex_ { get; set; }
    public bool correct_ { get; set; }
    public bool is_resolved_ { get; set; }
    public int points_ { get; set; } = 1;

    public const int RequiredAnswers = 4;
    public const int MinPoints = 1;
    public const int MaxPoints = 100;

    public int countPoints() => correct_ ? points_ : 0;

    public bool isValid(out string error)
    {
        if (string.IsNullOrWhiteSpace(content_) || content_.Length > 500)
        { error = "Текст вопроса пуст или длиннее 500 символов."; return false; }

        if (answers_.Count != RequiredAnswers)
        { error = $"Должно быть ровно {RequiredAnswers} варианта ответа."; return false; }

        for (int i = 0; i < answers_.Count; i++)
        {
            if (string.IsNullOrWhiteSpace(answers_[i]) || answers_[i].Length > 200)
            { error = $"Вариант {i + 1} пуст или длиннее 200 символов."; return false; }
        }

        if (correctAnswerIndex_ < 0 || correctAnswerIndex_ >= RequiredAnswers)
        { error = $"Правильный ответ должен быть 1..{RequiredAnswers}."; return false; }

        if (points_ < MinPoints || points_ > MaxPoints)
        { error = $"Баллы должны быть {MinPoints}..{MaxPoints}."; return false; }

        error = "";
        return true;
    }

    public void edit()
    {
        // Сбрасываем прежние runtime-флаги
        correct_ = false;
        is_resolved_ = false;

        while (true)
        {
            Console.Write("Текст вопроса: ");
            string text = (Console.ReadLine() ?? "").Trim();
            if (text.Length == 0) { Console.WriteLine("Текст не может быть пустым."); continue; }
            if (text.Length > 500) { Console.WriteLine("Слишком длинный текст (макс. 500)."); continue; }
            content_ = text;
            break;
        }

        answers_.Clear();
        for (int i = 0; i < RequiredAnswers; i++)
        {
            while (true)
            {
                Console.Write($"Вариант {i + 1}: ");
                string ans = (Console.ReadLine() ?? "").Trim();
                if (ans.Length == 0) { Console.WriteLine("Вариант не может быть пустым."); continue; }
                if (ans.Length > 200) { Console.WriteLine("Слишком длинный вариант (макс. 200)."); continue; }
                answers_.Add(ans);
                break;
            }
        }

        while (true)
        {
            Console.Write($"Номер правильного ответа (1-{RequiredAnswers}): ");
            if (int.TryParse(Console.ReadLine(), out int ans) && ans >= 1 && ans <= RequiredAnswers)
            { correctAnswerIndex_ = ans - 1; break; }
            Console.WriteLine("Некорректный номер.");
        }

        while (true)
        {
            Console.Write($"Баллы за вопрос ({MinPoints}-{MaxPoints}, по умолчанию 1): ");
            string s = (Console.ReadLine() ?? "").Trim();
            if (s.Length == 0) { points_ = 1; break; }
            if (int.TryParse(s, out int p) && p >= MinPoints && p <= MaxPoints) { points_ = p; break; }
            Console.WriteLine("Некорректные баллы.");
        }
    }

    public void show(int number, int total)
    {
        Console.WriteLine($"[Вопрос {number} из {total}] {content_} ({points_} б.)");
        for (int i = 0; i < answers_.Count; i++)
            Console.WriteLine($"{i + 1}. {answers_[i]}");
    }
}