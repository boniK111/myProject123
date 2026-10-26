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