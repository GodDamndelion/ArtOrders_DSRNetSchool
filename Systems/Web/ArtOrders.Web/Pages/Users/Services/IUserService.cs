namespace ArtOrders.Web;

using System.Threading.Tasks;

public interface IUserService
{
    Task<IEnumerable<UserListItem>> GetArtists(int offset = 0, int limit = 10);
    Task<UserListItem> GetCurrentUser();
    Task<UserListItem> GetUserById(Guid id);
    Task<HttpResponseMessage> SingUp(RegisterUserAccountModel userModel);
}
