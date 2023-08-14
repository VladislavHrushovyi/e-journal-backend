using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using EJournal.Application.Common.Exception;
using EJournal.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EJournal.Application.Features.User.Login;

public sealed class UserLoginHandler : IRequestHandler<UserLoginRequest, UserLoginResponse>
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly BaseUnitOfWork _unitOfWork;

    public UserLoginHandler(IConfiguration configuration, IMapper mapper, BaseUnitOfWork unitOfWork)
    {
        _configuration = configuration;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserLoginResponse> Handle(UserLoginRequest request, CancellationToken cancellationToken)
    {
        var loginInfo = _mapper.Map<Domain.Entities.User>(request);
        var foundUser = await _unitOfWork._userRepository.FindOneByProperties(loginInfo, cancellationToken);
        if (foundUser == null)
        {
            throw new BadLoginInformationException("Incorrect phone number or password");
        }

        var issuer = _configuration["JWT:issuer"];
        var audience = _configuration["JWT:audience"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        var claims = new[]
        {
            new Claim(ClaimTypes.MobilePhone, foundUser.PhoneNumber),
            new Claim(ClaimTypes.Name, foundUser.FirstName),
            new Claim(ClaimTypes.Surname, foundUser.LastName),
            new Claim(ClaimTypes.NameIdentifier, foundUser.Id.ToString())
        };
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(issuer, audience, claims,
            expires: DateTime.Now.AddHours(6), signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        return new UserLoginResponse() { JwtToken = token };
    }
}