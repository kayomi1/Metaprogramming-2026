using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

// Курси: скільки гривень коштує одиниця валюти
const double UsdToUah = 41.50;
const double EurToUah = 45.20;

Dictionary<string, double> ratesToUah = new()
{
    ["USD"] = UsdToUah,
    ["EUR"] = EurToUah,
    ["UAH"] = 1.0
};

Console.WriteLine("--- Конвертор валют ---");
Console.WriteLine("Доступні валюти: USD, EUR, UAH");

Console.Write("Введіть суму: ");
if (!double.TryParse(Console.ReadLine()?.Replace(',', '.'),
        System.Globalization.NumberStyles.Float,
        System.Globalization.CultureInfo.InvariantCulture, out double amount) || amount < 0)
{
    Console.WriteLine("Некоректна сума.");
    return;
}

string from = ReadCurrency("З якої валюти конвертувати: ");
string to = ReadCurrency("В яку валюту конвертувати: ");

// Спочатку перевожу в гривні, потім у цільову валюту
double inUah = amount * ratesToUah[from];
double result = inUah / ratesToUah[to];

Console.WriteLine($"{amount:F2} {from} = {result:F2} {to}");

string ReadCurrency(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string code = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();
        if (ratesToUah.ContainsKey(code))
            return code;
        Console.WriteLine("Невідома валюта. Доступні: USD, EUR, UAH.");
    }
}