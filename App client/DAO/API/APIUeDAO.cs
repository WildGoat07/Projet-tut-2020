using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAO.API
{
    public class APIUeDAO : IUeDAO
    {
        internal APIUeDAO(HttpClient client)
        {
            Client = client;
        }

        private HttpClient Client { get; }

        public Task<Ue[]> CreateAsync(IEnumerable<Ue> values)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IEnumerable<Ue> values)
        {
            throw new NotImplementedException();
        }

        public Task<Ue[]> GetByIdAsync(IEnumerable<string> code)
        {
            throw new NotImplementedException();
        }

        public Task<Ue[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, string? search = null, IEnumerable<char>? nature = null, IEnumerable<int>? ECTS = null, IEnumerable<string>? parent = null, IEnumerable<Semestre>? semester = null)
        {
            throw new NotImplementedException();
        }

        public Task<Ue[]> UpdateAsync(IEnumerable<(Ue, Ue)> values)
        {
            throw new NotImplementedException();
        }
    }
}