using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

Console.WriteLine("--- Статистика послідовності ---");

int n = ReadInt("Скільки чисел (5-100): ", 5, 100);
int[] numbers = new int[n];
for (int i = 0; i < n; i++)
{
    numbers[i] = ReadInt($"Число {i + 1}: ", int.MinValue, int.MaxValue);
}

//Прохід по масиву для мінімуму, максимуму, суми і лічильників
int min = numbers[0];
int max = numbers[0];
long sum = 0;
int positive = 0;
int negative = 0;
int zero = 0;
int even = 0;
int odd = 0;

foreach (int x in numbers)
{
    if (x < min) min = x;
    if (x > max) max = x;
    sum += x;

    if (x > 0) positive++;
    else if (x < 0) negative++;
    else zero++;

    if (x % 2 == 0) even++;
    else odd++;
}

double average = (double)sum / n;

HashSet<int> unique = new HashSet<int>(numbers);

int[] sorted = (int[])numbers.Clone();
Array.Sort(sorted);

Console.WriteLine();
Console.WriteLine("Початковий масив: [" + string.Join(", ", numbers) + "]");
Console.WriteLine("Відсортована копія: [" + string.Join(", ", sorted) + "]");
Console.WriteLine($"Мінімум: {min}; максимум: {max}; сума: {sum}; середнє: {average:F2}");
Console.WriteLine($"Додатних: {positive}; від'ємних: {negative}; нульових: {zero}");
Console.WriteLine($"Парних: {even}; непарних: {odd}; унікальних: {unique.Count}");

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