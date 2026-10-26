using System.Text.Json;

namespace TestSystemCSharp;

public class TestResult
{
    public string StudentName { get; set; } = "";
    public string CategoryName { get; set; } = "";
    public int Points { get; set; }
    public int MaxPoints { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
}

public class DataStorage
{
    private const string CategoriesFile = "categories.json";
    private const string ResultsFile = "results.json";
    private readonly JsonSerializerOptions options = new() { WriteIndented = true };

    public List<Category> loadCategories()
    {
        if (!File.Exists(CategoriesFile)) return new List<Category>();
        try
        {
            return JsonSerializer.Deserialize<List<Category>>(File.ReadAllText(CategoriesFile), options)
                   ?? new List<Category>();
        }
        catch
        {
            Console.WriteLine("Не удалось прочитать categories.json.");
            return new List<Category>();
        }
    }

    public void saveCategories(List<Category> categories)
    {
        File.WriteAllText(CategoriesFile, JsonSerializer.Serialize(categories, options));
    }

    public List<TestResult> LoadResults()
    {
        if (!File.Exists(ResultsFile)) return new List<TestResult>();
        try
        {
            return JsonSerializer.Deserialize<List<TestResult>>(File.ReadAllText(ResultsFile), options)
                   ?? new List<TestResult>();
        }
        catch
        {
            Console.WriteLine("Не удалось прочитать results.json.");
            return new List<TestResult>();
        }
    }

    public void SaveResult(TestResult result)
    {
        List<TestResult> results = LoadResults();
        results.Add(result);
        File.WriteAllText(ResultsFile, JsonSerializer.Serialize(results, options));
    }
}