using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAO.API
{
    public class APIComposanteDAO : IComposanteDAO
    {
        internal APIComposanteDAO(HttpClient client)
        {
            Client = client;
        }

        private HttpClient Client { get; }

        public Task<Composante[]> CreateAsync(IEnumerable<Composante> values)
        {
            throw new NotImplementedException();
        }

        public Task<Composante> CurrentAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IEnumerable<Composante> values)
        {
            throw new NotImplementedException();
        }

        public Task<Composante[]> GetByIdAsync(IEnumerable<string> id)
        {
            throw new NotImplementedException();
        }

        public Task<Composante[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, string? search = null, IEnumerable<string>? location = null)
        {
            throw new NotImplementedException();
        }

        public Task SetCurrentAsync(Composante newCurrent)
        {
            throw new NotImplementedException();
        }

        public Task<Composante[]> UpdateAsync(IEnumerable<(Composante, Composante)> values)
        {
            throw new NotImplementedException();
        }
    }
}