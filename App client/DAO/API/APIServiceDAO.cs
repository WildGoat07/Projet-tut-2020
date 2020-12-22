using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAO.API
{
    public class APIServiceDAO : IServiceDAO
    {
        internal APIServiceDAO(HttpClient client)
        {
            Client = client;
        }

        private HttpClient Client { get; }

        public Task<Service[]> CreateAsync(IEnumerable<Service> values)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IEnumerable<Service> value)
        {
            throw new NotImplementedException();
        }

        public Task<Service[]> GetByIdAsync(IEnumerable<(string, string, string)> id)
        {
            throw new NotImplementedException();
        }

        public Task<Service[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, IEnumerable<string>? teacher = null, IEnumerable<string>? ec = null, IEnumerable<string>? year = null, (int?, int?)? CmNumber = null, (int?, int?)? EiNumber = null, (int?, int?)? TdNumber = null, (int?, int?)? TpNumber = null, (int?, int?)? TplNumber = null, (int?, int?)? PrjNumber = null, (int?, int?)? equivalentHours = null)
        {
            throw new NotImplementedException();
        }

        public Task<Service[]> UpdateAsync(IEnumerable<(Service, Service)> values)
        {
            throw new NotImplementedException();
        }
    }
}