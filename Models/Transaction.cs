using System.ComponentModel.DataAnnotations;

namespace EasyPay.Models
{
    public class Transaction(User sender, User receiver, decimal value) : IEntity
    {
        [Key] public Guid Id { get; private set; } = Guid.Empty;
        public User Sender { get; } = sender;
        public User Receiver { get; } = receiver;
        public decimal Value { get; } = value;
        public DateTime CreatedAt { get; private set; }
    }

    public record TransactionDTO(Guid SenderId, Guid ReceiverId, decimal Value);
}