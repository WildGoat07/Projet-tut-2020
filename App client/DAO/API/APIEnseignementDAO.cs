using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAO.API
{
    public class APIEnseignementDAO : IEnseignementDAO
    {
        internal APIEnseignementDAO(HttpClient client)
        {
            Client = client;
        }

        private HttpClient Client { get; }

        public Task<Enseignement[]> CreateAsync(IEnumerable<Enseignement> values)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IEnumerable<Enseignement> value)
        {
            throw new NotImplementedException();
        }

        public Task<Enseignement[]> GetByIdAsync(IEnumerable<string> code)
        {
            throw new NotImplementedException();
        }

        public Task<Enseignement[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, IEnumerable<string>? ec = null, IEnumerable<string>? year = null, (int?, int?)? expectedQuantity = null, (int?, int?)? realQuantity = null, (int?, int?)? CmGroups = null, (int?, int?)? EiGroups = null, (int?, int?)? TdGroups = null, (int?, int?)? TpGroups = null, (int?, int?)? TplGroups = null, (int?, int?)? PrjGroups = null, (int?, int?)? CmGroupsSer = null, (int?, int?)? EiGroupsSer = null, (int?, int?)? TdGroupsSer = null, (int?, int?)? TpGroupsSer = null, (int?, int?)? TplGroupsSer = null, (int?, int?)? PrjGroupsSer = null)
        {
            throw new NotImplementedException();
        }

        public Task<Enseignement[]> UpdateAsync(IEnumerable<(Enseignement, Enseignement)> values)
        {
            throw new NotImplementedException();
        }
    }
}