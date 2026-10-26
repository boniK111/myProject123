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
}