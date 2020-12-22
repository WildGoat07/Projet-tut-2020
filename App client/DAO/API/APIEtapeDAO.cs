using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAO.API
{
    public class APIEtapeDAO : IEtapeDAO
    {
        internal APIEtapeDAO(HttpClient client)
        {
            Client = client;
        }

        private HttpClient Client { get; }

        public Task<Etape[]> CreateAsync(IEnumerable<Etape> values)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IEnumerable<Etape> value)
        {
            throw new NotImplementedException();
        }

        public Task<Etape[]> GetByIdAsync(IEnumerable<(string, int)> id)
        {
            throw new NotImplementedException();
        }

        public Task<Etape[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, string? search = null, IEnumerable<string>? comp = null, IEnumerable<(string, int)>? diplome = null)
        {
            throw new NotImplementedException();
        }

        public Task<Etape[]> UpdateAsync(IEnumerable<(Etape, Etape)> values)
        {
            throw new NotImplementedException();
        }
    }
}