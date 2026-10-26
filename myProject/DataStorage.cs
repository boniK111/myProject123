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
    private readonly JsonSerializerOptions options = new()
    {
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    // ---------- Категории ----------

    public List<Category> loadCategories()
    {
        if (!File.Exists(CategoriesFile)) return new List<Category>();
        try
        {
            List<Category>? list = JsonSerializer.Deserialize<List<Category>>(
                File.ReadAllText(CategoriesFile), options);

            if (list == null) return new List<Category>();

            // Валидация после загрузки — отбрасываем битые категории
            List<Category> valid = new();
            foreach (Category c in list)
            {
                if (c.isValid(out string error))
                    valid.Add(c);
                else
                    Console.WriteLine($"[warn] Категория «{c.name_}» пропущена: {error}");
            }
            return valid;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Не удалось прочитать {CategoriesFile}: {ex.Message}");
            return new List<Category>();
        }
    }

    public void saveCategories(List<Category> categories)
    {
        // Валидация перед записью
        foreach (Category c in categories)
            if (!c.isValid(out string error))
                throw new InvalidOperationException($"Категория «{c.name_}»: {error}");

        // Атомарная запись: пишем во временный файл, затем подменяем
        string temp = CategoriesFile + ".tmp";
        File.WriteAllText(temp, JsonSerializer.Serialize(categories, options));
        File.Move(temp, CategoriesFile, overwrite: true);
    }

    // ---------- Результаты ----------

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
            Console.WriteLine($"Не удалось прочитать {ResultsFile}.");
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