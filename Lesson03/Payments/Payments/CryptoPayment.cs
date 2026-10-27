using System;

namespace Payments
{
    public class CryptoPayment : Payment
    {
        public string TxHash { get; }

        public CryptoPayment(decimal amount, string currency, string txHash)
            : base(amount, currency)
        {
            TxHash = txHash;
        }

        protected override void DoProcess()
        {
            Console.WriteLine($"[Крипто] Транзакція {TxHash}, сума {Amount:N2} {Currency}.");
        }
    }
}