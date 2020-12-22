using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAO.API
{
    public class APIEcDAO : IEcDAO
    {
        internal APIEcDAO(HttpClient client)
        {
            Client = client;
        }

        private HttpClient Client { get; }

        public Task<Ec[]> CreateAsync(IEnumerable<Ec> values)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IEnumerable<Ec> values)
        {
            throw new NotImplementedException();
        }

        public Task<Ec[]> GetByIdAsync(IEnumerable<string> code)
        {
            throw new NotImplementedException();
        }

        public Task<Ec[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, string? search = null, IEnumerable<int>? Cnu = null, IEnumerable<int>? owner = null, IEnumerable<char>? nature = null, IEnumerable<int>? ue = null, IEnumerable<int>? category = null, (int?, int?)? CmHours = null, (int?, int?)? EiHours = null, (int?, int?)? TdHours = null, (int?, int?)? TpHours = null, (int?, int?)? TplHours = null, (int?, int?)? PrjHours = null, (int?, int?)? NbEpr = null, (int?, int?)? stepCount = null)
        {
            throw new NotImplementedException();
        }

        public Task<Ec[]> UpdateAsync(IEnumerable<(Ec, Ec)> values)
        {
            throw new NotImplementedException();
        }
    }
}