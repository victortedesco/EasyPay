namespace Cards.API.Application.ViewModels;

public record CardViewModel(int Id, Guid UserId, string UserName, string CardNumber, string SecurityNumber, DateOnly ExpireDate, decimal CardLimit, decimal TotalExpenses)
{

    public string CardFlag
    {
        get
        {
            if (CardNumber.StartsWith("5258"))
                return "MasterCard";
            if (CardNumber.StartsWith("4111"))
                return "Visa";
            if (CardNumber.StartsWith("3400") || CardNumber.StartsWith("3700"))
                return "American Express";
            if (CardNumber.StartsWith("1718"))
                return "Cielo";
            return "Unknown";
        }
    }

    public string Color => "Black";
}

