using System;

namespace Payments
{
    public class CashPayment : Payment
    {
        public CashPayment(decimal amount, string currency) : base(amount, currency) { }

        protected override void DoProcess()
        {
            Console.WriteLine($"[Готівка] Прийнято {Amount:N2} {Currency}.");
        }
    }
}