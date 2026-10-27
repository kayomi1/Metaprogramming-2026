using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

int[] numbers = { 4, -2, 8, 0, 15 };

Console.WriteLine("--- PrintArray ---");
ArrayService.PrintArray(numbers);            //з роздільником за замовчуванням
ArrayService.PrintArray(numbers, " | ");     //з власним роздільником

Console.WriteLine("\n--- GetStatistics ---");
ArrayService.GetStatistics(numbers, out int min, out int max, out double average);
Console.WriteLine($"Мін: {min}, макс: {max}, середнє: {average:F2}");

try
{
    ArrayService.GetStatistics(Array.Empty<int>(), out _, out _, out _);
}
catch (ArgumentException ex)
{
    Console.WriteLine("Помилка: " + ex.Message);
}

Console.WriteLine("\n--- Swap ---");
int a = 10;
int b = 20;
ArrayService.Swap(ref a, ref b);
Console.WriteLine($"Після обміну: a={a}, b={b}");
ArrayService.Swap(ref a, ref b);
Console.WriteLine($"Ще раз обмінявши: a={a}, b={b}");

Console.WriteLine("\n--- Sum ---");
Console.WriteLine($"Sum(1, 2, 3) = {ArrayService.Sum(1, 2, 3)}");
Console.WriteLine($"Sum(5) = {ArrayService.Sum(5)}");
Console.WriteLine($"Sum() = {ArrayService.Sum()}"); //порожній виклик

Console.WriteLine("\n--- IndexOf ---");
Console.WriteLine($"IndexOf(numbers, 8) = {ArrayService.IndexOf(numbers, 8)}");
Console.WriteLine($"IndexOf(numbers, 99) = {ArrayService.IndexOf(numbers, 99)}"); //немає
Console.WriteLine($"IndexOf(\"hello\", 'l') = {ArrayService.IndexOf("hello", 'l')}");
Console.WriteLine($"IndexOf(\"hello\", 'z') = {ArrayService.IndexOf("hello", 'z')}"); //немає

Console.WriteLine("\n--- ReadIntInRange ---");
int index1 = ArrayService.ReadIntInRange("Оберіть індекс (0-4): ", 0, 4);
Console.WriteLine($"Значення: {numbers[index1]}");
int index2 = ArrayService.ReadIntInRange("Оберіть ще один індекс (0-4): ", 0, 4);
Console.WriteLine($"Значення: {numbers[index2]}");