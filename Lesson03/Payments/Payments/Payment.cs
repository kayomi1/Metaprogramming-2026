using System;

namespace Payments
{
    public abstract class Payment
    {
        public decimal Amount { get; }
        public string Currency { get; }
        public DateTime CreatedUtc { get; }
        public DateTime? ProcessedUtc { get; private set; }

        public event EventHandler<PaymentProcessedEventArgs>? Processed;

        protected Payment(decimal amount, string currency)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Сума має бути більшою за 0.");
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Валюта не може бути порожньою.", nameof(currency));

            Amount = amount;
            Currency = currency;
            CreatedUtc = DateTime.UtcNow;
        }

        // NVI: публічний метод не віртуальний, порядок кроків фіксований
        public void Process()
        {
            Validate();
            OnBeforeProcess();
            DoProcess();
            OnAfterProcess();
            OnProcessed(new PaymentProcessedEventArgs(this));
        }

        private void Validate()
        {
            if (Amount <= 0)
                throw new InvalidOperationException("Некоректна сума.");
            if (string.IsNullOrWhiteSpace(Currency))
                throw new InvalidOperationException("Некоректна валюта.");
        }

        protected virtual void OnBeforeProcess() { }

        protected abstract void DoProcess();

        protected virtual void OnAfterProcess()
        {
            ProcessedUtc = DateTime.UtcNow;
        }

        protected virtual void OnProcessed(PaymentProcessedEventArgs e)
        {
            Processed?.Invoke(this, e);
        }
    }
}