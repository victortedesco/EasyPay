using System.ComponentModel.DataAnnotations;

namespace EasyPay.Models
{
    public class Transaction : IEntity
    {
        [Key] public Guid Id { get; private set; } = Guid.Empty;
        public User Sender { get; private set; }
        public User Receiver { get; private set; }
        public decimal Value { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }

    public record TransactionDTO(Guid SenderId, Guid ReceiverId, decimal Value);
}