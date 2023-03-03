
using OnlineShopWebAPIs.Models;

namespace Domains.Interfaces.IServices
{
    public interface ITokenService
    {
        public string CreateToken(IdentityUserContext applicationUser);

    }
}