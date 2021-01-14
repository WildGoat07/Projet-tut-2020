using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAO.API
{
    public class APIEcDAO : IEcDAO
    {
        internal APIEcDAO(HttpClient client)
        {
            Client = client;
        }

        private HttpClient Client { get; }

        public async Task<Ec[]> CreateAsync(IEnumerable<Ec> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            var obj = new
            {
                values = values.ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("ec/CreateEc.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Ec>>(await response.Content.ReadAsStringAsync());
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

        public async Task DeleteAsync(IEnumerable<Ec> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            var obj = new
            {
                values = (from value in values
                          select new { value.code_ec }).ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("ec/DeleteEc.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            //var ok = await response.Content.ReadAsStringAsync();

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

        public async Task<Ec[]> GetByIdAsync(IEnumerable<string> code)
        {
            if (code == null)
                throw new ArgumentNullException(nameof(code));
            var obj = new Dictionary<string, object>();
            var filters = new Dictionary<string, object>();
            obj.Add("filters", filters);
            obj.Add("quantity", code.Count());
            obj.Add("skip", 0);
            filters.Add("code_ec", code.ToArray());
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("ec/SelectEc.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Ec>>(await response.Content.ReadAsStringAsync());
            if (status.success)
                return status.values.Length == code.Count() ? status.values : throw new DAOException("An entry is missing", DAOException.ErrorCode.MISSING_ENTRY);
            else
            {
                var err = status.errors.First();
                throw new DAOException(err.error_desc, DAOException.ErrorCode.UNKNOWN);
            }
        }

        public async Task<Ec[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, string? search = null, IEnumerable<int>? Cnu = null, IEnumerable<string>? owner = null, IEnumerable<char>? nature = null, IEnumerable<string>? ue = null, IEnumerable<int>? category = null, (int?, int?)? CmHours = null, (int?, int?)? EiHours = null, (int?, int?)? TdHours = null, (int?, int?)? TpHours = null, (int?, int?)? TplHours = null, (int?, int?)? PrjHours = null, (int?, int?)? stepCount = null)
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
            if (Cnu != null)
                filters.Add("CNU", Cnu.ToArray());
            if (owner != null)
                filters.Add("code_ec_pere", owner.ToArray());
            if (nature != null)
                filters.Add("nature", nature.ToArray());
            if (ue != null)
                filters.Add("code_ue", ue.ToArray());
            if (category != null)
                filters.Add("no_cat", category.ToArray());
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
            if (stepCount != null)
            {
                object? range = stepCount.Value.Item1.HasValue && stepCount.Value.Item2.HasValue ?
                    new { min = stepCount.Value.Item1.Value, max = stepCount.Value.Item2.Value } :
                    stepCount.Value.Item1.HasValue ? new { min = stepCount.Value.Item1.Value } :
                    stepCount.Value.Item2.HasValue ?
                    new { min = stepCount.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("NbEpr", range);
            }

            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("ec/SelectEc.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Ec>>(await response.Content.ReadAsStringAsync());
            if (status.success)
                return status.values;
            else
            {
                var err = status.errors.First();
                throw new DAOException(err.error_desc, DAOException.ErrorCode.UNKNOWN);
            }
        }

        public async Task<Ec[]> UpdateAsync(IEnumerable<(Ec, Ec)> values)
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
                                  value.Item1.code_ec
                              },
                              data = value.Item2
                          }).ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("ec/EditEc.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Ec>>(await response.Content.ReadAsStringAsync());
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