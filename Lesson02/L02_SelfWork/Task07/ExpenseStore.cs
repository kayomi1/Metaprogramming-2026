public class ExpenseStore
{
    private readonly List<Expense> _items = new();
    private int _nextId = 1; // наступний Id, який ніколи не повторюється

    public List<Expense> Items => _items;

    public Expense Add(DateOnly date, decimal amount, ExpenseCategory category, string description)
    {
        Expense expense = new Expense(_nextId, date, amount, category, description);
        _nextId++;
        _items.Add(expense);
        return expense;
    }

    // Повертає true, якщо запис знайдено і видалено
    public bool Remove(int id)
    {
        Expense? found = _items.Find(e => e.Id == id);
        if (found is null) return false;

        _items.Remove(found);
        return true;
    }
}