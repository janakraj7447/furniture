using NS.FAM.Data.CustomEntities;
using NS.FAM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NS.FAM.Business;

public interface IUserBusiness
{
    Task<int> AddUser(UserViewModel userViewModel);
    Task<List<Country>> GetCountryList();
    List<SelectListItem> GetStateItems(int countryId);
    List<SelectListItem> GetCityItems(int stateId);
    Task<List<Users>> GetAllUsers(string searchData);
    Task<int> ActivateDeactivateRecord(int Id);
    Task<Users> GetUserDetailsByEmail(string email);
    Task<Users> GetProfileById(long id);
}
