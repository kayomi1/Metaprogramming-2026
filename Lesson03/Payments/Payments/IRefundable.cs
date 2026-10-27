namespace Payments
{
    public interface IRefundable
    {
        void Refund(decimal amount);
    }
}