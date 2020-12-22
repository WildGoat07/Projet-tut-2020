using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAO.API
{
    public class APIHorsCompDAO : IHorsCompDAO
    {
        internal APIHorsCompDAO(HttpClient client)
        {
            Client = client;
        }

        private HttpClient Client { get; }

        public Task<HorsComp[]> CreateAsync(IEnumerable<HorsComp> values)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IEnumerable<HorsComp> value)
        {
            throw new NotImplementedException();
        }

        public Task<HorsComp[]> GetByIdAsync(IEnumerable<(string, int)> id)
        {
            throw new NotImplementedException();
        }

        public Task<HorsComp[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, IEnumerable<string>? course = null, IEnumerable<string>? comp = null, IEnumerable<string>? year = null, (int?, int?)? CmHours = null, (int?, int?)? EiHours = null, (int?, int?)? TdHours = null, (int?, int?)? TpHours = null, (int?, int?)? TplHours = null, (int?, int?)? PrjHours = null, (float?, float?)? equivalentHours = null)
        {
            throw new NotImplementedException();
        }

        public Task<HorsComp[]> UpdateAsync(IEnumerable<(HorsComp, HorsComp)> values)
        {
            throw new NotImplementedException();
        }
    }
}