using webNET_Hits_backend_aspnet_project_1.Data;
using webNET_Hits_backend_aspnet_project_1.Models;

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

    public User GetUserProfile()
    {
        // return _context.Users.Select(x => new User(Guid.NewGuid(), "Blablabla", DateTime.Now, Gender.Male, "+79539231131", "gbhfns@gmail.com"));
        return null;
    }

    public async Task Add(User model)
    {
        await _context.Users.AddAsync(model);
        await _context.SaveChangesAsync();
    }
}