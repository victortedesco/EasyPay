using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyPay.Models
{
    public class User : IEntity
    {
        [Key] public Guid Id { get; private set; } = Guid.Empty;

        public required string Document { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public decimal Balance { get; set; }
        public required string Password { get; set; }
        public UserType UserType { get; set; }
        [JsonIgnore] public List<Transaction> SentTransactions { get; private set; } = [];
        [JsonIgnore] public List<Transaction> ReceivedTransactions { get; private set; } = [];
    }

    public enum UserType : ushort
    {
        Costumer,
        Shopkeeper
    }

    public record UserDTO(string Document, string Email, string Name, decimal Balance, string Password, UserType UserType);
}