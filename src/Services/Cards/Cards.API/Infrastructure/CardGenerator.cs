using System.Text;

namespace Cards.API.Infrastructure;

public class CardGenerator
{
    private static readonly string[] _begin = { "5258", "4111", "3400", "3700", "1718" };

    public static string GenerateCardNumber()
    {
        var cardNumber = new StringBuilder();
        var randomBegin = new Random().Next(0, _begin.Length);
        cardNumber.Append(_begin[randomBegin]);

        for (int i = 0; i < 12; i++)
        {
            cardNumber.Append(new Random().Next(0, 9));
        }

        return cardNumber.ToString();
    }

    public static string GenerateSecurityNumber()
    {
        var securityNumber = new StringBuilder();
        for (int i = 0; i < 3; i++)
        {
            securityNumber.Append(new Random().Next(0, 9));
        }
        return securityNumber.ToString();
    }
}
