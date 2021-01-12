using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DAO.API
{
    public class APIEnseignementDAO : IEnseignementDAO
    {
        internal APIEnseignementDAO(HttpClient client)
        {
            Client = client;
        }

        private HttpClient Client { get; }

        public async Task<Enseignement[]> CreateAsync(IEnumerable<Enseignement> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            var obj = new
            {
                values = values.ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("enseignement/CreateEnseignement.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Enseignement>>(await response.Content.ReadAsStringAsync());
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

        public async Task DeleteAsync(IEnumerable<Enseignement> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            var obj = new
            {
                values = (from value in values
                          select new
                          {
                              value.code_ec,
                              value.annee
                          }).ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("enseignement/DeleteEnseignement.php", UriKind.Relative);
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

        public async Task<Enseignement[]> GetByIdAsync(IEnumerable<(string, string)> code)
        {
            if (code == null)
                throw new ArgumentNullException(nameof(code));
            var obj = new Dictionary<string, object>();
            var filters = new Dictionary<string, object>();
            obj.Add("filters", filters);
            obj.Add("quantity", code.Count());
            obj.Add("skip", 0);
            filters.Add("code_ec", (from c in code select c.Item1).ToArray());
            filters.Add("annee", (from c in code select c.Item2).ToArray());
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("enseignement/SelectEnseignement.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Enseignement>>(await response.Content.ReadAsStringAsync());
            if (status.success)
                return status.values.Length == code.Count() ? status.values : throw new DAOException("An entry is missing", DAOException.ErrorCode.MISSING_ENTRY);
            else
            {
                var err = status.errors.First();
                throw new DAOException(err.error_desc, DAOException.ErrorCode.UNKNOWN);
            }
        }

        public async Task<Enseignement[]> GetFilteredAsync(int maxCount, int page, string? orderBy = null, bool reverseOrder = false, IEnumerable<string>? ec = null, IEnumerable<string>? year = null, (int?, int?)? expectedQuantity = null, (int?, int?)? realQuantity = null, (int?, int?)? CmGroups = null, (int?, int?)? EiGroups = null, (int?, int?)? TdGroups = null, (int?, int?)? TpGroups = null, (int?, int?)? TplGroups = null, (int?, int?)? PrjGroups = null, (int?, int?)? CmGroupsSer = null, (int?, int?)? EiGroupsSer = null, (int?, int?)? TdGroupsSer = null, (int?, int?)? TpGroupsSer = null, (int?, int?)? TplGroupsSer = null, (int?, int?)? PrjGroupsSer = null)
        {
            var obj = new Dictionary<string, object>();
            var filters = new Dictionary<string, object>();
            obj.Add("filters", filters);
            obj.Add("quantity", maxCount);
            obj.Add("skip", maxCount * page);
            if (orderBy != null)
                obj.Add("order", orderBy);
            obj.Add("reverse_order", reverseOrder);
            if (ec != null)
                filters.Add("code_ec", ec.ToArray());
            if (year != null)
                filters.Add("annee", year.ToArray());
            if (expectedQuantity != null)
            {
                object? range = expectedQuantity.Value.Item1.HasValue && expectedQuantity.Value.Item2.HasValue ?
                    new { min = expectedQuantity.Value.Item1.Value, max = expectedQuantity.Value.Item2.Value } :
                    expectedQuantity.Value.Item1.HasValue ? new { min = expectedQuantity.Value.Item1.Value } :
                    expectedQuantity.Value.Item2.HasValue ?
                    new { min = expectedQuantity.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("eff_prev", range);
            }
            if (realQuantity != null)
            {
                object? range = realQuantity.Value.Item1.HasValue && realQuantity.Value.Item2.HasValue ?
                    new { min = realQuantity.Value.Item1.Value, max = realQuantity.Value.Item2.Value } :
                    realQuantity.Value.Item1.HasValue ? new { min = realQuantity.Value.Item1.Value } :
                    realQuantity.Value.Item2.HasValue ?
                    new { min = realQuantity.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("eff_reel", range);
            }
            if (CmGroups != null)
            {
                object? range = CmGroups.Value.Item1.HasValue && CmGroups.Value.Item2.HasValue ?
                    new { min = CmGroups.Value.Item1.Value, max = CmGroups.Value.Item2.Value } :
                    CmGroups.Value.Item1.HasValue ? new { min = CmGroups.Value.Item1.Value } :
                    CmGroups.Value.Item2.HasValue ?
                    new { min = CmGroups.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("GpCM", range);
            }
            if (EiGroups != null)
            {
                object? range = EiGroups.Value.Item1.HasValue && EiGroups.Value.Item2.HasValue ?
                    new { min = EiGroups.Value.Item1.Value, max = EiGroups.Value.Item2.Value } :
                    EiGroups.Value.Item1.HasValue ? new { min = EiGroups.Value.Item1.Value } :
                    EiGroups.Value.Item2.HasValue ?
                    new { min = EiGroups.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("GpEI", range);
            }
            if (TdGroups != null)
            {
                object? range = TdGroups.Value.Item1.HasValue && TdGroups.Value.Item2.HasValue ?
                    new { min = TdGroups.Value.Item1.Value, max = TdGroups.Value.Item2.Value } :
                    TdGroups.Value.Item1.HasValue ? new { min = TdGroups.Value.Item1.Value } :
                    TdGroups.Value.Item2.HasValue ?
                    new { min = TdGroups.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("GpTD", range);
            }
            if (TpGroups != null)
            {
                object? range = TpGroups.Value.Item1.HasValue && TpGroups.Value.Item2.HasValue ?
                    new { min = TpGroups.Value.Item1.Value, max = TpGroups.Value.Item2.Value } :
                    TpGroups.Value.Item1.HasValue ? new { min = TpGroups.Value.Item1.Value } :
                    TpGroups.Value.Item2.HasValue ?
                    new { min = TpGroups.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("GpTP", range);
            }
            if (TplGroups != null)
            {
                object? range = TplGroups.Value.Item1.HasValue && TplGroups.Value.Item2.HasValue ?
                    new { min = TplGroups.Value.Item1.Value, max = TplGroups.Value.Item2.Value } :
                    TplGroups.Value.Item1.HasValue ? new { min = TplGroups.Value.Item1.Value } :
                    TplGroups.Value.Item2.HasValue ?
                    new { min = TplGroups.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("GpTPL", range);
            }
            if (PrjGroups != null)
            {
                object? range = PrjGroups.Value.Item1.HasValue && PrjGroups.Value.Item2.HasValue ?
                    new { min = PrjGroups.Value.Item1.Value, max = PrjGroups.Value.Item2.Value } :
                    PrjGroups.Value.Item1.HasValue ? new { min = PrjGroups.Value.Item1.Value } :
                    PrjGroups.Value.Item2.HasValue ?
                    new { min = PrjGroups.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("GpPRJ", range);
            }
            if (CmGroupsSer != null)
            {
                object? range = CmGroupsSer.Value.Item1.HasValue && CmGroupsSer.Value.Item2.HasValue ?
                    new { min = CmGroupsSer.Value.Item1.Value, max = CmGroupsSer.Value.Item2.Value } :
                    CmGroupsSer.Value.Item1.HasValue ? new { min = CmGroupsSer.Value.Item1.Value } :
                    CmGroupsSer.Value.Item2.HasValue ?
                    new { min = CmGroupsSer.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("GpCMSer", range);
            }
            if (EiGroupsSer != null)
            {
                object? range = EiGroupsSer.Value.Item1.HasValue && EiGroupsSer.Value.Item2.HasValue ?
                    new { min = EiGroupsSer.Value.Item1.Value, max = EiGroupsSer.Value.Item2.Value } :
                    EiGroupsSer.Value.Item1.HasValue ? new { min = EiGroupsSer.Value.Item1.Value } :
                    EiGroupsSer.Value.Item2.HasValue ?
                    new { min = EiGroupsSer.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("GpEISer", range);
            }
            if (TdGroupsSer != null)
            {
                object? range = TdGroupsSer.Value.Item1.HasValue && TdGroupsSer.Value.Item2.HasValue ?
                    new { min = TdGroupsSer.Value.Item1.Value, max = TdGroupsSer.Value.Item2.Value } :
                    TdGroupsSer.Value.Item1.HasValue ? new { min = TdGroupsSer.Value.Item1.Value } :
                    TdGroupsSer.Value.Item2.HasValue ?
                    new { min = TdGroupsSer.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("GpTDSer", range);
            }
            if (TpGroupsSer != null)
            {
                object? range = TpGroupsSer.Value.Item1.HasValue && TpGroupsSer.Value.Item2.HasValue ?
                    new { min = TpGroupsSer.Value.Item1.Value, max = TpGroupsSer.Value.Item2.Value } :
                    TpGroupsSer.Value.Item1.HasValue ? new { min = TpGroupsSer.Value.Item1.Value } :
                    TpGroupsSer.Value.Item2.HasValue ?
                    new { min = TpGroupsSer.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("GpTPSer", range);
            }
            if (TplGroupsSer != null)
            {
                object? range = TplGroupsSer.Value.Item1.HasValue && TplGroupsSer.Value.Item2.HasValue ?
                    new { min = TplGroupsSer.Value.Item1.Value, max = TplGroupsSer.Value.Item2.Value } :
                    TplGroupsSer.Value.Item1.HasValue ? new { min = TplGroupsSer.Value.Item1.Value } :
                    TplGroupsSer.Value.Item2.HasValue ?
                    new { min = TplGroupsSer.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("GpTPLSer", range);
            }
            if (PrjGroupsSer != null)
            {
                object? range = PrjGroupsSer.Value.Item1.HasValue && PrjGroupsSer.Value.Item2.HasValue ?
                    new { min = PrjGroupsSer.Value.Item1.Value, max = PrjGroupsSer.Value.Item2.Value } :
                    PrjGroupsSer.Value.Item1.HasValue ? new { min = PrjGroupsSer.Value.Item1.Value } :
                    PrjGroupsSer.Value.Item2.HasValue ?
                    new { min = PrjGroupsSer.Value.Item2.Value } : null;
                if (range != null)
                    filters.Add("GpPRJSer", range);
            }
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("enseignement/SelectEnseignement.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Enseignement>>(await response.Content.ReadAsStringAsync());
            if (status.success)
                return status.values;
            else
            {
                var err = status.errors.First();
                throw new DAOException(err.error_desc, DAOException.ErrorCode.UNKNOWN);
            }
        }

        public async Task<Enseignement[]> UpdateAsync(IEnumerable<(Enseignement, Enseignement)> values)
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
                                  value.Item1.code_ec,
                                  value.Item1.annee
                              },
                              data = value.Item2
                          }).ToArray()
            };
            var jsonObj = JsonConvert.SerializeObject(obj, Formatting.None);
            var url = new Uri("enseignement/EditEnseignement.php", UriKind.Relative);
            var response = await Client.PostAsync(url, new StringContent(jsonObj, Encoding.UTF8, "application/json"));
            var status = JsonConvert.DeserializeObject<Response<Enseignement>>(await response.Content.ReadAsStringAsync());
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
}