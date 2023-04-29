namespace ArtOrders.Web;

using System.Threading.Tasks;

public interface IUserService
{
    Task<IEnumerable<UserListItem>> GetArtists(int offset = 0, int limit = 10);
    Task<UserListItem> GetCurrentUser();
    Task SingUp(RegisterUserAccountModel userModel);
}
