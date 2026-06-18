namespace Backend.Modules.ProductCatalog.Application.Interfaces
{
    public interface IuserNameProvider
    {
        Task<string> GetUserNameByIdAsync(int userId);
    }

}





