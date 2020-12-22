using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAO.API
{
    public class APICategorieDAO : ICategorieDAO
    {
        internal APICategorieDAO(HttpClient client)
        {
            Client = client;
        }

        private HttpClient Client { get; }

        public Task<Categorie[]> CreateAsync(IEnumerable<Categorie> values)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IEnumerable<Categorie> values)
        {
            throw new NotImplementedException();
        }

        public Task<Categorie[]> GetByIdAsync(IEnumerable<int> id)
        {
            throw new NotImplementedException();
        }

        public Task<Categorie[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, string? search = null)
        {
            throw new NotImplementedException();
        }

        public Task<Categorie[]> UpdateAsync(IEnumerable<(Categorie, Categorie)> values)
        {
            throw new NotImplementedException();
        }
    }
}