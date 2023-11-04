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

    private readonly GarContext _garContext;

    private readonly IConfiguration _configuration;

    public UserService(AppDbContext context, IConfiguration configuration, GarContext garContext)
    {
        _context = context;
        _configuration = configuration;
        _garContext = garContext;
    }

    public UserDTO GetUserProfile(string token)
    {
        ActiveToken userToken = _context.ActiveTokens.FirstOrDefault(tkn => tkn.token == token);

        User user = _context.Users.FirstOrDefault(us => us.Id == userToken.userId);

        return new UserDTO
        {
            Id = user.Id,
            FullName = user.FullName,
            BirthDate = user.BirthDate,
            gender = user.gender,
            Phone = user.Phone,
            Email = user.Email,
            Address = user.Address,
        };
    }

    public string LoginUser(LoginCredentials userData)
    {
        User? user =
            _context.Users.FirstOrDefault(us =>
                us.Email == userData.Email && us.Password == CreateSHA256(userData.Password));

        if (user == null) return null;

        ActiveToken? token = _context.ActiveTokens.FirstOrDefault(token => token.userId == user.Id);

        var encodedJWT = CreateToken(user);

        AddToken(user, encodedJWT);

        return encodedJWT;
    }

    public async Task<ActionResult> LogoutUser(string token)
    {
        ActiveToken? currentToken = _context.ActiveTokens.FirstOrDefault(tkn => tkn.token == token);

        if (currentToken == null)
        {
            throw new InvalidOperationException("Current user doesn't have active tokens");
        }
        
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

        AsAddrObj? address = _garContext.AsAddrObjs.FirstOrDefault(obj => obj.Objectguid == model.Address);

        if (address != null)
        {
            throw new InvalidOperationException("Address must be a house");
        }

        if (user != null)
        {
            if (user.Email == model.Email)
            {
                throw new InvalidOperationException("Username '" + model.Email + "' is already taken");
            }
        }


        AsHouse? house = _garContext.AsHouses.FirstOrDefault(h => h.Objectguid == model.Address);

        if (model.Address != null && house == null && address == null)
        {
            throw new InvalidOperationException("Address not found");
        }

        user = new User(new Guid(), model.FullName, model.BirthDate, model.gender, model.Phone, model.Email,
            CreateSHA256(model.Password));

        if (house != null)
        {
            user.Address = house.Objectguid;
        }


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
        
        AsAddrObj? address = _garContext.AsAddrObjs.FirstOrDefault(obj => obj.Objectguid == model.Address);
        
        if (address != null)
        {
            throw new InvalidOperationException("Address must be a house");
        }
        
        AsHouse? house = _garContext.AsHouses.FirstOrDefault(h => h.Objectguid == model.Address);

        if (model.Address != new Guid() && house == null && address == null)
        {
            throw new InvalidOperationException("Address not found");
        }

        if (userToken != null)
        {
            var user = _context.Users.FirstOrDefault(us => us.Id == userToken.userId);

            if (user != null)
            {
                if (!IsValidDate(model.BirthDate))
                {
                    throw new InvalidDataException("Invalid birth date");
                }

                user.FullName = model.FullName;
                user.BirthDate = model.BirthDate;
                user.gender = model.gender;
                user.Address = model.Address;
                user.Phone = model.Phone;
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
        ActiveToken? tkn = _context.ActiveTokens.FirstOrDefault(t => t.userId == user.Id);
        if (tkn != null)
        {
            tkn.ExpirationDate = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:Time"]));
            tkn.token = token;
        }
        else
        {
            tkn = new ActiveToken(new Guid(), user.Id, token, DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:Time"])));
            await _context.ActiveTokens.AddAsync(tkn);
        }

        await _context.SaveChangesAsync();
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
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(Convert.ToDouble(_configuration["JwtSettings:Time"]))),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"])),
                SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}