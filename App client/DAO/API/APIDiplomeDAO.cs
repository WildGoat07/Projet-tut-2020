using Newtonsoft.Json;
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

        public async Task<Diplome[]> CreateAsync(IEnumerable<Diplome> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            var obj = new
            {
                values = values.ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("diplome/CreateDiplome.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Diplome>>(await response.Content.ReadAsStringAsync());
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

        public async Task DeleteAsync(IEnumerable<Diplome> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            var obj = new
            {
                values = (from value in values
                          select new
                          {
                              value.code_diplome,
                              value.vdi
                          }).ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("diplome/DeleteDiplome.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Diplome>>(await response.Content.ReadAsStringAsync());
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

        public async Task<Diplome[]> GetByIdAsync(IEnumerable<(string, int)> code)
        {
            if (code == null)
                throw new ArgumentNullException(nameof(code));
            var obj = new Dictionary<string, object>();
            var filters = new Dictionary<string, object>();
            obj.Add("filters", filters);
            obj.Add("quantity", code.Count());
            obj.Add("skip", 0);
            filters.Add("code_diplome", (from c in code select c.Item1).ToArray());
            filters.Add("vdi", (from c in code select c.Item1).ToArray());
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("diplome/SelectDiplome.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Diplome>>(await response.Content.ReadAsStringAsync());
            if (status.success)
                return status.values.Length == code.Count() ? status.values : throw new DAOException("An entry is missing", DAOException.ErrorCode.MISSING_ENTRY);
            else
            {
                var err = status.errors.First();
                throw new DAOException(err.error_desc, DAOException.ErrorCode.UNKNOWN);
            }
        }

        public async Task<Diplome[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, string? search = null, IEnumerable<int>? begin = null, IEnumerable<int>? end = null)
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
            if (begin != null)
                filters.Add("annee_deb", begin.ToArray());
            if (end != null)
                filters.Add("annee_fin", end.ToArray());

            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);

            Console.WriteLine(jsonObj);

            var url = new Uri("diplome/SelectDiplome.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));

            Console.WriteLine(await response.Content.ReadAsStringAsync());

            var status = JsonConvert.DeserializeObject<Response<Diplome>>(await response.Content.ReadAsStringAsync());
            if (status.success)
                return status.values;
            else
            {
                var err = status.errors.First();
                throw new DAOException(err.error_desc, DAOException.ErrorCode.UNKNOWN);
            }
        }

        public async Task<Diplome[]> UpdateAsync(IEnumerable<(Diplome, Diplome)> values)
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
                                  value.Item1.code_diplome,
                                  value.Item1.vdi
                              },
                              data = new
                              {
                                  value.Item2.annee_deb,
                                  value.Item2.annee_fin,
                                  value.Item2.libelle_diplome,
                                  value.Item2.libelle_vdi
                              }
                          }).ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("diplome/EditDiplome.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Diplome>>(await response.Content.ReadAsStringAsync());
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