using System;
using System.Globalization;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

Console.WriteLine("--- Калькулятор ---");

double a = ReadNumber("Введіть перше число: ");
double b = ReadNumber("Введіть друге число: ");

Console.WriteLine("Оберіть дію: + (додавання), - (віднімання), * (множення), / (ділення)");
string? op   = Console.ReadLine()?.Trim();

double result;
switch (op)
{
    case "+": result = a + b; break;
    case "-": result = a - b; break;
    case "*": result = a * b; break;
    case "/":
        if (b == 0)
        {
            Console.WriteLine("Ділення на нуль неможливе.");
            return;
        }
        result = a / b;
        break;
    default:
        Console.WriteLine("Невідома операція.");
        return;
}

Console.WriteLine($"Результат: {a} {op} {b} = {result}");

static double ReadNumber(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine()?.Replace(',', '.');
        if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
            return value;
        Console.WriteLine("Некоректне число, спробуйте ще раз.");
    }
}