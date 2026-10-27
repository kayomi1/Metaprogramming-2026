using System;

namespace Payments
{
    public class CardPayment : Payment, IRefundable
    {
        public string CardMasked { get; }
        public string AuthCode { get; }
        private decimal _refunded;

        public CardPayment(decimal amount, string currency, string cardMasked, string authCode)
            : base(amount, currency)
        {
            CardMasked = cardMasked;
            AuthCode = authCode;
        }

        protected override void DoProcess()
        {
            Console.WriteLine($"[Картка {CardMasked}] Списано {Amount:N2} {Currency}, код {AuthCode}.");
        }

        public void Refund(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Сума повернення має бути більшою за 0.");
            if (_refunded + amount > Amount)
                throw new ArgumentOutOfRangeException(nameof(amount), "Сума повернення перевищує сплачену.");

            _refunded += amount;
            Console.WriteLine($"[Картка {CardMasked}] Повернено {amount:N2} {Currency}.");
        }
    }
}