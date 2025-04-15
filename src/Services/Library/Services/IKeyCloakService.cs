namespace EasyPay.Library.Services;

public interface IKeyCloakService
{
    public Guid? GetUserId();
    public string GetUserName();
    public string GetUserDisplayName();
    public IEnumerable<string> GetUserRoles();
    public string GetUserEmail();
}