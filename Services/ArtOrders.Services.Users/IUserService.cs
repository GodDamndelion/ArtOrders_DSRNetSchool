namespace ArtOrders.Services.Users;

public interface IUserService
{
    /// <summary>
    /// Create user account
    /// </summary>
    Task<UserAccountModel> Create(RegisterUserAccountModel model);
    Task<IEnumerable<UserAccountModel>> GetArtists(int offset = 0, int limit = 10);

    // TODO: Доделать CRUD для Users
    // .. Также здесь можно разместить методы для изменения данных учетной записи, восстановления и смены пароля, подтверждения электронной почты, установки телефона и его подтверждения и т.д.
}
