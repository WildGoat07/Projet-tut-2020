using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAO.API
{
    public class APIServiceDAO : IServiceDAO
    {
        internal APIServiceDAO(HttpClient client)
        {
            Client = client;
        }

        private HttpClient Client { get; }

        public async Task<Service[]> CreateAsync(IEnumerable<Service> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            var obj = new
            {
                values = values.ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("service/CreateService.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));

            var status = JsonConvert.DeserializeObject<Response<Service>>(await response.Content.ReadAsStringAsync());
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

        public async Task DeleteAsync(IEnumerable<Service> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            var obj = new
            {
                values = (from value in values
                          select new
                          {
                              value.id_ens, value.code_ec, value.annee
                          }).ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("service/DeleteService.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Service>>(await response.Content.ReadAsStringAsync());
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

        public async Task<Service[]> GetByIdAsync(IEnumerable<(string, string, string)> id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            var obj = new Dictionary<string, object>();
            var filters = new Dictionary<string, object>();
            obj.Add("filters", filters);
            obj.Add("quantity", id.Count());
            obj.Add("skip", 0);
            filters.Add("id_ens", (from c in id select c.Item1).ToArray());
            filters.Add("code_ec", (from c in id select c.Item2).ToArray());
            filters.Add("annee", (from c in id select c.Item3).ToArray());
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("service/SelectService.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Service>>(await response.Content.ReadAsStringAsync());
            if (status.success)
                return status.values.Length == id.Count() ? status.values : throw new DAOException("An entry is missing", DAOException.ErrorCode.MISSING_ENTRY);
            else
            {
                var err = status.errors.First();
                throw new DAOException(err.error_desc, DAOException.ErrorCode.UNKNOWN);
            }
        }

        public async Task<Service[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, IEnumerable<string>? teacher = null, IEnumerable<string>? ec = null, IEnumerable<string>? year = null, (int?, int?)? CmNumber = null, (int?, int?)? EiNumber = null, (int?, int?)? TdNumber = null, (int?, int?)? TpNumber = null, (int?, int?)? TplNumber = null, (int?, int?)? PrjNumber = null, (int?, int?)? equivalentHours = null)
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
            if (ec != null)
                filters.Add("code_ec", ec.ToArray());
            if (year != null)
                filters.Add("annee", year.ToArray());
            if (CmNumber != null) 
            {
                object? range = CmNumber.Value.Item1.HasValue && CmNumber.Value.Item2.HasValue ?
                    new { min = CmNumber.Value.Item1.Value, max = CmNumber.Value.Item2.Value } :
                    CmNumber.Value.Item1.HasValue ? new { min = CmNumber.Value.Item1.Value } :
                    CmNumber.Value.Item2.HasValue ?
                    new { min = CmNumber.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("NbGpCM", range);
            }
                
            if (EiNumber != null) 
            {
                object? range = EiNumber.Value.Item1.HasValue && EiNumber.Value.Item2.HasValue ?
                    new { min = EiNumber.Value.Item1.Value, max = EiNumber.Value.Item2.Value } :
                    EiNumber.Value.Item1.HasValue ? new { min = EiNumber.Value.Item1.Value } :
                    EiNumber.Value.Item2.HasValue ?
                    new { min = EiNumber.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("NbGpEI", range);
            }
            if (TdNumber != null)
            {
                 object? range = TdNumber.Value.Item1.HasValue && TdNumber.Value.Item2.HasValue ?
                    new { min = TdNumber.Value.Item1.Value, max = TdNumber.Value.Item2.Value } :
                    TdNumber.Value.Item1.HasValue ? new { min = TdNumber.Value.Item1.Value } :
                    TdNumber.Value.Item2.HasValue ?
                    new { min = TdNumber.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("NbGpTD", range);
            }
            if (TpNumber != null)
            {
                 object? range = TpNumber.Value.Item1.HasValue && TpNumber.Value.Item2.HasValue ?
                    new { min = TpNumber.Value.Item1.Value, max = TpNumber.Value.Item2.Value } :
                    TpNumber.Value.Item1.HasValue ? new { min = TpNumber.Value.Item1.Value } :
                    TpNumber.Value.Item2.HasValue ?
                    new { min = TpNumber.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("NbGpTP", range);
            }
            if (TplNumber != null)
            {
                 object? range = TplNumber.Value.Item1.HasValue && TplNumber.Value.Item2.HasValue ?
                    new { min = TplNumber.Value.Item1.Value, max = TplNumber.Value.Item2.Value } :
                    TplNumber.Value.Item1.HasValue ? new { min = TplNumber.Value.Item1.Value } :
                    TplNumber.Value.Item2.HasValue ?
                    new { min = TplNumber.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("NbGpTPL", range);
            }
            if (PrjNumber != null)
            {
                 object? range = PrjNumber.Value.Item1.HasValue && PrjNumber.Value.Item2.HasValue ?
                    new { min = PrjNumber.Value.Item1.Value, max = PrjNumber.Value.Item2.Value } :
                    PrjNumber.Value.Item1.HasValue ? new { min = PrjNumber.Value.Item1.Value } :
                    PrjNumber.Value.Item2.HasValue ?
                    new { min = PrjNumber.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("NbGpPRJ", range);
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
            var url = new Uri("service/SelectService.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));


            var status = JsonConvert.DeserializeObject<Response<Service>>(await response.Content.ReadAsStringAsync());
            if (status.success)
                return status.values;
            else
            {
                var err = status.errors.First();
                throw new DAOException(err.error_desc, DAOException.ErrorCode.UNKNOWN);
            }
        }

        public async Task<Service[]> UpdateAsync(IEnumerable<(Service, Service)> values)
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
                                  value.Item1.code_ec,
                                  value.Item1.annee
                              },
                              data = value.Item2
                          }).ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("service/EditService.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));

            var status = JsonConvert.DeserializeObject<Response<Service>>(await response.Content.ReadAsStringAsync());
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