
using Models.Models;
using OnlineShopWebAPIs.Models;

namespace Models.Interfaces.IServices
{
    public interface ITokenService
    {
        public string CreateToken(IdentityUserContext applicationUser);

    }
}