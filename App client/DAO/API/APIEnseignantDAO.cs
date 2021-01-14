using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAO.API
{
    public class APIEnseignantDAO : IEnseignantDAO
    {
        internal APIEnseignantDAO(HttpClient client)
        {
            Client = client;
        }

        private HttpClient Client { get; }

        public async Task<Enseignant[]> CreateAsync(IEnumerable<Enseignant> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            var obj = new
            {
                values = values.ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("enseignant/CreateEnseignant.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Enseignant>>(await response.Content.ReadAsStringAsync());
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

        public async Task DeleteAsync(IEnumerable<Enseignant> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            var obj = new
            {
                values = (from value in values
                          select new
                          {
                              value.id_ens
                          }).ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("enseignant/DeleteEnseignant.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<DeleteResponse>(await response.Content.ReadAsStringAsync());
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

        public async Task<Enseignant[]> GetByIdAsync(IEnumerable<string> id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            var obj = new Dictionary<string, object>();
            var filters = new Dictionary<string, object>();
            obj.Add("filters", filters);
            obj.Add("quantity", id.Count());
            obj.Add("skip", 0);
            filters.Add("id_ens", id.ToArray());
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("enseignant/SelectEnseignant.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Enseignant>>(await response.Content.ReadAsStringAsync());
            if (status.success)
                return status.values.Length == id.Count() ? status.values : throw new DAOException("An entry is missing", DAOException.ErrorCode.MISSING_ENTRY);
            else
            {
                var err = status.errors.First();
                throw new DAOException(err.error_desc, DAOException.ErrorCode.UNKNOWN);
            }
        }

        public async Task<Enseignant[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, string? search = null, IEnumerable<string>? function = null, IEnumerable<int>? comp = null, IEnumerable<char>? CRCT = null, IEnumerable<char>? PesPedr = null, (float?, float?)? forcedHours = null, (float?, float?)? maxHours = null)
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
            if (function != null)
                filters.Add("fonction", function.ToArray());
            if (comp != null)
                filters.Add("id_comp", comp.ToArray());
            if (CRCT != null)
                filters.Add("CRCT", CRCT.ToArray());
            if (PesPedr != null)
                filters.Add("PES_PEDR", PesPedr.ToArray());
            if (forcedHours != null)
            {
                object? range = forcedHours.Value.Item1.HasValue && forcedHours.Value.Item2.HasValue ?
                    new { min = forcedHours.Value.Item1.Value, max = forcedHours.Value.Item2.Value } :
                    forcedHours.Value.Item1.HasValue ? new { min = forcedHours.Value.Item1.Value } :
                    forcedHours.Value.Item2.HasValue ?
                    new { min = forcedHours.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("HOblig", range);
            }
            if (maxHours != null)
            {
                object? range = maxHours.Value.Item1.HasValue && maxHours.Value.Item2.HasValue ?
                    new { min = maxHours.Value.Item1.Value, max = maxHours.Value.Item2.Value } :
                    maxHours.Value.Item1.HasValue ? new { min = maxHours.Value.Item1.Value } :
                    maxHours.Value.Item2.HasValue ?
                    new { min = maxHours.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("HMax", range);
            }
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("enseignant/SelectEnseignant.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Enseignant>>(await response.Content.ReadAsStringAsync());
            if (status.success)
                return status.values;
            else
            {
                var err = status.errors.First();
                throw new DAOException(err.error_desc, DAOException.ErrorCode.UNKNOWN);
            }
        }

        public async Task<Enseignant[]> UpdateAsync(IEnumerable<(Enseignant, Enseignant)> values)
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
                                  value.Item1.id_ens
                              },
                              data = new
                              {
                                  value.Item2.CRCT,
                                  value.Item2.fonction,
                                  value.Item2.HMax,
                                  value.Item2.HOblig,
                                  value.Item2.id_comp,
                                  value.Item2.nom,
                                  value.Item2.PES_PEDR,
                                  value.Item2.prenom
                              }
                          }).ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("enseignant/EditEnseignant.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Enseignant>>(await response.Content.ReadAsStringAsync());
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