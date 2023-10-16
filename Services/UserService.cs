using webNET_Hits_backend_aspnet_project_1.Data;
using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_1.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }
    
    public User RegisterUser()
    {
        return null;
    }

    public UserDTO GetUserProfile()
    {
        return _context.Users.Select(x => new UserDTO
        {
            FullName = x.FullName,
            BirthDate = x.BirthDate,
            gender = x.gender,
            Phone = x.Phone,
            Email = x.Email,
            Address = x.Address
        }).FirstOrDefault();
        //return null;
    }

    public async Task Add(UserDTO model)
    {
        await _context.Users.AddAsync(new User(new Guid(), model.FullName, model.BirthDate, model.gender, model.Phone, model.Email));
        await _context.SaveChangesAsync();
    }
}