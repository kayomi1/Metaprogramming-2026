using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

Console.WriteLine("--- Контроль доступу до події ---");

int age = ReadInt("Вік (0-120): ", 0, 120);
int hour = ReadInt("Година початку (0-23): ", 0, 23);
bool hasTicket = ReadYesNo("Є квиток? (так/ні): ");
bool isBlocked = ReadYesNo("Є Блокування? (так/ні): ");
bool hasAdult = ReadYesNo("Супровід дорослого? (так/ні): ");

// Перевіряємо правила по черзі. Перша причина відмови зупиняє перевірку.
string? reason = null;
if (isBlocked)
    reason = "обліковий запис заблоковано";
else if (!hasTicket)
    reason = "відсутній квиток";
else if (age < 14 && !hasAdult)
    reason = "вік до 14 років без дорослого";
else if (age < 18 && hour >= 22)
    reason = "подія о 22:00 або пізніше для осіб молодше 18 років";

//Категорія за віком
string category = age switch
{
    <= 13 => "дитина",
    <= 17 => "підліток",
    <= 64 => "дорослий",
    _ => "старша особа"
};

Console.WriteLine(reason is null
    ? $"Доступ дозволено. Категорія: {category}."
    : $"Доступ заборонено. Причина: {reason}.");

//Допоміжні методи

static int ReadInt(string prompt, int min, int max)
{
    while (true)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out int number) && number >= min && number <= max)
            return number;
        Console.WriteLine($"Помилка: введіть ціле число від {min} до {max}.");
    }
}

static bool ReadYesNo(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string answer = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();
        if (answer == "так") return true;
        if (answer == "ні") return false;
        Console.WriteLine("Помилка: введіть «так» або «ні».");
    }
}