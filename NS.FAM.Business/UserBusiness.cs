using NS.FAM.Data.CustomEntities;
using NS.FAM.Data.Entities;
using NS.FAM.Repository;
using System.Web.Mvc;

namespace NS.FAM.Business;

public class UserBusiness : IUserBusiness
{
    private readonly IUserRepository _iUserRepository;


    public UserBusiness(IUserRepository iUserRepository)
    {
        _iUserRepository = iUserRepository;

    }

    public async Task<int> AddUser(UserViewModel userViewModel)
    {
        try
        {
            var user = new Users(userViewModel);
            return await _iUserRepository.AddUser(user);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<List<Country>> GetCountryList()
    {
        return await _iUserRepository.GetCountryList();
    }

    public List<SelectListItem> GetStateItems(int countryId)
    {
        try
        {
            var states = _iUserRepository.GetStateValue(countryId);
            return states.Select(m => new SelectListItem
            {
                Text = m.StateName,
                Value = m.StateId.ToString()
            }).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }


    }

    public List<SelectListItem> GetCityItems(int stateId)
    {
        try
        {
            var cities = _iUserRepository.GetCityValue(stateId);
            return cities.Select(m => new SelectListItem
            {
                Text = m.CityName,
                Value = m.CityId.ToString()
            }).ToList();
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
                return await  _iUserRepository.GetAllUsers(searchData);
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
                return await _iUserRepository.ActivateDeactivateRecord(Id);
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
                return await _iUserRepository.GetUserDetailsByEmail(email);
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
                return await _iUserRepository.GetProfileById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

}
