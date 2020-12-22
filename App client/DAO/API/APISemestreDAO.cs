using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAO.API
{
    public class APISemestreDAO : ISemestreDAO
    {
        internal APISemestreDAO(HttpClient client)
        {
            Client = client;
        }

        private HttpClient Client { get; }

        public Task<Semestre[]> CreateAsync(IEnumerable<Semestre> values)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IEnumerable<Semestre> value)
        {
            throw new NotImplementedException();
        }

        public Task<Semestre[]> GetByIdAsync(IEnumerable<string> code)
        {
            throw new NotImplementedException();
        }

        public Task<Semestre[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, string? search = null, IEnumerable<int>? number = null, IEnumerable<(string?, int?)>? step = null)
        {
            throw new NotImplementedException();
        }

        public Task<Semestre[]> UpdateAsync(IEnumerable<(Semestre, Semestre)> values)
        {
            throw new NotImplementedException();
        }
    }
}