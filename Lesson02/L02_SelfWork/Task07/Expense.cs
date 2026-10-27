public enum ExpenseCategory
{
    Food,
    Transport,
    Study,
    Entertainment,
    Other
}

// record — запис, який не можна змінити після створення
public record Expense(int Id, DateOnly Date, decimal Amount, ExpenseCategory Category, string Description);