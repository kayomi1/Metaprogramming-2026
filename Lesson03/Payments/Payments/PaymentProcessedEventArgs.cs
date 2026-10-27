using System;

namespace Payments
{
    public class PaymentProcessedEventArgs : EventArgs
    {
        public Payment Payment { get; }

        public PaymentProcessedEventArgs(Payment payment)
        {
            Payment = payment;
        }
    }
}