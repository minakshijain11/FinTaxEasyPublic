using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTaxEasy_Services.common
{
   public class DirectoryService :IDirectoryService
    {
        #region Fields

        private readonly FinTaxEasy_Data.Model.FinTaxEasyDBEntities dbContext;

        #endregion 

        public DirectoryService()
        {
            this.dbContext = new FinTaxEasy_Data.Model.FinTaxEasyDBEntities();
        }

        public List<FinTaxEasy_Data.Model.Country>  GetCountryList()
        {
            return dbContext.Countries.OrderBy(k => k.CountryName).ToList();
        }

        public List<FinTaxEasy_Data.Model.State> GetStateList(int countryid)
        {
            return dbContext.States.Where(k => k.CountyID == countryid).OrderBy(k => k.StateName).ToList();
        }

        public List<FinTaxEasy_Data.Model.City> GetCityList(int stateid)
        {
            return dbContext.Cities.Where(k => k.StateID == stateid).OrderBy(k => k.CityName).ToList();
        }

        public void InsertAddress(FinTaxEasy_Data.Model.Address address)
        {
            if (address != null)
            {
                dbContext.Addresses.Add(address);
                dbContext.SaveChanges();
            }
        }
        public void UpdateAddress(FinTaxEasy_Data.Model.Address address)
        {
            if (address != null)
            {
                dbContext.SaveChanges();
            }
        }

        public void DeleteAddress(FinTaxEasy_Data.Model.Address address)
        {
            if (address != null)
            {
                dbContext.Addresses.Remove(address);
                dbContext.SaveChanges();
            }
        }
    }
}
