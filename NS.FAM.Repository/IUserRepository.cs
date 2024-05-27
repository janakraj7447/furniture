using NS.FAM.Data;
using NS.FAM.Data.Entities;

namespace NS.FAM.Repository;

public interface IUserRepository
{
    Task<int> AddUser(Users user);
    Task<List<Country>> GetCountryList();
    IList<State> GetStateValue(int countryId);
    IList<City> GetCityValue(int stateId);
    Task<List<Users>> GetAllUsers(string searchData);
    Task<int> ActivateDeactivateRecord(int Id);
    Task<Users> GetUserDetailsByEmail(string email);
    Task<Users> GetProfileById(long id);

}
