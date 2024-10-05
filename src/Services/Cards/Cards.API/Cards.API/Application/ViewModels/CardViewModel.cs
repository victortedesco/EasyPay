namespace Cards.API.Application.ViewModels;

public record CardViewModel(int Id, string NumberCard, string SegureNumber, DateOnly ExpireDate, string UserId, decimal CardLimite, decimal TotalExpenseve)
{

    public string CardFlags
    {
        get
        {
            if (NumberCard.StartsWith("5258"))
                return "MasterCard";
            if (NumberCard.StartsWith("4111"))
                return "Visa";
            if (NumberCard.StartsWith("3400") || NumberCard.StartsWith("3700"))
                return "American Express";
            if (NumberCard.StartsWith("1718"))
                return "Cielo";
            return "Unknow";
        }
    } 
}

