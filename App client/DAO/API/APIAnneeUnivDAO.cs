using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace DAO.API
{
    public class APIAnneeUnivDAO : IAnneeUnivDAO
    {
        internal APIAnneeUnivDAO(HttpClient client)
        {
            Client = client;
        }

        private HttpClient Client { get; }

        public Task<AnneeUniv[]> CreateAsync(IEnumerable<AnneeUniv> values)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IEnumerable<AnneeUniv> values)
        {
            throw new NotImplementedException();
        }

        public Task<AnneeUniv[]> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AnneeUniv[]> UpdateAsync(IEnumerable<(AnneeUniv, AnneeUniv)> values)
        {
            throw new NotImplementedException();
        }
    }
}