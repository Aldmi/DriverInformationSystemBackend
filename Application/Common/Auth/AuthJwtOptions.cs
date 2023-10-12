using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Application.Common.Auth;

//TODO: Убрать в хранилище секретов
public static class AuthJwtOptions
{
    public const string ISSUER = "DriverInformationSystemBackend"; // издатель токена
    public const string AUDIENCE = "DriverInformationSystemClient"; // потребитель токена
    private const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => 
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}