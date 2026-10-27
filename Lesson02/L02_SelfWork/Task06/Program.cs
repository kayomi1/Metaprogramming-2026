using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

//Рекурсія завжди наближається до базового випадку, тому що це є єдиним способом запобігти безкінечному виконанню програми
const int MaxValue = 1_000_000_000;

Console.WriteLine("--- Рекурсивна лабораторія ---");

int n = ReadInt($"Введіть n (0-{MaxValue}): ", 0, MaxValue);
Console.WriteLine($"Сума цифр: {SumDigits(n)}");
Console.WriteLine($"Двійкове представлення: {ToBinary(n)}");

int a = ReadInt($"Перше число для НСД (0-{MaxValue}): ", 0, MaxValue);
int b = ReadInt($"Друге число для НСД (0-{MaxValue}): ", 0, MaxValue);
Console.WriteLine($"НСД рекурсивно: {Gcd(a, b)}");
Console.WriteLine($"НСД ітеративно: {GcdIterative(a, b)}");

//Порівняння двох версій НСД на 5 наборах
Console.WriteLine("\n--- Перевірка на 5 наборах ---");
int[] firsts = { 84, 17, 0, 100, 1071 };
int[] seconds = { 30, 5, 9, 0, 462 };

for (int i = 0; i < firsts.Length; i++)
{
    int recursive = Gcd(firsts[i], seconds[i]);
    int iterative = GcdIterative(firsts[i], seconds[i]);
    string status = recursive == iterative ? "OK" : "ПОМИЛКА";
    Console.WriteLine($"НСД({firsts[i]}, {seconds[i]}): рекурсія={recursive}, ітерація={iterative} -> {status}");
}

//Рекурсивні методи

static int SumDigits(int n)
{
    if (n < 10) return n;                  //одна цифра
    return n % 10 + SumDigits(n / 10);     //остання цифра + сума решти
}

static string ToBinary(int n)
{
    if (n < 2) return n.ToString();        //0 або 1
    return ToBinary(n / 2) + (n % 2).ToString();
}

static int Gcd(int a, int b)
{
    if (b == 0) return a;                  //НСД(a, 0) = a
    return Gcd(b, a % b);                  //алгоритм Евкліда
}

//Ітеративна версія

static int GcdIterative(int a, int b)
{
    while (b != 0)
    {
        int remainder = a % b;
        a = b;
        b = remainder;
    }
    return a;
}

//Допоміжний метод

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