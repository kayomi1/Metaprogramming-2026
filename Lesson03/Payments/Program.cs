using System;
using System.Collections.Generic;
using System.Text;
using Payments;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

var cash = new CashPayment(150m, "UAH");
var card = new CardPayment(500m, "UAH", "**** 4242", "A1B2");
var crypto = new CryptoPayment(0.01m, "BTC", "0xabc123");

var payments = new List<Payment> { cash, card, crypto };
ProcessAll(payments);

var refundables = new List<IRefundable> { card };
RefundAll(refundables, 200m);

// Перевірка інваріантів
TryCreate(() => new CashPayment(0m, "UAH"), "Сума 0");
TryCreate(() => new CashPayment(10m, " "), "Порожня валюта");

// Перевірка ProcessedUtc
Console.WriteLine($"ProcessedUtc для готівки: {cash.ProcessedUtc}");

static void ProcessAll(IEnumerable<Payment> payments)
{
    foreach (Payment payment in payments)
    {
        payment.Processed += (sender, e) =>
            Console.WriteLine($"  -> подія Processed: {e.Payment.GetType().Name}");
        payment.Process();
    }
}

static void RefundAll(IEnumerable<IRefundable> items, decimal amount)
{
    foreach (IRefundable item in items)
        item.Refund(amount);
}

static void TryCreate(Func<Payment> factory, string caseName)
{
    try
    {
        factory();
        Console.WriteLine($"{caseName}: помилки не було (очікувалась)");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"{caseName}: виняток - {ex.Message}");
    }
}