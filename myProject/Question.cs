namespace TestSystemCSharp;

public class Question
{
    public string content_ { get; set; } = "";
    public List<string> answers_ { get; set; } = new();
    public int correctAnswerIndex_ { get; set; }
    public bool correct_ { get; set; }
    public bool is_resolved_ { get; set; }
    public int points_ { get; set; } = 1;

    public int countPoints() => correct_ ? points_ : 0;

    public void edit()
    {
        Console.Write("Текст вопроса: ");
        content_ = Console.ReadLine() ?? "";
        answers_.Clear();

        for (int i = 0; i < 4; i++)
        {
            Console.Write($"Вариант {i + 1}: ");
            answers_.Add(Console.ReadLine() ?? "");
        }

        Console.Write("Номер правильного ответа (1-4): ");
        if (int.TryParse(Console.ReadLine(), out int answer) && answer >= 1 && answer <= 4)
            correctAnswerIndex_ = answer - 1;
        else
            correctAnswerIndex_ = 0;

        Console.Write("Баллы за вопрос: ");
        if (!int.TryParse(Console.ReadLine(), out int points) || points < 1)
            points = 1;
        points_ = points;
        correct_ = false;
        is_resolved_ = false;
    }
}