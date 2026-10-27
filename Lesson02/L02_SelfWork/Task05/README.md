# Task05. Сервіс методів для масиву

Бібліотека статичних методів `ArrayService` (файл `ArrayService.cs`) і демонстрація їх роботи (файл `Program.cs`).

## Запуск

```cmd
dotnet run --project Task05
```

## Методи

| Метод | Опис |
| --- | --- |
| `ReadIntInRange(prompt, min, max)` | Безпечне введення числа в діапазоні |
| `PrintArray(values, separator = ", ")` | Виведення масиву з роздільником за замовчуванням |
| `GetStatistics(values, out min, out max, out average)` | Три результати через out; порожній масив → `ArgumentException` |
| `Swap(ref left, ref right)` | Обмін значеннями через ref |
| `Sum(params values)` | Сума довільної кількості аргументів |
| `IndexOf(int[], int)` і `IndexOf(string, char)` | Перевантаження; якщо не знайдено, повертає -1 |

## Контрольні набори

| № | Виклик | Очікуваний результат |
| --- | --- | --- |
| 1 | `PrintArray({4, -2, 8, 0, 15})` | `[4, -2, 8, 0, 15]` |
| 2 | `GetStatistics({4, -2, 8, 0, 15})` | min -2, max 15, average 5.00 |
| 3 | `GetStatistics({})` | `ArgumentException`, повідомлення про помилку |
| 4 | `Swap(10, 20)` | a=20, b=10 |
| 5 | `Sum()` | 0 |
| 6 | `Sum(1, 2, 3, 4)` | 10 |
| 7 | `IndexOf("hello", 'z')` | -1 |
| 8 | Введення індексу `7` для діапазону 0–4 | Помилка, програма питає знову |
