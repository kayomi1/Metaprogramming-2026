using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

Console.WriteLine("--- Кінозал ---");

int rows = ReadInt("Кількість рядів (3-10): ", 3, 10);
int seatsPerRow = ReadInt("Місць у ряду (4-12): ", 4, 12);
bool[,] hall = new bool[rows, seatsPerRow]; // false = вільно, true = зайнято

bool running = true;
while (running)
{
    ShowMenu();
    string choice = (Console.ReadLine() ?? "").Trim();

    switch (choice)
    {
        case "1": ShowHall(hall); break;
        case "2": BookSeat(hall); break;
        case "3": CancelSeat(hall); break;
        case "4": ShowStatistics(hall); break;
        case "5": running = false; break;
        default: Console.WriteLine("Такого пункту немає."); break;
    }
}

Console.WriteLine("Роботу завершено.");

//Методи для пунктів меню

static void ShowMenu()
{
    Console.WriteLine("\n1 - Показати зал");
    Console.WriteLine("2 - Забронювати місце");
    Console.WriteLine("3 - Скасувати бронювання");
    Console.WriteLine("4 - Статистика");
    Console.WriteLine("5 - Завершити");
    Console.Write("Ваш вибір: ");
}

static void ShowHall(bool[,] hall)
{
    //Заголовок з номерами місць
    Console.Write("       ");
    for (int c = 0; c < hall.GetLength(1); c++)
        Console.Write($"{c + 1,5}");
    Console.WriteLine();

    //Кожен ряд
    for (int r = 0; r < hall.GetLength(0); r++)
    {
        Console.Write($"Ряд {r + 1,2}:");
        for (int c = 0; c < hall.GetLength(1); c++)
            Console.Write(hall[r, c] ? "  [X]" : "  [.]");
        Console.WriteLine();
    }
}

static void BookSeat(bool[,] hall)
{
    var (row, seat) = ReadSeat(hall);

    if (hall[row, seat])
    {
        Console.WriteLine("Це місце вже заброньовано.");
        return;
    }
    hall[row, seat] = true;
    Console.WriteLine("Місце заброньовано.");
}

static void CancelSeat(bool[,] hall)
{
    var (row, seat) = ReadSeat(hall);

    if (!hall[row, seat])
    {
        Console.WriteLine("Це місце вже вільне, скасовувати нічого.");
        return;
    }
    hall[row, seat] = false;
    Console.WriteLine("Бронювання скасовано.");
}

static void ShowStatistics(bool[,] hall)
{
    int occupied = 0;
    int bestRow = 0;
    int bestFree = -1;

    for (int r = 0; r < hall.GetLength(0); r++)
    {
        int freeInRow = 0;
        for (int c = 0; c < hall.GetLength(1); c++)
        {
            if (hall[r, c]) occupied++;
            else freeInRow++;
        }

        if (freeInRow > bestFree)
        {
            bestFree = freeInRow;
            bestRow = r;
        }
    }

    int total = hall.GetLength(0) * hall.GetLength(1);
    double percent = occupied * 100.0 / total;
    Console.WriteLine($"Зайнято: {occupied} з {total} ({percent:F1}%)");
    Console.WriteLine($"Найбільше вільних місць у ряду {bestRow + 1}: {bestFree}");
}

static (int, int) ReadSeat(bool[,] hall)
{
    int row = ReadInt($"Ряд (1-{hall.GetLength(0)}): ", 1, hall.GetLength(0)) - 1;
    int seat = ReadInt($"Місце (1-{hall.GetLength(1)}): ", 1, hall.GetLength(1)) - 1;
    return (row, seat);
}

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