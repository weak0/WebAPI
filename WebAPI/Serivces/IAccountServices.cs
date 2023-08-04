using WebAPI.Models;

namespace WebAPI.Serivces
{
    public interface IAccountServices
    {
        int CreateUser(AccountDto account);
    }
}