using Newtonsoft.Json;
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

        public async Task<Etape[]> CreateAsync(IEnumerable<Etape> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            var obj = new
            {
                values = values.ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("etape/CreateEtape.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Etape>>(await response.Content.ReadAsStringAsync());
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

        public async Task DeleteAsync(IEnumerable<Etape> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            var obj = new
            {
                values = (from value in values
                          select new
                          {
                              value.code_etape,
                              value.vet
                          }).ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("etape/DeleteEtape.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Etape>>(await response.Content.ReadAsStringAsync());
            if (!status.success)
            {
                var err = status.errors.First();
                throw new DAOException(err.error_desc, err.error_code switch
                {
                    "66666" => DAOException.ErrorCode.MISSING_ENTRY,
                    "23000" => DAOException.ErrorCode.ENTRY_LINKED,
                    _ => DAOException.ErrorCode.UNKNOWN
                });
            }
        }

        public async Task<Etape[]> GetByIdAsync(IEnumerable<(string, int)> code)
        {
            if (code == null)
                throw new ArgumentNullException(nameof(code));
            var obj = new Dictionary<string, object>();
            var filters = new Dictionary<string, object>();
            obj.Add("filters", filters);
            obj.Add("quantity", code.Count());
            obj.Add("skip", 0);
            filters.Add("code_etape", (from c in code select c.Item1).ToArray());
            filters.Add("vet", (from c in code select c.Item2).ToArray());
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("etape/SelectEtape.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Etape>>(await response.Content.ReadAsStringAsync());
            if (status.success)
                return status.values.Length == code.Count() ? status.values : throw new DAOException("An entry is missing", DAOException.ErrorCode.MISSING_ENTRY);
            else
            {
                var err = status.errors.First();
                throw new DAOException(err.error_desc, DAOException.ErrorCode.UNKNOWN);
            }
        }

        public async Task<Etape[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, string? search = null, IEnumerable<string>? comp = null, IEnumerable<(string, int)>? diplome = null)
        {
            var obj = new Dictionary<string, object>();
            var filters = new Dictionary<string, object>();
            obj.Add("filters", filters);
            obj.Add("quantity", maxCount);
            obj.Add("skip", maxCount * page);
            if (orderBy != null)
                obj.Add("order", orderBy);
            obj.Add("reverse_order", reverseOrder);
            if (search != null)
                obj.Add("search", search);
            if (comp != null)
                filters.Add("id_comp", comp.ToArray());
            if (diplome != null)
            {
                filters.Add("code_diplome", (from d in diplome select d.Item1).ToArray());
                filters.Add("vdi", (from d in diplome select d.Item2).ToArray());
            }

            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("etape/SelectEtape.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Etape>>(await response.Content.ReadAsStringAsync());
            if (status.success)
                return status.values;
            else
            {
                var err = status.errors.First();
                throw new DAOException(err.error_desc, DAOException.ErrorCode.UNKNOWN);
            }
        }

        public async Task<Etape[]> UpdateAsync(IEnumerable<(Etape, Etape)> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            var obj = new
            {
                values = (from value in values
                          select new
                          {
                              target = new
                              {
                                  value.Item1.code_etape,
                                  value.Item1.vet
                              },
                              data = value.Item2
                          }).ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("etape/EditEtape.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Etape>>(await response.Content.ReadAsStringAsync());
            if (status.success)
                return (from value in values select value.Item2).ToArray();
            else
            {
                var err = status.errors.First();
                throw new DAOException(err.error_desc, err.error_code switch
                {
                    "66666" => DAOException.ErrorCode.MISSING_ENTRY,
                    "23000" => DAOException.ErrorCode.ENTRY_LINKED,
                    _ => DAOException.ErrorCode.UNKNOWN
                });
            }
        }
    }
}