using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAO.API
{
    public class APIHorsCompDAO : IHorsCompDAO
    {
        internal APIHorsCompDAO(HttpClient client)
        {
            Client = client;
        }

        private HttpClient Client { get; }

        public async Task<HorsComp[]> CreateAsync(IEnumerable<HorsComp> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            var obj = new
            {
                values = values.ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("horscomposante/CreateHorsComp.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<HorsComp>>(await response.Content.ReadAsStringAsync());
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

        public async Task DeleteAsync(IEnumerable<HorsComp> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            var obj = new
            {
                values = (from value in values
                          select new
                          {
                              value.id_ens,
                              value.id_comp,
                              value.annee
                          }).ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("horscomposante/DeleteHorscomp.php", UriKind.Relative);
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

        public async Task<HorsComp[]> GetByIdAsync(IEnumerable<(string, string, string)> id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            var obj = new Dictionary<string, object>();
            var filters = new Dictionary<string, object>();
            obj.Add("filters", filters);
            obj.Add("quantity", id.Count());
            obj.Add("skip", 0);
            filters.Add("id_ens", (from c in id select c.Item1).ToArray());
            filters.Add("id_comp", (from c in id select c.Item2).ToArray());
            filters.Add("annee", (from c in id select c.Item3).ToArray());
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("horscomposante/SelectHorscomp.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<HorsComp>>(await response.Content.ReadAsStringAsync());
            Console.Write(await response.Content.ReadAsStringAsync());
            if (status.success)
                return status.values.Length == id.Count() ? status.values : throw new DAOException("An entry is missing", DAOException.ErrorCode.MISSING_ENTRY);
            else
            {
                var err = status.errors.First();
                throw new DAOException(err.error_desc, DAOException.ErrorCode.UNKNOWN);
            }
        }

        public async Task<HorsComp[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, IEnumerable<string>? teacher = null, IEnumerable<string>? comp = null, IEnumerable<string>? year = null, (int?, int?)? CmHours = null, (int?, int?)? EiHours = null, (int?, int?)? TdHours = null, (int?, int?)? TpHours = null, (int?, int?)? TplHours = null, (int?, int?)? PrjHours = null, (float?, float?)? equivalentHours = null)
        {
            var obj = new Dictionary<string, object>();
            var filters = new Dictionary<string, object>();
            obj.Add("filters", filters);
            obj.Add("quantity", maxCount);
            obj.Add("skip", maxCount * page);
            if (orderBy != null)
                obj.Add("order", orderBy);
            obj.Add("reverse_order", reverseOrder);
            if (teacher != null)
                filters.Add("id_ens", teacher.ToArray());
            if (comp != null)
                filters.Add("id_comp", comp.ToArray());
            if (year != null)
                filters.Add("annee", year.ToArray());
            if (CmHours != null)
            {
                object? range = CmHours.Value.Item1.HasValue && CmHours.Value.Item2.HasValue ?
                    new { min = CmHours.Value.Item1.Value, max = CmHours.Value.Item2.Value } :
                    CmHours.Value.Item1.HasValue ? new { min = CmHours.Value.Item1.Value } :
                    CmHours.Value.Item2.HasValue ?
                    new { min = CmHours.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("HCM", range);
            }
            if (EiHours != null)
            {
                object? range = EiHours.Value.Item1.HasValue && EiHours.Value.Item2.HasValue ?
                    new { min = EiHours.Value.Item1.Value, max = EiHours.Value.Item2.Value } :
                    EiHours.Value.Item1.HasValue ? new { min = EiHours.Value.Item1.Value } :
                    EiHours.Value.Item2.HasValue ?
                    new { min = EiHours.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("HEI", range);
            }
            if (TdHours != null)
            {
                object? range = TdHours.Value.Item1.HasValue && TdHours.Value.Item2.HasValue ?
                    new { min = TdHours.Value.Item1.Value, max = TdHours.Value.Item2.Value } :
                    TdHours.Value.Item1.HasValue ? new { min = TdHours.Value.Item1.Value } :
                    TdHours.Value.Item2.HasValue ?
                    new { min = TdHours.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("HTD", range);
            }
            if (TpHours != null)
            {
                object? range = TpHours.Value.Item1.HasValue && TpHours.Value.Item2.HasValue ?
                    new { min = TpHours.Value.Item1.Value, max = TpHours.Value.Item2.Value } :
                    TpHours.Value.Item1.HasValue ? new { min = TpHours.Value.Item1.Value } :
                    TpHours.Value.Item2.HasValue ?
                    new { min = TpHours.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("HTP", range);
            }
            if (TplHours != null)
            {
                object? range = TplHours.Value.Item1.HasValue && TplHours.Value.Item2.HasValue ?
                    new { min = TplHours.Value.Item1.Value, max = TplHours.Value.Item2.Value } :
                    TplHours.Value.Item1.HasValue ? new { min = TplHours.Value.Item1.Value } :
                    TplHours.Value.Item2.HasValue ?
                    new { min = TplHours.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("HTPL", range);
            }
            if (PrjHours != null)
            {
                object? range = PrjHours.Value.Item1.HasValue && PrjHours.Value.Item2.HasValue ?
                    new { min = PrjHours.Value.Item1.Value, max = PrjHours.Value.Item2.Value } :
                    PrjHours.Value.Item1.HasValue ? new { min = PrjHours.Value.Item1.Value } :
                    PrjHours.Value.Item2.HasValue ?
                    new { min = PrjHours.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("HPRJ", range);
            }
            if (equivalentHours != null)
            {
                object? range = equivalentHours.Value.Item1.HasValue && equivalentHours.Value.Item2.HasValue ?
                    new { min = equivalentHours.Value.Item1.Value, max = equivalentHours.Value.Item2.Value } :
                    equivalentHours.Value.Item1.HasValue ? new { min = equivalentHours.Value.Item1.Value } :
                    equivalentHours.Value.Item2.HasValue ?
                    new { min = equivalentHours.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("HEqTD", range);
            }

            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("horscomposante/SelectHorscomp.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<HorsComp>>(await response.Content.ReadAsStringAsync());
            if (status.success)
                return status.values;
            else
            {
                var err = status.errors.First();
                throw new DAOException(err.error_desc, DAOException.ErrorCode.UNKNOWN);
            }
        }

        public async Task<HorsComp[]> UpdateAsync(IEnumerable<(HorsComp, HorsComp)> values)
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
                                  value.Item1.id_ens,
                                  value.Item1.id_comp,
                                  value.Item1.annee
                              },
                              data = value.Item2
                          }).ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("horscomposante/EditHorscomp.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<HorsComp>>(await response.Content.ReadAsStringAsync());
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