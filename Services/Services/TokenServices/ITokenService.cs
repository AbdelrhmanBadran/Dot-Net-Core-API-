using Core.IdentityEntities;

namespace Services.Services.TokenServices
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}
