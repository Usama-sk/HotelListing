using DataModels.DTOModels;
using System.Threading.Tasks;

namespace Infrasturcture.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO userDTO);
        Task<string> CreateToken();
    }
}
