using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
            Id = user.Id,
            FullName = user.FullName,
            BirthDate = user.BirthDate,
            gender = user.gender,
            Phone = user.Phone,
            Email = user.Email,
            Address = user.Address,
        }).FirstOrDefault();
    }

    public string LoginUser(LoginCredentials userData)
    {
        User? user =
            _context.Users.FirstOrDefault(us => us.Email == userData.Email && us.Password == CreateSHA256(userData.Password));

        if (user == null) return null;
        else
        {
            ActiveToken? token = _context.ActiveTokens.FirstOrDefault(token => token.userId == user.Id);
            if (token != null)
            {
                if (token.ExpirationDate > DateTime.Now)
                {
                    return token.token;
                }
            }
        }

        var encodedJWT = CreateToken(user);

        AddToken(user, encodedJWT);

        return encodedJWT;
    }

    public async Task<ActionResult> LogoutUser(string token)
    {
        var currentToken = _context.ActiveTokens.FirstOrDefault(tkn => tkn.token == token);
        _context.ActiveTokens.Remove(currentToken);
        await _context.SaveChangesAsync();

        return new OkObjectResult(new
        {
            response = "Token removed"
        });
    }

    public async Task<ActionResult> RegisterUser(UserRegisterModel model)
    {
        User? user =
            _context.Users.FirstOrDefault(us => us.Email == model.Email);

        if (user != null)
        {
            if (user.Email == model.Email)
            {
                throw new InvalidOperationException("Account with this email already exists");
            }
        }
        
        if (model.Phone != null && !IsValidPhoneNumber(model.Phone) )
        {
            throw new InvalidOperationException("Invalid phone number");
        }

        if (model.BirthDate != null && !IsValidDate(model.BirthDate))
        {
            throw new InvalidOperationException("Invalid Birth Date");
        }

        user = new User(new Guid(), model.FullName, model.BirthDate, model.gender, model.Phone, model.Email,
            CreateSHA256(model.Password));

        var encodedJWT = CreateToken(user);
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        AddToken(user, encodedJWT);
        
        return new OkObjectResult(new
        {
            access_token = encodedJWT
        });
    }
    
    public static string CreateSHA256(string input)
    {
        using SHA256 hash = SHA256.Create();
        return Convert.ToHexString(hash.ComputeHash(Encoding.UTF8.GetBytes(input)));
    }

    public async Task<ActionResult> EditUserProfile(UserEditModel model, string token)
    {
        var userToken = _context.ActiveTokens.FirstOrDefault(tkn => tkn.token == token);

        if (userToken != null)
        {
            var user = _context.Users.FirstOrDefault(us => us.Id == userToken.userId);

            if (user != null)
            {
                if (!IsValidDate(model.BirthDate))
                {
                    throw new InvalidDataException("Invalid birth date");
                }
                
                if (!IsValidPhoneNumber(model.Phone))
                {
                    throw new InvalidDataException("Invalid phone number");
                }
                
                user.FullName = model.FullName;
                user.gender = model.gender;
                user.Address = model.Address;
            }
        }

        await _context.SaveChangesAsync();

        return new OkObjectResult(new
        {
            status = "200",
            message = "Profile successfully edited"
        });
    }

    public async Task AddToken(User user, string token)
    {
        ActiveToken tkn = new ActiveToken(new Guid(), user.Id, token, DateTime.UtcNow.AddMinutes(30));
        await _context.ActiveTokens.AddAsync(tkn);
        await _context.SaveChangesAsync();
    }
    
    private bool IsValidPhoneNumber(string phoneNumber)
    {
        return Regex.IsMatch(phoneNumber, @"^\+7 \(\d{3}\) \d{3}-\d{2}-\d{2}$");
    }

    private bool IsValidDate(DateTime date)
    {
        return date <= DateTime.UtcNow;
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };
        var jwt = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(30)),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"])), SecurityAlgorithms.HmacSha256)
        );

        return  new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}