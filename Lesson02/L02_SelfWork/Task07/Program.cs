using System.Globalization;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

ExpenseStore store = new ExpenseStore();

// Якщо запуск із --seed, додаємо демонстраційні записи
if (args.Contains("--seed"))
{
    store.Add(new DateOnly(2026, 10, 20), 350m, ExpenseCategory.Food, "Продукти");
    store.Add(new DateOnly(2026, 10, 21), 120.50m, ExpenseCategory.Transport, "Проїзд");
    store.Add(new DateOnly(2026, 10, 22), 775m, ExpenseCategory.Study, "Підручник");
    WriteSuccess("Додано 3 демонстраційні записи.");

    // with створює копію з іншою сумою, оригінал не змінюється
    Expense original = store.Items[0];
    Expense changed = original with { Amount = 400m };
    Console.WriteLine($"Оригінал: {original.Amount} грн, копія: {changed.Amount} грн");
}

bool running = true;
while (running)
{
    ShowMenu();
    string choice = (Console.ReadLine() ?? "").Trim();

    switch (choice)
    {
        case "1": AddExpense(store); break;
        case "2": ShowList(store.Items); break;
        case "3": FilterByCategory(store); break;
        case "4": RemoveExpense(store); break;
        case "5": ShowReport(store); break;
        case "6": running = false; break;
        default: WriteWarning("Такого пункту немає."); break;
    }
}

WriteInfo("Роботу завершено.");

// Меню і дії

static void ShowMenu()
{
    Console.WriteLine("\n--- Облік витрат ---");
    Console.WriteLine("1 - Додати витрату");
    Console.WriteLine("2 - Показати всі");
    Console.WriteLine("3 - Фільтр за категорією");
    Console.WriteLine("4 - Видалити за Id");
    Console.WriteLine("5 - Звіт");
    Console.WriteLine("6 - Завершити");
    Console.Write("Ваш вибір: ");
}

static void AddExpense(ExpenseStore store)
{
    DateOnly date = ReadDate();
    decimal amount = ReadAmount();
    ExpenseCategory category = ReadCategory();
    string description = ReadDescription();

    Expense added = store.Add(date, amount, category, description);
    WriteSuccess($"Додано витрату #{added.Id}");
}

static void ShowList(List<Expense> items)
{
    if (items.Count == 0)
    {
        WriteWarning("Список порожній.");
        return;
    }

    foreach (Expense e in items)
    {
        Console.WriteLine($"#{e.Id} | {e.Date:dd.MM.yyyy} | {e.Amount,10:N2} грн | {e.Category} | {e.Description}");
    }
}

static void FilterByCategory(ExpenseStore store)
{
    ExpenseCategory category = ReadCategory();

    List<Expense> filtered = new List<Expense>();
    foreach (Expense e in store.Items)
    {
        if (e.Category == category)
            filtered.Add(e);
    }

    if (filtered.Count == 0)
        WriteWarning($"У категорії {category} витрат немає.");
    else
        ShowList(filtered);
}

static void RemoveExpense(ExpenseStore store)
{
    if (!int.TryParse(Ask("Id для видалення: "), out int id))
    {
        WriteError("Id має бути цілим числом.");
        return;
    }

    if (store.Remove(id))
        WriteSuccess($"Витрату #{id} видалено.");
    else
        WriteWarning($"Витрати з Id {id} не знайдено.");
}

static void ShowReport(ExpenseStore store)
{
    if (store.Items.Count == 0)
    {
        WriteWarning("Звіт порожній: записів немає.");
        return;
    }

    decimal total = 0;
    decimal biggest = 0;
    Dictionary<ExpenseCategory, decimal> byCategory = new Dictionary<ExpenseCategory, decimal>();

    foreach (Expense e in store.Items)
    {
        total += e.Amount;
        if (e.Amount > biggest) biggest = e.Amount;

        if (byCategory.ContainsKey(e.Category))
            byCategory[e.Category] += e.Amount;
        else
            byCategory[e.Category] = e.Amount;
    }

    // Список не порожній, тому ділення на нуль неможливе
    decimal average = total / store.Items.Count;

    Console.WriteLine("\n=== Звіт ===");
    Console.WriteLine($"Загальна сума: {total:N2} грн");
    Console.WriteLine($"Середня витрата: {average:N2} грн");
    Console.WriteLine($"Найбільша витрата: {biggest:N2} грн");
    Console.WriteLine("Суми за категоріями:");
    foreach (var pair in byCategory)
    {
        Console.WriteLine($"  {pair.Key}: {pair.Value:N2} грн");
    }
}

// Читання даних із перевіркою

static DateOnly ReadDate()
{
    while (true)
    {
        string input = Ask("Дата (дд.мм.рррр): ");
        if (DateOnly.TryParseExact(input, "dd.MM.yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateOnly date))
            return date;

        WriteError("Невірна дата. Приклад: 25.10.2026");
    }
}

static decimal ReadAmount()
{
    while (true)
    {
        string input = Ask("Сума (більше 0): ").Replace(',', '.');
        if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal amount)
            && amount > 0)
            return amount;

        WriteError("Сума має бути числом більше 0.");
    }
}

static ExpenseCategory ReadCategory()
{
    Console.WriteLine("1 - Food (їжа)");
    Console.WriteLine("2 - Transport (транспорт)");
    Console.WriteLine("3 - Study (навчання)");
    Console.WriteLine("4 - Entertainment (розваги)");
    Console.WriteLine("5 - Other (інше)");

    while (true)
    {
        string input = Ask("Оберіть категорію (1-5): ");
        ExpenseCategory? picked = input switch
        {
            "1" => ExpenseCategory.Food,
            "2" => ExpenseCategory.Transport,
            "3" => ExpenseCategory.Study,
            "4" => ExpenseCategory.Entertainment,
            "5" => ExpenseCategory.Other,
            _ => null
        };

        if (picked is not null)
            return picked.Value;

        WriteError("Введіть номер від 1 до 5.");
    }
}

static string ReadDescription()
{
    while (true)
    {
        string text = Ask("Опис: ");
        if (text.Length > 0) return text;
        WriteError("Опис не може бути порожнім.");
    }
}

static string Ask(string prompt)
{
    Console.Write(prompt);
    return (Console.ReadLine() ?? "").Trim();
}

//Кольорові повідомлення

// Зберігаємо початковий колір і повертаємо його після повідомлення
static void WriteColor(ConsoleColor color, string tag, string message)
{
    ConsoleColor original = Console.ForegroundColor;
    Console.ForegroundColor = color;
    Console.WriteLine($"[{tag}] {message}");
    Console.ForegroundColor = original;
}

static void WriteInfo(string message) => WriteColor(ConsoleColor.Cyan, "INFO", message);
static void WriteSuccess(string message) => WriteColor(ConsoleColor.Green, "OK", message);
static void WriteWarning(string message) => WriteColor(ConsoleColor.Yellow, "WARN", message);
static void WriteError(string message) => WriteColor(ConsoleColor.Red, "ERROR", message);