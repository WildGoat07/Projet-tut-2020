using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAO.API
{
    public class APIDiplomeDAO : IDiplomeDAO
    {
        internal APIDiplomeDAO(HttpClient client)
        {
            Client = client;
        }

        private HttpClient Client { get; }

        public Task<Diplome[]> CreateAsync(IEnumerable<Diplome> values)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IEnumerable<Diplome> values)
        {
            throw new NotImplementedException();
        }

        public Task<Diplome[]> GetByIdAsync(IEnumerable<(string, int)> id)
        {
            throw new NotImplementedException();
        }

        public Task<Diplome[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, string? search = null, IEnumerable<int>? begin = null, IEnumerable<int>? end = null)
        {
            throw new NotImplementedException();
        }

        public Task<Diplome[]> UpdateAsync(IEnumerable<(Diplome, Diplome)> values)
        {
            throw new NotImplementedException();
        }
    }
}