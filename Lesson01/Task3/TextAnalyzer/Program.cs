using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

Console.WriteLine("Введіть текст (порожній рядок — завершити введення):");

var lines = new List<string>();
string? line;
while (!string.IsNullOrEmpty(line = Console.ReadLine()))
{
    lines.Add(line);
}

string text = string.Join("\n", lines);

char[] separators = [' ', '\t', '\n', '\r'];
int wordCount = text.Split(separators, StringSplitOptions.RemoveEmptyEntries).Length;
int spaceCount = text.Count(c => c == ' ');
int totalChars = text.Length;
int visibleChars = text.Count(c => !char.IsWhiteSpace(c));
int lineCount = lines.Count;

Console.WriteLine();
Console.WriteLine("--- Результати аналізу ---");
Console.WriteLine($"Кількість слів:            {wordCount}");
Console.WriteLine($"Кількість пробілів:        {spaceCount}");
Console.WriteLine($"Символів (усіх):           {totalChars}");
Console.WriteLine($"Символів (лише видимих):   {visibleChars}");
Console.WriteLine($"Кількість рядків:          {lineCount}");