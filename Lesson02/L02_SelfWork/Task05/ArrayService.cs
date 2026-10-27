public static class ArrayService
{
    //Питаємо число, поки воно не потрапить у діапазон
    public static int ReadIntInRange(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int number) && number >= min && number <= max)
                return number;
            Console.WriteLine($"Помилка: введіть ціле число від {min} до {max}.");
        }
    }

    //separator має значення за замовчуванням ", "
    public static void PrintArray(int[] values, string separator = ", ")
    {
        Console.WriteLine("[" + string.Join(separator, values) + "]");
    }

    //Три результати повертаємо через out
    public static void GetStatistics(int[] values, out int min, out int max, out double average)
    {
        if (values.Length == 0)
            throw new ArgumentException("Масив порожній, статистику не можна обчислити.", nameof(values));

        min = values[0];
        max = values[0];
        long sum = 0;

        foreach (int x in values)
        {
            if (x < min) min = x;
            if (x > max) max = x;
            sum += x;
        }

        average = (double)sum / values.Length;
    }

    public static void Swap(ref int left, ref int right)
    {
        int temp = left;
        left = right;
        right = temp;
    }

    //params дозволяє передати будь-яку кількість аргументів, навіть жодного
    public static int Sum(params int[] values)
    {
        int total = 0;
        foreach (int x in values)
            total += x;
        return total;
    }

    //Перевантаження 1: пошук числа в масиві
    public static int IndexOf(int[] values, int target)
    {
        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] == target) return i;
        }
        return -1;
    }

    //Перевантаження 2: пошук символу в рядку
    public static int IndexOf(string text, char target)
    {
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == target) return i;
        }
        return -1;
    }
}