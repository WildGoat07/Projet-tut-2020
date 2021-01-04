using Newtonsoft.Json;
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

        public async Task<Composante[]> CreateAsync(IEnumerable<Composante> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            var obj = new
            {
                values = values.ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("composante/CreateComposante.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Composante>>(await response.Content.ReadAsStringAsync());
            if (status.success)
                return status.values;
            else
            {
                var err = status.errors.First();
                throw new DAOException(err.error_desc, err.error_code switch
                {
                    "23000" => DAOException.ErrorCode.EXISTING_ENTRY,
                    _ => DAOException.ErrorCode.UNKNOWN
                });
            }
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