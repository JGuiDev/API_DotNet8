using CURSO_API.Models;

namespace CURSO_API.Interfaces
{
    public interface ITokenService
    {
        string  CreateToken(User user);
    }
}
