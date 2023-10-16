using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using webNET_Hits_backend_aspnet_project_1.Data;
using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_1.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    private readonly IConfiguration _configuration;

    public UserService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public UserDTO GetUserProfile()
    {
        return _context.Users.Select(user => new UserDTO
        {
            FullName = user.FullName,
            BirthDate = user.BirthDate,
            gender = user.gender,
            Phone = user.Phone,
            Email = user.Email,
            Address = user.Address,
            Password = user.Password
        }).FirstOrDefault();
    }

    public IResult LoginUser(LoginCredentials userData)
    {
        User? user =
            _context.Users.FirstOrDefault(us => us.Email == userData.Email && us.Password == CreateSHA256(userData.Password));

        if (user == null) return null;
        
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };
        var jwt = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(30)),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"])), SecurityAlgorithms.HmacSha256)
        );

        var encodedJWT = new JwtSecurityTokenHandler().WriteToken(jwt);
        
        return Results.Json(new
        {
            access_token = encodedJWT
        });
    }

    public async Task RegisterUser(UserDTO model)
    {
        await _context.Users.AddAsync(new User(new Guid(), model.FullName, model.BirthDate, model.gender, model.Phone, model.Email, CreateSHA256(model.Password) ));
        await _context.SaveChangesAsync();
    }
    
    public static string CreateSHA256(string input)
    {
        using SHA256 hash = SHA256.Create();
        return Convert.ToHexString(hash.ComputeHash(Encoding.UTF8.GetBytes(input)));
    }
}