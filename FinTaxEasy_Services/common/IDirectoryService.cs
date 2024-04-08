using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTaxEasy_Services.common
{
    public partial interface IDirectoryService
    {
        List<FinTaxEasy_Data.Model.Country> GetCountryList();
        List<FinTaxEasy_Data.Model.State> GetStateList(int countryid);
        List<FinTaxEasy_Data.Model.City> GetCityList(int stateid);


        void InsertAddress(FinTaxEasy_Data.Model.Address address);
        void UpdateAddress(FinTaxEasy_Data.Model.Address address);
        void DeleteAddress(FinTaxEasy_Data.Model.Address address);
    }
}
