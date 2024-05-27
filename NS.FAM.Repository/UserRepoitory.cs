using Microsoft.EntityFrameworkCore;
using NS.FAM.Data;
using NS.FAM.Data;
using NS.FAM.Data.Entities;
namespace NS.FAM.Repository;

public class UserRepository : IUserRepository
{
    private readonly FAMDBContext context;
    public UserRepository(FAMDBContext context)
    {
        this.context = context;
    }
    public async Task<int> AddUser(Users user)
    {
        try
        {
            context.Add(user);
            var result = await context.SaveChangesAsync();
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<List<Country>> GetCountryList()
    {
        return await context.Country.ToListAsync();
    }

    public IList<State> GetStateValue(int countryId)
    {
        try
        {
            return context.State.Where(m => m.CountryId == countryId).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public IList<City> GetCityValue(int stateId)
    {
        try
        {
            return context.City.Where(m => m.StateId == stateId).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<List<Users>> GetAllUsers(string searchData)
    {
        try
        {
            var users = from user in context.Users.Where(x => x.RoleId == 2) select user;
            if (!string.IsNullOrEmpty(searchData))
            {
                users = users.Where(it => it.FirstName.Contains(searchData));
            }

            return users.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<int> ActivateDeactivateRecord(int Id)
    {
        try
        {
            var result = 0;
            var user = context.Users.FirstOrDefault(x => x.Id == Id);
            if (user != null)
            {
                user.IsActive = !user.IsActive;
                result = await context.SaveChangesAsync();
                return result;
            }
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<Users> GetUserDetailsByEmail(string email)
    {
        try
        {
            var result = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<Users> GetProfileById(long id)
    {
        try
        {
            var user = context.Users
                .Include(it => it.Country)
                .Include(it => it.State)
                .Include(it => it.City)
                .FirstOrDefault(x => x.Id == id);
            return user;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


}
