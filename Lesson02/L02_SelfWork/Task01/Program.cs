using System.Globalization;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

Console.WriteLine("--- Конвертер температур ---");

decimal value = 0;
char from = 'C';

// Питаємо, поки температура не буде допустимою
while (true)
{
    value = ReadNumber("Введіть число: ");
    from = ReadUnit("Початкова одиниця (C, F, K): ");

    if (IsAboveAbsoluteZero(value, from))
        break;

    Console.WriteLine("Помилка: температура нижча за абсолютний нуль. Спробуйте ще раз.");
}

char to = ReadUnit("Одиниця результату (C, F, K): ");

decimal result;
if (from == to)
{
    result = value; // однакові одиниці
}
else
{
    // Спочатку переводимо в Кельвіни, потім у потрібну шкалу
    decimal kelvin = from switch
    {
        'C' => value + 273.15m,
        'F' => (value + 459.67m) * 5m / 9m,
        _ => value
    };

    result = to switch
    {
        'C' => kelvin - 273.15m,
        'F' => kelvin * 9m / 5m - 459.67m,
        _ => kelvin
    };
}

Console.WriteLine($"Результат: {value:F2} {from} = {result:F2} {to}");

//Допоміжні методи

static decimal ReadNumber(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string input = (Console.ReadLine() ?? "").Replace(',', '.');
        if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal number))
            return number;
        Console.WriteLine("Помилка: введіть число.");
    }
}

static char ReadUnit(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string input = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();
        if (input == "C" || input == "F" || input == "K")
            return input[0];
        Console.WriteLine("Помилка: введіть C, F або K.");
    }
}

static bool IsAboveAbsoluteZero(decimal value, char unit)
{
    if (unit == 'C') return value >= -273.15m;
    if (unit == 'F') return value >= -459.67m;
    return value >= 0m; // K
}