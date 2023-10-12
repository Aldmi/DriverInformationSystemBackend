using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Common;
using Application.Common.Auth;
using Application.Domain.PersonAgregat;
using Application.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Login;

public class GetJwtController : ApiControllerBase
{
    [HttpPost("/login")]
    public async Task<ActionResult<JwtResponse>> GetJwt([FromBody]GetJwtQuery query)
    {
        var result=await Mediator.Send(query);
        if (result.IsSuccess) {
            return result.Value;
        }
        return result.Error switch {
            "Login not found" => Unauthorized("Пользователь не найден"),
            "Password invalid"=> Unauthorized("Пароль не верный"),
            _ => Problem(title:"Не известная ошибка авторизации", detail:result.Error)
        };
    }
}


public class JwtResponse
{
    public string access_token { get; init; }
    public string user_name { get; init; }
    public string role_name { get; init; }
    public DateTime generate_time { get; init; }
    public DateTime expired_time { get; init; }
}


public class GetJwtQuery: IRequest<Result<JwtResponse>>
{
    public string Login { get; init; }
    public string Password { get; init; } //TODO: передавать в защифрованном виде (ключ шифрования известен и на клиенте и на сервере)
}


internal sealed class GetJwtQueryHandler : IRequestHandler<GetJwtQuery, Result<JwtResponse>>
{
    private readonly IPersoneRepository _personeRepository;
    private readonly JwtTokenGenerator _tokenGenerator;

    public GetJwtQueryHandler(IPersoneRepository personeRepository, JwtTokenGenerator tokenGenerator)
    {
        _personeRepository = personeRepository;
        _tokenGenerator = tokenGenerator;
    }
    
    
    public async Task<Result<JwtResponse>> Handle(GetJwtQuery request, CancellationToken cancellationToken)
    {
        var person= await _personeRepository.GetOrDefaultAsync(persone => persone.Name == request.Login);
        if(person is null) 
            return Result.Failure<JwtResponse>("Login not found");

        var jwtDataRes = _tokenGenerator.GenerateJwtToken(person, request.Password);
        if (jwtDataRes.IsFailure) 
            return Result.Failure<JwtResponse>(jwtDataRes.Error);
        
        var response = new JwtResponse()
        {
            access_token = jwtDataRes.Value.EncodedJwt,
            user_name = person.Name,
            role_name = person.Role.Name,
            generate_time = jwtDataRes.Value.GenerateTime,
            expired_time = jwtDataRes.Value.ExpiresTime
        };
        return response;
    }
}


internal sealed class JwtTokenGenerator
{
    private static readonly TimeSpan TokenLifeTime= TimeSpan.FromHours(1);
    
    public Result<JwtData> GenerateJwtToken(Person person, string password)
    {
        var passwordFromDb = person.Password; //TODO: Декодировать пароль из БД
        if (passwordFromDb != password) {
            return Result.Failure<JwtData>("Password invalid");
        }
        
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, person.Name),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role.Name)
        };
        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
            issuer: AuthJwtOptions.ISSUER,
            audience: AuthJwtOptions.AUDIENCE,
            claims: claims,
            signingCredentials: new SigningCredentials(AuthJwtOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256),
                
            expires: DateTime.UtcNow.Add(TokenLifeTime), //exp
            notBefore:DateTime.UtcNow                    //nbf
        );
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        return new JwtData(encodedJwt, jwt.ValidFrom, jwt.ValidTo);
    }
}

public record JwtData(string EncodedJwt, DateTime GenerateTime, DateTime ExpiresTime);